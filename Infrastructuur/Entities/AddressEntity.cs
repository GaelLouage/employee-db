using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Entities
{
    public  class AddressEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("addressLine")]
        public string AddressLine { get; set; }
    }
}
