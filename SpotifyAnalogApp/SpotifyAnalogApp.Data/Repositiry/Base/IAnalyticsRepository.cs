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
        public Task DeleteAnalytics(int userId);
        public Task UpdateAnalytics(Analytics obj);

        public Task<IEnumerable<Analytics>> GetAllAnalytics();

        public Task<Analytics> GetAnalyticsByUserId(int userId);
        public  Task<IEnumerable<Analytics>> GetAnalyticsByUserIds(int[] userId);

    }
}
