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

       public  AppUsersController(IAppUserService userService, IPlaylistService playlistService)
        {
            this.userService = userService;
            this.playlistService = playlistService;
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
        [Route("addsongstousersfavorites")]
        public async Task<IActionResult> AddSongsToUsersFavorites([FromBody] ChangeUsersFavoriteSongsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.AddSongsToUsersFavoritesAsync(model.UserId, model.SongIds);
                return Ok(user);
            }

            return BadRequest();
            
        }
        [HttpDelete]
        [Route("RemoveSongsFromUsersFavorites")]
        public async Task<IActionResult> RemoveSongsFromUsersFavorites([FromBody] ChangeUsersFavoriteSongsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.AddSongsToUsersFavoritesAsync(model.UserId, model.SongIds);
                return Ok(user);
            }

            return BadRequest();
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
