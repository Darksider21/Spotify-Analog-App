using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IUserService userService;
        private IPlaylistService playlistService;

       public  AppUsersController(IUserService userService, IPlaylistService playlistService)
        {
            this.userService = userService;
            this.playlistService = playlistService;
        }



        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }
        [HttpGet]
        [Route("getUsersById")]
        public async Task<IActionResult> GetUsersById(int userId)
        {
            var user = await userService.GetUserById(userId);

            return Ok(user);
        }
        
        
        

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser(string name , string email)
        {
           var user =  await userService.CreateUser(name, email);

            return new ObjectResult(user) { StatusCode=StatusCodes.Status201Created};
        }
        
        
        

        [HttpPatch]
        [Route("changeUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(string name , string email, int userId)
        {
            var user = await userService.UpdateUserInfo(name, email, userId);

            return Ok(user);


        }

        [HttpPatch]
        [Route("modifyUsersFavorites")]
        public async Task<IActionResult> ModifyFavorites(string action, int userId , [FromQuery]int[] songIds)
        {
            var user = await userService.ModifyFavorites(action, userId, songIds);
            return Ok(user);
        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await userService.DeleteUser(userId);
            return NoContent();
        }

    }
}
