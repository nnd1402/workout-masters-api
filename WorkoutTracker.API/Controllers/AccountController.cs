using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.Domain.DTO.UserDTOs;
using WorkoutTracker.Domain.Exceptions;
using WorkoutTracker.Domain.Repositories.Interfaces;

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
        public async Task<ActionResult<UserOutputDTO>> Register([FromBody] UserInputDTO user)
        {
            try
            {
                return Ok(await _accountRepo.Register(user));
            }
            catch (ValidationException e)
            {
                return BadRequest($"Validation error: {e.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserInputDTO userDto)
        {
            try
            {
                return Ok(await _accountRepo.Login(userDto));
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
            return Ok(await _accountRepo.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email)));
        }
    }
}
