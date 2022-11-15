using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.DTO.UserDTOs;
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
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
        }


        [HttpPost("create")]
        public async Task<ActionResult<UserOutputDTO>> Register([FromBody] UserInputDTO userDto)
        {
            try
            {
                return Ok(await _accountService.Register(userDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
