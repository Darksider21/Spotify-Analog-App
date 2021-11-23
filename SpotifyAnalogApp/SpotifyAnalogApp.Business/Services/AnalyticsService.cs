using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.ModificationsDTOs;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class AnalyticsService : IAnalyticsService
    {

        public AnalyticsService()
        {

        }

        public Task AddSongsToUsersAnalytics(int userId, IEnumerable<Song> songs)
        {
            throw new NotImplementedException();
        }

        public Task<AnalyticsModel> GetAnalyticsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSongsFromUsersAnalytics(int userId, IEnumerable<Song> songs)
        {
            throw new NotImplementedException();
        }



        public AnalyticsUpdateModel CountTotalSongsAndTheirGenres(IEnumerable<Song> songsToCount)
        {

            var analytics = new AnalyticsUpdateModel();
            analytics.TotalSongsCount = songsToCount.Count();
            analytics.MetalSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Metal").Count();
            analytics.JazzSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Jazz").Count();
            analytics.RockSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Rock").Count();
            analytics.PopSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Pop").Count();
            analytics.JPopSongsCount = songsToCount.Where(x => x.Genre.GenreName == "JPop").Count();
            analytics.ElectronicSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Electronic").Count();
            analytics.ClassicalSongsCount = songsToCount.Where(x => x.Genre.GenreName == "Classical").Count();

            return analytics;
        }
    }
}
