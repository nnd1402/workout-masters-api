using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.API.Models;
using WorkoutTracker.API.Services;
using WorkoutTracker.Domain.DTO;
using WorkoutTracker.Domain.Repository;

namespace WorkoutTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        public AccountController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [AllowAnonymous]
        [HttpPost("create")]     
        public async Task<ActionResult<UserDto>> Register([FromBody] User user)
        {
            try
            {
                return Ok(await _accountRepo.Register(user.UserName, user.Password));
            }
            catch (Exception e)
            {
                return BadRequest("Could not finish registration");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] User user)
        {
            try
            {
                return Ok(await _accountRepo.Login(user.UserName, user.Password));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            return Ok(await _accountRepo.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email)));
        }
    }
}
