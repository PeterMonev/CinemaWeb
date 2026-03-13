using AutoMapper;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Services.Models.Watchlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CinemaApp.GCommon.Exceptions;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;

        public WatchlistService(IWatchlistRepository watchlistRepository, IMapper mapper,IMovieRepository movieRepository)
        {
            this.watchlistRepository = watchlistRepository;
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userId)
        {
            IEnumerable<UserMovie> userWatchlistEntries = (await watchlistRepository
                .GetAllUserMoviesAsync())
                .Where(um => um.UserId.ToLower() == userId.ToLower())
                .ToArray();

            IEnumerable<WatchlistMovieDto> watchlistMoviesDto =
             mapper.Map<IEnumerable<WatchlistMovieDto>>(userWatchlistEntries.Select(um => um.Movie));

            return watchlistMoviesDto;
        }

        public async Task<bool> MovieIsUserWatchlistAsync(string userId, Guid movieID)
        {
            try
            {
                bool userWatchlistEntryExists = await watchlistRepository.ExistsAsync(userId, movieID);

                return userWatchlistEntryExists;

            } catch (NullReferenceException ex)
            {
                throw new EntryPointNotFoundException(ex.Message);
            }
        }

        public async Task AddMovieToUserWatchlistAsync(string userId, Guid movieId)
        {
            bool userWatchlistEntryExists = await watchlistRepository.ExistsAsync(userId, movieId);

            if(userWatchlistEntryExists)
            {
                throw new EntityAlreadyExistsException();
            }

            bool movieExists = await movieRepository.ExistsByAsync(movieId);

            if (!movieExists)
            {
                throw new EntityNotFoundException();
            }

            UserMovie newUserMoive = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId
            };

            bool successAdd = await watchlistRepository.AddUserMovieAsync(newUserMoive);

            if (!successAdd)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
