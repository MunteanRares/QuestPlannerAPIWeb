using System.Security.Policy;
using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;

namespace QuestPlannerAPI.Data
{
    public class Activities
    {
        public int Id { get; set; }
        public int DaysId { get; set; }
        public string Location{ get; set; }
        public string ImageUrl { get; set; }
        public Days Day { get; set; }
    }
}
