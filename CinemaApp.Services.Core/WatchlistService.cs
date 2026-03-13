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

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository watchlistRepository;
        private readonly IMapper mapper;

        public WatchlistService(IWatchlistRepository watchlistRepository, IMapper mapper)
        {
            this.watchlistRepository = watchlistRepository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userId)
        {
            IEnumerable<UserMovie> userWatchlistEntries = (await watchlistRepository
                .GetAllUserMoviesAsync())
                .Where(um => um.UserId.ToLower() == userId.ToLower())
                .ToArray();

            IEnumerable<WatchlistMovieDto> watchlistMoviesDto =
                mapper.Map<IEnumerable<WatchlistMovieDto>>(userWatchlistEntries);

            return watchlistMoviesDto;
        }
    }
}
