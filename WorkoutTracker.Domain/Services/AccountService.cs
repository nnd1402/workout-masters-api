using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;
using WorkoutTracker.API.Services;
using WorkoutTracker.Domain.DTO.UserDTO;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Domain.Repositories.Interfaces;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
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
            _logService.Create("register started");

            if (await _userManager.Users.AnyAsync(x => x.UserName == userDto.UserName) && await _userManager.Users.AnyAsync(x => x.Email == userDto.UserName))
            {
                throw new ValidationException("User with this email address already exists");
            }
            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new ValidationException("Please fill in all fields!");
            }
            var emailMatch = Regex.Match(userDto.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }
            var passwordMatch = Regex.Match(userDto.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            if (!passwordMatch.Success)
            {
                throw new ValidationException("Invalid password");
            }
            _logService.Create("register validation successful");

            var newUser = new AppUser()
            {
                Email = userDto.UserName,
                UserName = userDto.UserName,
            };
            var res = await _userManager.CreateAsync(newUser, userDto.Password);
            if (res.Succeeded)
            {
                _logService.Create("user created");
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string tokenHtmlVersion = HttpUtility.UrlEncode(token);
                string confirmationLink = $"http://localhost:3000/account-confirmation?Email={newUser.Email}&token={tokenHtmlVersion}";
                _emailService.SendVerifyAccountEmail(newUser.Email, confirmationLink);
                _logService.Create("email sent");
                return CreateUserObject(newUser);
            }
            return null;
        }

        public async Task<UserOutputDTO?> Login(UserInputDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
            {
                throw new ValidationException("Please fill in all fields!");
            }

            var emailMatch = Regex.Match(userDto.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }

            var user = await _userManager.FindByEmailAsync(userDto.UserName);

            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, userDto.Password);

            var passwordMatch = Regex.Match(userDto.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$");
            if (!passwordMatch.Success)
            {
                throw new ValidationException("Invalid password");
            }

            if (checkPassword == false)
            {
                throw new ValidationException("Wrong password");
            }

            if (user.EmailConfirmed == false)
            {
                throw new ValidationException("Your account must be confirmed first! Check your email inbox to find your confirmation link");
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
            if (string.IsNullOrEmpty(sendEmailDTO.UserName))
            {
                throw new ValidationException("Please fill the empty field");
            }

            var emailMatch = Regex.Match(sendEmailDTO.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }

            var user = await _userManager.FindByEmailAsync(sendEmailDTO.UserName);
            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist.");
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string tokenHtmlVersion = HttpUtility.UrlEncode(token);
            string confirmationLink = $"http://localhost:3000/account-confirmation?Email={user.Email}&token={tokenHtmlVersion}";
            _emailService.SendVerifyAccountEmail(user.Email, confirmationLink);
            return true;
        }
        public async Task<bool> ForgotPassword(SendEmailDTO sendEmailDTO)
        {
            if (string.IsNullOrEmpty(sendEmailDTO.UserName))
            {
                throw new ValidationException("Please fill the empty field");
            }

            var emailMatch = Regex.Match(sendEmailDTO.UserName, "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
            if (!emailMatch.Success)
            {
                throw new ValidationException("Invalid email address");
            }

            var user = await _userManager.FindByEmailAsync(sendEmailDTO.UserName);
            if (user == null)
            {
                throw new ValidationException("User with this email address doesn't exist.");
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string tokenHtmlVersion = HttpUtility.UrlEncode(token);
            string resetPasswordLink = $"http://localhost:3000/reset-password?Email={user.Email}&token={tokenHtmlVersion}";
            _emailService.SendForgotPasswordEmail(user.Email, resetPasswordLink);
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
                throw new ValidationException("Reseting password was not successfull");
            }
            return true;
        }

        public async Task<UserOutputDTO> GetCurrentUser(string email)
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
    }
}
