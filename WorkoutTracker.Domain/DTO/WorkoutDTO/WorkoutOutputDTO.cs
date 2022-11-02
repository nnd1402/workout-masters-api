using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.DTO.WorkoutDTO
{
    public class WorkoutOutputDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
    }
}
