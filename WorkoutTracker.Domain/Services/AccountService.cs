using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using WorkoutMasters.API.Services;
using WorkoutMasters.Domain.DTO.UserDTO;
using WorkoutMasters.Domain.DTO.UserDTOs;
using WorkoutMasters.Domain.Exceptions;
using WorkoutMasters.Domain.Models;
using WorkoutMasters.Domain.Repositories.Interfaces;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly ILogService _logService;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService,
            IEmailService emailService,
            ILogService logService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _logService = logService;
        }

        public async Task<UserOutputDTO?> Register(UserInputDTO userDto)
        {
            await RegisterValidation(userDto);

            var newUser = new AppUser()
            {
                Email = userDto.UserName,
                UserName = userDto.UserName,
            };
            var res = await _userManager.CreateAsync(newUser, userDto.Password);

            if (res.Succeeded)
            {
                await CreateAndSendConfirmationEmail(newUser);
                return CreateUserObject(newUser);
            }
            return null;
        }

        public async Task<UserOutputDTO?> Login(UserInputDTO userDto)
        {
            await LoginValidation(userDto);

            var user = await _userManager.FindByEmailAsync(userDto.UserName);

            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return null;
        }

        public async Task<UserOutputDTO?> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return null;
        }

        public async Task<bool> SendNewConfirmationEmail(SendEmailDTO sendEmailDTO)
        {
            CheckAllFields(sendEmailDTO);

            CheckIfEmailIsValid(sendEmailDTO);

            var user = await _userManager.FindByEmailAsync(sendEmailDTO.UserName);
            CheckIfUserExists(user);

            await CreateAndSendConfirmationEmail(user);
            return true;
        }

        public async Task<bool> ForgotPassword(SendEmailDTO sendEmailDTO)
        {
            CheckAllFields(sendEmailDTO);

            CheckIfEmailIsValid(sendEmailDTO);

            var user = await _userManager.FindByEmailAsync(sendEmailDTO.UserName);
            CheckIfUserExists(user);

            await CreateAndSendForgotPasswordEmail(user);
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var passwordMatch = Regex.Match(resetPasswordDTO.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            if (!passwordMatch.Success)
            {
                throw new ValidationException("Invalid password");
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.UserName);
            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist.");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);

            if (!resetPassResult.Succeeded)
            {
                throw new ValidationException("Reseting password was not successful");
            }
            return true;
        }

        public async Task<UserOutputDTO?> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return CreateUserObject(user);
        }

        public UserOutputDTO CreateUserObject(AppUser user)
        {
            return new UserOutputDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task RegisterValidation(UserInputDTO userDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == userDto.UserName) && await _userManager.Users.AnyAsync(x => x.Email == userDto.UserName))
            {
                throw new ValidationException("User with this email address already exists");
            }

            CheckAllFields(userDto);

            CheckIfEmailIsValid(userDto);

            CheckInvalidPassword(userDto);
        }

        private async Task LoginValidation(UserInputDTO userDto)
        {
            CheckAllFields(userDto);

            CheckIfEmailIsValid(userDto);

            CheckInvalidPassword(userDto);

            var user = await _userManager.FindByEmailAsync(userDto.UserName);
            if (user == null)
            {
                return;
            }

            if (!await _userManager.CheckPasswordAsync(user, userDto.Password))
            {
                throw new ValidationException("Wrong password");
            }

            if (!user.EmailConfirmed)
            {
                throw new ValidationException("Your account must be confirmed first! Check your email inbox to find your confirmation link");
            }
        }

        private static void CheckInvalidPassword(UserInputDTO userDto)
        {
            var passwordMatch = Regex.Match(userDto.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            if (!passwordMatch.Success)
            {
                throw new ValidationException("Invalid password");
            }
        }

        private static void CheckIfEmailIsValid(UserInputDTO userDto)
        {
            var emailMatch = Regex.Match(userDto.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }
        }

        private static void CheckIfEmailIsValid(SendEmailDTO userDto)
        {
            var emailMatch = Regex.Match(userDto.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }
        }

        private static void CheckAllFields(UserInputDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new ValidationException("Please fill in all fields!");
            }
        }

        private static void CheckAllFields(SendEmailDTO sendEmailDTO)
        {
            if (string.IsNullOrEmpty(sendEmailDTO.UserName))
            {
                throw new ValidationException("Please fill in all fields!");
            }
        }

        private static void CheckIfUserExists(AppUser user)
        {
            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist.");
            }
        }

        private async Task CreateAndSendConfirmationEmail(AppUser newUser)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string tokenHtmlVersion = HttpUtility.UrlEncode(token);
            string confirmationLink = $"https://www.workoutmasters.pro/account-confirmation?Email={newUser.Email}&token={tokenHtmlVersion}";
            _emailService.VerifyAccountEmail(newUser.Email, confirmationLink);
        }

        private async Task CreateAndSendForgotPasswordEmail(AppUser user)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string tokenHtmlVersion = HttpUtility.UrlEncode(token);
            string resetPasswordLink = $"https://www.workoutmasters.pro/reset-password?Email={user.Email}&token={tokenHtmlVersion}";
            _emailService.ForgotPasswordEmail(user.Email, resetPasswordLink);
        }
    }
}
