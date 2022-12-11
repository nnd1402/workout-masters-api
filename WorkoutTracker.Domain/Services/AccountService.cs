using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;
using WorkoutTracker.API.Services;
using WorkoutTracker.Domain.DTO.UserDTO;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<UserOutputDTO?> Register(UserInputDTO userDto)
        {
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

            var newUser = new AppUser()
            {
                Email = userDto.UserName,
                UserName = userDto.UserName,
            };
            var res = await _userManager.CreateAsync(newUser, userDto.Password);
            if (res.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string tokenHtmlVersion = HttpUtility.UrlEncode(token);
                string confirmationLink = $"http://localhost:3000/account-confirmation?Email={newUser.Email}&token={tokenHtmlVersion}";
                _emailService.SendEmail(newUser.Email, confirmationLink);
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
            _emailService.SendEmail(user.Email, confirmationLink);
            return true;
        }

        public void SendBla(string email)
        {

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
