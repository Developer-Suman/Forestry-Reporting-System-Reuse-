using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class FinancialStatementDTO
    {
        public int FinancialStatementId { get; set; }
        [Required]
        public string PreviousMonthIncome { get; set; } = string.Empty;
        [Required]
        public string CurrentMonthIncome { get; set; } = string.Empty;
        [Required]
        public string TotalIncome { get;set; } = string.Empty;
        [Required]
        public string PreviousMonthExpenses { get; set; } = string.Empty;
        [Required] 
        public string CurrentMonthExpenses { get; set; } = string.Empty;
        [Required] 
        public string Totalexpenses { get; set; } = string.Empty;
        [Required]
        public string RemainingCashAmount { get; set; } = string.Empty;
        [Required]
        public string RemainingBankAmount { get; set; } = string.Empty;
        [Required]  
        public string TotalRemaining { get; set; }= string.Empty;
        [Required]
        public string ActualRemainingAmountInBank { get; set; } = string.Empty;
        [Required]
        public string DifferenceBetweenActualAmountAndRemainingAmount { get; set; } = string.Empty;
        public string? ReasaonForDifference { get; set; }

        public int BranchId { get;set;} 
        public string? Branch { get; set; }

        public int MonthId { get; set; }
        public string Month { get; set; }
        [Required]
        public int FiscalYearId { get; set; }
        public string? FiscalYear { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public string? ApplicantName { get; set; }
        public string? ApplicationNepaliDate { get; set; }
        public DateTime? ApplicationEnglishDate { get;set; }
        public string? VerifierName { get; set; }
        public string? VerifiedNepaliDate { get; set; }
        public DateTime? VerifiedEnglishDate { get; set; }
        public string? NepaliDate { get; set; }
        public DateTime? EnglishDate { get; set; }
        public int? ApplicantDesignationId { get; set; }
        public string ApplicantDesignationName { get; set;}
        public int? VerifierDesignationId { get; set; }
        public string? VerifierDesignationName { get; set; }

        public FinancialStatementDTO ToFinancialStatementDTO(FinancialStatement financialStatement)
        {
            FinancialStatementDTO financialStatementDTO = new()
            {
                FinancialStatementId = financialStatement.FinancialStatementId,
                PreviousMonthIncome = financialStatement.PreviousMonthIncome,
                CurrentMonthIncome = financialStatement.CurrentMonthIncome,
                TotalIncome = financialStatement.TotalIncome,
                PreviousMonthExpenses = financialStatement.PreviousMonthExpenses,
                CurrentMonthExpenses = financialStatement.CurrentMonthExpenses,
                Totalexpenses = financialStatement.Totalexpenses,
                RemainingCashAmount = financialStatement.RemainingCashAmount,
                RemainingBankAmount = financialStatement.RemainingBankAmount,
                TotalRemaining = financialStatement.TotalRemaining,
                ActualRemainingAmountInBank = financialStatement.ActualRemainingAmountInBank,
                DifferenceBetweenActualAmountAndRemainingAmount = financialStatement.DifferenceBetweenActualAmountAndRemainingAmount,
                BranchId = financialStatement.BranchId,
                MonthId = financialStatement.MonthId,
                FiscalYearId = financialStatement.FiscalYearId,
                CreatedBy = financialStatement.CreatedBy,
                UpdatedBy = financialStatement.UpdatedBy,
                IsActive = financialStatement.IsActive,
                ReasaonForDifference = financialStatement.ReasaonForDifference,
                ApplicantName = financialStatement.ApplicantName,
                ApplicationNepaliDate = financialStatement.ApplicationNepaliDate,
                ApplicationEnglishDate = financialStatement.ApplicationEnglishDate,
                VerifierName = financialStatement.VerifierName,
                VerifiedNepaliDate = financialStatement.VerifiedNepaliDate,
                VerifiedEnglishDate = financialStatement.VerifiedEnglishDate,
                NepaliDate = financialStatement.NepaliDate,
                EnglishDate = financialStatement.EnglishDate,
                ApplicantDesignationId = financialStatement.ApplicantDesignationId,
                VerifierDesignationId = financialStatement.VerifierDesignationId
            };
            return financialStatementDTO;
        }

        public FinancialStatement ToFinanicailStatement(FinancialStatementDTO financialStatementDTO )
        {
            FinancialStatement financialStatement = new()
            {
                FinancialStatementId = financialStatementDTO.FinancialStatementId,
                PreviousMonthIncome = financialStatementDTO.PreviousMonthIncome,
                CurrentMonthIncome = financialStatementDTO.CurrentMonthIncome,
                TotalIncome = financialStatementDTO.TotalIncome,
                PreviousMonthExpenses = financialStatementDTO.PreviousMonthExpenses,
                CurrentMonthExpenses = financialStatementDTO.CurrentMonthExpenses,
                Totalexpenses = financialStatementDTO.Totalexpenses,
                RemainingCashAmount = financialStatementDTO.RemainingCashAmount,
                RemainingBankAmount = financialStatementDTO.RemainingBankAmount,
                TotalRemaining = financialStatementDTO.TotalRemaining,
                ActualRemainingAmountInBank = financialStatementDTO.ActualRemainingAmountInBank,
                DifferenceBetweenActualAmountAndRemainingAmount = financialStatementDTO.DifferenceBetweenActualAmountAndRemainingAmount,
                BranchId = financialStatementDTO.BranchId,
                MonthId = financialStatementDTO.MonthId,
                FiscalYearId = financialStatementDTO.FiscalYearId,
                CreatedBy = financialStatementDTO.CreatedBy,
                UpdatedBy = financialStatementDTO.UpdatedBy,
                IsActive = financialStatementDTO.IsActive,
                ReasaonForDifference = financialStatementDTO.ReasaonForDifference,
                ApplicantName = financialStatementDTO.ApplicantName,
                ApplicationNepaliDate = financialStatementDTO.ApplicationNepaliDate,
                ApplicationEnglishDate = financialStatementDTO.ApplicationEnglishDate,
                VerifierName = financialStatementDTO.VerifierName,
                VerifiedNepaliDate = financialStatementDTO.VerifiedNepaliDate,
                VerifiedEnglishDate = financialStatementDTO.VerifiedEnglishDate,
                NepaliDate = financialStatementDTO.NepaliDate,
                EnglishDate = financialStatementDTO.EnglishDate,
                ApplicantDesignationId = financialStatementDTO.ApplicantDesignationId,
                VerifierDesignationId = financialStatementDTO.VerifierDesignationId

            };
            return financialStatement;
        }

    }
}
