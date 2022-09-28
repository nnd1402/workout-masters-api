using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Data
{
    public class WorkoutDbContext : DbContext
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout>? Workouts { get; set; }
    }
}
