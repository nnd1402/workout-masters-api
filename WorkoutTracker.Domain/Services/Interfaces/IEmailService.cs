namespace WorkoutMasters.Domain.Services.Interfaces
{
    public interface IEmailService
    {
        void SendVerifyAccountEmail(string email, string token);
        void SendForgotPasswordEmail(string email, string token);
    }
}
