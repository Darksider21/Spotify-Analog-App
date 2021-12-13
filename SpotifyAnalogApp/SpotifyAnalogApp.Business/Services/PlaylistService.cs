using SpotifyAnalogApp.Business.DTO;
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
    public class PlaylistService : IPlaylistService
    {
        private IPlaylistRepository playlistRepository;
        private ISongRepository songRepository;
        private IAppUserRepository userRepository;
        private IAnalyticsService analyticsService;

        public PlaylistService(IPlaylistRepository repository , ISongRepository songRepo, IAppUserRepository userRepo , IAnalyticsService analyticsService)
        {
            playlistRepository = repository;
            songRepository = songRepo;
            userRepository = userRepo;
            this.analyticsService = analyticsService;
            
        }

        public async Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistModel playlistModel)
        {
            var user = await userRepository.GetUserByIdAsync(playlistModel.UserId);
            if (user==null)
            {
                throw new InvalidUserIdException();
            }
            var SongsToAdd = await songRepository.GetSongsByIdsAsync(playlistModel.SongsIds);
            if (!SongsToAdd.Any())
            {
                var newPlaylistWithoutSongs = new Playlist() { PlaylistName = playlistModel.PlaylistName, SongsInPlaylist = SongsToAdd.ToList(), User = user };
                await playlistRepository.CreatePlaylistForUserAsync(newPlaylistWithoutSongs);

                var mappedWithoutSongs = ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylistWithoutSongs);
                return mappedWithoutSongs;
            }
            SongsToAdd = SongsToAdd.Distinct();
            var newPlaylist = new Playlist() { PlaylistName= playlistModel.PlaylistName , SongsInPlaylist = SongsToAdd.ToList() , User = user};

            await analyticsService.AddSongsToUserAnalyticsAsync(user,SongsToAdd);
            await playlistRepository.CreatePlaylistForUserAsync(newPlaylist);

            var mapped = ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
            return mapped;
            
        }

        public async Task<IEnumerable<PlaylistModel>> GetAllPlaylistsAsync()
        {
            var playlists = await playlistRepository.GetPlaylistsAsync();
            if (!playlists.Any())
            {
                throw new ContentNotFoundException();
            }

            var mapped = ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);

            return mapped;

        }

        public async Task<PlaylistModel> GetPlaylistByIdAsync(int playlistId)
        {
            var playlist =  await playlistRepository.GetPlaylistByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new InvalidPlaylistIdException();
            }

            return ObjectMapper.Mapper.Map<PlaylistModel>(playlist);

        }

        

        public  async Task<IEnumerable<PlaylistModel>> GetPlaylistsByUserIdAsync(int[] userIds)
        {
            var user = await userRepository.GetUsersByIdsAsync(userIds);
            if (!user.Any())
            {
                throw new InvalidUserIdException();
            }
            var playlists = await playlistRepository.GetPlaylistsByMultipleUsersIds(userIds);
            
            
                return ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);
            
            
                
        }

        public async Task<PlaylistModel> AddSongsToPlaylistAsync(RequestPlaylistModel playlistModel)
        {

            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(playlistModel.SongsIds);

            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }

            var originalPlaylist = await playlistRepository.GetPlaylistByIdAsync(playlistModel.PlaylistId);
            if (originalPlaylist == null)
            {
                throw new InvalidPlaylistIdException();
            }
            List<Song> newSongs = new List<Song>();

            newSongs.AddRange(originalPlaylist.SongsInPlaylist);

            newSongs.AddRange(songsToWorkWith);

            newSongs = newSongs.Distinct().ToList();

            var songsToAddToAnalytics = songsToWorkWith.Except(originalPlaylist.SongsInPlaylist).ToList().Distinct();
            var user = originalPlaylist.User;
            await analyticsService.AddSongsToUserAnalyticsAsync(user,songsToAddToAnalytics);

            var newPlaylistModel = new ModifyPlaylistModel() { SongsInPlaylist = newSongs };

            var newPlaylist = ObjectMapper.Mapper.Map<ModifyPlaylistModel, Playlist>(newPlaylistModel, originalPlaylist);
            await playlistRepository.UpdatePlaylistAsync(newPlaylist);
            return ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
        }

        public async Task<PlaylistModel> RemoveSongsFromPlaylistAsync(RequestPlaylistModel playlistModel)
        {
            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(playlistModel.SongsIds);

            if (!songsToWorkWith.Any())
            {
                throw new InvalidSongIdException();
            }

            var originalPlaylist = await playlistRepository.GetPlaylistByIdAsync(playlistModel.PlaylistId);

            if (originalPlaylist == null)
            {
                throw new InvalidPlaylistIdException();
            }

            List<Song> newSongs = new List<Song>();
            newSongs.AddRange(originalPlaylist.SongsInPlaylist);

            
            newSongs = newSongs.Except(songsToWorkWith).ToList();
            newSongs = newSongs.Distinct().ToList();

            var songsToRemoveFromAnalytics = songsToWorkWith.Where(x => originalPlaylist.SongsInPlaylist.Contains(x)).Distinct().ToList();
            var user = originalPlaylist.User;
            await analyticsService.RemoveSongsFromUserAnalyticsAsync(user ,songsToRemoveFromAnalytics);

            var newPlaylistModel = new ModifyPlaylistModel() { SongsInPlaylist = newSongs };

            var newPlaylist = ObjectMapper.Mapper.Map<ModifyPlaylistModel, Playlist>(newPlaylistModel, originalPlaylist);
            await playlistRepository.UpdatePlaylistAsync(newPlaylist);
            return ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
        }

        public async Task DeletePlaylistAsync(int playlistId)
        {
            
            
            var playlist = await playlistRepository.GetPlaylistByIdAsync(playlistId);
                
                
            if (playlist == null)
            {
                throw new InvalidPlaylistIdException();
            }
            
            var user = playlist.User;
            await analyticsService.RemoveSongsFromUserAnalyticsAsync(user, playlist.SongsInPlaylist);
            await playlistRepository.DeletePlaylistAsync(playlist);
            
        }


    }
        
    }

