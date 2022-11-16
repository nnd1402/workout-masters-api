using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.DTO.WorkoutDTO;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        public ActionResult List()
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
        public ActionResult ListByUser()
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
        public ActionResult GetById(Guid id)
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
        public ActionResult Create(WorkoutInputDTO workout)
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
        public ActionResult Update(Guid id, WorkoutInputDTO workout)
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
        public ActionResult<Workout> Delete(Guid id)
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

