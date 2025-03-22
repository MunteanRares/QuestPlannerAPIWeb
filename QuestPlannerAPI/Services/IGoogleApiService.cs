using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using QuestPlannerAPI.Models.Main_Page;

namespace QuestPlannerAPI.Services
{
    public interface IGoogleApiService
    {
        List<CityModel> GetCityBySearch(string cityName);
        List<DetailedCityModel> GetDetailedCity(string placeId);
        List<MostVisitedCityModel> GetMostVisitedCities();
        (string, string) GetPhotoNameAndPlaceId(string cityName);
    }
}