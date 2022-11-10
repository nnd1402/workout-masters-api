using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Domain.DTO.WorkoutDTO
{
    public class WorkoutOutputDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
    }
}
