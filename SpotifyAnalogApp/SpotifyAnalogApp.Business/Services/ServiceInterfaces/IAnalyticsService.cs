using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IAnalyticsService
    {
        public Task<IEnumerable<GenreAnalyticsModel>> GetAnalyticsByUserIdAsync(int userId);
        public Task<IEnumerable<GenreAnalyticsModel>> GetAnalyticsByUserIdsAsync(int[] userIds);

        public Task AddSongsToUserAnalyticsAsync(AppUser appUser, IEnumerable<Song> songs);
        public Task RemoveSongsFromUserAnalyticsAsync(AppUser appUser, IEnumerable<Song> songs);

        public Task DeleteAllUserAnalyticsAsync(AppUser appUser);

        
    }
}
