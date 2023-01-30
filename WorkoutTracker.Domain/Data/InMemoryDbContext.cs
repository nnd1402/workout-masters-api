using Microsoft.EntityFrameworkCore;
using WorkoutMasters.API.Models;

namespace WorkoutMasters.API.Data
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
