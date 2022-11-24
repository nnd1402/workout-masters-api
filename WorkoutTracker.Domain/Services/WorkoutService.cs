using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.DTO.WorkoutDTO;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Repositories.Interfaces;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkoutService(IWorkoutRepository workoutRepository, IHttpContextAccessor httpContextAccessor)
        {
            _workoutRepository = workoutRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<WorkoutOutputDTO> GetAll()
        {
            var workouts = _workoutRepository.GetAll();
            var workoutsDTO = workouts.ToList().ConvertAll(e => e.ConvertToDTO());
            _workoutRepository.Save();
            return workoutsDTO;
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
            if (string.IsNullOrEmpty(workoutDTO.Title))
            {
                throw new ValidationException("Title is required");
            }

            DateTime? dateTime = workoutDTO.Date;

            if (!dateTime.HasValue)
            {
                throw new ValidationException("Date is required");
            }

            workoutDTO.Date = workoutDTO.Date.ToLocalTime();
            var dtoToEntity = new Workout(workoutDTO);
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            dtoToEntity.AppUserId = userId;
            var workoutEntity = _workoutRepository.Add(dtoToEntity);
            _workoutRepository.Save();
            var result = workoutEntity.ConvertToDTO();
            return result;
        }

        public void Update(Guid id, WorkoutInputDTO workoutDTO)
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
            workoutDTO.Date = workoutDTO.Date.ToLocalTime();
            var dtoToEntity = new Workout(workoutDTO);
            dtoToEntity.Id = id;
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            dtoToEntity.AppUserId = userId;
            _workoutRepository.Update(id, dtoToEntity);
            _workoutRepository.Save();
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
            var result = new List<WorkoutOutputDTO>();
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workouts = _workoutRepository.GetAll();

            foreach (var workout in workouts)
            {
                if (workout.AppUserId == userId)
                {
                    var workoutDTO = workout.ConvertToDTO();
                    result.Add(workoutDTO);
                }
            }
            return result;
        }
    }
}
