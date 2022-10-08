using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetEmployeesAsync();
        Task<EmployeeEntity> GetEmployeeByIdAsync(int id);
        Task<EmployeeResultDto> CreateEmployeeAsync(EmployeeEntity entity);
        Task<EmployeeResultDto> UpdateEmployeeAsync(EmployeeEntity entity);
        Task<EmployeeResultDto> DeleteEmployeeAsync(EmployeeEntity entity);
        //get all Employees with same address
        Task<List<EmployeeWithAddress>> GetAllEmployeesWithSameAddress(int addressId);
    }
}
