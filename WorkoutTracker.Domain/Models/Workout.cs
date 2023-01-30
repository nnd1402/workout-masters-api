using WorkoutMasters.Domain.DTO.WorkoutDTO;

namespace WorkoutMasters.API.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Duration { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public string AppUserId { get; set; }

        public Workout() { }

        public WorkoutOutputDTO ConvertToDTO()
        {
            return new WorkoutOutputDTO
            {
                Id = this.Id,
                Title = this.Title,
                Duration = this.Duration,
                Description = this.Description,
                Date = this.Date,
                AppUserId = this.AppUserId
            };
        }

        public Workout(WorkoutInputDTO workoutOutputDTO)
        {
            Title = workoutOutputDTO.Title;
            Duration = workoutOutputDTO.Duration;
            Description = workoutOutputDTO.Description;
            Date = workoutOutputDTO.Date;
        }
    }
}
