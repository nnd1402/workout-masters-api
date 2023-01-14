using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Models;

namespace WorkoutTracker.API.Data
{
    public class WorkoutDbContext : IdentityDbContext<AppUser>
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout>? Workouts { get; set; }
        public DbSet<Log>? Logs { get; set; }
        public DbSet<EmailSettings>? EmailSettings { get; set; }
    }
}
