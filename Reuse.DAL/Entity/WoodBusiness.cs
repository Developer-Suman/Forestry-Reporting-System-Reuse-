using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class WoodBusiness
    {
        public int WoodBusinessId { get; set; }
        public string WoodBusinessCode { get; set; } = string.Empty;
        public string WoodBusinessName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
