using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class ExpenseType
    {
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeCode { get; set; }
        public string ExpenseTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
