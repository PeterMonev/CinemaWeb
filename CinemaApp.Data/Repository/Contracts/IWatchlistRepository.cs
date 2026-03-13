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

        Task<bool> ExistsAsync(string userId, Guid movieID);

        Task<bool> AddUserMovieAsync(UserMovie userMovie);
    }
}
