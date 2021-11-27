using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.ModificationsDTOs;
using SpotifyAnalogApp.Business.DTO.RequestDto;
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
    public class AppUserService : IAppUserService
    {

        private IAppUserRepository userRepository;
        private ISongRepository songRepository;
        private IPlaylistRepository playlistRepository;
        private IAnalyticsService analyticsService;


        public AppUserService(IAppUserRepository userRepo , ISongRepository songRepo , IPlaylistRepository playlistRepo, IAnalyticsService analyticsService)
        {
            songRepository = songRepo;
            userRepository = userRepo;
            playlistRepository = playlistRepo;
            this.analyticsService = analyticsService;
        }
        public  async  Task<AppUserModel> CreateUserAsync(string name, string Email)
        {
            var now = DateTime.Now;
            var user = new AppUser { Name = name, Email = Email, DateCreated = now};
           await  userRepository.CreateUserAsync(user);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var usersPlaylistsToDelete = await playlistRepository.GetPlaylistsByUserIdAsync(new int[] { userId});
            foreach (var playlist in usersPlaylistsToDelete)
            {
                await playlistRepository.DeletePlaylistAsync(playlist);
            }
            await userRepository.DeleteUserAsync(userId);
        }

        public  async Task<AppUserModel> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async  Task<ICollection<AppUserModel>> GetUsersAsync()
        {
            var users = await userRepository.GetUsersListAsync();
            var mapped = ObjectMapper.Mapper.Map<ICollection<AppUserModel>>(users);
            return mapped;
        }
        public async Task<AppUserModel> AddSongsToUsersFavoritesAsync(int userId, int[] songsIds)
        {
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(songsIds);
            var user = await userRepository.GetUserByIdAsync(userId);
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

        public  async Task<AppUserModel> RemoveSongsFromUsersFavoritesAsync(int userId, int[] songsIds)
        {
            var songsToWorkWith =  await songRepository.GetSongsByIdsAsync(songsIds);
            var user = await userRepository.GetUserByIdAsync(userId);
            IEnumerable<Song> usersSongs = user.FavoriteSongs;

            List<Song> newSongs = new List<Song>() { };
            newSongs.AddRange(usersSongs);

            songsToWorkWith = songsToWorkWith.Where(x => usersSongs.Contains(x)).Distinct().ToList();
            newSongs = newSongs.Except(songsToWorkWith).ToList();
            newSongs = newSongs.Distinct().ToList();

           await analyticsService.RemoveSongsFromUserAnalyticsAsync(user , songsToWorkWith);
           
            
            var model = new ModifyUserModel { FavoriteSongs = newSongs };
            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, user);

            await userRepository.UpdateUserAsync(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

        public async Task<AppUserModel> UpdateUserInfoAsync(RequestUserModel userModel)
        {
            var currentuser = await userRepository.GetUserByIdAsync(userModel.AppUserId);

            ModifyUserModel model = new ModifyUserModel { Name = userModel.Name, Email = userModel.Email };

            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, currentuser);

            await userRepository.UpdateUserAsync(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

        
    }
}
