using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Dtos
{
    public  class AddressDto
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
    }
}
