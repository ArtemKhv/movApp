using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movApp.Models
{
    public class User : IdentityUser
    {
        public List<Film> Films { get; set; }

    }
}
