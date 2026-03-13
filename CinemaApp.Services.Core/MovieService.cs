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
        private readonly IWatchlistRepository watchlistRepository;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IWatchlistRepository watchlistRepository)
        {
            this.mapper = mapper;
            this.movieRepository = movieRepository;
            this.watchlistRepository = watchlistRepository;
        }

        public async Task CreateMovieAsync(MovieDetailsDto model)
        {
            Movie movie = mapper.Map<Movie>(model);

            bool successAdd = await movieRepository.AddMovieAsync(movie);

            if (!successAdd)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task EditMovieAsync(Guid id, MovieDetailsDto movieDetailsDto)
        {
          Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if(movieDb == null)
            {
                throw new EntityNotFoundException();
            }

            movieDb.Title = movieDetailsDto.Title;
            movieDb.Genre = movieDetailsDto.Genre;
            movieDb.ReleaseDate = movieDetailsDto.ReleaseDate;
            movieDb.Description = movieDetailsDto.Description;
            movieDb.Director = movieDetailsDto.Director;
            movieDb.ImageUrl = movieDetailsDto.ImageUrl;
            movieDb.Duration = movieDetailsDto.Duration;

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

        public async Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync(string? userId = null)
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
            IEnumerable<MovieAllDto> AllMoviesDtos = mapper.Map<IEnumerable<MovieAllDto>>(allMoviesDb)
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Genre)
                .ThenBy(m => m.Director)
                .ToList();

            if (!string.IsNullOrEmpty(userId))
            {
                foreach(MovieAllDto movieDto in AllMoviesDtos)
                {
                     movieDto.IsInUserWatchlist = await watchlistRepository
                        .ExistsAsync(userId, movieDto.Id);
                }
            }

            return AllMoviesDtos;
        }

        public async Task<MovieDetailsDto?> GetDetailsByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if(movieDb == null)
            {
                return null;
            }

            return mapper.Map<MovieDetailsDto>(movieDb);

        }

        public async Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid id)
        {
            Movie? movieDb = await movieRepository.GetMovieByIdAsync(id);

            if (movieDb == null)
            {
                return null;
            }

            return mapper?.Map<MovieDetailsDto>(movieDb);
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
