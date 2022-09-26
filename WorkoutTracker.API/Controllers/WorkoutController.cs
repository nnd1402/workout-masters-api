//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WorkoutTracker.API.Data;
//using WorkoutTracker.API.Models;

//namespace WorkoutTracker.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class WorkoutController : ControllerBase
//    {
//        private readonly WorkoutDbContext _context;
//        public WorkoutController(WorkoutDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IEnumerable<Workout>> Get()
//        {
//            return await _context.Workouts.ToListAsync();
//        }

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        public async Task<IActionResult> Create(Workout workout)
//        {
//            workout.Date = workout.Date.ToLocalTime();
//            await _context.Workouts.AddAsync(workout);
//            await _context.SaveChangesAsync();

//            return Ok(workout);
//        }

//        [HttpPut("{id}")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Update(Guid id, Workout workout)
//        {
//            if (id != workout.Id) return BadRequest();

//            _context.Entry(workout).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            var workoutToDelete = await _context.Workouts.FindAsync(id);
//            if (workoutToDelete == null) return NotFound();

//            _context.Workouts.Remove(workoutToDelete);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
