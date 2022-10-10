using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.DTO
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; internal set; }
    }
}
