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

        [Route("songbyauthorname")]
        [HttpGet]
        public async Task<IActionResult> GetAllSongsBuAuthorName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            var songs = await songService.GetSongsByAuthorName(name);

            return Ok(songs);

        }

        [Route("songsbygenrename")]
        [HttpGet]
        public async Task<IActionResult> GetAllSongsByGenreName(string name)
        {

            if (name == null)
            {
                return BadRequest();
            }
            var songs = await songService.GetSongsByGenreName(name);

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

        [Route("songsByMultipleAuthors")]
        [HttpGet]
        public async Task<IActionResult> GetAllSongsByMultipleAuthors([FromQuery] string[] authors)
        {
            if (authors == null)
            {
                return BadRequest();
            }

            var songs = await songService.GetSongsByMultipleAuthors(authors);

            return Ok(songs);

        }

        [Route("songsByMultipleGenres")]
        [HttpGet]
        public async Task<IActionResult> GetAllSongsByMultipleGenres([FromQuery]string[] genres)
        {
            if (genres == null)
            {
                return BadRequest();
            }
            var songs = await songService.GetSongsByMultipleGenres(genres);

            return Ok(songs);

        }

        [Route("getRandomSongs")]
        [HttpGet]
        public async Task<IActionResult> GetRandomSongs(int amountOfSongs)
        {

            if (String.IsNullOrEmpty(amountOfSongs.ToString()))
            {
                return BadRequest();
            }

            var songs = await songService.GetRandomSongsList(amountOfSongs);

            return Ok(songs);

        }

        [Route("getRandomSongsByGenre")]
        [HttpGet]
        public async Task<IActionResult> GetRandomSongsByGenre(int amountOfSongs, [FromQuery]string[] genres)
        {

            if (String.IsNullOrEmpty(amountOfSongs.ToString()) || genres == null)
            {
                return BadRequest();
            }

            var songs = await songService.GetRandomSongsListByGenres(amountOfSongs, genres);

            return Ok(songs);

        }

        [Route("getRandomSongsByAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetRandomSongsByAuthor(int amountOfSongs, [FromQuery]string[] authors)
        {

            if (String.IsNullOrEmpty(amountOfSongs.ToString()) || authors == null)
            {
                return BadRequest();
            }

            var songs = await songService.GetRandomSongsListByAuthors(amountOfSongs, authors);

            return Ok(songs);

        }

    }
}
