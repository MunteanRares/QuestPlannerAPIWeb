using Newtonsoft.Json;

namespace QuestPlannerAPI.Models.Get_Photo_Name
{
    public class PlacesResponse
    {
        [JsonProperty("places")]
        public List<Place> Places { get; set; }
    }
}
