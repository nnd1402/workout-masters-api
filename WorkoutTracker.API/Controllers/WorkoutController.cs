using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.API.Models;
using WorkoutTracker.API.Repository;
using WorkoutTracker.Domain.DTO.WorkoutDTO;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        [Route("allWorkouts")]
        public ActionResult GetWorkouts()
        {
            try
            {
                return Ok(_workoutService.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public ActionResult GetWorkoutsByUser()
        {
            try
            {
                return Ok(_workoutService.GetAllWorkoutsByUser());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetWorkout(Guid id)
        {
            try
            {
                return Ok(_workoutService.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound($"Workout with the Id: {id} was not found");
            }
        }

        [HttpPost]
        public ActionResult CreateWorkout(WorkoutInputDTO workout)
        {
            try
            {
                return Ok(_workoutService.Add(workout));
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateWorkout(Guid id, WorkoutInputDTO workout)
        {
            try
            {
                _workoutService.Update(id, workout);
                return Ok(workout);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Workout> DeleteWorkout(Guid id)
        {
            try
            {
                _workoutService.Delete(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound($"Workout with the Id: {id} was not found");
            }
        }
    }
}

