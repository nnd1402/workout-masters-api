using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Data
{
    public class InMemoryDbContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryWorkoutDb");
        }
        public DbSet<Workout>? Workouts { get; set; }
    }
}
