using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Controllers
{


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {

        private IAnalyticsService analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        [HttpGet]
        [Route("GetUserAnalytics")] 
        public async Task<IActionResult> GetUserAnalytics(int userId)
        {
            var analytics = await analyticsService.GetAnalyticsByUserIdAsync(userId);
            
            
            return Ok(analytics);
            
            
        }

        [HttpGet]
        [Route("GetMultipleUsersAnalytics")]
        public async Task<IActionResult> GetMultipleUsersAnalytics([FromQuery]int[] userIds)
        {
            var analytics = await analyticsService.GetAnalyticsByUserIdsAsync(userIds);
            if (analytics.Any())
            {
                return Ok(analytics);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
