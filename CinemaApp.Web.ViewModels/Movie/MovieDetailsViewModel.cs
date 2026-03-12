using CinemaApp.Services.Mapping;
using CinemaApp.Services.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class MovieDetailsViewModel : AllMoviesIndexViewModel, IMapFrom<MovieDetailsDto>
    {
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
    }
}
