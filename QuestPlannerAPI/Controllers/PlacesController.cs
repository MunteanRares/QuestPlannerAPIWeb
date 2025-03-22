using Microsoft.AspNetCore.Mvc;
using QuestPlannerAPI.Models;
using QuestPlannerAPI.Services;

namespace QuestPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IGoogleApiService _cityApiService;
        public PlacesController(IGoogleApiService cityApiService)
        {
            _cityApiService = cityApiService;
        }

        [HttpGet("getNearby")]
        public IEnumerable<NearbyPlacesModel> GetNearby(float lat, float lng)
        {
            return _cityApiService.SerachNearby(lat, lng);
        }
    }
}
