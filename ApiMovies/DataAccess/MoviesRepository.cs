using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMovies.DataAccess.Interfaces;
using ApiMovies.Models;


namespace ApiMovies.DataAccess
{
    public class MoviesRepository : IMovieRepository
    {
        private IMovieDbContext _movieDbContext;

        public MoviesRepository(IMovieDbContext dbContext)
        {
            _movieDbContext = dbContext;
        }

        public List<Movie> GetAll()
        {
            List<Movie> returnList = new List<Movie>();
            returnList = _movieDbContext.GetAll();

            return returnList;
        }

        public Movie Insert(Movie movieData)
        {
            Movie createdMovie = this._movieDbContext.Store(movieData);
            return createdMovie;
        }
    }
}
