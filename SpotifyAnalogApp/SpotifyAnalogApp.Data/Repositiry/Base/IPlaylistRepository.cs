using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public interface IPlaylistRepository
    {
        public Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        public Task<Playlist> GetPlaylistByIdAsync(int playlistId);

        public Task<IEnumerable<Playlist>> GetPlaylistsByUserIdAsync(int[] userIds);

        public Task CreatePlaylistForUserAsync(Playlist playlis);

        public Task UpdatePlaylistAsync(Playlist playlis);

        public Task DeletePlaylistAsync(Playlist playlist);



    }
}
