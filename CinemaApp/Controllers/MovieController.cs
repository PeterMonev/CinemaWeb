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

            } catch (EntityPersistFailureException ex)
            {
                logger.LogError(ex, CrudMovieFailureMessage);
                ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "creating"));
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

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Details(Guid id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            MovieDetailsViewModel? movieDetailsVm = await movieService.GetDetailsByIdAsync(id);

            if(movieDetailsVm == null)
            {
                return NotFound();
            }

            return View(movieDetailsVm);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            MovieFormModel? model = await movieService.GetMovieFormModelByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]Guid id, MovieFormModel model)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            bool existById = await movieService.ExistsByIdAsync(id);

            if (!existById)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);

            }

            try
            {
               await movieService.EditMovieAsync(id, model);
            } catch (EntityNotFoundException ex)
            {
                return NotFound();
            } catch (EntityPersistFailureException ex)
            {
                logger.LogError(ex, CrudMovieFailureMessage);
                ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "edit"));
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            MovieDetailsViewModel? movieDetailsViewModel = await movieService.GetDetailsByIdAsync(id);

            if(movieDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(movieDetailsViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromRoute]Guid id,MovieDetailsViewModel? deleteView)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await movieService.SoftDeleteMovieAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (EntityPersistFailureException ex)
            {
                logger.LogError(ex, string.Format(CrudMovieFailureMessage, nameof(Delete)));
                ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "deleting"));
                return View(deleteView);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
 