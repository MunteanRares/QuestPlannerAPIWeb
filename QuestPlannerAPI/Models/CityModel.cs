using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace QuestPlannerAPI.Models
{
    public class CityModel
    {
        public string Description { get; set; }

        [JsonProperty(PropertyName = "structured_formatting")]
        public StructuredFormattingModel StructuredFormatting { get; set; }
    }
}