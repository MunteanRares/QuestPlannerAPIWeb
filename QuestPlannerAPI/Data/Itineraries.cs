namespace QuestPlannerAPI.Data
{
    public class Itineraries
    {
        public int Id { get; set; }
        public int UsersId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public Users User { get; set; }
        public ICollection<Days> Days { get; set; } = new List<Days>();
    }
}
