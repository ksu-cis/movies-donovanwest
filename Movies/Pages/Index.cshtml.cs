using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        //private MovieDatabase movieDatabase = new MovieDatabase();
        public List<Movie> Movies { get; set; } = MovieDatabase.All;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> rating { get; set; } = new List<string>();

        [BindProperty]
        public float? minIMDB { get; set; }

        [BindProperty]
        public float? maxIMDB { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All;
        }

        public void OnPost()
        {
            Movies = MovieDatabase.All;

            if(search != null)            
                Movies = MovieDatabase.Search(search, Movies);
           
            if(rating.Count > 0)           
                Movies = MovieDatabase.FilterByMPAA(rating, Movies);
                       
            if(minIMDB != null)
                Movies = MovieDatabase.FilterByMinIMDB(minIMDB, Movies);

            if(maxIMDB != null)
                Movies = MovieDatabase.FilterByMaxIMDB(maxIMDB, Movies);

        }
    }
}
