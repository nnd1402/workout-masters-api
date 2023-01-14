namespace WorkoutTracker.Domain.Models
{
    public class EmailSettings
    {
        public string Id { get; set; }
        public string VerifyAccountTemplate { get; set; }
        public string ForgotPasswordTemplate { get; set; }
    }
}
