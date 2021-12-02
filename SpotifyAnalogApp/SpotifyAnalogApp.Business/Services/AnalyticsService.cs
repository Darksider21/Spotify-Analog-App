using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.ModificationsDTOs;
using SpotifyAnalogApp.Business.Exceptions;
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
        public IAppUserRepository appUserRepository;
        public IGenreRepository genreRepository;
        public AnalyticsService(IAnalyticsRepository analyticsRepository , IAppUserRepository appUserRepository, IGenreRepository genreRepository)
        {
            this.analyticsRepository = analyticsRepository;
            this.appUserRepository = appUserRepository;
            this.genreRepository = genreRepository;
        }


        public async Task DeleteAllUserAnalyticsAsync(AppUser appUser)
        {
            var user = await appUserRepository.GetUserByIdAsync(appUser.AppUserId);
            if (user == null)
            {
                throw new BaseCustomException(404, "No User avaliable for this id");
            }
                await analyticsRepository.DeleteAnalyticsAsync(appUser.AppUserId);
        }

        public async Task<IEnumerable<GenreAnalyticsModel>> GetAnalyticsByUserIdAsync(int userId)
        {
            var analytics = await analyticsRepository.GetAnalyticsByUserIdAsync(userId);
            if (!analytics.Any())
            {
                throw new BaseCustomException(404, "No analytics availiable for this user id");
            }

            return ObjectMapper.Mapper.Map<IEnumerable<GenreAnalyticsModel>>(analytics);
        }

        public async Task<IEnumerable<GenreAnalyticsModel>> GetAnalyticsByUserIdsAsync(int[] userIds)
        {
            var users = await appUserRepository.GetUsersByIdsAsync(userIds);
            if (!users.Any())
            {
                throw new BaseCustomException(404, "No analytics Availiable for those user ids");
            }
            var analytics = await analyticsRepository.GetAnalyticsByUserIdsAsync(userIds);

            return ObjectMapper.Mapper.Map<IEnumerable<GenreAnalyticsModel>>(analytics);
        }

        public async Task AddSongsToUserAnalyticsAsync(AppUser appUser, IEnumerable<Song> songs)
        {
            
            if (songs.Any())
            {
                await AddMissingAnalyticsForUser(appUser, songs);

                var usersAnalytics = await analyticsRepository.GetAnalyticsByUserIdAsync(appUser.AppUserId);
                List<GenreAnalytics> modifiedAnalytics = new List<GenreAnalytics>();

                foreach (var song in songs)
                {
                    var modifiedObject = usersAnalytics.Where(x => x.Genre.GenreId.Equals(song.Genre.GenreId)).FirstOrDefault();
                    modifiedObject.SongsOfThisGenreCount += 1;
                    modifiedAnalytics.Add(modifiedObject);
                }

                await analyticsRepository.UpdateMultipleAnalyticsAsync(modifiedAnalytics);

            }
        }
        public async Task RemoveSongsFromUserAnalyticsAsync(AppUser appUser, IEnumerable<Song> songs)
        {
            if (songs.Any())
            {
                var usersAnalytics = await analyticsRepository.GetAnalyticsByUserIdAsync(appUser.AppUserId);
                List<GenreAnalytics> modifiedAnalytics = new List<GenreAnalytics>();

                foreach (var song in songs)
                {
                    var modifiedObject = usersAnalytics.Where(x => x.Genre.GenreId.Equals(song.Genre.GenreId)).FirstOrDefault();
                    modifiedObject.SongsOfThisGenreCount -= 1;
                    modifiedAnalytics.Add(modifiedObject);
                }

                await analyticsRepository.UpdateMultipleAnalyticsAsync(modifiedAnalytics);

            }
        }

        public async Task AddMissingAnalyticsForUser(AppUser appUser, IEnumerable<Song> songs)
        {
            
            var usersAnalytics = await analyticsRepository.GetAnalyticsByUserIdAsync(appUser.AppUserId);

            var usersExistingGenres = usersAnalytics.Select(x => x.Genre.GenreId).ToList();
            var allSongsGenres = songs.Select(x => x.Genre.GenreId).ToList();

            var genreIdsToAdd = allSongsGenres.Except(usersExistingGenres).ToArray();

            if (genreIdsToAdd.Any())
            {
                List<GenreAnalytics> newAnalyticsList = new List<GenreAnalytics>();
                var genres = await genreRepository.GetGenresByIdsAsync(genreIdsToAdd);

                foreach (var genre in genres)
                {
                    var newAnalyticsObject = new GenreAnalytics() { AppUser = appUser, SongsOfThisGenreCount = 0, Genre = genre };
                    newAnalyticsList.Add(newAnalyticsObject);
                }

                await analyticsRepository.CreateMultipleAnalyticsForUserAsync(newAnalyticsList);
            }

        }
    }
}
