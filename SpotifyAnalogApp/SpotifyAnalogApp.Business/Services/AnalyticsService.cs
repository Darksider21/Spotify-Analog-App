using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.ModificationsDTOs;
using SpotifyAnalogApp.Business.Mapper;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public IAnalyticsRepository analyticsRepository;
        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            this.analyticsRepository = analyticsRepository;
        }
        public async Task DeleteAnalytics(int userId)
        {
            await analyticsRepository.DeleteAnalytics(userId);
        }

        public async Task AddSongsToUsersAnalytics(int userId, IEnumerable<Song> songs)
        {
            var analyticsToWorkWith = await analyticsRepository.GetAnalyticsByUserId(userId);

            var countedSongs = CountTotalSongsAndTheirGenres(songs);

            analyticsToWorkWith.ClassicalSongsCount += countedSongs.ClassicalSongsCount;
            analyticsToWorkWith.ElectronicSongsCount += countedSongs.ElectronicSongsCount;
            analyticsToWorkWith.JazzSongsCount += countedSongs.JazzSongsCount;
            analyticsToWorkWith.JPopSongsCount += countedSongs.JPopSongsCount;
            analyticsToWorkWith.MetalSongsCount += countedSongs.MetalSongsCount;
            analyticsToWorkWith.RockSongsCount += countedSongs.RockSongsCount;
            analyticsToWorkWith.PopSongsCount += countedSongs.PopSongsCount;
            analyticsToWorkWith.TotalSongsCount += countedSongs.TotalSongsCount;

            await analyticsRepository.UpdateAnalytics(analyticsToWorkWith);

        }

        public async Task<IEnumerable<AnalyticsModel>> GetAnalytics(int[] userIds)
        {
            IEnumerable<Analytics> analytics = new List<Analytics>();
            if (userIds.Any())
            {
                analytics = await analyticsRepository.GetAnalyticsByUserIds(userIds);
            }
            else
            {
                analytics = await analyticsRepository.GetAllAnalytics();
            }

            return ObjectMapper.Mapper.Map<IEnumerable<AnalyticsModel>>(analytics);

            
        }
        public async Task<AnalyticsModel> GetAnalyticsByUserId(int userId)
        {
            var analytics = await analyticsRepository.GetAnalyticsByUserId(userId);

            return ObjectMapper.Mapper.Map<AnalyticsModel>(analytics);

        }

        public async Task RemoveSongsFromUsersAnalytics(int userId, IEnumerable<Song> songs)
        {
            var analyticsToWorkWith = await analyticsRepository.GetAnalyticsByUserId(userId);

            var countedSongs = CountTotalSongsAndTheirGenres(songs);

            analyticsToWorkWith.ClassicalSongsCount -= countedSongs.ClassicalSongsCount;
            analyticsToWorkWith.ElectronicSongsCount -= countedSongs.ElectronicSongsCount;
            analyticsToWorkWith.JazzSongsCount -= countedSongs.JazzSongsCount;
            analyticsToWorkWith.JPopSongsCount -= countedSongs.JPopSongsCount;
            analyticsToWorkWith.MetalSongsCount -= countedSongs.MetalSongsCount;
            analyticsToWorkWith.RockSongsCount -= countedSongs.RockSongsCount;
            analyticsToWorkWith.PopSongsCount -= countedSongs.PopSongsCount;
            analyticsToWorkWith.TotalSongsCount -= countedSongs.TotalSongsCount;

            await analyticsRepository.UpdateAnalytics(analyticsToWorkWith);
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
