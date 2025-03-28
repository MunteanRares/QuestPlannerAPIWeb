using Newtonsoft.Json;

namespace QuestPlannerAPI.Models.Detailed_City_Model
{
    public class DetailedCityModel
    {
        [JsonProperty(PropertyName = "formattedAddress")]
        public string FormattedAddress { get; set; }

        public LocationModel Location { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public List<PhotosModel> PhotosReferences { get; set; }

        public DisplayName DisplayName { get; set; }

        public string PhotoLinks { get; set; }

        public string Vicinity { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }
    }
}
