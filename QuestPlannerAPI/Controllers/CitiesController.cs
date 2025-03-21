using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using QuestPlannerAPI.Services;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IGoogleApiService _cityApiService;

        public CitiesController(ILogger<CitiesController> logger, IGoogleApiService cityApiService)
        {
            _logger = logger;
            _cityApiService = cityApiService;
        }

        // GET: api/<CitiesController>
        [HttpGet("getDetailedCity")]
        public List<DetailedCityModel> GetDetails(string placeId)
        {
            return _cityApiService.GetDetailedCity(placeId);
        }

        // GET api/<CitiesController>
        [HttpGet("search")]
        public IEnumerable<CityModel> Get(string cityName)
        {
            return _cityApiService.GetCityBySearch(cityName); ;
        }
    }
}
