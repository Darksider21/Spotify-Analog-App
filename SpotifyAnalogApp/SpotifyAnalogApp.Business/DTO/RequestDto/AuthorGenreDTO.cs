using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class AuthorGenreDTO
    {
        [JsonProperty("Authors")]
        public string[] Authors { get; set; }

        [JsonProperty("Genres")]
        public string[] Genres { get; set; }
    }
}
