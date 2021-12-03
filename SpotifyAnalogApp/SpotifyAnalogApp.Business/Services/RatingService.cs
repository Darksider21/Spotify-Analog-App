using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Mapper;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class RatingService :IRatingService
    {
        private IAppUserRepository userRepository;
        private ISongRepository songRepository;
        private IDislikedSongRepository dislikedSongRepository;
        private IAnalyticsService analyticsService;


        public RatingService(IAppUserRepository userRepository,ISongRepository songRepository,IDislikedSongRepository dislikedSongRepository,
            IAnalyticsService analyticsService)
        {
            this.userRepository = userRepository;
            this.songRepository = songRepository;
            this.dislikedSongRepository = dislikedSongRepository;
            this.analyticsService = analyticsService;
        }
        public async Task<ICollection<DislikedSongModel>> AddSongsToUsersDislikesAsync(int userId, int[] songsId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(songsId);
            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }

            songsToWorkWith = songsToWorkWith.Where(x => !user.DislikedSongs.Select(x => x.Song).Contains(x)).ToList();
            List <DislikedSong> newDislikedSongs = new();
            foreach (var song in songsToWorkWith)
            {
                var newDislikedSong = new DislikedSong() { AppUser = user, Song = song };
                newDislikedSongs.Add(newDislikedSong);
            }

            await dislikedSongRepository.CreateMultipleDislikedSongsForUser(newDislikedSongs);
            var usersDislikes = await dislikedSongRepository.GetDislikedSongsByUserIdAsync(userId);
            var dislikeModels = usersDislikes.Select(x => x.Song).ToList();

            return ObjectMapper.Mapper.Map<ICollection<DislikedSongModel>>(usersDislikes);

            


        }
        public async Task<ICollection<DislikedSongModel>> RemoveSongsFromUsersDislikesAsync(int userId, int[] songsId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(songsId);
            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }


            var DislikedSongsToRemove = user.DislikedSongs.Where(x => songsToWorkWith.Contains(x.Song)).ToList();
            await dislikedSongRepository.DeleteMultipleDislikedSongsAsync(DislikedSongsToRemove);

            var usersDislikes = await dislikedSongRepository.GetDislikedSongsByUserIdAsync(userId);
            var dislikeModels = usersDislikes.Select(x => x.Song).ToList();

            return ObjectMapper.Mapper.Map<ICollection<DislikedSongModel>>(usersDislikes);

        }

        public async Task<AppUserModel> AddSongsToUsersFavoritesAsync(int userId, int[] songsIds)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(songsIds);
            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }
            IEnumerable<Song> usersSongs = new List<Song>();

            List<Song> newSongs = new List<Song>() { };
            if (user.FavoriteSongs.Any())
            {
                usersSongs = user.FavoriteSongs;

            }


            songsToWorkWith = songsToWorkWith.Except(usersSongs).ToList();
            newSongs.AddRange(usersSongs);
            newSongs.AddRange(songsToWorkWith);
            newSongs = newSongs.Distinct().ToList();

            await analyticsService.AddSongsToUserAnalyticsAsync(user, songsToWorkWith);

            var model = new ModifyUserModel { FavoriteSongs = newSongs };
            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, user);

            await userRepository.UpdateUserAsync(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;


        }


        public async Task<AppUserModel> RemoveSongsFromUsersFavoritesAsync(int userId, int[] songsIds)
        {

            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(songsIds);
            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }

            IEnumerable<Song> usersSongs = user.FavoriteSongs;

            List<Song> newSongs = new List<Song>() { };
            newSongs.AddRange(usersSongs);

            songsToWorkWith = songsToWorkWith.Where(x => usersSongs.Contains(x)).Distinct().ToList();
            newSongs = newSongs.Except(songsToWorkWith).ToList();
            newSongs = newSongs.Distinct().ToList();

            await analyticsService.RemoveSongsFromUserAnalyticsAsync(user, songsToWorkWith);


            var model = new ModifyUserModel { FavoriteSongs = newSongs };
            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, user);

            await userRepository.UpdateUserAsync(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

       
    }
}
