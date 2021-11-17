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
    public class PlaylistController : ControllerBase
    {
        private IUserService userService;
        private IPlaylistService playlistService;

        public PlaylistController(IUserService userService, IPlaylistService playlistService)
        {
            this.userService = userService;
            this.playlistService = playlistService;
        }
        [HttpGet]
        [Route("getPlaylist")]
        public async Task<IActionResult> GetPlaylists([FromQuery] int[] userIds)
        {
            if (userIds.Any())
            {
                var playlistsByUsers = await playlistService.GetPlaylistsByUserId(userIds);
                return Ok(playlistsByUsers);

            }
            var playlists = await playlistService.GetAllPlaylists();
            return Ok(playlists);
        }
        [HttpGet]
        [Route("getPlaylistByid")]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            var playlist = await playlistService.GetPlaylistById(playlistId);
            return Ok(playlist);
        }
        [HttpPost]
        [Route("createPlaylistForUser")]
        public async Task<IActionResult> CreatePlaylist(int userId, string playlistName, [FromQuery] int[] songIds)
        {
            var playlist = await playlistService.CreatePlaylist(userId, songIds, playlistName);

            return Ok(playlist);
        }
        [HttpPatch]
        [Route("modifyPlaylist")]
        public async Task<IActionResult> ModifyPlaylist(string action, int playlistId, string playlistName, [FromQuery] int[] songIds)
        {
            var playlist = await playlistService.ModifyPlaylist(action, playlistId, songIds, playlistName);
            return Ok(playlist);
        }

        [HttpDelete]
        [Route("deletePlaylist")]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            if (string.IsNullOrEmpty(playlistId.ToString()))
            {
                return StatusCode(422);
            }

            await playlistService.DeletePlaylist(playlistId);
            return NoContent();
        }

    }
}
