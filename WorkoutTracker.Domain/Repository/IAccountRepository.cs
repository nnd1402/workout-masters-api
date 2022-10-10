using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Domain.DTO;

namespace WorkoutTracker.Domain.Repository
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(string username, string password);
        Task<UserDto> Login(string username, string password);
        Task<UserDto> GetCurrentUser(string email);
    }
}
