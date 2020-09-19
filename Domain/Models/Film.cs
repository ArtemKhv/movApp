using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movApp.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string PathImg { get; set; }
        public User User { get; set; }

    }
}
