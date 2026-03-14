using CinemaApp.Services.Models.Watchlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userdId);

        Task<bool> MovieIsUserWatchlistAsync(string userId, Guid movieID);

        Task AddMovieToUserWatchlistAsync(string userId, Guid movieId);

        Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId);
    }
}
