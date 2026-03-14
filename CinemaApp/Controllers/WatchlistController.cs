using AutoMapper;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Services.Models.Watchlist;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;

using static CinemaApp.GCommon.OutputMessages.Watchlist;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController :BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly IMapper mapper;
        private readonly ILogger<WatchlistController> logger;
        public WatchlistController(IWatchlistService watchlistService, IMapper mapper, ILogger<WatchlistController> logger)
        {
            this.watchlistService = watchlistService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var watchlistDtos = await watchlistService.GetUserWatchlistByIdAsync(userId);

            var viewModels = mapper.Map<IEnumerable<WatchlistMovieViewModel>>(watchlistDtos);

            return View(viewModels);
        }

        [HttpGet]

        public async Task<IActionResult> Add([FromRoute(Name = "id")] Guid movieId)
        {
            string userId = GetUserId();

            try
            {
                await watchlistService.AddMovieToUserWatchlistAsync(userId, movieId);

            } catch (EntityAlreadyExistsException ex)
            {
                logger.LogError(ex, string.Format(MovieAlreadyInWatchlistMessage, movieId, userId));
                return BadRequest();
            } catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Movie not found.");
                return NotFound();
            } catch (EntityPersistFailureException ex)
            {
                logger.LogError(ex, string.Format(AddToWatchlistFailureMessage ));
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid movieId)
        {
            string userdId = GetUserId();

            try
            {
                await watchlistService.RemoveMovieFromUserWatchlistAsync(userdId, movieId);

            } catch (EntityNotFoundException)
            {
                return BadRequest();
            } catch (EntityPersistFailureException ex)
            {
                logger.LogError(ex, string.Format(RemoveFromWatchlistFailureMessage));
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
