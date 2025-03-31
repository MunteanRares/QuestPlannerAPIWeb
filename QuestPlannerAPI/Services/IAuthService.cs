using QuestPlannerAPI.Data;

namespace QuestPlannerAPI.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username, string email);
        string Login(Users request);
    }
}