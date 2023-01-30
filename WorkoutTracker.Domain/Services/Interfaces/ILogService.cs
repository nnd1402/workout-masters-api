using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutMasters.Domain.Services.Interfaces
{
    public interface ILogService
    {
        void Create(string message);
    }
}
