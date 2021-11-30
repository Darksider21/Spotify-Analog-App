using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var authors = await authorService.GetAuthorListAsync(name,genre);

            return Ok(authors);

        }

        //Here lies randomness test for Guid.NewGuid()
        // It Passed!
        
        //[HttpGet]
        //[Route("GuidRandomnessTest")]
        //public async Task<IActionResult> StartGuidRandomnessTest()
        //{
            
        //    int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11,12,13,14,15,16 };
        //    List<int> numbersList = new List<int>();
        //    for (int i = 0; i < 10000000; i++)
        //    {
                
        //        var number = numbers.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
        //        numbersList.Add(number);
        //    }
        //    var Results = numbersList.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() }).OrderBy(x => x.Count);
            
        //    return Ok(Results);
        //}

        
    }
}
