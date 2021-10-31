using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {

        private ISongService songService;
       

        public SongController (ISongService songService)
        {
            this.songService = songService;
            
        }
        [Route("songs")]
        [HttpGet]
        
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await songService.GetSongList();

            return Ok(songs);

        }

        [Route("songswithauthors")]
        [HttpGet]
        public async Task<IActionResult> GetAllSongsWithAuthors()
        {
            var songs = await songService.GetSongsWithAuthorsList();

            return Ok(songs);

        }
    }
}
