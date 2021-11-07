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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

       public  UsersController(IUserService userService)
        {
            this.userService = userService;
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
        [HttpGet]
        [Route("getPlaylist")]
        public async Task<IActionResult> GetPlaylists(int userId = 0 )
        {
            return Ok();
        }
        [HttpGet]
        [Route("getPlaylistByid")]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser(string name , string email)
        {
           var user =  await userService.CreateUser(name, email);

            return new ObjectResult(user) { StatusCode=StatusCodes.Status201Created};
        }
        [HttpPost]
        [Route("createPlaylistForUser")]
        public async Task<IActionResult> CreatePlaylist( int userId , int[] songIds)
        {
            return Ok();
        }
        
        [HttpPatch]
        [Route("modifyPlaylist")]
        public async Task<IActionResult> ModifyPlaylist(string action, int playlistId, int[] songIds)
        {
            return Ok();
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
        public async Task<IActionResult> ModifyFavorites(string action, int userId , int[] songIds)
        {
            return Ok();
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
