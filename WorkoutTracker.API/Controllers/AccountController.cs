using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;
using WorkoutMasters.Domain;
using WorkoutMasters.Domain.DTO.UserDTO;
using WorkoutMasters.Domain.DTO.UserDTOs;
using WorkoutMasters.Domain.Services;
using WorkoutMasters.Domain.Services.Interfaces;

namespace WorkoutMasters.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogService _logService;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService, ILogService logService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _logService = logService;
        }

        [HttpPost]
        public ActionResult<UserOutputDTO> Register([FromBody] UserInputDTO userDto)
        {
            try
            {
                _logService.Create("entered registration controller method");
                return Ok(_accountService.Register(userDto));
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

        [HttpGet]
        public ActionResult Ping()
        {
            return Ok();
        }
    }
}
