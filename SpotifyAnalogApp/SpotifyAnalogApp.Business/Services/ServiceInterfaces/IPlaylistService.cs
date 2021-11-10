using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IPlaylistService
    {
        public Task<IEnumerable<PlaylistModel>> GetAllPlaylists();
        public Task<PlaylistModel> GetPlaylistById(int playlistId);
        public Task<IEnumerable<PlaylistModel>> GetPlaylistsByUserId(int[] userId);

        public Task<PlaylistModel> CreatePlaylist(int userId, int[] songsid, string playlistName);
        public Task<PlaylistModel> ModifyPlaylist(string action, int playlistId, int[] songsid, string playlistName);
    }
}
