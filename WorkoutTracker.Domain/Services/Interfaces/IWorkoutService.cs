using WorkoutMasters.Domain.DTO.WorkoutDTO;

namespace WorkoutMasters.Domain.Services.Interfaces
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
