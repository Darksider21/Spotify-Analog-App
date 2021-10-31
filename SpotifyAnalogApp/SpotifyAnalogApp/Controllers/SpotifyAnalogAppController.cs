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
using SpotifyAnalogApp.Data.Repositiry.Base;

namespace SpotifyAnalogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class SpotifyAnalogAppController : ControllerBase
    {
        private ISongService songService;
        private IGenreService genreService;
        private IAuthorService authorService;
        private IAuthorRepository AuthorRepoTest;

        public SpotifyAnalogAppController(ISongService songService, IAuthorService authorService, IGenreService genreService, IAuthorRepository authorRepository)
        {
            this.songService = songService;
            this.authorService = authorService;
            this.genreService = genreService;
            this.AuthorRepoTest = authorRepository;
        }

        [HttpGet]
        public IActionResult GetSongsTest()
        {
            var test = AuthorRepoTest.GetAllAuthorsAsync().Result;

            var genres = genreService.GetGenreList().Result;
            var songs = songService.GetSongList().Result;
            var songsWithAuthors = songService.GetSongsWithAuthorsList().Result;
            var authors = authorService.GetAuthorList().Result;


            return Ok(songs);
        }

        //[HttpGet("{name}")]
        //public IActionResult GetByName(string name)
        //{
        //    if (name == null)
        //    {
        //        return BadRequest();
        //    }
        //    var authorsByName = authorService.GetAuthorByNameList(name).Result;
        //    // var authorsByName = AuthorRepoTest.GetByNameAsync(name).Result;
        //    return Ok(authorsByName);
        //}

        [Route("authors")]
        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var authorsByName = await authorService.GetAuthorByNameList(name);
            // var authorsByName = AuthorRepoTest.GetByNameAsync(name).Result;
            return Ok(authorsByName);
        }
        [HttpPost]
        public IActionResult PostData()
        {
            
            return Ok();
        }

    }

    
}
