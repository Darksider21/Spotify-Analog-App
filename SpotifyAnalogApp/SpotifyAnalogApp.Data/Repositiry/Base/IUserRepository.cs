using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
   public interface IUserRepository
    {

        public Task<IEnumerable<AppUser>> GetUsersListAsync();
        public Task<AppUser> GetUserById(int id);

        public Task CreateUser(AppUser user);
        public Task DeleteUser(int id);
        public Task UpdateUser(AppUser user);


       



    }
}
