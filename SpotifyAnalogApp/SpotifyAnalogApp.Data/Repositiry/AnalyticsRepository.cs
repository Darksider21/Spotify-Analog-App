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
    public class AnalyticsRepository : Repository<GenreAnalytics>, IAnalyticsRepository
    {


        public AnalyticsRepository(SpotifyAnalogAppContext context) : base(context)
        {

        }

        public async Task CreateAnalyticsForUserAsync(GenreAnalytics analytics)
        {
            await base.AddAsync(analytics);
            
        }

        public async Task CreateMultipleAnalyticsForUserAsync(IEnumerable<GenreAnalytics> analytics)
        {
            await base._dbContext.Set<GenreAnalytics>().AddRangeAsync(analytics);
            await base._dbContext.SaveChangesAsync();
            
        }

        public async Task DeleteAnalyticsAsync(int userId)
        {

            var analytics = await GetAnalyticsByUserIdAsync(userId);

            _dbContext.Set<GenreAnalytics>().RemoveRange(analytics);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<GenreAnalytics>> GetAnalyticsByUserIdAsync(int userId)
        {
            return await _dbContext.GenreAnalytics.Where(x => x.AppUser.AppUserId.Equals(userId)).Include(x => x.Genre).Include(x => x.AppUser).ToListAsync();
        }

        public async Task<IEnumerable<GenreAnalytics>> GetAnalyticsByUserIdsAsync(int[] userIds)
        {
            return await _dbContext.GenreAnalytics.Where(x => userIds.Contains(x.AppUser.AppUserId)).Include(x => x.Genre).Include(x => x.AppUser).ToListAsync();
        }

        public async Task UpdateAnalyticsAsync(GenreAnalytics obj)
        {
            await UpdateAsync(obj);
        }

        public async Task UpdateMultipleAnalyticsAsync(IEnumerable<GenreAnalytics> obj)
        {
             _dbContext.Set<GenreAnalytics>().UpdateRange(obj);
            await _dbContext.SaveChangesAsync();
        }
    }
}
