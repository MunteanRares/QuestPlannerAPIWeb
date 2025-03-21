using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using RestSharp;

namespace QuestPlannerAPI.Services
{
    public class GoogleApiService : IGoogleApiService
    {
        private readonly string _baseUrl = "https://maps.googleapis.com/maps/api/place";
        private readonly RestClient _client;
        private readonly string _key = "AIzaSyBQvQLke_0JE3jt9Ov3ZWOSUe04c3ESEAo";

        public GoogleApiService()
        {
            _client = new RestClient();
        }

        public List<CityModel> GetCityBySearch(string cityName)
        {
            //var client = new RestClient();
            var request = new RestRequest($"{_baseUrl}/autocomplete/json", Method.Post);

            request.AddQueryParameter("input", cityName);
            request.AddQueryParameter("types", "locality");
            request.AddQueryParameter("key", _key);

            RestResponse response = _client.Execute(request);

            JsonModel jsonResponse = JsonConvert.DeserializeObject<JsonModel>(response.Content);

            List<CityModel> output = new List<CityModel>();

            foreach (CityModel city in jsonResponse.Predictions)
            {
                output.Add(city);
            }

            return output;
        }

        public List<DetailedCityModel> GetDetailedCity(string placeId)
        {
            var request = new RestRequest($"https://places.googleapis.com/v1/places/{placeId}", Method.Get);

            //request.AddQueryParameter("place_id", placeId);
            request.AddQueryParameter("key", _key);
            request.AddQueryParameter("fields", "name,formattedAddress,photos,websiteUri,rating,userRatingCount");

            RestResponse response = _client.Execute(request);

            DetailedCityModel jsonResponse = JsonConvert.DeserializeObject<DetailedCityModel>(response.Content);


            List<DetailedCityModel> output = new List<DetailedCityModel>();

            Random random = new Random();
            try
            {
                if (jsonResponse.PhotosReferences != null)
                {
                    int randomIndex = random.Next(0, jsonResponse.PhotosReferences.Count);
                    jsonResponse.PhotoLinks = GetImageFromReferenceCode(jsonResponse.PhotosReferences[randomIndex].Name);
                }
            }
            catch { 
            }
            
            output.Add(jsonResponse);

            return output;
        }

        public string GetImageFromReferenceCode(string photoName)
        {
            var request = new RestRequest($"https://places.googleapis.com/v1/{photoName}/media", Method.Get);

            request.AddQueryParameter("maxWidthPx", "2400");
            request.AddQueryParameter("key", _key);
            request.AddQueryParameter("skipHttpRedirect", "true");

            RestResponse response = _client.Execute(request);
            PhotoFromGoogleModel photoString = JsonConvert.DeserializeObject<PhotoFromGoogleModel>(response.Content);

            string imageUrl = photoString.PhotoUri;
            
            return imageUrl;
        }
    }
}
