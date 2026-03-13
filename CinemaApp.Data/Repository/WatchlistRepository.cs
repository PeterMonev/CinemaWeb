using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class WatchlistRepository : BaseRepository, IWatchlistRepository
    {

        public WatchlistRepository(CinemaAppDbContext dbContext)
            :base(dbContext) 
        {
            
        }

        public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync()
        {
            IEnumerable<UserMovie> userMovies = await this.DbContext.UsersMovies.AsNoTracking().Include(um => um.Movie).ToArrayAsync();

            return userMovies;
        }
    }
}
