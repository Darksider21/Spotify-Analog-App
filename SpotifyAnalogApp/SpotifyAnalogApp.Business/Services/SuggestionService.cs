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
        private IRandomService randomService;
        private IPlaylistRepository playlistRepository;
        private IDislikedSongRepository dislikedSongRepository;

        public SuggestionService(IAppUserRepository appUserRepository,ISongRepository songRepository,
            IAnalyticsRepository analyticsRepository , IRandomService randomService , IPlaylistRepository playlistRepository
            ,IDislikedSongRepository dislikedSongRepository)
        {
            this.analyticsRepository = analyticsRepository;
            this.songRepository = songRepository;
            this.appUserRepository = appUserRepository;
            this.randomService = randomService;
            this.playlistRepository = playlistRepository;
            this.dislikedSongRepository = dislikedSongRepository;
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
            var usersPlaylistSongs = await playlistRepository.GetAllUsersPlaylistSongsAsync(user);
            var usersFavoritesSongs = user.FavoriteSongs.Select(x => x).ToList();
            var UsersdislikedSongs = await dislikedSongRepository.GetDislikedSongsByUserIdAsync(user.AppUserId);
            var dislikedSongs = UsersdislikedSongs.Select(x => x.Song).ToList();

            var analyticsToWokrWith = usersAnalytics.Where(x => x.SongsOfThisGenreCount > 0).ToList();

            if (!analyticsToWokrWith.Any())
            {
                var songs = await songRepository.GetRandomSongsListAsync(amountOfsongs);
                return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songs);
            }

            
            var availibleGenres = analyticsToWokrWith.Select(x => x.Genre.GenreName).ToArray();
            var songsToWorkWith = await songRepository.GetSongsByMultipleGenresAsync(availibleGenres);

            List<Song> ShuffledsongsList = songsToWorkWith.OrderBy(_ => Guid.NewGuid()).ToList();
            var calculatedGenresWeight = CalculateGenresWeight(availibleGenres, usersPlaylistSongs, usersFavoritesSongs , dislikedSongs);
            var calculatedGenresWeightProportion = CalculateGenresProportions(calculatedGenresWeight).OrderBy(x => x.Percentage).Reverse().ToArray();

            var weightedSongsCount = amountOfsongs - amountOfsongs * 0.10;
            List<Song> suggestedSongs = new();
            while (suggestedSongs.Count < weightedSongsCount)
            {
                for (int i = 0; i < calculatedGenresWeightProportion.Length; i++)
                {
                    var item = calculatedGenresWeightProportion[i];
                    var randomNumber = randomService.GetRandom().Next(0, 101);
                    if (randomNumber <= item.Percentage)
                    {
                        var songToAdd = ShuffledsongsList
                            .Where(x => x.Genre.GenreName.Equals(item.GenreName) && !dislikedSongs.Select(x=> x.Author.Name).Contains(x.Author.Name))
                            .FirstOrDefault();
                        suggestedSongs.Add(songToAdd);
                        ShuffledsongsList.Remove(songToAdd);
                    }
                }
            }

            var randomSongsFromRepository = await songRepository.GetRandomSongsListAsync(amountOfsongs * 4);
            List<Song> randomSongsToFillLastSuggestions = randomSongsFromRepository.OrderBy(_ => Guid.NewGuid()).ToList();
            while (suggestedSongs.Count < amountOfsongs)
            {
                var songToAdd = randomSongsToFillLastSuggestions.Where(x => !dislikedSongs.Select(x => x.Author.Name).Contains(x.Author.Name)).FirstOrDefault();
                suggestedSongs.Add(songToAdd);
                randomSongsToFillLastSuggestions.Remove(songToAdd);

            }


            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(suggestedSongs);
        }



        private IEnumerable<GenreWeight> CalculateGenresWeight(string[] availiableGenres, IEnumerable<Song> playlistSongs, IEnumerable<Song> favoritesSong,
            IEnumerable<Song> dislikedSongs)
        {
            List<GenreWeight> genreWeights = new();

            foreach (var genre in availiableGenres)
            {
                var newGenreWeright = new GenreWeight() { GenreName = genre };
                genreWeights.Add(newGenreWeright);

            }

            AddWeightToGenreWeight(genreWeights, playlistSongs, 1);

            AddWeightToGenreWeight(genreWeights, favoritesSong, 5);

            AddWeightToGenreWeight(genreWeights, dislikedSongs, -10);





            return genreWeights;
        }

        private void AddWeightToGenreWeight(List<GenreWeight> genreWeights, IEnumerable<Song> songCollection, int weightValue)
        {
            foreach (var song in songCollection)
            {
                var currentGenreWeight = genreWeights.FirstOrDefault(x => x.GenreName == song.Genre.GenreName);
                if (currentGenreWeight != null)
                {
                    currentGenreWeight.Weight += weightValue;
                }
            }
        }
        private IEnumerable<GenreProportion> CalculateGenresProportions(IEnumerable<GenreWeight> genreWeights)
        {
            List<GenreProportion> genreProportions = new();
            var totalWeigt = genreWeights.Where(x => x.Weight >= 0).Sum(x => x.Weight);


            genreProportions = genreWeights.Select(
                x => new GenreProportion()
            {
                GenreName = x.GenreName,
                Percentage = (x.Weight / totalWeigt) * 100
            }).ToList();
            return genreProportions;
        }














        //private IEnumerable<GenreProportion> CalculateSongsProportions(IEnumerable<GenreAnalytics> usersAnalytics)
        //{
        //    int totalSongs = usersAnalytics.Sum(x => x.SongsOfThisGenreCount);
        //    List<GenreProportion> usersSongsProportions = new();

        //    foreach (var analytics in usersAnalytics)
        //    {
        //        int percentage = Convert.ToInt32(Math.Round(((decimal)analytics.SongsOfThisGenreCount / totalSongs) * 100, 0));
        //        var calculatedProportion = new GenreProportion() { GenreName = analytics.Genre.GenreName, Percentage = percentage };
        //        usersSongsProportions.Add(calculatedProportion);
        //    }
        //    return usersSongsProportions;

        //}
    }
}
