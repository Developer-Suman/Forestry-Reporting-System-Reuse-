using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class Municipality
    {
        public int MunicipalityId { get; set; }
        public string MunicipalityNameInNepali { get; set; }
        public string MunicipalityNameInEnglish { get; set; }
        public virtual District? District { get; set; }
        public int DistrictId { get; set; }
    }
}
