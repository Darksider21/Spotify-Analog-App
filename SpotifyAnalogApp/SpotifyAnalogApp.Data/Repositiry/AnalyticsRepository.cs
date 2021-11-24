using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Data;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry
{
    public class AnalyticsRepository : Repository<Analytics> , IAnalyticsRepository
    {

        
        public AnalyticsRepository(SpotifyAnalogAppContext context) : base(context)
        {

        }

        public async Task DeleteAnalytics(int userId)
        {
           var analytics = await GetAnalyticsByUserId(userId);
            if (analytics != null)
            {
              await  base.DeleteAsync(analytics);
            }

        }

        public async Task<IEnumerable<Analytics>> GetAllAnalytics()
        {
            return await _dbContext.Analytics.Select(x => x).ToListAsync();
        }

        public async Task<Analytics> GetAnalyticsByUserId(int userId)
        {
            return await base.GetByIdAsync(userId);
        }
        public async Task<IEnumerable<Analytics>> GetAnalyticsByUserIds(int[] userId)
        {
            return await _dbContext.AppUsers.Where(x => userId.Contains(x.AppUserId)).Select(x => x.Analytics).ToListAsync();
        }

        public async Task UpdateAnalytics(Analytics obj)
        {
            await base.UpdateAsync(obj);
        }
    }
}
