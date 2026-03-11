using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using static CinemaApp.GCommon.ApplicationConstants;
using static CinemaApp.GCommon.Exceptions.DatabaseEntityCreatePersistFailureException;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task CreateMovieAsync(MovieFormModel model)
        {
            Movie movie = new Movie()
            {
                Title = model.Title,
                Genre = model.Genre,
                ReleaseDate = model.ReleaseDate,
                Description = model.Description,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Duration = model.Duration,
            };

            bool successAdd = await movieRepository.AddMovieAsync(movie);

            if (!successAdd)
            {
                throw new DatabaseEntityCreatePersistFailureException();
            }
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync()
        {
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = await movieRepository
                .GetAllMoviesNoTracking()
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
