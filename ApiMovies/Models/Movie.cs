using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMovies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMovies.Models
{
    public class Movie
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
    }
}
