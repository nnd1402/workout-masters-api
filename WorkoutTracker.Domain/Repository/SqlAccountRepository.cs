using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Services;
using WorkoutTracker.Domain.DTO;

namespace WorkoutTracker.Domain.Repository
{
    public class SqlAccountRepository : IAccountRepository
    {
        private readonly WorkoutDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public SqlAccountRepository(
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

        public async Task<UserDto> Register(string username, string password)
        {
            if (!await _userManager.Users.AnyAsync(x => x.UserName == username) && !await _userManager.Users.AnyAsync(x => x.Email == username))
            {
                var user = new AppUser
                {
                    Email = username,
                    UserName = username,
                };
                var res = await _userManager.CreateAsync(user, password);
                if (res.Succeeded)
                {
                    return CreateUserObject(user);
                }
                return null;
            }
            return null;
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                return CreateUserObject(user);
            }
            return null;
        }

        public async Task<UserDto> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return CreateUserObject(user);
        }

        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
