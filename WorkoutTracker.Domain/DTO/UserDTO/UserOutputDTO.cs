using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.DTO.UserDTOs
{
    public class UserOutputDTO
    {
        public string UserName { get; set; }
        public string Token { get; internal set; }
    }
}
