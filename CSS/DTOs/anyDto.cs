using CSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.DTOs
{
    public class AnyDto
    {
        public int Id { get; set; }
        public int CreatedByID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        public int Tin { get; set; }
        public string SerialNo { get; set; }
        public string Phone { get; set; }
        public int DeviceId { get; set; }
        public Device? Device { get; set; }
        public virtual List<Complain>? Complains { get; set; }
    }
}
