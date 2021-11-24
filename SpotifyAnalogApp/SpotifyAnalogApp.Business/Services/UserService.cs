﻿using SpotifyAnalogApp.Business.DTO;
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
    public class UserService : IUserService
    {

        private IUserRepository userRepository;
        private ISongRepository songRepository;
        private IPlaylistRepository playlistRepository;
        private IAnalyticsService analyticsService;


        public UserService(IUserRepository userRepo , ISongRepository songRepo , IPlaylistRepository playlistRepo, IAnalyticsService analyticsService)
        {
            songRepository = songRepo;
            userRepository = userRepo;
            playlistRepository = playlistRepo;
            this.analyticsService = analyticsService;
        }
        public  async  Task<AppUserModel> CreateUser(string name, string Email)
        {
            var now = DateTime.Now;
            var user = new AppUser { Name = name, Email = Email, DateCreated = now , Analytics = new Analytics()};
           await  userRepository.CreateUser(user);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async Task DeleteUser(int userId)
        {
            var usersPlaylistsToDelete = await playlistRepository.GetPlaylistsByUserId(new int[] { userId});
            foreach (var playlist in usersPlaylistsToDelete)
            {
                await playlistRepository.DeletePlaylist(playlist.PlaylistId);
            }
            var usersAnalytics = await analyticsService.GetAnalyticsByUserId(userId);
            await userRepository.DeleteUser(userId);
            await analyticsService.DeleteAnalytics(userId);
        }

        public  async Task<AppUserModel> GetUserById(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(user);
            return mapped;
        }

        public async  Task<ICollection<AppUserModel>> GetUsers()
        {
            var users = await userRepository.GetUsersListAsync();
            var mapped = ObjectMapper.Mapper.Map<ICollection<AppUserModel>>(users);
            return mapped;
        }
        public async Task<AppUserModel> AddSongsToUsersFavorites(int userId, int[] songsIds)
        {
            var songsToWorkWith = await songRepository.GetSongsByIds(songsIds);
            var user = await userRepository.GetUserById(userId);
            IEnumerable<Song> usersSongs = new List<Song>();

            List<Song> newSongs = new List<Song>() { };
            if (user.FavoriteSongs.Any())
            {
                usersSongs = user.FavoriteSongs;

            }

            
            songsToWorkWith = songsToWorkWith.Except(usersSongs).ToList();
            newSongs.AddRange(usersSongs);
            newSongs.AddRange(songsToWorkWith);
            newSongs.Distinct();

            await analyticsService.AddSongsToUsersAnalytics(userId ,songsToWorkWith);
            
            var model = new ModifyUserModel { FavoriteSongs = newSongs };
            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, user);

            await userRepository.UpdateUser(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;


        }

        public  async Task<AppUserModel> RemoveSongsFromUsersFavorites(int userId, int[] songsIds)
        {
            var songsToWorkWith =  await songRepository.GetSongsByIds(songsIds);
            var user = await userRepository.GetUserById(userId);
            IEnumerable<Song> usersSongs = user.FavoriteSongs;

            List<Song> newSongs = new List<Song>() { };
            newSongs.AddRange(usersSongs);

            songsToWorkWith = songsToWorkWith.Where(x => usersSongs.Contains(x)).Distinct().ToList();
            newSongs = newSongs.Except(songsToWorkWith).ToList();
            newSongs = newSongs.Distinct().ToList();

            await analyticsService.RemoveSongsFromUsersAnalytics(userId, songsToWorkWith);
           
            
            var model = new ModifyUserModel { FavoriteSongs = newSongs };
            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, user);

            await userRepository.UpdateUser(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

        public async Task<AppUserModel> UpdateUserInfo(RequestUserModel userModel)
        {
            var currentuser = await userRepository.GetUserById(userModel.AppUserId);

            ModifyUserModel model = new ModifyUserModel { Name = userModel.Name, Email = userModel.Email };

            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, AppUser>(model, currentuser);

            await userRepository.UpdateUser(newUser);
            var mapped = ObjectMapper.Mapper.Map<AppUserModel>(newUser);
            return mapped;
        }

        
    }
}
