using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.Models
{
    public class Device:Entity
    {
        public string DeviceModel { get; set; }
        public virtual List<TaxPayer> TaxPayers { get; set; }
    }
}
