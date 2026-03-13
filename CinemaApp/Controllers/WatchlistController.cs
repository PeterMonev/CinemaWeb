using AutoMapper;
using CinemaApp.Services.Core.Interfaces;
using CinemaApp.Services.Models.Watchlist;
using CinemaApp.Web.ViewModels.Watchlist;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController :BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly IMapper mapper;
        public WatchlistController(IWatchlistService watchlistService, IMapper mapper)
        {
            this.watchlistService = watchlistService;
            this.mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var watchlistDtos = await watchlistService.GetUserWatchlistByIdAsync(userId);

            var viewModels = mapper.Map<IEnumerable<WatchlistMovieViewModel>>(watchlistDtos);

            return View(viewModels);
        }
    }
}
