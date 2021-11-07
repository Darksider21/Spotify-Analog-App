using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class PlaylistService : IPlaylistService
    {
        private IPlaylistRepository playlistRepository;

        public PlaylistService(IPlaylistRepository repository)
        {
            playlistRepository = repository;
        }

        public Task<ICollection<PlaylistModel>> GetAllPlaylists()
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistModel> GetPlaylistById(int playlistId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PlaylistModel>> GetPlaylistsByUserId(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
