using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutMasters.API.Data;
using WorkoutMasters.API.Models;
using WorkoutMasters.Domain.Models;
using WorkoutMasters.Domain.Repositories.Interfaces;

namespace WorkoutMasters.Domain.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
