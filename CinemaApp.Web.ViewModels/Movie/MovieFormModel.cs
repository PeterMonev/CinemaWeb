using System.ComponentModel.DataAnnotations;

using static CinemaApp.GCommon.ViewModelValidation.MovieViewModels;
using static CinemaApp.GCommon.OutputMessages.Movie;

namespace CinemaApp.Web.ViewModels.Movie
{
    public class MovieFormModel
    {
        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength, ErrorMessage = GenreMinLengthMessage)]
        [MaxLength(GenreMaxLength, ErrorMessage = GenreMaxLengthMessage)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = DirectorRequiredMessage)]
        [MinLength(DirectorNameMinLength, ErrorMessage = DirectorNameMinLengthMessage)]
        [MaxLength(DirectorNameMaxLength, ErrorMessage = DirectorNameMaxLengthMessage)]
        public string Director { get; set; } = null!;

        [Required(ErrorMessage = DurationRequiredMessage)]
        [Range(DurationMin, DurationMax, ErrorMessage = DurationRangeMessage)]
        public int Duration { get; set; }

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;

        [Url]
        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
        public string? ImageUrl { get; set; }
    }
}
