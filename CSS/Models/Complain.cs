using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSS.Models
{
    public class Complain:Entity
    {
        public string Phone { get; set; }
        public int TaxPayerId { get; set; }
        public TaxPayer TaxPayer { get; set; }
        public int ComplainTypeId { get; set; }
        public ComplainType ComplainType { get; set; }
        public string ReportedIssue  { get; set; }
        public string?  ConfirmedIssue { get; set; }
        public string?  ImplementedFixes { get; set; }
        public string? UsedSpareparts { get; set; }
        public string  Status { get; set; }

    }
}
