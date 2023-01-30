using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutMasters.API.Models;

namespace WorkoutMasters.Domain.Repositories.Interfaces
{
    public interface IInMemoryWorkoutRepository
    {
        Task<List<Workout>> GetWorkouts();
        Task<Workout?> GetWorkout(Guid Id);
        Task<Workout?> AddWorkout(Workout Workout);
        Task<Workout?> UpdateWorkout(Workout Workout);
        Task<Workout?> DeleteWorkout(Guid Id);
    }
}
