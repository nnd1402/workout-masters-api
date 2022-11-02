using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.DTO.WorkoutDTO;

namespace WorkoutTracker.Domain.Services.Interfaces
{
    public interface IWorkoutService
    {
        IEnumerable<WorkoutOutputDTO> GetAll();

        IEnumerable<WorkoutOutputDTO> GetAllWorkoutsByUser();
        WorkoutOutputDTO GetById(Guid id);
        WorkoutOutputDTO Add(WorkoutInputDTO workout);
        void Update(Guid id, WorkoutInputDTO workout);
        void Delete(Guid id);
    }
}
