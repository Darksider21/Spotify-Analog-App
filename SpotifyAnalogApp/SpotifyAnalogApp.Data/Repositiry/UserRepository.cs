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
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(SpotifyAnalogAppContext dbContext) : base(dbContext)
        {
        }

        

        

        public async Task CreateUser(AppUser user)
        {
            await AddAsync(user);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await GetUserById(userId);
            await DeleteAsync(user);
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _dbContext.AppUsers.Where(x => x.AppUserId.Equals(id)).Include(x => x.UsersPlaylists).ThenInclude(x => x.SongsInPlaylist).Include(x => x.FavoriteSongs).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUsersListAsync()
        {
            return await GetAllAsync();
        }

        

        public async  Task UpdateUser(AppUser user)
        {
           await UpdateAsync(user);
        }
    }
}
