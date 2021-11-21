using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        

        

        [Route("songbyid")]
        [HttpGet]
        public async Task<IActionResult> GetSongById(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                return BadRequest();
            }
            var songs = await songService.GetSongById(id);

            return Ok(songs);

        }
        [HttpPost]
        [Route("songsByAuthorsAndGenres")]
        public async Task<IActionResult> GetSongsByAuthorsAndGenres([FromBody]AuthorGenreDTO authorGenreDTO)
        {
            if (authorGenreDTO.Authors == null && authorGenreDTO.Genres== null)
            {
                return StatusCode(422);
            }
            var songs = await songService.GetSongsByAuthorsAndGenres(authorGenreDTO);
            return Ok(songs);
        }

        [HttpPost]
        [Route("randomSongsByAuthorsAndGenres")]
        public async Task<IActionResult> GetRandomSongsByAuthorsAndGenres(int amountOfSongs, [FromBody] AuthorGenreDTO authorGenreDTO)
        {
            var randomSongs = await songService.GetRandomSongsByAuthorsAndGenres(amountOfSongs, authorGenreDTO);
            return Ok(randomSongs);
        }

    }
}
