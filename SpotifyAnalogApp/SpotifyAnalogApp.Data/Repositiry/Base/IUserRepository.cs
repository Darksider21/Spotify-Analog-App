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

        public Task<IEnumerable<User>> GetUsersListAsync();
        public Task<User> GetUserById(int id);

        public Task CreateUser(User user);
        public Task DeleteUser(int id);
        public Task UpdateUser(User user);


       



    }
}
