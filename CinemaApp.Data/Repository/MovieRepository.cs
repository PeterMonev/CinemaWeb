using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Func<Movie, Movie>? projectFunc = null)
        {
            IQueryable<Movie> movieFetchQuery = this.DbContext.Movies
          .AsNoTracking()
          .OrderBy(m => m.Title);

            if (projectFunc != null)
            {
                movieFetchQuery = movieFetchQuery.Select(m => projectFunc(m)).AsQueryable();
            }

            return await movieFetchQuery.ToArrayAsync();
        }


        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await this.DbContext.Movies
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(Movie movie)
        {
           await this.DbContext.Movies.AddAsync(movie);
           int resultCount = await SaveChangesAsync();

           return resultCount == 1;
        }

        private async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await DbContext.Movies
                 .FindAsync(id);
        }

        public async Task<bool> ExistsByAsync(Guid id)
        {
            return await DbContext.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> EditMovieAsync(Movie movie)
        {
            DbContext.Movies.Update(movie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteMovieAsync(Movie movie)
        {
            movie.IsDeleted = true;
            DbContext.Movies.Update(movie);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> HardDeleteMovieAsync(Movie movie)
        {
            DbContext.Movies.Remove(movie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
