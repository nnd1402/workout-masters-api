using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutMasters.Domain.DTO.UserDTO
{
    public class SendEmailDTO
    {
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Username is not valid. It should be in standard E-mail form")]
        public string UserName { get; set; }
    }
}
