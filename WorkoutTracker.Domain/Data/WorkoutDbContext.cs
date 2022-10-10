
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain;

namespace WorkoutTracker.API.Data
{
    public class WorkoutDbContext : IdentityDbContext<AppUser>
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout>? Workouts { get; set; }
    }
}
