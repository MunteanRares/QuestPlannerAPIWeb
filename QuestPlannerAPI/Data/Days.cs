namespace QuestPlannerAPI.Data
{
    public class Days
    {
        public int Id { get; set; }
        public int ItinerariesId { get; set; }
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
        public Itineraries Itinerary { get; set; }
        public ICollection<Activities> Activities { get; set; } = new List<Activities>();
    }
}