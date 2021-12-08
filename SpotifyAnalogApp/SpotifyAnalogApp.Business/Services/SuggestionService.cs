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
        private const int playlistWeight = 1;
        private const int favoriteWeight = 5;
        private const int dislikeWeight = -10;

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
            var usersDislikedSongs = await dislikedSongRepository.GetDislikedSongsByUserIdAsync(user.AppUserId);
            var dislikedSongs = usersDislikedSongs.Select(x => x.Song).ToList();

            var analyticsToWokrWith = usersAnalytics.Where(x => x.SongsOfThisGenreCount > 0).ToList();

            if (!analyticsToWokrWith.Any())
            {
                var songs = await songRepository.GetRandomSongsListAsync(amountOfsongs);
                return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songs);
            }
            var genreProportions = GenerateGenreProportionsFromAnalytics(analyticsToWokrWith);
            
            var availibleGenres = analyticsToWokrWith.Select(x => x.Genre.GenreName).ToArray();
            var songsToWorkWith = await songRepository.GetSongsByMultipleGenresAsync(availibleGenres);

            List<Song> ShuffledsongsListWithoutDislikedAuthors = songsToWorkWith
                .Where(x => !usersDislikedSongs.Select(y => y.Song.Author.Name).Contains(x.Author.Name)).Select(x => x)
                .OrderBy(_ => Guid.NewGuid()).ToList();

            var UnweightedSongsInitialCollection = InitializeWeightedSongs(ShuffledsongsListWithoutDislikedAuthors);

            ApplyWeight(UnweightedSongsInitialCollection, usersPlaylistSongs , playlistWeight);
            ApplyWeight(UnweightedSongsInitialCollection,usersFavoritesSongs, favoriteWeight);
            ApplyWeight(UnweightedSongsInitialCollection,usersDislikedSongs.Select(x => x.Song), dislikeWeight);

            var fullyWeightedSongsCollection = UnweightedSongsInitialCollection.OrderBy(x => x.Weight).Reverse();

            double percentsOfRandomSuggestions = 0.10;
            var weightedSongsCount = amountOfsongs - amountOfsongs * percentsOfRandomSuggestions;

            List<Song> suggestedSongs = new();

            foreach (var genre in genreProportions)
            {
                int numberOfSongsToTakeOfThisGenre = Convert.ToInt32(weightedSongsCount * (genre.Percentage / 100));
                var songsToAdd = fullyWeightedSongsCollection.Where(x => x.Song.Genre.GenreName.Equals(genre.Genre.GenreName))
                    .Select(x => x.Song).Take(numberOfSongsToTakeOfThisGenre);
                suggestedSongs.AddRange(songsToAdd);
            }            

            var randomSongsFromRepository = await songRepository.GetRandomSongsListAsync(amountOfsongs * 4);
            List<Song> randomSongsToFillLastSuggestions = randomSongsFromRepository.OrderBy(_ => Guid.NewGuid()).ToList();
            while (suggestedSongs.Count < amountOfsongs)
            {
                var songToAdd = randomSongsToFillLastSuggestions.Where(x => !dislikedSongs.Select(x => x.Author.Name).Contains(x.Author.Name)).FirstOrDefault();
                suggestedSongs.Add(songToAdd);
                randomSongsToFillLastSuggestions.Remove(songToAdd);

            }
            
            var shuffledSuggestedSongs = suggestedSongs.OrderBy(_ => Guid.NewGuid()).ToList();


            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(shuffledSuggestedSongs);
        }

        private List<GenreProportion> GenerateGenreProportionsFromAnalytics(List<GenreAnalytics> analyticsToWokrWith)
        {
            List<GenreProportion> genresToWorkWith = analyticsToWokrWith.Select(x => new GenreProportion() { Genre = x.Genre , Percentage = 0 }).ToList();
            double SumOfAllSongs = analyticsToWokrWith.Sum(x => x.SongsOfThisGenreCount);
            
            genresToWorkWith
                .ForEach(x => x.Percentage = analyticsToWokrWith.Where(y => y.Genre.GenreName.Equals(x.Genre.GenreName))
                .Select(z => z.SongsOfThisGenreCount)
                .FirstOrDefault() / SumOfAllSongs * 100);
            

            return genresToWorkWith;
        }

        private void ApplyWeight(List<WeightedSong> unweightedSongsInitialCollection, IEnumerable<Song> criteriaCollection , double weightIndex)
        {
            foreach (var weightedsong in unweightedSongsInitialCollection)
            {
                var songsWithSameGenre = criteriaCollection.Where(x => x.Genre.GenreName.Equals(weightedsong.Song.Genre.GenreName)).ToList();

                songsWithSameGenre.ForEach(x => weightedsong.Weight += weightIndex);

            }
        }

        

        private List<WeightedSong> InitializeWeightedSongs(List<Song> shuffledsongsList)
        {
            return shuffledsongsList.Select(x => new WeightedSong() { Song = x, Weight = 1 }).ToList();
        }
    }
}
