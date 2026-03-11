using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class MovieDetailsViewModel : AllMoviesIndexViewModel
    {
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
    }
}
