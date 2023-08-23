using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class VDC
    {
        public int VDCId { get; set; }
        public string VDCNameInEnglish { get; set; }
        public string VDCNameInNepali { get; set; }
        public virtual District? District { get; set; }
        public int DistrictId { get; set; }
    }
}
