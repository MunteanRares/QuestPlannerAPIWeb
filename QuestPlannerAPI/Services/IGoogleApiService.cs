using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;

namespace QuestPlannerAPI.Services
{
    public interface IGoogleApiService
    {
        List<CityModel> GetCityBySearch(string cityName);
        List<DetailedCityModel> GetDetailedCity(string placeId);
    }
}