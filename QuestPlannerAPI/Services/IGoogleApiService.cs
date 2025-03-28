using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using QuestPlannerAPI.Models.Main_Page;
using QuestPlannerAPI.Models.Search_Nearby_Places;

namespace QuestPlannerAPI.Services
{
    public interface IGoogleApiService
    {
        IEnumerable<CityModel> GetCityBySearch(string cityName);
        IEnumerable<DetailedCityModel> GetDetailedCity(string placeId);
        IEnumerable<MostVisitedCityModel> GetMostVisitedCities();
        (string, string) GetPhotoNameAndPlaceId(string cityName);
        IEnumerable<NearbyPlacesModel> SerachNearby(float lat, float lng);
        IEnumerable<SeachNearbyPlacesCleanModel> SearchNearbyPlaces(string searchTerm, float lat, float lng);
    }
}