using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.Models
{
    public class TaxPayer:Entity
    {
        public string  Name  { get; set; }
        public int TIN { get; set; }
        public string SerialNo { get; set; }
        public string Phone { get; set; }
        public int DeviceId { get; set; }
        public Device? Device { get; set; }
        public virtual List<Complain>? Complains { get; set; }
    }
}
