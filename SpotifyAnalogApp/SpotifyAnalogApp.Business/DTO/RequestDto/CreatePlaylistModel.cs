using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO.RequestDto
{
    public class CreatePlaylistModel
    {
        [Required]
        [RegularExpression("a-zA-Z")]
        public string PlaylistName { get; set; }
        [Required]
        public int[] SongsIds { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int UserId { get; set; }
    }
}
