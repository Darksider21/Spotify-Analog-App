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
        public int GenreId { get; protected set; }
        [Required]

        public string GenreName { get; set; }
        

        public ICollection<Author> Authors { get; set; }
       
    }
}
