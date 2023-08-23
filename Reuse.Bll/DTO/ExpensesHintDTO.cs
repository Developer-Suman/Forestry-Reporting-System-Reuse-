using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class ExpensesHintDTO
    {
        public int ExpensesHintId { get; set; }
        [Required]
        public string ExpensesHintName { get; set; } = string.Empty;
        [Required]
        public string ExpenseHintCode { get; set; } = string.Empty;
        [Required]
        public int ExpensesTypeId { get; set; }
        public bool IsActive { get; set; }
        public string ExpensesTypeCode { get;set; } = string.Empty;

        public ExpensesHintDTO ToExpensesHindDTO(ExpensesHint expensesHint)
        {
            ExpensesHintDTO expensesHintDTO = new ExpensesHintDTO()
            {
                ExpensesHintId = expensesHint.ExpensesHintId,
                ExpensesHintName = expensesHint.ExpenseHintName,
                ExpenseHintCode = expensesHint.ExpensesHintCode,
                ExpensesTypeId = expensesHint.ExpenseTypeId,
                IsActive = expensesHint.IsActive
            };
            return expensesHintDTO;

        }

        public ExpensesHint ToExpenseHint(ExpensesHintDTO expensesHintdto)
        {
            ExpensesHint expensesHint1 = new ExpensesHint()
            {
                ExpensesHintId = expensesHintdto.ExpensesHintId,
                ExpensesHintCode = expensesHintdto.ExpenseHintCode,
                ExpenseHintName = expensesHintdto .ExpensesHintName,
                ExpenseTypeId = expensesHintdto.ExpensesTypeId,
                IsActive = expensesHintdto .IsActive
            };

            return expensesHint1;
        }

        public List<ExpensesHintDTO> ToExpenseHintDTOList(List<ExpensesHint> expensesHints)
        {
            List<ExpensesHintDTO> expenseHintDTOs = new();
            for(int i=0; i< expensesHints.Count(); i++)
            {
                expenseHintDTOs.Add(new ExpensesHintDTO().ToExpensesHindDTO(expensesHints[i]));
            }

            return expenseHintDTOs;
        }
    }
}
