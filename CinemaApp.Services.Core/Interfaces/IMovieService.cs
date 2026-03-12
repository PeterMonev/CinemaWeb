using CinemaApp.Data.Models;
using CinemaApp.Services.Models.Movie;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedByTitleAsync();

        Task CreateMovieAsync(MovieDetailsDto model);

        Task<MovieDetailsDto> GetDetailsByIdAsync(Guid id);

        Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid id);

        Task<bool> ExistsByIdAsync(Guid id);

        Task EditMovieAsync(Guid id, MovieDetailsDto model);

        Task SoftDeleteMovieAsync(Guid id);

        Task HardDeleteMovieAsync(Guid id);
    }
}
