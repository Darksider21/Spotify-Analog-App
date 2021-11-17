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
        public Task<ICollection<AppUserModel>> GetUsers();

        public Task<AppUserModel> GetUserById(int userId);

        public Task<AppUserModel> CreateUser(string name, string Email);

        public Task<AppUserModel> UpdateUserInfo(string name, string Email, int userId);

        public Task<AppUserModel> ModifyFavorites(string action, int userId, int[] songsIds);

        public Task DeleteUser(int userId);

    }
}
