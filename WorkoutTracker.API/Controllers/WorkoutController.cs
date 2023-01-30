using Microsoft.AspNetCore.Mvc;
using WorkoutMasters.API.Models;
using WorkoutMasters.Domain.DTO.WorkoutDTO;
using WorkoutMasters.Domain.Exceptions;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.API.Controllers
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
                return NotFound($"Workout was not found");
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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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
                return NotFound($"Workout was not found");
            }
        }
    }
}

