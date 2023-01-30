using System.ComponentModel.DataAnnotations;

namespace WorkoutMasters.Domain.DTO.WorkoutDTO
{
    public class WorkoutInputDTO
    {
        public string Title { get; set; }
        public int? Duration { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
