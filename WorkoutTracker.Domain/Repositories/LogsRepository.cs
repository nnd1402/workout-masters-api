using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Domain.Repositories.Interfaces;

namespace WorkoutTracker.Domain.Repositories
{
    public class LogsRepository : GenericRepository<Logs>, ILogsRepository
    {
        public LogsRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
