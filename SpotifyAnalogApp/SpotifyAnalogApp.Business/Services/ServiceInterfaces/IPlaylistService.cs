using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.RequestDto;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IPlaylistService
    {
        public Task<IEnumerable<PlaylistModel>> GetAllPlaylistsAsync();
        public Task<PlaylistModel> GetPlaylistByIdAsync(int playlistId);
        public Task<IEnumerable<PlaylistModel>> GetPlaylistsByUserIdAsync(int[] userIds);

        public Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistModel playlistModel);
        public Task<PlaylistModel> AddSongsToPlaylistAsync(RequestPlaylistModel playlistModel);
        public Task<PlaylistModel> RemoveSongsFromPlaylistAsync(RequestPlaylistModel playlistModel);

        public Task DeletePlaylistAsync(int playlistId);
    }
}
