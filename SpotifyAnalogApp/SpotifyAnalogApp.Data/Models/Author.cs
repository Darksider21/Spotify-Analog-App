using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class Author
    {
        public int AuthorId { get; protected set; }
        [Required]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }
    }
}
