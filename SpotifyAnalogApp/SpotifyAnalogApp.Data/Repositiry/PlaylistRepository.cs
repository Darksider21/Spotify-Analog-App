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


        
        public async Task CreatePlaylistForUser(Playlist playlis)
        {
            await AddAsync(playlis);
        }

        public async Task DeletePlaylist(int playlistId)
        {
            var playlist =  await GetPlaylistById(playlistId);
            await DeleteAsync(playlist);
        }

        public async Task<Playlist> GetPlaylistById(int playlistId)
        {
            return  await _dbContext.Playlists.Where(x => x.PlaylistId.Equals(playlistId)).Include(x => x.SongsInPlaylist).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylists()
        {
            return await _dbContext.Playlists.Select(x => x).Include(x => x.SongsInPlaylist).ToListAsync();
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsByUserId(int[] userIds)
        {
            return await _dbContext.Playlists.Where(x => userIds.Contains(x.User.AppUserId)).Select(x => x).Include(x => x.SongsInPlaylist).ToListAsync();
        }

        public async Task UpdatePlaylist(Playlist playlis)
        {

            await UpdateAsync(playlis);
        }
    }
}
