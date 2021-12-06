using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public interface IDislikedSongRepository
    {
        public Task CreateDislikedSongForUser(DislikedSong song);
        public Task CreateMultipleDislikedSongsForUser(IEnumerable<DislikedSong> songs);
        public Task<IEnumerable<DislikedSong>> GetDislikedSongsByUserIdAsync(int userId);
        public Task<IEnumerable<DislikedSong>> GetDislikedSongsByMultipleUsersIdAsync(int[] userIds);

        public Task UpdateDislikedSongAsync(DislikedSong song);
        public Task UpdateMultipleDislikedSongsAsync(IEnumerable<DislikedSong> songs);
        public Task DeleteDislikedSongAsync(DislikedSong song);
        public Task DeleteMultipleDislikedSongsAsync(IEnumerable<DislikedSong> songs);
    }
}
