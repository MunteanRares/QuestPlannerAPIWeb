using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestPlannerAPI.Models;
using QuestPlannerAPI.Models.Detailed_City_Model;
using QuestPlannerAPI.Models.Get_Photo_Name;
using QuestPlannerAPI.Models.Main_Page;
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

        public IEnumerable<CityModel> GetCityBySearch(string cityName)
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

        public IEnumerable<DetailedCityModel> GetDetailedCity(string placeId)
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

        public IEnumerable<MostVisitedCityModel> GetMostVisitedCities()
        {
            var request = new RestRequest("https://en.wikipedia.org/wiki/List_of_cities_by_international_visitors", Method.Get);

            RestResponse response = _client.Execute(request);

            HtmlParser parser = new HtmlParser();

            IHtmlDocument mostPopularCities = parser.ParseDocument(response.Content);            

            var cityTable = mostPopularCities.QuerySelector("table");

            var rows = cityTable.QuerySelectorAll("tr").Skip(1).ToList();

            List<MostVisitedCityModel> output  = new List<MostVisitedCityModel>();


            foreach (var row in rows)
            {
                var cells = row.QuerySelectorAll("td");
                string cityName = cells[1].TextContent.Replace("\n", "").Replace(" ", "");
                string countryName = cells[2].TextContent.Replace("\n", "").Replace(" ", "");

                (string photoName, string placeId) = GetPhotoNameAndPlaceId(cityName);
                string imageUrl = GetImageFromReferenceCode(photoName);

                MostVisitedCityModel tempObj = new MostVisitedCityModel { CityName = cityName, Country=countryName, ImageUrl = imageUrl, placeId = placeId };
                output.Add(tempObj);
            }

            return output;
        }

        public (string, string) GetPhotoNameAndPlaceId(string cityName)
        {
            var request = new RestRequest("https://places.googleapis.com/v1/places:searchText", Method.Post);

            request.AddQueryParameter("textQuery", cityName);
            request.AddHeader("X-Goog-Api-Key", _key);
            request.AddHeader("X-Goog-FieldMask", "places.photos,places.id");

            RestResponse response = _client.Execute(request);

            PlacesResponse jsonContent = JsonConvert.DeserializeObject<PlacesResponse>(response.Content);

            Random rnd = new Random();
            int rndIndex = rnd.Next(0, jsonContent.Places[0].Photos.Count - 1);

            return (jsonContent.Places[0].Photos[rndIndex].Name, jsonContent.Places[0].id);
        }

        public IEnumerable<NearbyPlacesModel> SerachNearby(float lat, float lng)
        {
            var payload = new
            {
                maxResultCount = 7,
                locationRestriction = new
                {
                    circle = new
                    {
                        center = new
                        {
                            latitude = lat,
                            longitude = lng
                        },
                        radius = 1000
                    }
                }
            };

            string jsonbody = JsonConvert.SerializeObject(payload);

            var request = new RestRequest("https://places.googleapis.com/v1/places:searchNearby", Method.Post);
            request.AddQueryParameter("key", _key);
            request.AddQueryParameter("languageCode", "en");
            request.AddHeader("X-Goog-FieldMask", "places.photos,places.displayName,places.formattedAddress,places.addressComponents");
            request.AddBody(jsonbody, contentType: "application/json");

            RestResponse response = _client.Execute(request);
            Root objectResponse = JsonConvert.DeserializeObject<Root>(response.Content);

            string currentCityName = objectResponse.places[0].addressComponents.FirstOrDefault(elementInAddressComponents => elementInAddressComponents.types.Contains("locality")).longText;

            (string photoName, string placeId) = GetPhotoNameAndPlaceId(currentCityName);

            List<NearbyPlacesModel> output = new List<NearbyPlacesModel>();


            foreach (var place in objectResponse.places)
            {
                Random rnd = new Random();
                int rndIndex = rnd.Next(0, place.photos.Count - 1);
                string imageUrl = GetImageFromReferenceCode(place.photos[rndIndex].name);
                output.Add(new NearbyPlacesModel { PlaceName = place.displayName.text, CurrentCityName = currentCityName, formattedAddress = place.formattedAddress, ImageUrl = imageUrl, PlaceId = placeId });
            }

            return output;
        }
    }
}
