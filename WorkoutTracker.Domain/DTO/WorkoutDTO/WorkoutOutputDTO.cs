using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Domain.DTO.WorkoutDTO
{
    public class WorkoutOutputDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Duration { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
    }
}
