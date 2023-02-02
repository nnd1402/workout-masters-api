namespace WorkoutMasters.Domain.Services.Interfaces
{
    public interface IEmailService
    {
        void VerifyAccountEmail(string email, string token);
        void ForgotPasswordEmail(string email, string token);
    }
}
