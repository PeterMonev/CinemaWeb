using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private readonly CinemaAppDbContext dbContext;
        private bool isDisposed = false;
        public MovieRepository(CinemaAppDbContext dbContext)
        { 
           this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Func<Movie, Movie>? projectFunc = null)
        {
            IQueryable<Movie> movieFetchQuery = this.dbContext.Movies
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
            return await this.dbContext.Movies
                .AsNoTracking()
                .OrderBy(m => m.Title)
                .ToArrayAsync();
        }

        public async Task<bool> AddMovieAsync(Movie movie)
        {
           await this.dbContext.Movies.AddAsync(movie);
           int resultCount = await SaveChangesAsync();

           return resultCount == 1;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
                    isDisposed = true;
        }

        private async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await dbContext.Movies
                 .FindAsync(id);
        }

        public async Task<bool> ExistsByAsync(Guid id)
        {
            return await dbContext.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> EditMovieAsync(Movie movie)
        {
            dbContext.Movies.Update(movie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }
    }
}
