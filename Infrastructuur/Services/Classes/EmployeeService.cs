using Infrastructuur.Services.Interfaces;
using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructuur.Database;
using Infrastructuur.Dtos;
using Infrastructuur.Mappers;

namespace Infrastructuur.Services.Classes
{
    public sealed class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeService(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<EmployeeResultDto> CreateEmployeeAsync(EmployeeEntity entity)
        {
            var employeeDto = new EmployeeResultDto();
            var addresses = (await _employeeDbContext.GetAllAddressesAsync()).Any(x => x.Id == entity.AddressId);
            if(addresses == false)
            {
                employeeDto.Errors.Add("Ivanlid address id, this address is not available!");
                return employeeDto;
            }
            var createdEmployee =  await _employeeDbContext.CreateEmployee(entity);
            employeeDto.Name = createdEmployee.Name;
            employeeDto.IdNumber = createdEmployee.IdNumber;
            employeeDto.AddressId = createdEmployee.AddressId;

            
            return employeeDto;
        }

        public async Task<EmployeeResultDto> DeleteEmployeeAsync(EmployeeEntity entity)
        {
            var employeeDto = new EmployeeResultDto();
        
            await _employeeDbContext.DeleteEmployeeAsync(entity);
            return entity.EmployeeMapper(employeeDto.Errors);
        }

        public async Task<EmployeeEntity> GetEmployeeByIdAsync(int id) =>
            (await _employeeDbContext.GetAllEmployeesAsync()).FirstOrDefault(x => x.IdNumber == id);

        public async Task<List<EmployeeEntity>> GetEmployeesAsync() =>
            await _employeeDbContext.GetAllEmployeesAsync() ?? new List<EmployeeEntity>();

        public async Task<EmployeeResultDto> UpdateEmployeeAsync(EmployeeEntity entity)
        {
            return await _employeeDbContext.UpdateEmployeeAsync(entity);
        }
        //get all employees that has same address
        public async Task<List<EmployeeWithAddress>> GetAllEmployeesWithSameAddress(int addressId)
        {
            var allEmployees = await _employeeDbContext.GetAllEmployeesAsync();
            var allAddresses = await _employeeDbContext.GetAllAddressesAsync();
            var employees = allEmployees.ToList()
                .Join(allAddresses.ToList(),
                 e => e.AddressId,
                 a => a.Id,
                 (e, a) => new EmployeeWithAddress
                 {
                     Id = e.Id,
                     IdNumber = e.IdNumber,
                     Name = e.Name,
                     AddressId = e.AddressId,
                     AddressLine = a.AddressLine,
                     IdOfAddress = a.Id
                 }).Where(x => x.AddressId == addressId);
            return employees.ToList();
        }
    }
}
