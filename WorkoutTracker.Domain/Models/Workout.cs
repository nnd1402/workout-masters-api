using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.API.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
