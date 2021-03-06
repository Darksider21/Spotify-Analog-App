using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyAnalogApp.Business.DTO.RequestDto;
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
        private IAppUserService userService;
        private IPlaylistService playlistService;

        public PlaylistController(IAppUserService userService, IPlaylistService playlistService)
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
                var playlistsByUsers = await playlistService.GetPlaylistsByUserIdAsync(userIds);
                return Ok(playlistsByUsers);

            }
            var playlists = await playlistService.GetAllPlaylistsAsync();
            return Ok(playlists);
        }
        [HttpGet]
        [Route("getPlaylistByid")]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            var playlist = await playlistService.GetPlaylistByIdAsync(playlistId);
            return Ok(playlist);
        }
        [HttpPost]
        [Route("createPlaylistForUser")]
        public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylistModel playlistModel)
        {
            var playlist = await playlistService.CreatePlaylistAsync(playlistModel);

            return Ok(playlist);
        }
        [HttpPost]
        [Route("AddSongsToPlaylist")]
        public async Task<IActionResult> AddSongsToPlaylist([FromBody]RequestPlaylistModel playlistModel)
        {
            var playlist = await playlistService.AddSongsToPlaylistAsync(playlistModel);
            return Ok(playlist);
        }
        [HttpPost]
        [Route("RemoveSongsFromPlaylist")]
        public async Task<IActionResult> RemoveSongsFromPlaylist([FromBody] RequestPlaylistModel playlistModel)
        {
            var playlist = await playlistService.RemoveSongsFromPlaylistAsync(playlistModel);
            return Ok(playlist);
        }

        [HttpDelete]
        [Route("deletePlaylist")]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            if (string.IsNullOrEmpty(playlistId.ToString()))
            {
                return BadRequest("Invalid id");
            }

            await playlistService.DeletePlaylistAsync(playlistId);
            return NoContent();
        }
        

    }
}
