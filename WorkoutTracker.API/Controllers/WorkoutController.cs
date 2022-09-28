using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.API.Models;
using WorkoutTracker.API.Repository;

namespace WorkoutTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        readonly IWorkoutRepository _workoutRepository;
        public WorkoutController(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetWorkouts()
        {
            try
            {
                return Ok(await _workoutRepository.GetWorkouts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(Guid id)
        {
            try
            {
                var result = await _workoutRepository.GetWorkout(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Workout>> CreateWorkout(Workout workout)
        {
            try
            {
                if (workout == null)
                    return BadRequest();

                var createdWorkout = await _workoutRepository.AddWorkout(workout);
                return CreatedAtAction(nameof(GetWorkout),
                new { id = createdWorkout.Id }, createdWorkout);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new workout");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Workout>> UpdateWorkout(Guid id, Workout workout)
        {
            try
            {
                if (id != workout.Id)
                    return BadRequest("Workout ID mismatch");

                var workoutToUpdate = await _workoutRepository.GetWorkout(id);

                if (workoutToUpdate == null)
                    return NotFound($"Workout with Id = {id} not found");

                return await _workoutRepository.UpdateWorkout(workout);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Workout>> DeleteWorkout(Guid id)
        {
            try
            {
                var workoutToDelete = await _workoutRepository.GetWorkout(id);

                if (workoutToDelete == null)
                {
                    return NotFound($"Workout with Id = {id} not found");
                }

                return await _workoutRepository.DeleteWorkout(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}

