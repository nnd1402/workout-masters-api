using WorkoutMasters.Domain.Models;
using WorkoutMasters.Domain.Repositories;
using WorkoutMasters.Domain.Repositories.Interfaces;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.Domain.Services
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
