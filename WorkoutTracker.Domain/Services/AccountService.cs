using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Services;
//using WorkoutTracker.Domain.DTO.EmailDTO;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly WorkoutDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public AccountService(
            WorkoutDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserOutputDTO?> Register(UserInputDTO userDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == userDto.UserName) && await _userManager.Users.AnyAsync(x => x.Email == userDto.UserName))
            {
                throw new ValidationException("This username already exists");
            }
            var newUser = new AppUser()
            {
                Email = userDto.UserName,
                UserName = userDto.UserName,
            };
            var res = await _userManager.CreateAsync(newUser, userDto.Password);
            if (res.Succeeded)
            {
                return CreateUserObject(newUser);
            }
            return null;
        }

        public async Task<UserOutputDTO?> Login(UserInputDTO userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.UserName);

            var checkPassword = await _userManager.CheckPasswordAsync(user, userDto.Password);

            if (checkPassword == false)
            {
                throw new ValidationException("Wrong password");
            }

            if (user == null)
            {
                throw new NotFoundException();
            };

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return null;
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
