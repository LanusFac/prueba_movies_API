using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMovies.DataAccess.Interfaces;
using ApiMovies.Models;
using MySql.Data.MySqlClient;

namespace ApiMovies.DataAccess
{
    public class MovieDbContext : IMovieDbContext
    {
        public List<Movie> GetAll()
        {
            List<Movie> returnList = new List<Movie>();
            MySqlConnection connection = SqlDataConnectionManager.GetCloudSQLConnection();

            try
            {
                using(connection)
                {
                    connection.Open();
                    using(MySqlCommand cmd = new MySqlCommand("SELECT * FROM movies", connection))
                    {
                        using(MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Movie movie = new Movie();
                                movie.MovieId = Convert.ToInt32(reader["movieId"]);
                                movie.Title = Convert.ToString(reader["movieTitle"]);

                                returnList.Add(movie);
                            }
                        }
                    }
                connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done.");
            }
            return returnList;
        }

        public Movie Store(Movie movieData)
        {
            Movie createdMovie = null;
            MySqlConnection connection = SqlDataConnectionManager.GetCloudSQLConnection();

            try
            {
                using (connection)
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Movie (MovieId, Title) VALUES (1, 'la peli de facu')", connection))
                    {
                        
                        cmd.ExecuteNonQuery();

                        createdMovie = movieData;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done.");
            }

            return createdMovie;
        }
    }
}
