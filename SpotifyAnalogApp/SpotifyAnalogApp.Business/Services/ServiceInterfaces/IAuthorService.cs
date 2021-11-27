﻿using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
   public interface IAuthorService
    {
        public Task<IEnumerable<AuthorModel>> GetAuthorListAsync(string name , string genre);

       


    }
}
