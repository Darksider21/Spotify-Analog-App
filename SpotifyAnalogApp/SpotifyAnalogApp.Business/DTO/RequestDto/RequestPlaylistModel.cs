using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO.RequestDto
{
    public class RequestPlaylistModel
    {
        [Required]
        public int PlaylistId { get; set; }
        
        [Required]
        public int[] SongsIds { get; set; }
    }
}
