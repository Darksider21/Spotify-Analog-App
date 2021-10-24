using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAnalogApp.Data;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Data;
using SpotifyAnalogApp.Data.Models;
using System.Collections;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;

namespace SpotifyAnalogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyAnalogAppController : ControllerBase
    {
        private ISongService songService;
        
        public SpotifyAnalogAppController( ISongService songService  )
        {
            this.songService = songService;
        }
        
        [HttpGet]
        public IActionResult GetSongsTest()
        {
            var songs = songService.GetSongList().Result;

            return Ok(songs);
        }

        [HttpPost]
        public IActionResult PostData()
        {
            
            return Ok();
        }

    }

    
}
