using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.ModificationsDTOs;
using SpotifyAnalogApp.Business.DTO.RequestDto;
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
        public  async  Task<AppUserModel> CreateUserAsync(string name, string email)
        {
            if (name == null || email == null)
            {
                throw new NullFieldsException();
            }
            var userWithSameEmail = await userRepository.GetUserByEmail(email);
            if (userWithSameEmail != null)
            {
                throw new DuplicateEmailException();
            }
            var now = DateTime.Now;
            var user = new AppUser { Name = name, Email = email, DateCreated = now};
           await  userRepository.CreateUserAsync(user);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            
            var usersPlaylistsToDelete = await playlistRepository.GetPlaylistsByUserIdAsync(new int[] { userId });
            foreach (var playlist in usersPlaylistsToDelete)
            {
                await playlistRepository.DeletePlaylistAsync(playlist);
            }
            await analyticsService.DeleteAllUserAnalyticsAsync(user);
            await userRepository.DeleteUserAsync(userId);
        }

        public  async Task<AppUserModel> GetUserByIdAsync(int userId)
        {

            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidUserIdException();
            }
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async  Task<ICollection<AppUserModel>> GetUsersAsync()
        {
            var users = await userRepository.GetUsersListAsync();
            if (!users.Any())
            {
                throw new InvalidUserIdException();
            }
            var mapped = ObjectMapper.Mapper.Map<ICollection<AppUserModel>>(users);
            return mapped;
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

        public  async Task<AppUserModel> RemoveSongsFromUsersFavoritesAsync(int userId, int[] songsIds)
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
            if (currentuser == null)
            {
                throw new InvalidUserIdException();
            }

            ModifyUserModel model = new ModifyUserModel { Name = userModel.Name, Email = userModel.Email };

            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, currentuser);

            await userRepository.UpdateUserAsync(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

        
    }
}
