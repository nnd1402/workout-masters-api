using Microsoft.EntityFrameworkCore;
using System;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Repository
{
    public class SqlWorkoutRepository : IWorkoutRepository
    {
        private readonly WorkoutDbContext _context;
        public SqlWorkoutRepository(WorkoutDbContext context)
        {
            _context = context;
        }
        public Task<List<Workout>> GetWorkouts()
        {
           return _context.Workouts.ToListAsync();      
        }
        public async Task<Workout> GetWorkout(Guid Id)
        {
            return await _context.Workouts
               .FirstOrDefaultAsync(e => e.Id == Id);
        }
        public async Task<Workout> AddWorkout(Workout workout)
        {
            workout.Date = workout.Date.ToLocalTime();
            var result = await _context.Workouts.AddAsync(workout);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Workout> UpdateWorkout(Workout workout)
        {
            var result = await _context.Workouts
                .FirstOrDefaultAsync(e => e.Id == workout.Id);

            if (result != null)
            {
                result.Title = workout.Title;
                result.Duration = workout.Duration;
                result.Description = workout.Description;
                result.Date = workout.Date;

                await _context.SaveChangesAsync();

                return result;
            }
            return null;
        }
        public async Task<Workout> DeleteWorkout(Guid Id)
        {
            var result = await _context.Workouts
                            .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Workouts.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
