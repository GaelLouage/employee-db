using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Mappers
{
    public static class EmployeeMap
    {
        public static EmployeeResultDto EmployeeMapper(this EmployeeEntity entity, List<string> errors)
        {
            return new EmployeeResultDto
            {
                Errors = errors,
                Id = entity.Id,
                Name = entity.Name,
                AddressId = entity.AddressId,
                IdNumber = entity.IdNumber,
            };
        }
    }
}
