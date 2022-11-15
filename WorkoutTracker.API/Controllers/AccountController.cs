using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Helpers;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }


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
                string tokenHtmlVersion = HttpUtility.UrlEncode(token);
                var confirmationLink = $"http://localhost:3000/account-confirmation?Email={newUser.Email}&token={tokenHtmlVersion}";
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(newUser.Email, confirmationLink);
                return Ok(_accountService.CreateUserObject(newUser));
            }
            return BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Account confirmation failed");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Account confirmation successful");
            }
            return BadRequest();

        }


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
