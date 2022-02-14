using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public int CreatedByID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}
