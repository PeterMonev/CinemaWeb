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
        IQueryable<Movie> GetAllMoviesNoTracking();

        Task<IEnumerable<Movie>> GetAllMovies();
    }
}
