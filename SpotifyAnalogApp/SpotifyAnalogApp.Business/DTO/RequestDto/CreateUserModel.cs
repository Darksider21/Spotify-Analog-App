using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO.RequestDto
{
    public class CreateUserModel
    {
        [Required]
        

        public string Name { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }
    }
}
