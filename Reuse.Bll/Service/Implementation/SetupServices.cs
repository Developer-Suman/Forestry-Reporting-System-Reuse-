using Reuse.Bll.DTO;
using Reuse.Bll.Repository.Interface;
using Reuse.Bll.Service.Interface;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Implementation
{
    public class SetupServices : ISetupServices
    {
        private readonly IRepository<ExpenseType> _expensesType;
        private readonly IRepository<ExpensesHint> _expensesHint;
        private readonly IRepository<WoodBusiness> _woodBusiness;
        private readonly IRepository<TaxRate> _taxRate;
        private readonly IRepository<BranchType> _branchType;

        public SetupServices(IRepository<ExpenseType> expesnsesType, IRepository<ExpensesHint> expesnsesHint, IRepository<WoodBusiness> woodBusiness, IRepository<TaxRate> texRates, IRepository<BranchType> branchType)
        {
            _expensesHint = expesnsesHint;
            _woodBusiness = woodBusiness;
            _taxRate = texRates;
            _expensesType = expesnsesType;
            _branchType = branchType;

        }
        public async Task<ServiceResult<ExpensesHintDTO>> AddExpensesHint(ExpensesHintDTO entity)
        {
            try
            {
                if(await _expensesType.CheckIdIfExists(x=>x.ExpenseTypeId == entity.ExpensesTypeId))
                {
                    entity.IsActive = true;
                    var expesnseHint = await _expensesHint.AddAsync(new ExpensesHintDTO().ToExpenseHint(entity));
                    return new ServiceResult<ExpensesHintDTO>(true, new ExpensesHintDTO().ToExpensesHindDTO(expesnseHint));
                }

                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { "Invalid Expense Type" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> AddExpensesType(ExpenseTypeDTO expenseType)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TaxRateDTO>> AddTaxRate(TaxRateDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<WoodBusinessDTO>> AddWoodBusiness(WoodBusinessDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpensesHintDTO>> DeleteExpensesHint(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> DeleteExpenseType(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TaxRateDTO>> DeleteTaxRate(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<WoodBusinessDTO>> DeleteWoodBusiness(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<BranchTypeDTO>>> GetAllBranchTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<ExpensesHintDTO>>> GetAllExpensesHint(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpensesHintDTO>> GetAllExpensesHintById(int ExpensesHintById)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<ExpenseTypeDTO>>> GetAllExpenseType(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<TaxRateDTO>>> GetAllTaxRate(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<WoodBusinessDTO>>> GetAllWoodBusiness(string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<BranchTypeDTO>>> GetBranchTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<ExpensesHintDTO>>> GetExpensesHintByExpensesTypeId(int ExpensesTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpensesHintDTO>> GetExpensesHintById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> GetExpensesTypeById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TaxRateDTO>> GetTaxRateById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<WoodBusinessDTO>> GetWoodBusinessById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpensesHintDTO>> UpdateExpensesHint(ExpensesHintDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> UpdateExpensesType(ExpenseTypeDTO expenseTypeDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<TaxRateDTO>> UpdateTaskRate(TaxRateDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<WoodBusinessDTO>> UpdateWoodBusiness(WoodBusinessDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
