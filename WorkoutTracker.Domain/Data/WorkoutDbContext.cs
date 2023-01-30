using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutMasters.API.Models;
using WorkoutMasters.Domain;
using WorkoutMasters.Domain.Models;

namespace WorkoutMasters.API.Data
{
    public class WorkoutDbContext : IdentityDbContext<AppUser>
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public DbSet<Workout>? Workouts { get; set; }
        public DbSet<Log>? Logs { get; set; }
    }
}
