using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using QuestPlannerAPI.Models;
using RestSharp;

namespace QuestPlannerAPI.Services
{
    public class CityApiService : ICityApiService
    {
        private readonly string baseUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";

        public List<CityModel> GetCityBySearch(string cityName)
        {
            var client = new RestClient();
            var request = new RestRequest(baseUrl, Method.Post);

            request.AddQueryParameter("input", cityName);
            request.AddQueryParameter("types", "locality");
            request.AddQueryParameter("key", "AIzaSyBQvQLke_0JE3jt9Ov3ZWOSUe04c3ESEAo");

            RestResponse response = client.Execute(request);

            JsonModel jsonResponse = JsonConvert.DeserializeObject<JsonModel>(response.Content);

            List<CityModel> output = new List<CityModel>();

            foreach (CityModel city in jsonResponse.Predictions)
            {
                output.Add(city);
            }

            return output;
        }
    }
}
