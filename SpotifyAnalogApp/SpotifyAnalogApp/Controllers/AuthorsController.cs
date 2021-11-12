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

        public async Task<IActionResult> GetAllAuthors(string name , string genre)
        {
            var authors = await authorService.GetAuthorList(name,genre);

            return Ok(authors);

        }

        
    }
}
