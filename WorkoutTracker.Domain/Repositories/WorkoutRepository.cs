using WorkoutTracker.API.Data;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.Repositories;
using WorkoutTracker.Domain.Repositories.Interfaces;

namespace WorkoutTracker.API.Repository
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(WorkoutDbContext context) : base(context)
        {
        }
        //    private readonly WorkoutDbContext _context;
        //    public WorkoutRepository(WorkoutDbContext context)
        //    {
        //        _context = context;
        //    }
        //    public Task<List<Workout>> GetWorkouts()
        //    {
        //       return _context.Workouts.ToListAsync();      
        //    }

        //    //public Task<List<Workout>> GetWorkoutsByAppUserId(string AppUserId)
        //    //{
        //    //    List<Workout> result = new List<Workout>();
        //    //    List<Workout> workouts = _context.Workouts.FindById(AppUserId);
        //    //    return result;

        //    //}
        //    public async Task<Workout?> GetWorkout(Guid Id)
        //    {
        //        return await _context.Workouts
        //           .FirstOrDefaultAsync(e => e.Id == Id);
        //    }
        //public async Task<Workout?> AddWorkout(Workout workout)
        //{
        //    workout.Date = workout.Date.ToLocalTime();
        //    var result = await _context.Workouts.AddAsync(workout);
        //    await _context.SaveChangesAsync();
        //    return result.Entity;
        //}
        //    public async Task<Workout?> UpdateWorkout(Workout workout)
        //    {
        //        var result = await _context.Workouts
        //            .FirstOrDefaultAsync(e => e.Id == workout.Id);

        //        if (result != null)
        //        {
        //            result.Title = workout.Title;
        //            result.Duration = workout.Duration;
        //            result.Description = workout.Description;
        //            result.Date = workout.Date;

        //            await _context.SaveChangesAsync();

        //            return result;
        //        }
        //        return null;
        //    }
        //    public async Task<Workout?> DeleteWorkout(Guid Id)
        //    {
        //        var result = await _context.Workouts
        //                        .FirstOrDefaultAsync(e => e.Id == Id);
        //        if (result != null)
        //        {
        //            _context.Workouts.Remove(result);
        //            await _context.SaveChangesAsync();
        //            return result;
        //        }
        //        return null;
        //    }
        //}
    }
}
