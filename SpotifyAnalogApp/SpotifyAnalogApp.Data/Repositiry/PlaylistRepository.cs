using Microsoft.EntityFrameworkCore;
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


        
        public async Task CreatePlaylistForUserAsync(Playlist playlis)
        {
            await AddAsync(playlis);
        }

        public async Task DeletePlaylistAsync(Playlist playlist)
        {
            
            await DeleteAsync(playlist);
        }

        public async Task<Playlist> GetPlaylistByIdAsync(int playlistId)
        {
            return  await _dbContext.Playlists.Where(x => x.PlaylistId.Equals(playlistId)).Include(x => x.SongsInPlaylist).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            return await _dbContext.Playlists.Select(x => x).Include(x => x.SongsInPlaylist).ToListAsync();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsByUserIdAsync(int[] userIds)
        {
            return await _dbContext.Playlists.Where(x => userIds.Contains(x.User.AppUserId)).Select(x => x).Include(x => x.SongsInPlaylist).ToListAsync();
        }

        public async Task UpdatePlaylistAsync(Playlist playlis)
        {

            await UpdateAsync(playlis);
        }
    }
}
