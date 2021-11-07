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
        public Task<ICollection<PlaylistModel>> GetAllPlaylists();
        public Task<PlaylistModel> GetPlaylistById(int playlistId);
        public Task<ICollection<PlaylistModel>> GetPlaylistsByUserId(int UserId);
    }
}
