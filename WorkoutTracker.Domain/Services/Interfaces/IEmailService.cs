namespace WorkoutTracker.Domain.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email, string token);
    }
}
