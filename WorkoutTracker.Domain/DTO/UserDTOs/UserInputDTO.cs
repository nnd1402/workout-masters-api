using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Domain.DTO.UserDTOs
{
    public class UserInputDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Username is not valid. It should be in standard E-mail form")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,}$", ErrorMessage = "Password must have at least one upper case and numeric character and it should be at least 6 characters long")]
        public string Password { get; set; }
    }
}
