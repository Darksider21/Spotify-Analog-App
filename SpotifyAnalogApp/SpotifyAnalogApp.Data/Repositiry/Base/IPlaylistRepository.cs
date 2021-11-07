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

        public Task CreatePlaylistForUser(Playlist playlis);

        public Task UpdatePlaylist(Playlist playlis);

        public Task DeletePlaylist(Playlist playlis);



    }
}
