namespace WorkoutTracker.Domain.Models
{
    public class EmailSettings
    {
        public string Id { get; set; }
        public string verifyAccountTemplate { get; set; }
        public string forgotPasswordTemplate { get; set; }
    }
}
