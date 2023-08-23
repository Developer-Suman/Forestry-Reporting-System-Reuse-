using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class Province
    {
        public int ProvinceId { get;set; }
        public string ProvinceNameInEnglish { get; set; } = string.Empty;
        public string ProvinceNameInNepali { get; set; } = string.Empty;
    }
}
