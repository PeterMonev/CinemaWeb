using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync();

        Task CreateMovieAsync(MovieFormModel model);

        Task<MovieDetailsViewModel> GetDetailsByIdAsync(Guid id);
    }
}
