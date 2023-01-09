using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.DTO.UserDTO;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
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


        [HttpPost]
        public async Task<ActionResult<UserOutputDTO>> Register([FromBody] UserInputDTO userDto)
        {
            try
            {
                // return Ok(await _accountService.Register(userDto));
                var registerResult = await _accountService.Register(userDto);
                if (registerResult != null)
                {
                    return Ok(registerResult);
                }
                else
                {
                    return BadRequest("O NEEE JEBIGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                return Ok(await _accountService.ConfirmEmail(token, email));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserInputDTO userDto)
        {
            try
            {
                return Ok(await _accountService.Login(userDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendNewConfirmationEmail([FromBody] SendEmailDTO sendEmailDTO)
        {
            try
            {
                return Ok(await _accountService.SendNewConfirmationEmail(sendEmailDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword([FromBody] SendEmailDTO sendEmailDTO)
        {
            try
            {
                return Ok(await _accountService.ForgotPassword(sendEmailDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                return Ok(await _accountService.ResetPassword(resetPasswordDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
