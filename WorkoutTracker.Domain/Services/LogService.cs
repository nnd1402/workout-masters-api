using WorkoutTracker.Domain.Models;
using WorkoutTracker.Domain.Repositories;
using WorkoutTracker.Domain.Repositories.Interfaces;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public void Create(string message)
        {
            Log log = new Log();
            log.Time = DateTime.Now.ToString();
            log.Id = Guid.NewGuid().ToString();
            log.Message = message;
            _logRepository.Add(log);
            _logRepository.Save();
        }
    }
}
