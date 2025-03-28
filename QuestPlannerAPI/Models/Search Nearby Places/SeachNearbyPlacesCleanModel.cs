namespace QuestPlannerAPI.Models.Search_Nearby_Places
{
    public class SeachNearbyPlacesCleanModel
    {
        public string PlaceName { get; set; }
        public string PlaceId { get; set; }
        public List<string> PlaceTags { get; set; }
    }
}
