namespace CinemaApp.Services.Models.Watchlist
{
    using Data.Models;
    using Mapping;

    public class WatchlistMovieDto : IMapFrom<Movie>
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        public string? ImageUrl { get; set; }
    }
}