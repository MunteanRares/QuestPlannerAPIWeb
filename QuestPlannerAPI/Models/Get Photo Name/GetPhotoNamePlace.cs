using Newtonsoft.Json;

namespace QuestPlannerAPI.Models.Get_Photo_Name
{
    public class GetPhotoNamePlace
    {
        [JsonProperty("photos")]
        public List<GetPhotoNamePhoto> Photos { get; set; }
        public string id { get; set; }
    }
}
