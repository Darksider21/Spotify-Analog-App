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
        private IUserRepository userRepository;
        private IAnalyticsService analyticsService;

        public PlaylistService(IPlaylistRepository repository , ISongRepository songRepo, IUserRepository userRepo , IAnalyticsService analyticsService)
        {
            playlistRepository = repository;
            songRepository = songRepo;
            userRepository = userRepo;
            this.analyticsService = analyticsService;
            
        }

        public async Task<PlaylistModel> CreatePlaylist(int userId, int[] songsid , string playlistName)
        {
            var SongsToAdd = await songRepository.GetSongsByIds(songsid);
            var user = await userRepository.GetUserById(userId);
            SongsToAdd = SongsToAdd.Distinct();
            var newPlaylist = new Playlist() { PlaylistName= playlistName , SongsInPlaylist = SongsToAdd.ToList() , User = user};

            await analyticsService.AddSongsToUsersAnalytics(userId,SongsToAdd);
            await playlistRepository.CreatePlaylistForUser(newPlaylist);

            var mapped = ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
            return mapped;
            
        }

        public async Task<IEnumerable<PlaylistModel>> GetAllPlaylists()
        {
            var playlists = await playlistRepository.GetPlaylists();

            var mapped = ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);

            return mapped;

        }

        public async Task<PlaylistModel> GetPlaylistById(int playlistId)
        {
            var playlist =  await playlistRepository.GetPlaylistById(playlistId);

            return ObjectMapper.Mapper.Map<PlaylistModel>(playlist);

        }

        

        public  async Task<IEnumerable<PlaylistModel>> GetPlaylistsByUserId(int[] userId)
        {
            var playlists = await playlistRepository.GetPlaylistsByUserId(userId);
            return ObjectMapper.Mapper.Map<IEnumerable<PlaylistModel>>(playlists);
        }

        public async Task<PlaylistModel> AddSongsToPlaylist(RequestPlaylistModel playlistModel)
        {

            var songsToWorkWith = await songRepository.GetSongsByIds(playlistModel.SongsIds);
            var originalPlaylist = await playlistRepository.GetPlaylistById(playlistModel.PlaylistId);
            List<Song> newSongs = new List<Song>();

            newSongs.AddRange(originalPlaylist.SongsInPlaylist);

            newSongs.AddRange(songsToWorkWith);

            newSongs = newSongs.Distinct().ToList();

            var songsToAddToAnalytics = songsToWorkWith.Except(originalPlaylist.SongsInPlaylist).ToList().Distinct();
            var userid = originalPlaylist.User.AppUserId;
            await analyticsService.AddSongsToUsersAnalytics(userid,songsToAddToAnalytics);

            var newPlaylistModel = new ModifyPlaylistModel() { SongsInPlaylist = newSongs };

            var newPlaylist = ObjectMapper.Mapper.Map<ModifyPlaylistModel, Playlist>(newPlaylistModel, originalPlaylist);
            await playlistRepository.UpdatePlaylist(newPlaylist);
            return ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
        }

        public async Task<PlaylistModel> RemoveSongsFromPlaylist(RequestPlaylistModel playlistModel)
        {
            var songsToWorkWith = await songRepository.GetSongsByIds(playlistModel.SongsIds);
            var originalPlaylist = await playlistRepository.GetPlaylistById(playlistModel.PlaylistId);
            List<Song> newSongs = new List<Song>();
            newSongs.AddRange(originalPlaylist.SongsInPlaylist);

            
            newSongs = newSongs.Except(songsToWorkWith).ToList();
            newSongs = newSongs.Distinct().ToList();

            var songsToRemoveFromAnalytics = songsToWorkWith.Where(x => originalPlaylist.SongsInPlaylist.Contains(x)).Distinct().ToList();
            var userId = originalPlaylist.User.AppUserId;
            await analyticsService.RemoveSongsFromUsersAnalytics(userId ,songsToRemoveFromAnalytics);

            var newPlaylistModel = new ModifyPlaylistModel() { SongsInPlaylist = newSongs };

            var newPlaylist = ObjectMapper.Mapper.Map<ModifyPlaylistModel, Playlist>(newPlaylistModel, originalPlaylist);
            await playlistRepository.UpdatePlaylist(newPlaylist);
            return ObjectMapper.Mapper.Map<PlaylistModel>(newPlaylist);
        }

        public async Task DeletePlaylist(int playlistId)
        {
            var playlist = await GetPlaylistById(playlistId);
            if (playlist != null)
            {
                await playlistRepository.DeletePlaylist(playlistId);

            }
        }


    }
        
    }

