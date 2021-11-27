using SpotifyAnalogApp.Business.DTO;
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

        public async Task<PlaylistModel> CreatePlaylistAsync(int userId, int[] songsid , string playlistName)
        {
            var SongsToAdd = await songRepository.GetSongsByIdsAsync(songsid);
            var user = await userRepository.GetUserByIdAsync(userId);
            SongsToAdd = SongsToAdd.Distinct();
            var newPlaylist = new Playlist() { PlaylistName= playlistName , SongsInPlaylist = SongsToAdd.ToList() , User = user};

            await analyticsService.AddSongsToUserAnalyticsAsync(user,SongsToAdd);
            await playlistRepository.CreatePlaylistForUserAsync(newPlaylist);

            var mapped = ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
            return mapped;
            
        }

        public async Task<IEnumerable<PlaylistModel>> GetAllPlaylistsAsync()
        {
            var playlists = await playlistRepository.GetPlaylistsAsync();

            var mapped = ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);

            return mapped;

        }

        public async Task<PlaylistModel> GetPlaylistByIdAsync(int playlistId)
        {
            var playlist =  await playlistRepository.GetPlaylistByIdAsync(playlistId);

            return ObjectMapper.Mapper.Map<PlaylistModel>(playlist);

        }

        

        public  async Task<IEnumerable<PlaylistModel>> GetPlaylistsByUserIdAsync(int[] userId)
        {
            var playlists = await playlistRepository.GetPlaylistsByUserIdAsync(userId);
            return ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);
        }

        public async Task<PlaylistModel> AddSongsToPlaylistAsync(RequestPlaylistModel playlistModel)
        {

            var songsToWorkWith = await songRepository.GetSongsByIdsAsync(playlistModel.SongsIds);
            var originalPlaylist = await playlistRepository.GetPlaylistByIdAsync(playlistModel.PlaylistId);
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
            var originalPlaylist = await playlistRepository.GetPlaylistByIdAsync(playlistModel.PlaylistId);
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
            if (!String.IsNullOrEmpty(playlistId.ToString()))
            {
                var playlist = await playlistRepository.GetPlaylistByIdAsync(playlistId);

                
                if (playlist != null)
                {
                    var user = playlist.User;
                    await analyticsService.RemoveSongsFromUserAnalyticsAsync(user, playlist.SongsInPlaylist);
                    await playlistRepository.DeletePlaylistAsync(playlist);

                }
            }
            
        }


    }
        
    }

