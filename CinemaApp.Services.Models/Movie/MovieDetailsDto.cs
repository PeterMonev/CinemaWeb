using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Services.Models.Movie
{
    public class MovieDetailsDto : MovieAllDto
    {
         public string Description { get; set; } = null!;
        public int Duration { get; set; }
    }
}
