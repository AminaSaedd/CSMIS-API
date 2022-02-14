using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSS.Models
{
    public class User:Entity
    {
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
