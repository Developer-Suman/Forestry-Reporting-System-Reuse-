using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
