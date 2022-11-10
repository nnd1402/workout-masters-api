using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Domain.DTO.WorkoutDTO
{
    public class WorkoutInputDTO
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
