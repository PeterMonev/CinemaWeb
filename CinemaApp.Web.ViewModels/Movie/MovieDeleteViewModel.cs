namespace CinemaApp.Web.ViewModels.Movie
{
    using Services.Mapping;
    using Services.Models.Movie;

    public class MovieDeleteViewModel : IMapFrom<MovieDetailsDto>
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}