using Newtonsoft.Json;

namespace QuestPlannerAPI.Models.Get_Photo_Name
{
    public class Place
    {
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
        public string id { get; set; }
    }
}
