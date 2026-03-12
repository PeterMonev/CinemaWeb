using AutoMapper;
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Services.Models.Movie;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using static CinemaApp.GCommon.ApplicationConstants;
using static CinemaApp.GCommon.Exceptions.EntityPersistFailureException;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
        private readonly IMapper mapper;
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            this.mapper = mapper;
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
                throw new EntityPersistFailureException();
            }
        }

        public async Task EditMovieAsync(Guid id, MovieFormModel model)
        {
          Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if(movieDb == null)
            {
                throw new EntityNotFoundException();
            }

            movieDb.Title = model.Title;
            movieDb.Genre = model.Genre;
            movieDb.ReleaseDate = model.ReleaseDate;
            movieDb.Description = model.Description;
            movieDb.Director = model.Director;
            movieDb.ImageUrl = model.ImageUrl;
            movieDb.Duration = model.Duration;

            bool editSuccess = await movieRepository.EditMovieAsync(movieDb);

            if (!editSuccess)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
          return await movieRepository.ExistsByAsync(id);
        }

        public async Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync()
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
            IEnumerable<MovieAllDto> allMoviesViewModel = mapper.Map<IEnumerable<MovieAllDto>>(allMoviesDb)
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

        public async Task<MovieFormModel?> GetMovieFormModelByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return new MovieFormModel()
            {
                Title = movieDb.Title,
                Genre = movieDb.Genre,
                ReleaseDate = movieDb.ReleaseDate,
                Description = movieDb.Description,
                Director = movieDb.Director,
                Duration = movieDb.Duration,
                ImageUrl = movieDb.ImageUrl
            };
        }

        public async Task HardDeleteMovieAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                throw new EntityNotFoundException();
            }

            bool deleteSucuccess = await movieRepository.HardDeleteMovieAsync(movieDb);
            if (!deleteSucuccess)
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task SoftDeleteMovieAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if(movieDb == null)
            {
                throw new EntityNotFoundException();
            }

            bool deleteSucuccess = await movieRepository.SoftDeleteMovieAsync(movieDb);
            if (!deleteSucuccess)
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
