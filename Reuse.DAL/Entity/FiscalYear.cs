using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class FiscalYear
    {
        public int FiscalYearId { get; set; }
        public string FyName { get; set; }= string.Empty;
        public DateTime StartDt { get; set; }
        public DateTime EndDt { get; set; }
    }
}
