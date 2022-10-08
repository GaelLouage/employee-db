using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Services.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressEntity>> GetAllAddressesAsync();
        Task<AddressEntity> GetAddresseByIdAsync(int id);

        Task<AddressEntity> CreateAddressAsync(AddressEntity entity);
        Task<AddressEntity> UpdateAddressAsync(AddressEntity entity);
        Task<AddressResultDto> DeleteAddressAsync(AddressEntity entity);
      
    }
}
