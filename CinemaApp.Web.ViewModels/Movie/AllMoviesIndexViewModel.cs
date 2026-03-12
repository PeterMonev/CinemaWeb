namespace CinemaApp.Web.ViewModels.Movie
{
    using System.Globalization;

    using Services.Mapping;
    using Services.Models.Movie;
    using static GCommon.ApplicationConstants;

    using AutoMapper;

    public class AllMoviesIndexViewModel : IMapFrom<MovieAllDto>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsInUserWatchlist { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MovieAllDto, AllMoviesIndexViewModel>()
                .ForMember(d => d.ReleaseDate,
                    y => y.MapFrom(s => s.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture)))
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.ImageUrl ?? DefaultImageUrl));
        }
    }
}