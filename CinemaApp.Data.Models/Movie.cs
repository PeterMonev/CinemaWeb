using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CinemaApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static GCommon.EntityValidation.Movie;

    public class Movie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        [Required]
        [MaxLength(DirectorMaxLength)]
        public string Director { get; set; } = null!;

        public int Duration { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}