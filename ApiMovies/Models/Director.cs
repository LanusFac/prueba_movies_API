using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMovies.Models;

namespace ApiMovies.Models
{
    public class Director
    {
        public long DirectorId { get; set; }
        public string Firstname {get; set; }
        public string Lastname { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
