using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class ExpensesHint
    {
        public int ExpensesHintId { get; set; }
        public string ExpensesHintCode { get; set; } = string.Empty;
        public string ExpenseHintName { get; set; } = string.Empty;

        public virtual ExpenseType? ExpenseType { get; set; }
        public int ExpenseTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
