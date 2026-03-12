using CinemaApp.Data.Models;
using CinemaApp.Services.Models.Movie;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync();

        Task CreateMovieAsync(MovieFormModel model);

        Task<MovieDetailsViewModel> GetDetailsByIdAsync(Guid id);

        Task<MovieFormModel?> GetMovieFormModelByIdAsync(Guid id);

        Task<bool> ExistsByIdAsync(Guid id);

        Task EditMovieAsync(Guid id, MovieFormModel model);

        Task SoftDeleteMovieAsync(Guid id);

        Task HardDeleteMovieAsync(Guid id);
    }
}
