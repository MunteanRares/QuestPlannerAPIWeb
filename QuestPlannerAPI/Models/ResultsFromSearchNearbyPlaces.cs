using Newtonsoft.Json;

namespace QuestPlannerAPI.Models
{
    public partial class ResultsFromSearchNearbyPlaces
    {
        [JsonProperty("suggestions")]
        public Suggestion[] Suggestions { get; set; }
    }

    public partial class Suggestion
    {
        [JsonProperty("placePrediction")]
        public PlacePrediction PlacePrediction { get; set; }
    }

    public partial class PlacePrediction
    {
        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("placeId")]
        public string PlaceId { get; set; }

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("structuredFormat")]
        public StructuredFormat StructuredFormat { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public partial class StructuredFormat
    {
        [JsonProperty("mainText")]
        public Text MainText { get; set; }

        [JsonProperty("secondaryText")]
        public SecondaryText SecondaryText { get; set; }
    }

    public partial class Text
    {
        [JsonProperty("text")]
        public string TextText { get; set; }

        [JsonProperty("matches")]
        public Match[] Matches { get; set; }
    }

    public partial class Match
    {
        [JsonProperty("endOffset")]
        public long EndOffset { get; set; }
    }

    public partial class SecondaryText
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
