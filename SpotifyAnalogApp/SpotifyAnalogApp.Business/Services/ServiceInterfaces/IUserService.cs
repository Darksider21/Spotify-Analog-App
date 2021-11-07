using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IUserService
    {
        public Task<ICollection<UserModel>> GetUsers();

        public Task<UserModel> GetUserById(int userId);

        public Task<UserModel> CreateUser(string name, string Email);

        public Task<UserModel> UpdateUserInfo(string name, string Email, int userId);

        public Task<UserModel> ModifyFavorites(string action, int userId, int[] songsIds);

        public Task DeleteUser(int userId);

    }
}
