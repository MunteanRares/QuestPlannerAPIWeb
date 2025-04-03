using QuestPlannerAPI.Data;

namespace QuestPlannerAPI.Models
{
    public class DaysDTO
    {
        public DateTime Date { get; set; }
        public ICollection<ActivitiesDTO> Activities { get; set; } = new List<ActivitiesDTO>();
    }
}
