using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace QuestPlannerAPI.Models
{
    public class StructuredFormattingModel
    {
        [JsonProperty(PropertyName = "main_text")]
        public string MainText { get; set; }
    }
}
