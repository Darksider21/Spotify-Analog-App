using Microsoft.AspNetCore.Authorization;
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
    public class AuthorsController : ControllerBase
    {
        private IAuthorService authorService;


        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;

        }

        [HttpGet]

        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await authorService.GetAuthorList();

            return Ok(authors);

        }

        [Route("getbyname")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var authors = await authorService.GetAuthorByNameList(name);

            return Ok(authors);

        }
        //зліпити разом
        [Route("getbygenre")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorByGenre(string genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            var authors = await authorService.GetAuthorByGenreList(genre);

            return Ok(authors);

        }
    }
}
