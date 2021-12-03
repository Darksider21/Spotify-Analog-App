using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.DTO.RequestDto;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private IAppUserService userService;
        private IPlaylistService playlistService;
        private IRatingService ratingService;

       public  AppUsersController(IAppUserService userService, IPlaylistService playlistService, IRatingService ratingService)
        {
            this.userService = userService;
            this.playlistService = playlistService;
            this.ratingService = ratingService;
        }



        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsersAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("getUsersById")]
        public async Task<IActionResult> GetUsersById(int userId)
        {
            var user = await userService.GetUserByIdAsync(userId);

            return Ok(user);
        }
        
        
        

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.CreateUserAsync(userModel.Name, userModel.Email);

                return new ObjectResult(user) { StatusCode = StatusCodes.Status201Created };
            }

            return BadRequest();
          
        }
        
        
        

        [HttpPut]
        [Route("changeUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] RequestUserModel model)
        {
            var user = await userService.UpdateUserInfoAsync(model);

            return Ok(user);


        }

        [HttpPost]
        [Route("AddSongsToUsersFavorites")]
        public async Task<IActionResult> AddSongsToUsersFavorites([FromBody] ChangeUsersRatedSongsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await ratingService.AddSongsToUsersFavoritesAsync(model.UserId, model.SongIds);
                return Ok(user);
            }

            return BadRequest();
            
        }


        [HttpDelete]
        [Route("RemoveSongsFromUsersFavorites")]
        public async Task<IActionResult> RemoveSongsFromUsersFavorites([FromBody] ChangeUsersRatedSongsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await ratingService.RemoveSongsFromUsersFavoritesAsync(model.UserId, model.SongIds);
                return Ok(user);
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("AddSongsToUsersDislikes")]
        public async Task<IActionResult> AddSongsToUsersDislikes([FromBody] ChangeUsersRatedSongsModel model)
        {
            var dislikedSongs = await ratingService.AddSongsToUsersDislikesAsync(model.UserId, model.SongIds);
            return Ok(dislikedSongs);
        }
        [HttpDelete]
        [Route("RemoveSongsFromUsersDislikes")]
        public async Task<IActionResult> RemoveSongsFromUsersDislikes([FromBody] ChangeUsersRatedSongsModel model)
        {
            var dislikedSongs = await ratingService.RemoveSongsFromUsersDislikesAsync(model.UserId, model.SongIds);
            if (dislikedSongs.Any())
            {
                return Ok(dislikedSongs);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await userService.DeleteUserAsync(userId);
            return NoContent();
        }

    }
}
