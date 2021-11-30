using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private ISuggestionsService suggestionsService;

        public SuggestionController(ISuggestionsService suggestionsService)
        {
            this.suggestionsService = suggestionsService;
        }
        
       [HttpGet]
       [Route("GetSuggestionSongsForUser")]
       public async Task<IActionResult> GetSuggestionSongsForUser(int userId,int amountOfSongs)
        {
            var songs = await suggestionsService.GetSuggestionSongsForUser(userId, amountOfSongs);
            if (songs.Any())
            {
                return Ok(songs);
            }
            return BadRequest();
        }

    }
}
