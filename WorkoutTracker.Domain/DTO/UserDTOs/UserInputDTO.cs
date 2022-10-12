using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Domain.DTO.UserDTOs
{
    public class UserInputDTO
    {
        
        public string UserName { get; set; }      
        public string Password { get; set; }
    }
}
