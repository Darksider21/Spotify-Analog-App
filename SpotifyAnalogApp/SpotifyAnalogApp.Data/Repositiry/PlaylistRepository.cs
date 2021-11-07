using SpotifyAnalogApp.Data.Data;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry
{
    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(SpotifyAnalogAppContext dbContext) : base(dbContext)
        {
        }

        
        public async Task CreatePlaylistForUser(Playlist playlis)
        {
            await AddAsync(playlis);
        }

        public async Task DeletePlaylist(Playlist playlis)
        {
            await DeleteAsync(playlis);
        }

        public async Task UpdatePlaylist(Playlist playlis)
        {
            await UpdateAsync(playlis);
        }
    }
}
