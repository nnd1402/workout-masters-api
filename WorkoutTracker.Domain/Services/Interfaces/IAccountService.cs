using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.API.Models;
using WorkoutTracker.Domain.DTO.UserDTOs;

namespace WorkoutTracker.Domain.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserOutputDTO?> Register(UserInputDTO userDto);
        Task<UserOutputDTO?> Login(UserInputDTO userDto);
        Task<UserOutputDTO?> GetCurrentUser(string email);
        UserOutputDTO CreateUserObject(AppUser user);
    }
}
