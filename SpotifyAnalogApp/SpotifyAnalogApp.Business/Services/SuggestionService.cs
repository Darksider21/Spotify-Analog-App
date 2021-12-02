using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Mapper;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class SuggestionService : ISuggestionsService
    {

        private IAppUserRepository appUserRepository;
        private ISongRepository songRepository;
        private IAnalyticsRepository analyticsRepository;

        public SuggestionService(IAppUserRepository appUserRepository,ISongRepository songRepository,IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
            this.songRepository = songRepository;
            this.appUserRepository = appUserRepository;
        }

        public async Task<IEnumerable<SongModel>> GetSuggestionSongsForUser(int userId, int amountOfsongs)
        {
            var user = await appUserRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            if (amountOfsongs <= 0)
            {
                throw new BaseCustomException(404, "amount of songs must be greater than 0");
            }
            
            var usersAnalytics = await analyticsRepository.GetAnalyticsByUserIdAsync(userId);
            var analyticsToWokrWith = usersAnalytics.Where(x => x.SongsOfThisGenreCount > 0).ToList();
            if (!analyticsToWokrWith.Any())
            {
                var songs = await songRepository.GetRandomSongsListAsync(amountOfsongs);
                return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songs);
            }

            
            var availibleGenres = analyticsToWokrWith.Select(x => x.Genre.GenreName).ToArray();
            var songsToWorkWith = await songRepository.GetSongsByMultipleGenresAsync(availibleGenres);

            List<Song> ShuffledsongsList = songsToWorkWith.OrderBy(_ => Guid.NewGuid()).ToList();
            var calculatedProportions = CalculateSongsProportions(analyticsToWokrWith).OrderBy(x => x.Percentage).Reverse().ToArray();
            List<Song> suggestedSongs = new();
            while (suggestedSongs.Count < amountOfsongs)
            {
                for (int i = 0; i < calculatedProportions.Length; i++)
                {
                    var item = calculatedProportions[i];
                    var randomNumber = ThreadSafeRandom.ThisThreadsRandom.Next(0, 101);
                    if (randomNumber <= item.Percentage)
                    {
                        var songToAdd = songsToWorkWith.Where(x => x.Genre.GenreName.Equals(item.GenreName)).FirstOrDefault();
                        suggestedSongs.Add(songToAdd);
                        ShuffledsongsList.Remove(songToAdd);
                    }
                }
            }

            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(suggestedSongs);
        }


        private IEnumerable<GenreProportion> CalculateSongsProportions(IEnumerable<GenreAnalytics> usersAnalytics)
        {
            int totalSongs = usersAnalytics.Sum(x => x.SongsOfThisGenreCount);
            List<GenreProportion> usersSongsProportions = new();

            foreach (var analytics in usersAnalytics)
            {
                int percentage = Convert.ToInt32(Math.Round(((decimal)analytics.SongsOfThisGenreCount / totalSongs) * 100, 0));
                var calculatedProportion = new GenreProportion() { GenreName = analytics.Genre.GenreName, Percentage = percentage };
                usersSongsProportions.Add(calculatedProportion);
            }
            return usersSongsProportions;

        }
    }
}
