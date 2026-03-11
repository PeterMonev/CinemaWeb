namespace CinemaApp.GCommon
{
    public static class OutputMessages
    {
        public static class Movie
        {
            // Error messages
            public const string TitleRequiredMessage = "Title is required.";
            public const string TitleMinLengthMessage = "Title must be at least {1} characters.";
            public const string TitleMaxLengthMessage = "Title cannot exceed {1} characters.";

            public const string GenreRequiredMessage = "Genre is required.";
            public const string GenreMinLengthMessage = "Genre must be at least {1} characters.";
            public const string GenreMaxLengthMessage = "Genre cannot exceed {1} characters.";

            public const string DirectorRequiredMessage = "Director is required.";
            public const string DirectorNameMinLengthMessage = "Director name must be at least {1} characters.";
            public const string DirectorNameMaxLengthMessage = "Director name cannot exceed {1} characters.";

            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionMinLengthMessage = "Description must be at least {1} characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed {1} characters.";

            public const string DurationRequiredMessage = "Duration is required.";
            public const string DurationRangeMessage = "Duration must be between {1} and {2} minutes.";

            public const string ReleaseDateRequiredMessage = "Release date is required.";

            public const string ImageUrlMaxLengthMessage = "Image URL cannot exceed {1} characters.";

            public const string CrudMovieFailureMessage = "An error occurred while {0} the movie. Please try again in a few minutes.";
        }

        public static class Watchlist
        {
            public const string MovieAlreadyInWatchlistMessage = "The movie {0} is already in user {1} watchlist.";
            public const string AddToWatchlistFailureMessage = "An error occurred while adding the movie to your watchlist. Please try again in a few minutes.";
            public const string RemoveFromWatchlistFailureMessage = "An error occurred while removing the movie from your watchlist. Please try again in a few minutes.";
        }
    }
}