using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class ExpenseTypeDTO
    {
        public int ExpenseTypeId { get; set; }
        [Required]
        public string ExpenseTypeCode { get; set; }=  string.Empty;
        [Required]
        public string ExpenseTypeName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ExpenseTypeDTO ToExpenseTypeDTO(ExpenseType expenseType)
        {
            ExpenseTypeDTO expenseTypeDTO = new()
            {
                ExpenseTypeId = expenseType.ExpenseTypeId,
                ExpenseTypeCode = expenseType.ExpenseTypeCode,
                ExpenseTypeName = expenseType.ExpenseTypeName,
                IsActive = expenseType.IsActive,
            };
            return expenseTypeDTO;
        }

        public ExpenseType ToExpenseType(ExpenseTypeDTO expenseTypeDTO)
        {
            ExpenseType expenseType = new()
            {
                ExpenseTypeId = expenseTypeDTO.ExpenseTypeId,
                ExpenseTypeCode = expenseTypeDTO.ExpenseTypeCode,
                ExpenseTypeName = expenseTypeDTO.ExpenseTypeName,
                IsActive = expenseTypeDTO.IsActive,
            };

            return expenseType;
        }

        public List<ExpenseTypeDTO> ToExpenseTypeList(List<ExpenseType> expenseTypes)
        {
            List<ExpenseTypeDTO> expenseTypeDTOs = new();
            for(int i = 0; i<expenseTypes.Count; i++)
            {
                expenseTypeDTOs.Add(new ExpenseTypeDTO().ToExpenseTypeDTO(expenseTypes[i]));

            }
            return expenseTypeDTOs;
        }
    }
}
