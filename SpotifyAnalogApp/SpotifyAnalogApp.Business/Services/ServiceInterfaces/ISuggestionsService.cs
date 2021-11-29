using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface ISuggestionsService
    {
        public Task<IEnumerable<SongModel>> GetSuggestionSongsForUser(int userId, int amountOfsongs);

    }
}
