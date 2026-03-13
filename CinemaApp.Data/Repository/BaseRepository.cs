using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository
{
    public abstract class BaseRepository : IDisposable
    {
        private bool isDisposed = false;
        private readonly CinemaAppDbContext dbContext;

        protected BaseRepository(CinemaAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        protected CinemaAppDbContext DbContext => dbContext;

        protected async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
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

    }
}
