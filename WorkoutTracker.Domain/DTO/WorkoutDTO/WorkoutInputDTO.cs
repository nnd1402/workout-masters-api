using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WorkoutTracker.Domain.DTO.WorkoutDTO
{
    public class WorkoutInputDTO
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
