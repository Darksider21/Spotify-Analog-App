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
    public interface IAppUserService
    {
        public Task<ICollection<AppUserModel>> GetUsersAsync();

        public Task<AppUserModel> GetUserByIdAsync(int userId);

        public Task<AppUserModel> CreateUserAsync(string name, string Email);

        public Task<AppUserModel> UpdateUserInfoAsync(RequestUserModel userModel);

        public Task<AppUserModel> AddSongsToUsersFavoritesAsync(int userId, int[] songsIds);
        public Task<AppUserModel> RemoveSongsFromUsersFavoritesAsync(int userId, int[] songsIds);

        public Task DeleteUserAsync(int userId);

    }
}
