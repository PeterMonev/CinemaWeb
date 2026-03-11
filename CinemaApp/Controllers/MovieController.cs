using CinemaApp.Data.Models;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static CinemaApp.GCommon.OutputMessages.Movie;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> logger;
        private readonly IMovieService movieService;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            this.movieService = movieService;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = await movieService.GetAllMoviesOrderedByTitleAsync();

            return View(allMoviesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
             await movieService.CreateMovieAsync(model);

            } catch (DatabaseEntityCreatePersistFailureException ex)
            {
                logger.LogError(ex, CrudMovieFailureMessage);
                ModelState.AddModelError(string.Empty, CrudMovieFailureMessage);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);
                ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
