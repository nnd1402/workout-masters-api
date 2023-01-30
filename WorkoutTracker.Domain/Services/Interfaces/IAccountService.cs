using WorkoutMasters.Domain.DTO.UserDTO;
using WorkoutMasters.Domain.DTO.UserDTOs;

namespace WorkoutMasters.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserOutputDTO?> Register(UserInputDTO userDto);
        Task<UserOutputDTO?> Login(UserInputDTO userDto);
        Task<UserOutputDTO?> ConfirmEmail(string token, string email);
        Task<UserOutputDTO?> GetCurrentUser(string email);
        UserOutputDTO CreateUserObject(AppUser user);
        Task<bool> SendNewConfirmationEmail(SendEmailDTO sendEmailDTO);
        Task<bool> ForgotPassword(SendEmailDTO sendEmailDTO);
        Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
