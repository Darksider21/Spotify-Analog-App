using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpotifyAnalogApp.Data.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]

        public string GenreName { get; set; }

        
        

        
       
    }
}
