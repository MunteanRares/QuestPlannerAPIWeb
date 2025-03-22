using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using QuestPlannerAPI.Models.Main_Page;

namespace QuestPlannerAPI.Services
{
    public interface IGoogleApiService
    {
        IEnumerable<CityModel> GetCityBySearch(string cityName);
        IEnumerable<DetailedCityModel> GetDetailedCity(string placeId);
        IEnumerable<MostVisitedCityModel> GetMostVisitedCities();
        (string, string) GetPhotoNameAndPlaceId(string cityName);
        IEnumerable<NearbyPlacesModel> SerachNearby(float lat, float lng);   
    }
}