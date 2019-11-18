﻿using System;
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
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> SearchAndFilter(string search, List<string> rating)
        {
            if(search == null && rating.Count == 0)
            {
                return All;
            }
            List<Movie> result = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if (search != null && rating.Count > 0)
                {
                    if (movie.Title != null && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) && rating.Contains(movie.MPAA_Rating))
                    {
                        result.Add(movie);                       
                    }
                }
                else if (search != null)
                {
                    if (movie.Title != null && movie.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result.Add(movie);
                    }
                }
                else if(rating.Count > 0){
                    if (rating.Contains(movie.MPAA_Rating))
                    {
                        result.Add(movie);
                    }
                }
            }
            return result;
        }
    }
}
