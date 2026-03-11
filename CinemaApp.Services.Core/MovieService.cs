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
            //Fetch Data
            IEnumerable<Movie> allMoviesDb = await movieRepository.GetAllMoviesNoTrackingAsync(movie =>
            {
                return new Movie()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate,
                    Description = movie.Description,
                    ImageUrl = movie.ImageUrl,
                };
            });

            //Process data
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = allMoviesDb
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
                .ToList();

            return allMoviesViewModel;
        }

        public async Task<MovieDetailsViewModel> GetDetailsByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if(movieDb == null)
            {
                return null;
            }

            return new MovieDetailsViewModel()
            {
                Id = movieDb.Id,
                Title = movieDb.Title,
                Genre = movieDb.Genre,
                ReleaseDate = movieDb.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                Description = movieDb.Description,
                Director = movieDb.Director,
                ImageUrl = movieDb.ImageUrl ?? DefaultImageUrl
            };

        }
    }
}
