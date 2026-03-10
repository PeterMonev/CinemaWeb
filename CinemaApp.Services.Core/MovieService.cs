using CinemaApp.Data;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = await movieRepository
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                    Director = m.Director,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Genre)
                .ThenBy(m => m.Director)
                .ToArrayAsync();

            return allMoviesViewModel;
        } 
    }
}
