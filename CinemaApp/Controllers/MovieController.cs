using CinemaApp.Data.Models;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static CinemaApp.GCommon.OutputMessages.Movie;
using static CinemaApp.GCommon.ApplicationConstants;
using AutoMapper;
using CinemaApp.Services.Models.Movie;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> logger;
        private readonly IMovieService movieService;
        private readonly IMapper mapper;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger, IMapper mapper)
        {
            this.movieService = movieService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<MovieAllDto> movieAllDtios = await movieService.GetAllMoviesOrderedByTitleAsync();

            IEnumerable<AllMoviesIndexViewModel> allMoviesIndexVms = mapper.Map<IEnumerable<AllMoviesIndexViewModel>>(movieAllDtios);

            return View(allMoviesIndexVms);
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
                MovieDetailsDto movieDetailsDto = mapper.Map<MovieDetailsDto>(model);
             await movieService.CreateMovieAsync(movieDetailsDto);

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

            MovieDetailsDto? movieDetailsDto = await movieService.GetDetailsByIdAsync(id);

            if(movieDetailsDto == null)
            {
                return NotFound();
            }

           MovieDetailsViewModel movieDetailsViewModel = mapper.Map<MovieDetailsViewModel>(movieDetailsDto);

            return View(movieDetailsViewModel);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            MovieDetailsDto? movieDetailsDto = await movieService.GetMovieFormModelByIdAsync(id);

            if (movieDetailsDto == null)
            {
                return NotFound();
            }

            MovieFormModel movieFormModel = mapper.Map<MovieFormModel>(movieDetailsDto);

            return View(movieDetailsDto);
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
                MovieDetailsDto movieDetailsDto = mapper.Map<MovieDetailsDto>(model);
               await movieService.EditMovieAsync(id, movieDetailsDto);
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

            MovieDetailsDto? movieDetailsDto = await movieService.GetDetailsByIdAsync(id);

            if(movieDetailsDto == null)
            {
                return NotFound();
            }

            MovieDeleteViewModel movieDeleteViewModel = mapper.Map<MovieDeleteViewModel>(movieDetailsDto);

            return View(movieDeleteViewModel);
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
 