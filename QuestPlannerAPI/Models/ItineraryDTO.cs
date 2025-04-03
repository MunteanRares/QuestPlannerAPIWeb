using System.Text.Json.Serialization;
using QuestPlannerAPI.Data;

namespace QuestPlannerAPI.Models
{
    public class ItineraryDTO
    {
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<DaysDTO> Days { get; set; } = new List<DaysDTO>();
    }
}
