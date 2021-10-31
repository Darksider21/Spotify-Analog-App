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
    public class GenreController : ControllerBase
    {
        private IGenreService genreService;
        public GenreController(IGenreService service)
        {
            this.genreService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var genre = await genreService.GetGenreList();

            return Ok(genre);

        }
        
        [HttpGet]
        [Route("byname")]
        public async Task<IActionResult> GetAllSongsByName(string name)
        {
            var genre = await genreService.GetGenreByNameList(name);

            return Ok(genre);

        }
    }
}
