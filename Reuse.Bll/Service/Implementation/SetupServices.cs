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
            try
            {
                expenseType.IsActive = true;
                var expensesType = await _expensesType.AddAsync(new ExpenseTypeDTO().ToExpenseType(expenseType));
                return new ServiceResult<ExpenseTypeDTO>(true, new ExpenseTypeDTO().ToExpenseTypeDTO(expensesType));
            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TaxRateDTO>> AddTaxRate(TaxRateDTO entity)
        {
            try
            {
                entity.IsActive = true;
                var texRate = await _taxRate.AddAsync(new TaxRateDTO().ToTaxRate(entity));
                return new ServiceResult<TaxRateDTO>(true,new TaxRateDTO().ToTaxRateDTO(texRate));

            }
            catch(Exception ex)
            {
                return new ServiceResult<TaxRateDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<WoodBusinessDTO>> AddWoodBusiness(WoodBusinessDTO entity)
        {
            try
            {
                entity.IsActive = true;
                var woodBusiness = await _woodBusiness.AddAsync(new WoodBusinessDTO().ToWoodBusiness(entity));
                return new ServiceResult<WoodBusinessDTO>(true, new WoodBusinessDTO().ToWoodBusinessDTO(woodBusiness));

            }
            catch(Exception ex)
            {
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<ExpensesHintDTO>> DeleteExpensesHint(int Id)
        {
            try
            {
               var expenseHint = await _expensesHint.GetByIdAsync(Id);
                if(expenseHint != null)
                {
                    await _expensesHint.DeleteAsync(expenseHint);
                    return new ServiceResult<ExpensesHintDTO>(true, new ExpensesHintDTO().ToExpensesHindDTO(expenseHint));
                }
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { "Bad Request" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] {ex.Message });
            }
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> DeleteExpenseType(int Id)
        {
            try
            {
                var expenseType = await _expensesType.GetByIdAsync(Id);
                if(expenseType != null )
                {
                    await _expensesType.DeleteAsync(expenseType);
                    return new ServiceResult<ExpenseTypeDTO>(true, new ExpenseTypeDTO().ToExpenseTypeDTO(expenseType));
                }
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { "Bad Request" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<TaxRateDTO>> DeleteTaxRate(int Id)
        {
            try
            {
                var taxRate = await _taxRate.GetByIdAsync(Id);
                if(taxRate != null )
                {
                    await _taxRate.DeleteAsync(taxRate);
                    return new ServiceResult<TaxRateDTO>(true, new TaxRateDTO().ToTaxRateDTO(taxRate));
                }
                return new ServiceResult<TaxRateDTO>(false, errors: new[] { "Bad Request" });

            }
            catch (Exception ex)
            {
               return new ServiceResult<TaxRateDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<WoodBusinessDTO>> DeleteWoodBusiness(int Id)
        {
            try
            {
                var woodBusiness = await _woodBusiness.GetByIdAsync(Id);
                if(woodBusiness != null )
                {
                    await _woodBusiness.DeleteAsync(woodBusiness);
                    return new ServiceResult<WoodBusinessDTO>(true, new WoodBusinessDTO().ToWoodBusinessDTO(woodBusiness));
                }
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { "Bad Request" });
            }
            catch(Exception ex)
            {
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<BranchTypeDTO>>> GetAllBranchTypes()
        {
            try
            {
                var branchTypes = await _branchType.GetAllAsync();
                if(branchTypes.Count >0)
                {
                    return new ServiceResult<List<BranchTypeDTO>>(true, new BranchTypeDTO().ToBranchTypeDTOList(branchTypes));

                }
                return new ServiceResult<List<BranchTypeDTO>>(false, errors: new[] { "शाखाको प्रकार फेला परेन" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<BranchTypeDTO>>(false, errors: new[] { ex.Message });

            }
        }

        public async Task<ServiceResult<List<ExpensesHintDTO>>> GetAllExpensesHint(string filter)
        {
            try
            {
                if(filter != null)
                {
                    List<ExpensesHint> expensesHints = new();
                    if(filter.Equals("Active"))
                    {
                        expensesHints = await _expensesHint.WhereAsync(x => x.IsActive);
                    }else if(filter.Equals("Inactive"))
                    {
                        expensesHints = await _expensesHint.WhereAsync(x => !x.IsActive);
                    }
                    else
                    {
                        expensesHints = await _expensesHint.GetAllAsync();
                    }


                    List<ExpensesHintDTO> expensesHintDTOs = new();
                    if(expensesHintDTOs.Count >0)
                    {
                        for(int i=0; i< expensesHints.Count; i++)
                        {
                            expensesHintDTOs.Add(new ExpensesHintDTO()
                            {
                                ExpensesHintId = expensesHints[i].ExpensesHintId,
                                ExpenseHintCode = expensesHints[i].ExpensesHintCode,
                                ExpensesHintName = expensesHints[i].ExpenseHintName,
                                ExpensesTypeId = expensesHints[i].ExpenseTypeId,
                                IsActive = expensesHints[i].IsActive,
                                ExpensesTypeCode = (await _expensesType.GetByIdAsync(expensesHints[i].ExpensesHintId))?.ExpenseTypeCode?? null
                            });
                        }
                        return new ServiceResult<List<ExpensesHintDTO>>(true, expensesHintDTOs);
                    }
                    return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] { "कुनै डाटा फेला परेन !" });
                }

                return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] { "Something Went Wrong" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<ExpensesHintDTO>> GetAllExpensesHintById(int ExpensesHintById)
        {
            try
            {
                var expensesHint = await _expensesHint.GetByIdAsync(ExpensesHintById);
                if(expensesHint != null)
                {
                    return new ServiceResult<ExpensesHintDTO>(true, new ExpensesHintDTO().ToExpensesHindDTO(expensesHint));
                }

                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { "Item not found" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<ExpenseTypeDTO>>> GetAllExpenseType(string filter)
        {
            try
            {
                if(filter != null)
                {
                    if(filter.Equals("Active"))
                    {
                        var expenseType =await _expensesType.WhereAsync(x=>x.IsActive == true);
                        if(expenseType.Count > 0)
                        {
                            return new ServiceResult<List<ExpenseTypeDTO>>(true, new ExpenseTypeDTO().ToExpenseTypeList(expenseType));
                        }
                        return new ServiceResult<List<ExpenseTypeDTO>>(false, errors: new[] { "Data not Found" });
                    }

                    if(filter.Equals("Inactive"))
                    {
                        var expensetype = await _expensesType.WhereAsync(x => x.IsActive == false);
                        if(expensetype.Count > 0)
                        {
                            return new ServiceResult<List<ExpenseTypeDTO>>(true, new ExpenseTypeDTO().ToExpenseTypeList(expensetype));
                        }
                        return new ServiceResult<List<ExpenseTypeDTO>>(false, errors: new[] { "Item not found" });
                    }
                    if(filter.Equals("All"))
                    {
                        var expensestype = await _expensesType.GetAllAsync();
                        if(expensestype.Count > 0)
                        {
                            return new ServiceResult<List<ExpenseTypeDTO>>(true, new ExpenseTypeDTO().ToExpenseTypeList(expensestype));
                        }

                        return new ServiceResult<List<ExpenseTypeDTO>>(false, errors: new[] { "Item not Found" });
                    }
                }
                return new ServiceResult<List<ExpenseTypeDTO>>(false, errors: new[] { "Something went wrong" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<ExpenseTypeDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<TaxRateDTO>>> GetAllTaxRate(string filter)
        {
            try
            {
                if(filter != null)
                {
                    if (filter.Equals("Active"))
                    {
                        var taxRate = await _taxRate.WhereAsync(x => x.IsActive == true);
                        if(taxRate.Count > 0)
                        {
                            return new ServiceResult<List<TaxRateDTO>>(true, new TaxRateDTO().ToTaxRateDTOList(taxRate));
                        }
                        return new ServiceResult<List<TaxRateDTO>>(false, errors: new[] { "Item not found" });
                    }

                    if(filter.Equals("Inactive"))
                    {
                        var taxRate = await _taxRate.WhereAsync(x => x.IsActive == false);
                        if(taxRate.Count > 0)
                        {
                            return new ServiceResult<List<TaxRateDTO>>(true, new TaxRateDTO().ToTaxRateDTOList(taxRate));
                        }
                        return new ServiceResult<List<TaxRateDTO>>(false, errors: new[] { "Item not found" });
                    }

                    if(filter.Equals("All"))
                    {
                        var taxRate = await _taxRate.GetAllAsync();
                        if(taxRate.Count > 0)
                        {
                            return new ServiceResult<List<TaxRateDTO>>(true, new TaxRateDTO().ToTaxRateDTOList(taxRate));
                        }
                        return new ServiceResult<List<TaxRateDTO>>(false, errors: new[] { "Item not found" });
                    }

                }
                return new ServiceResult<List<TaxRateDTO>>(false, errors: new[] { "Something went wrong" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<TaxRateDTO>>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<List<WoodBusinessDTO>>> GetAllWoodBusiness(string filter)
        {
            try
            {
                if(filter != null)
                {
                    if(filter.Equals("Active"))
                    {
                        var woodBusiness = await _woodBusiness.WhereAsync(x => x.IsActive == true);
                        if(woodBusiness.Count > 0)
                        {
                            return new ServiceResult<List<WoodBusinessDTO>>(true, new WoodBusinessDTO().ToWoodBusinessDTOList(woodBusiness));
                        }
                        return new ServiceResult<List<WoodBusinessDTO>>(false, errors: new[] { "Item not found" });
                    }


                    if(filter.Equals("Inactive"))
                    {
                        var woodBusiness = await _woodBusiness.WhereAsync(x => x.IsActive == false);
                        if(woodBusiness.Count > 0)
                        {
                            return new ServiceResult<List<WoodBusinessDTO>>(true, new WoodBusinessDTO().ToWoodBusinessDTOList(woodBusiness));
                        }
                        return new ServiceResult<List<WoodBusinessDTO>>(false, errors: new[] { "Item not found" });
                    }


                    if(filter.Equals("All"))

                    {
                        var woodBusiness = await _woodBusiness.GetAllAsync();
                        if(woodBusiness.Count > 0)
                        {
                            return new ServiceResult<List<WoodBusinessDTO>>(true, new WoodBusinessDTO().ToWoodBusinessDTOList(woodBusiness));
                        }
                        return new ServiceResult<List<WoodBusinessDTO>>(false, errors: new[] { "Item not found" });
                    }

                }
                return new ServiceResult<List<WoodBusinessDTO>>(false, errors: new[] { "Something Went Wrong" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<WoodBusinessDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<BranchTypeDTO>>> GetBranchTypes()
        {
            try
            {
                var branchTypes = await _branchType.WhereAsync(x => x.BranchTypeId != 1);
                if(branchTypes.Count > 0)
                {
                    return new ServiceResult<List<BranchTypeDTO>>(true, new BranchTypeDTO().ToBranchTypeDTOList(branchTypes));
                }
                return new ServiceResult<List<BranchTypeDTO>>(false, errors: new[] { "शाखाको प्रकार फेला परेन" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<BranchTypeDTO>>(false, errors: new[] {ex.Message});
            }




        }

        public async Task<ServiceResult<List<ExpensesHintDTO>>> GetExpensesHintByExpensesTypeId(int ExpensesTypeId)
        {
            try
            {
                if(!await _expensesType.CheckIdIfExists(x=>x.ExpenseTypeId == ExpensesTypeId))
                {
                    List<ExpensesHint> expensesHints = await _expensesHint.WhereAsync(x => x.ExpenseTypeId == ExpensesTypeId && x.IsActive);
                    List<ExpensesHintDTO> expensesHintDTOs = new();

                    if(expensesHints.Count > 0)
                    {
                        for(int i=0; i<expensesHints.Count; i++)
                        {
                            expensesHintDTOs.Add(new ExpensesHintDTO()
                            {
                                ExpensesHintId = expensesHints[i].ExpenseTypeId,
                                ExpensesHintName = expensesHints[i].ExpenseHintName,
                                ExpenseHintCode = expensesHints[i].ExpensesHintCode,
                                ExpensesTypeCode= (await _expensesType.GetByIdAsync(expensesHints[i].ExpenseTypeId))?.ExpenseTypeCode??null,
                                IsActive= expensesHints[i].IsActive

                            });
                        }
                        return new ServiceResult<List<ExpensesHintDTO>>(true, expensesHintDTOs);
                    }

                    return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] { "Item not Found" });
                }

                return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] { "कुनै डाटा फेला परेन !" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<ExpensesHintDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<ExpensesHintDTO>> GetExpensesHintById(int Id)
        {
            try
            {
                var expenseshint = await _expensesHint.GetByIdAsync(Id);
                if(expenseshint != null)
                {
                    return new ServiceResult<ExpensesHintDTO>(true, new ExpensesHintDTO().ToExpensesHindDTO(expenseshint));
                }

                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { "Item not found" });
            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> GetExpensesTypeById(int Id)
        {
            try
            {
                var expenseType = await _expensesType.GetByIdAsync(Id);
                if(expenseType != null )
                {
                    return new ServiceResult<ExpenseTypeDTO>(true,new ExpenseTypeDTO().ToExpenseTypeDTO(expenseType));
                }
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { "Item not found" });

            }catch(Exception ex)
            {
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<TaxRateDTO>> GetTaxRateById(int Id)
        {
            try
            {
                var taxRate = await _taxRate.GetByIdAsync(Id);
                if(taxRate != null )
                {
                    return new ServiceResult<TaxRateDTO>(true, new TaxRateDTO().ToTaxRateDTO(taxRate));
                }
                return new ServiceResult<TaxRateDTO>(false, errors: new[] { "Item not found" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<TaxRateDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<WoodBusinessDTO>> GetWoodBusinessById(int Id)
        {
            try
            {
                var woodBusiness = await _woodBusiness.GetByIdAsync(Id);
                if(woodBusiness != null )
                {
                    return new ServiceResult<WoodBusinessDTO>(true, new WoodBusinessDTO().ToWoodBusinessDTO(woodBusiness));
                }
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { "Item not found" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<ExpensesHintDTO>> UpdateExpensesHint(ExpensesHintDTO entity)
        {
            try
            {
                if(!await _expensesHint.CheckIdIfExists(x=>x.ExpensesHintId == entity.ExpensesHintId))
                {
                    return new ServiceResult<ExpensesHintDTO>(false,errors: new[] { "Invalid ExpesnsesHint Type!!" });
                }

                if(await _expensesHint.CheckIdIfExists(x=>x.ExpensesHintId == entity.ExpensesHintId))
                {
                    var expenseHint = await _expensesHint.UpdateAsync(new ExpensesHintDTO().ToExpenseHint(entity));
                    return new ServiceResult<ExpensesHintDTO>(true, new ExpensesHintDTO().ToExpensesHindDTO(expenseHint));
                }

                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { "Bad Request" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpensesHintDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<ExpenseTypeDTO>> UpdateExpensesType(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                if(!await _expensesType.CheckIdIfExists(x=>x.ExpenseTypeId== expenseTypeDTO.ExpenseTypeId))
                {
                    return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { "Invalid Expenses Type" });
                }

                if(await _expensesType.CheckIdIfExists(x=>x.ExpenseTypeId ==  expenseTypeDTO.ExpenseTypeId))
                {
                    var expensesType = await _expensesType.UpdateAsync(new ExpenseTypeDTO().ToExpenseType(expenseTypeDTO));
                    return new ServiceResult<ExpenseTypeDTO>(true, new ExpenseTypeDTO().ToExpenseTypeDTO(expensesType));
                }
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { "Bad Request" });
            }
            catch(Exception ex)
            {
                return new ServiceResult<ExpenseTypeDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<TaxRateDTO>> UpdateTaskRate(TaxRateDTO entity)
        {
            try
            {
                if (!await _taxRate.CheckIdIfExists(x => x.TaxRateId == entity.TaxRateId))
                {
                    return new ServiceResult<TaxRateDTO>(false, errors: new[] { "Invalid TaxRate" });
                }

                if(await _taxRate.CheckIdIfExists(x=>x.TaxRateId != entity.TaxRateId))
                {
                    var taxRate = await _taxRate.UpdateAsync(new TaxRateDTO().ToTaxRate(entity));
                    return new ServiceResult<TaxRateDTO>(true, new TaxRateDTO().ToTaxRateDTO(taxRate));
                }

                return new ServiceResult<TaxRateDTO>(false, errors: new[] { "Bad Request" });


            }
            catch (Exception ex)
            {
                return new ServiceResult<TaxRateDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<WoodBusinessDTO>> UpdateWoodBusiness(WoodBusinessDTO entity)
        {
            try
            {
                if(!await _woodBusiness.CheckIdIfExists(x=>x.WoodBusinessId == entity.WoodBusinessId))
                {
                    return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { "Invalid woodBusiness" });
                }
                if(await _woodBusiness.CheckIdIfExists(x=>x.WoodBusinessId == entity.WoodBusinessId))
                {
                    var woodBusiness = await _woodBusiness.UpdateAsync(new WoodBusinessDTO().ToWoodBusiness(entity));
                    return new ServiceResult<WoodBusinessDTO>(true, new WoodBusinessDTO().ToWoodBusinessDTO(woodBusiness));
                }

                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { "Bad Request" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<WoodBusinessDTO>(false, errors: new[] { ex.Message });
            }
        }
    }
}
