using CinemaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Func<Movie, Movie>? projectFunc = null);

        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        Task<bool> AddMovieAsync(Movie movie);

        Task<Movie?> GetMovieByIdAsync(Guid id);

        Task<bool> ExistsByAsync(Guid id);

        Task<bool> EditMovieAsync(Movie movie);

        Task<bool> SoftDeleteMovieAsync(Movie movie);

        Task<bool> HardDeleteMovieAsync(Movie movie);

    }
}
