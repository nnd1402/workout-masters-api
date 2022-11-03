using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Models;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        //private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            //_emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("create")]     
        public async Task<ActionResult<UserOutputDTO>> Register([FromBody] UserInputDTO userDto)
        {
            var newUser = new AppUser()
            {
                Email = userDto.UserName,
                UserName = userDto.UserName,
            };
            var res = await _userManager.CreateAsync(newUser, userDto.Password);
            if (res.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { newUser.Email, token }, Request.Scheme);
                EmailHelper emailHelper = new EmailHelper();
                emailHelper.SendEmail(newUser.Email, confirmationLink);
                return Ok(_accountService.CreateUserObject(newUser));
            }
          
            
                return BadRequest();
            
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Fail");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok("Email confirmed!");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserInputDTO userDto)
        {
            try
            {
                return Ok(await _accountService.Login(userDto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<UserInputDTO>> GetCurrentUser()
        {
            return Ok(await _accountService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email)));
        }
    }
}
