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
        public Task<IEnumerable<AnalyticsModel>> GetAnalytics(int[] userIds);
        public Task<AnalyticsModel> GetAnalyticsByUserId(int userId);

        public Task AddSongsToUsersAnalytics(int userId , IEnumerable<Song> songs);
        public Task RemoveSongsFromUsersAnalytics(int userId , IEnumerable<Song> songs);
    }
}
