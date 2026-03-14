using CinemaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IWatchlistRepository
    {
        Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync();

        Task<UserMovie> GetUserMovieAsync(string userId, Guid movieId);

        Task<bool> ExistsAsync(string userId, Guid movieID);

        Task<bool> AddUserMovieAsync(UserMovie userMovie);

        Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie);

        Task<bool> UpdateUserMovieAsync(UserMovie userMovie);

        Task<UserMovie?> GetUserMovieIncludeDeleteAsync(string userId, Guid movieId);
    }
}
