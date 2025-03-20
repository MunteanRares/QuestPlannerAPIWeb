using QuestPlannerAPI.Models;

namespace QuestPlannerAPI.Services
{
    public interface ICityApiService
    {
        List<CityModel> GetCityBySearch(string cityName);
    }
}