using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movApp.Models
{
    public class User : IdentityUser
    {

        //public int Id { get; set; }
       // public string Name { get; set; }
        //public string Password { get; set; }
        public List<Film> Films { get; set; }

    }
}
