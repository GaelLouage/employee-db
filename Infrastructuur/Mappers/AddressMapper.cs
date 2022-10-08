using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Mappers
{
    public static class AddressMapper
    {
        public static AddressResultDto AddressesMapper(this AddressEntity address)
        {
            return new AddressResultDto()
            {
                Id = address.Id,
                AddressLine = address.AddressLine
            };
        }
    }
}
