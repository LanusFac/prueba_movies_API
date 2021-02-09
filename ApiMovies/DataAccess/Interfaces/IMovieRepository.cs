using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMovies.Models;

namespace ApiMovies.DataAccess.Interfaces
{
    public interface IMovieRepository
    {
        public List<Movie> GetAll();
        public Movie Insert(Movie obj);
    }
}
