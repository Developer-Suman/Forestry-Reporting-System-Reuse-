using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class BranchType
    {
        public int BranchTypeId { get; set; }
        public string BranchTypeNameInEnglish { get; set; } = string.Empty;
        public string BranchTypeNameInNepali { get; set;} = string.Empty;
    }
}
