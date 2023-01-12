using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Models
{
    public class Log
    {
        public string Id { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
    }
}
