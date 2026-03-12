namespace CinemaApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;

    using Services.Mapping;
    using Services.Models.Movie;
    using static GCommon.ViewModelValidation.MovieViewModels;
    using static GCommon.OutputMessages.Movie;

    using AutoMapper;

    public class MovieFormModel : IMapFrom<MovieDetailsDto>, IMapTo<MovieDetailsDto>, IHaveCustomMappings
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


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MovieFormModel, MovieDetailsDto>()
                .ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}