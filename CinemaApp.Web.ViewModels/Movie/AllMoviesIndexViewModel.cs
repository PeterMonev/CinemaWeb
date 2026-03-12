using CinemaApp.Services.Mapping;
using CinemaApp.Services.Models.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class AllMoviesIndexViewModel : IMapFrom<MovieAllDto>
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}