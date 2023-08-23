using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictNameNepali { get; set; } = string.Empty;
        public string DistrictNameEnglish { get; set; } = string.Empty;

        public virtual Province? Province { get; set; }
        public int ProvinceId { get; set; }

    }
}
