using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class FinancialStatement
    {
        public int FinancialStatementId { get; set; }
        public string PreviousMonthIncome { get; set; } = string.Empty;
        public string CurrentMonthIncome { get; set; } = string.Empty;
        public string TotalIncome { get; set; } = string.Empty;
        public string PreviousMonthExpenses { get; set; } = string.Empty;
        public string CurrentMonthExpenses { get; set; } = string.Empty;
        public string Totalexpenses { get; set; } = string.Empty;
        public string RemainingCashAmount { get; set; } = string.Empty;
        public string RemainingBankAmount { get; set; } = string.Empty;
        public string TotalRemaining { get; set; } = string.Empty;
        public string ActualRemainingAmountInBank { get; set; } = string.Empty;
        public string DifferenceBetweenActualAmountAndRemainingAmount { get; set; } = string.Empty;
        public string? ReasaonForDifference { get; set; }
        public virtual Branch? Branch { get; set; }
        public int BranchId { get; set; }
        public virtual Month? Month { get; set; }
        public int MonthId { get; set; }
        public virtual FiscalYear? FiscalYear { get; set; }
        public int FiscalYearId { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public string? ApplicantName { get; set; }
        public string? ApplicationNepaliDate { get; set; }
        public DateTime? ApplicationEnglishDate { get; set; }
        public string? VerifierName { get; set; }
        public string? VerifiedNepaliDate { get; set; }
        public DateTime? VerifiedEnglishDate { get; set; }
        public string? NepaliDate { get; set; }
        public DateTime? EnglishDate { get; set; }
        public int? ApplicantDesignationId { get; set; }
        public int? VerifierDesignationId { get; set; }
    }
}
