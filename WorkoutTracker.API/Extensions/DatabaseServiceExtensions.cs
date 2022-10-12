using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Repository;
using WorkoutTracker.Domain.Extensions;
using WorkoutTracker.Domain.Repository;

namespace WorkoutTracker.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection ManageDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            var configDatabaseType = config.GetValue(typeof(string), "DatabaseType").ToString();
            if (configDatabaseType == "Memory")
            {
                services.AddTransient<IWorkoutRepository, InMemoryWorkoutRepository>();
            }
            else if (configDatabaseType == "SQL")
            {
                services.AddDbContext<WorkoutDbContext>(
                    o => o.UseSqlServer(config.GetConnectionString("SqlServer")));
                services.AddTransient<IWorkoutRepository, SqlWorkoutRepository>();

                services.AddIdentityServices(config);

                //builder.Services.AddTransient<User>();
                services.AddTransient<IAccountRepository, SqlAccountRepository>();
            }
            return services;
        }
    }
}
