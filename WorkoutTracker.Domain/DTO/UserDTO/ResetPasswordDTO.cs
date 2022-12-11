using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.DTO.UserDTO
{
    public class ResetPasswordDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
