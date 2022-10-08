using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Dtos
{
    public class EmployeeResultDto : EmployeeDto
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}
