﻿using Microsoft.AspNetCore.Http;
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
            if (string.IsNullOrEmpty(workoutDTO.Title))
            {
                throw new ValidationException("Title is required");
            }

            DateTime? dateTime = workoutDTO.Date;

            if (!dateTime.HasValue)
            {
                throw new ValidationException("Date is required");
            }

            _logService.Create("Passed workout validation");
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
