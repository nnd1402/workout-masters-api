using WorkoutTracker.API.Data;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.Repositories;
using WorkoutTracker.Domain.Repositories.Interfaces;

namespace WorkoutTracker.API.Repository
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(WorkoutDbContext context) : base(context)
        {
        }

    }
}
