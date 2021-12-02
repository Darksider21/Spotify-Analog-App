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
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(SpotifyAnalogAppContext dbContext) : base(dbContext)
        {
        }

        

        

        public async Task CreateUserAsync(AppUser user)
        {
            await AddAsync(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                await DeleteAsync(user);
            }
            
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _dbContext.AppUsers.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _dbContext.AppUsers.Where(x => x.AppUserId.Equals(id))
                .Include(x => x.UsersPlaylists).ThenInclude(x => x.SongsInPlaylist).Include(x => x.FavoriteSongs).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersByIdsAsync(int[] ids)
        {
            return await _dbContext.AppUsers.Where(x => ids.Contains(x.AppUserId))
                .Include(x => x.UsersPlaylists).ThenInclude(x => x.SongsInPlaylist).Include(x => x.FavoriteSongs).ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersListAsync()
        {
            return await GetAllAsync();
        }

        

        public async  Task UpdateUserAsync(AppUser user)
        {
           await UpdateAsync(user);
        }
    }
}
