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
    public sealed class AddressService : IAddressService
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public  AddressService(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<AddressEntity> CreateAddressAsync(AddressEntity entity)
        {
            return await _employeeDbContext.CreateAddress(entity);
        }

        public async Task<AddressResultDto> DeleteAddressAsync(AddressEntity entity)
        {
            var addressResult = new AddressResultDto();
            var employees = (await _employeeDbContext.GetAllEmployeesAsync()).Any(x => x.AddressId == entity.Id);
            if(employees)
            {
                addressResult.Errors.Add("Cannot delete this user because some users have this address.");
                return addressResult;
            }
             addressResult = (await _employeeDbContext.DeleteAddressAsync(entity)).AddressesMapper();

            return addressResult;
        }
        public async Task<AddressEntity> UpdateAddressAsync(AddressEntity entity)
        {
            return await _employeeDbContext.UpdateAddressAsync(entity);
        }
        public async Task<AddressEntity> GetAddresseByIdAsync(int id) =>
              (await _employeeDbContext.GetAllAddressesAsync()).FirstOrDefault(x => x.Id == id);  
        

        public async Task<List<AddressEntity>> GetAllAddressesAsync() =>
            await _employeeDbContext.GetAllAddressesAsync() ?? new List<AddressEntity>();

      
    }
}
