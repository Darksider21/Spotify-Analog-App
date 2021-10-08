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

namespace SpotifyAnalogApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyAnalogAppController : ControllerBase
    {
        
        private readonly SpotifyAnalogAppContext _dbContext;
        public SpotifyAnalogAppController( SpotifyAnalogAppContext dbContext  )
        {
            _dbContext = dbContext;
        }
        public static List<Author> Authors
        {
            get
            {
                List<Author> list = new List<Author> { new Author {Name="kekos", Songs = new List<Song> { new Song { Name = "LOLKEK"} } }};
                return list;
            }
        }

        [HttpPost]
        public IActionResult PostData()
        {
            _dbContext.Genres.Add(new Genre { GenreName = "Kek",
                Authors=Authors   });
            _dbContext.SaveChanges();
            return Ok();
        }

    }

    
}
