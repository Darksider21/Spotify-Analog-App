﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetGenres(string genreName)
        {
            var genre = await genreService.GetGenreList(genreName);

            return Ok(genre);

        }
        
        
    }
}