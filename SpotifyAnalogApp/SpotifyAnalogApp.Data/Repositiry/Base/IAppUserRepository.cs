using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
   public interface IAppUserRepository
    {

        public Task<IEnumerable<AppUser>> GetUsersListAsync();
        public Task<AppUser> GetUserByIdAsync(int id);
        public Task<IEnumerable<AppUser>> GetUsersByIdsAsync(int[] ids);
        public Task<AppUser> GetUserByEmail(string email);

        public Task CreateUserAsync(AppUser user);
        public Task DeleteUserAsync(int id);
        public Task UpdateUserAsync(AppUser user);


       



    }
}
