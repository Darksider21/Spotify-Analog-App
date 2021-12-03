using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Data;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public class DislikedSongRepository : Repository<DislikedSong> , IDislikedSongRepository
    {

        public DislikedSongRepository(SpotifyAnalogAppContext context) : base(context)
        {

        }

        public async Task CreateDislikedSongForUser(DislikedSong song)
        {
            await base.AddAsync(song); 
        }

        public async Task CreateMultipleDislikedSongsForUser(IEnumerable<DislikedSong> songs)
        {
            _dbContext.Set<DislikedSong>().AddRange(songs);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDislikedSongAsync(DislikedSong song)
        {
            await DeleteAsync(song);
        }

        public async Task DeleteMultipleDislikedSongsAsync(IEnumerable<DislikedSong> songs)
        {
            _dbContext.DislikedSongs.RemoveRange(songs);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DislikedSong>> GetDislikedSongsByMultipleUsersIdAsync(int[] userIds)
        {
            return await _dbContext.DislikedSongs.Where(x => userIds.Contains(x.AppUser.AppUserId) && x.AppUser.IsDeleted == false)
                .Include(x => x.Song.Author.Genre).ToListAsync();
        }

        public async Task<IEnumerable<DislikedSong>> GetDislikedSongsByUserIdAsync(int userId)
        {
            return await _dbContext.DislikedSongs.Where(x => x.AppUser.AppUserId.Equals(userId) && x.AppUser.IsDeleted == false)
                .Include(x => x.Song.Author.Genre).ToListAsync();
        }

        public async Task UpdateDislikedSongAsync(DislikedSong song)
        {
            _dbContext.DislikedSongs.Update(song);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMultipleDislikedSongsAsync(IEnumerable<DislikedSong> songs)
        {
            _dbContext.DislikedSongs.UpdateRange(songs);
            await _dbContext.SaveChangesAsync();
        }
    }
}
