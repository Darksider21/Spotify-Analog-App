using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.RequestDto;
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

        public Task<AppUserModel> UpdateUserInfo(RequestUserModel userModel);

        public Task<AppUserModel> AddSongsToUsersFavorites(int userId, int[] songsIds);
        public Task<AppUserModel> RemoveSongsFromUsersFavorites(int userId, int[] songsIds);

        public Task DeleteUser(int userId);

    }
}
