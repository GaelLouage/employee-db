using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Entities
{
    public sealed class EmployeeEntity
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("id")]
        public int IdNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("addressId")]
        public int AddressId { get; set; }
    }
    public sealed class EmployeeWithAddress : AddressEntity
    {

        public string Id { get; set; }


        public int IdNumber { get; set; }


        public string Name { get; set; }

        public int AddressId { get; set; }

        public string AddressLine { get; set; }
        public int IdOfAddress { get; set; }
    }
}
