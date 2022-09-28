using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Repository
{
    public interface IWorkoutRepository
    {
        Task<List<Workout>> GetWorkouts();
        Task<Workout> GetWorkout(Guid Id);
        Task<Workout> AddWorkout(Workout Workout);
        Task<Workout> UpdateWorkout(Workout Workout);
        Task<Workout> DeleteWorkout(Guid Id);
    }
}
