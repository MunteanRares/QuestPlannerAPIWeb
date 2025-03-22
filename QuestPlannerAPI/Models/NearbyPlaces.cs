using System.Diagnostics;

namespace QuestPlannerAPI.Models
{
    public class AddressComponent
    {
        public string longText { get; set; }
        public string shortText { get; set; }
        public List<string> types { get; set; }
        public string languageCode { get; set; }
    }

    public class AuthorAttribution
    {
        public string displayName { get; set; }
        public string uri { get; set; }
        public string photoUri { get; set; }
    }

    public class DisplayName
    {
        public string text { get; set; }
        public string languageCode { get; set; }
    }

    public class Photo
    {
        public string name { get; set; }
        public int widthPx { get; set; }
        public int heightPx { get; set; }
        public List<AuthorAttribution> authorAttributions { get; set; }
        public string flagContentUri { get; set; }
        public string googleMapsUri { get; set; }
    }

    public class Place
    {
        public string formattedAddress { get; set; }
        public List<AddressComponent> addressComponents { get; set; }
        public DisplayName displayName { get; set; }
        public List<Photo> photos { get; set; }
    }

    public class Root
    {
        public List<Place> places { get; set; }
    }

    public class NearbyPlacesModel
    {
        public string PlaceName { get; set; }
        public string CurrentCityName { get; set; }
        public string formattedAddress { get; set; }
        public string ImageUrl { get; set; }
        public string PlaceId { get; set; }
    }
}
