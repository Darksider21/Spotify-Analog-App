using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
   public interface IAnalyticsRepository
    {
        public Task DeleteAnalyticsAsync(int userId);
        public Task UpdateAnalyticsAsync(GenreAnalytics obj);
        public Task UpdateMultipleAnalyticsAsync(IEnumerable<GenreAnalytics> obj);

        public Task CreateAnalyticsForUserAsync(GenreAnalytics analytics);
        public Task CreateMultipleAnalyticsForUserAsync(IEnumerable<GenreAnalytics> analytics);


        public Task<IEnumerable<GenreAnalytics>> GetAnalyticsByUserIdAsync(int userId);
        public  Task<IEnumerable<GenreAnalytics>> GetAnalyticsByUserIdsAsync(int[] userIds);

    }
}
