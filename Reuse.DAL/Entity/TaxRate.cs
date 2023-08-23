using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class TaxRate
    {
        public int TaxRateId { get; set; }
        public string TaxRateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
