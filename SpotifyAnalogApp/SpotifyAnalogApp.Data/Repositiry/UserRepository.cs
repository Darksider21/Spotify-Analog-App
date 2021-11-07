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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SpotifyAnalogAppContext dbContext) : base(dbContext)
        {
        }

        

        

        public async Task CreateUser(User user)
        {
            await AddAsync(user);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await GetUserById(userId);
            await DeleteAsync(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.Where(x => x.UserId.Equals(id)).Include(x => x.UsersPlaylists).Include(x => x.FavoriteSongs).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersListAsync()
        {
            return await GetAllAsync();
        }

        

        public async  Task UpdateUser(User user)
        {
           await UpdateAsync(user);
        }
    }
}
