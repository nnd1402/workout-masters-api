using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WorkoutMasters.API.Models;
using WorkoutMasters.Domain.DTO.WorkoutDTO;
using WorkoutMasters.Domain.Exceptions;
using WorkoutMasters.Domain.Repositories.Interfaces;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.Domain.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public WorkoutService(IWorkoutRepository workoutRepository, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _workoutRepository = workoutRepository;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        public WorkoutOutputDTO GetById(Guid id)
        {
            var workout = _workoutRepository.GetById(id);
            if (workout == null)
            {
                throw new NotFoundException();
            }
            var workoutDTO = workout.ConvertToDTO();
            return workoutDTO;
        }

        public WorkoutOutputDTO Add(WorkoutInputDTO workoutDTO)
        {
            ValidateWorkout(workoutDTO);
            workoutDTO.Date = workoutDTO.Date.ToLocalTime();

            var workoutEntity = ConvertWorkoutDTOtoEntity(workoutDTO);
            workoutEntity = _workoutRepository.Add(workoutEntity);

            _workoutRepository.Save();

            return workoutEntity.ConvertToDTO();
        }
        public void Update(Guid id, WorkoutInputDTO workoutDTO)
        {
            ValidateWorkout(workoutDTO);
            workoutDTO.Date = workoutDTO.Date.ToLocalTime();

            var workoutEntity = ConvertWorkoutDTOtoEntity(workoutDTO);
            workoutEntity.Id = id;

            _workoutRepository.Update(id, workoutEntity);
            _workoutRepository.Save();
        }

        private static void ValidateWorkout(WorkoutInputDTO workoutDTO)
        {
            if (string.IsNullOrEmpty(workoutDTO.Title))
            {
                throw new ValidationException("Title is required");
            }
            DateTime? dateTime = workoutDTO.Date;
            if (!dateTime.HasValue)
            {
                throw new ValidationException("Date is required");
            }
        }

        private Workout ConvertWorkoutDTOtoEntity(WorkoutInputDTO workoutDTO)
        {
            var workoutEntity = new Workout(workoutDTO);
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            workoutEntity.AppUserId = userId;
            return workoutEntity;
        }

        public void Delete(Guid id)
        {
            var workout = _workoutRepository.GetById(id);
            if (workout == null)
            {
                throw new NotFoundException();
            }
            _workoutRepository.Delete(id);
            _workoutRepository.Save();
        }

        public IEnumerable<WorkoutOutputDTO> GetAllWorkoutsByUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workouts = _workoutRepository.GetAll().Where(w => w.AppUserId == userId);
            return workouts.Select(w => w.ConvertToDTO());
        }
    }
}
