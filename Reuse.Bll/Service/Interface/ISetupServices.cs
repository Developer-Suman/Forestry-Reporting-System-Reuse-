using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface ISetupServices
    {
        #region BranchType
        Task<ServiceResult<List<BranchTypeDTO>>> GetBranchTypes();
        Task<ServiceResult<List<BranchTypeDTO>>> GetAllBranchTypes();
        #endregion

        #region ExpensesType
        Task<ServiceResult<ExpenseTypeDTO>> AddExpensesType(ExpenseTypeDTO expenseType);
        Task<ServiceResult<List<ExpenseTypeDTO>>> GetAllExpenseType(string filter);
        Task<ServiceResult<ExpenseTypeDTO>> GetExpensesTypeById(int Id);
        Task<ServiceResult<ExpenseTypeDTO>> UpdateExpensesType(ExpenseTypeDTO expenseTypeDTO);
        Task<ServiceResult<ExpenseTypeDTO>> DeleteExpenseType(int Id);
        #endregion

        #region ExpensesHint
        Task<ServiceResult<ExpensesHintDTO>> AddExpensesHint(ExpensesHintDTO entity);
        Task<ServiceResult<ExpensesHintDTO>> GetAllExpensesHintById(int ExpensesHintById);
        Task<ServiceResult<List<ExpensesHintDTO>>> GetAllExpensesHint(string filter);
        Task<ServiceResult<ExpensesHintDTO>> GetExpensesHintById(int Id);
        Task<ServiceResult<ExpensesHintDTO>> UpdateExpensesHint(ExpensesHintDTO entity);
        Task<ServiceResult<ExpensesHintDTO>> DeleteExpensesHint(int Id);
        Task<ServiceResult<List<ExpensesHintDTO>>> GetExpensesHintByExpensesTypeId(int ExpensesTypeId);

        #endregion

        #region woodbusiness

        Task<ServiceResult<WoodBusinessDTO>> AddWoodBusiness(WoodBusinessDTO entity);
        Task<ServiceResult<List<WoodBusinessDTO>>> GetAllWoodBusiness(string filter);
        Task<ServiceResult<WoodBusinessDTO>> GetWoodBusinessById(int Id);
        Task<ServiceResult<WoodBusinessDTO>> UpdateWoodBusiness(WoodBusinessDTO entity);
        Task<ServiceResult<WoodBusinessDTO>> DeleteWoodBusiness(int Id);

        #endregion

        #region TaxRate
        Task<ServiceResult<TaxRateDTO>> AddTaxRate(TaxRateDTO entity);
        Task<ServiceResult<List<TaxRateDTO>>> GetAllTaxRate(string filter);
        Task<ServiceResult<TaxRateDTO>> GetTaxRateById(int Id);
        Task<ServiceResult<TaxRateDTO>> UpdateTaskRate(TaxRateDTO entity);
        Task<ServiceResult<TaxRateDTO>> DeleteTaxRate(int Id);
        #endregion
    }
}
