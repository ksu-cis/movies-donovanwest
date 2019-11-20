using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies;

        public static List<Movie> All {
            get {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }
        public static List<Movie> Search(string search, List<Movie> movies)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (search != null)
                {
                    if (movie.Title != null && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result.Add(movie);
                    }
                }
            }
            return result;
        }

        public static List<Movie> FilterByMPAA(List<string> ratings, List<Movie> movies)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (ratings.Contains(movie.MPAA_Rating))
                {
                    result.Add(movie);
                }
            }
            return result;
        }
        public static List<Movie> FilterByMinIMDB(float? minIMDB, List<Movie> movies)
        {
            if (minIMDB == null)
                return movies;
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating >= minIMDB)
                {
                    result.Add(movie);
                }
            }
            return result;
        }

        public static List<Movie> FilterByMaxIMDB(float? maxIMDB, List<Movie> movies)
        {
            List<Movie> result = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.IMDB_Rating <= maxIMDB)
                {
                    result.Add(movie);
                }
            }
            return result;
        }
    }
}
