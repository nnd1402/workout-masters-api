using Microsoft.EntityFrameworkCore;
using WorkoutMasters.API.Data;
using WorkoutMasters.API.Repository;
using WorkoutMasters.Domain.Extensions;
using WorkoutMasters.Domain.Repositories;
using WorkoutMasters.Domain.Repositories.Interfaces;
using WorkoutMasters.Domain.Services;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection ManageDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            var configDatabaseType = config.GetValue(typeof(string), "DatabaseType").ToString();
            if (configDatabaseType == "Memory")
            {
                services.AddTransient<IInMemoryWorkoutRepository, InMemoryWorkoutRepository>();
            }
            else if (configDatabaseType == "SQL")
            {
                services.AddDbContext<WorkoutDbContext>(
                    o => o.UseSqlServer(config.GetConnectionString("SqlServer")));
                services.AddTransient<IWorkoutRepository, WorkoutRepository>();
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<IWorkoutService, WorkoutService>();
                services.AddTransient<IAccountService, AccountService>();
                services.AddTransient<ILogRepository, LogRepository>();
                services.AddTransient<ILogService, LogService>();
                services.AddIdentityServices(config);
            }
            return services;
        }
    }
}
