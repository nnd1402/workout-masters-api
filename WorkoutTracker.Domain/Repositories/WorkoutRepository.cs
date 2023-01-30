using WorkoutMasters.API.Data;
using WorkoutMasters.API.Models;
using WorkoutMasters.Domain.Repositories;
using WorkoutMasters.Domain.Repositories.Interfaces;

namespace WorkoutMasters.API.Repository
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(WorkoutDbContext context) : base(context)
        {
        }

    }
}
