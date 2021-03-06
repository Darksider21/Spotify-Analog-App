using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO.RequestDto
{
    public class ChangeUsersRatedSongsModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int[] SongIds { get; set; }
    }
}
