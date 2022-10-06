using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Data
{
    public class WorkoutDbContext : IdentityDbContext
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout>? Workouts { get; set; }
    }
}
