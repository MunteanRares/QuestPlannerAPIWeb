using Newtonsoft.Json;

namespace QuestPlannerAPI.Models.Get_Photo_Name
{
    public class GetPhotoNamePhoto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
