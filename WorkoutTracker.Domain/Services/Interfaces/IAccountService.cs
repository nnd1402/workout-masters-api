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
        //Task RegisterValidation(UserInputDTO userInputDTO);
        //void CheckInvalidPassword(UserInputDTO userDto);
        //void CheckIfEmailIsValid(UserInputDTO userDto);
        //void CheckIfEmailIsValid(SendEmailDTO userDto);
        //void CheckAllFields(UserInputDTO userDto);
        //void CheckAllFields(SendEmailDTO sendEmailDTO);
        //void CheckIfUserExists(AppUser user);
        //Task CreateAndSendConfirmationEmail(AppUser newUser);
        //Task CreateAndSendForgotPasswordEmail(AppUser user);


    }
}
