using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IRatingService
    {
        public Task<AppUserModel> AddSongsToUsersFavoritesAsync(int userId, int[] songsIds);
        public Task<AppUserModel> RemoveSongsFromUsersFavoritesAsync(int userId, int[] songsIds);
        public Task<ICollection<DislikedSongModel>> AddSongsToUsersDislikesAsync(int userId, int[] songsId);
        public Task<ICollection<DislikedSongModel>> RemoveSongsFromUsersDislikesAsync(int userId, int[] songsId);
    }
}
