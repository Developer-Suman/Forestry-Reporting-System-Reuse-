using Reuse.Bll.DTO;
using Reuse.Bll.Helpher;
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
    public class FinancialStatementServices : IFinancialStatementServices
    {
        private readonly IRepository<FinancialStatement> _financialStatement;
        private readonly IRepository<Month> _month;
        private readonly IRepository<FiscalYear> _fiscalYear;
        private readonly ITokenServices _tokenService;
        private readonly int _branchId;
        private readonly NepaliDateHelpher _nepaliDateHelpher;

        private readonly string _username;
        private readonly IRepository<Designation> _designation;
        private readonly FiscalYearHelpher _fiscalYearHelpher;
        private readonly UniCodeHelpher _uniCodeHelpher;


        public FinancialStatementServices(IRepository<FinancialStatement> financialStatement, IRepository<Month> month,
            IRepository<FiscalYear> fiscalYear, ITokenServices tokenServices, NepaliDateHelpher nepaliDateHelpher,
            IRepository<Designation> designation, FiscalYearHelpher fiscalYearHelpher, UniCodeHelpher uniCodeHelpher)
        {
            _financialStatement = financialStatement;
            _month = month;
            _fiscalYear = fiscalYear;
            _tokenService = tokenServices;
            _designation = designation;
            _branchId = _tokenService.GetBranchId();
            _nepaliDateHelpher = nepaliDateHelpher;
            _username = _tokenService.GetUsername();
            _designation = designation;
            _fiscalYearHelpher = fiscalYearHelpher;
            _uniCodeHelpher = uniCodeHelpher;

        }

        public async Task<ServiceResult<FinancialStatementDTO>> AddFinancialStatement(FinancialStatementDTO financialStatementDTO)
        {
            try
            {
                var month = await _month.CheckIdIfExists(x => x.MonthId == financialStatementDTO.MonthId);
                var fiscalYear = await _fiscalYear.CheckIdIfExists(x => x.FiscalYearId == financialStatementDTO.FiscalYearId);
                bool doesApplicationDesignationExists = await _designation.CheckIdIfExists(x => x.DesignationId == financialStatementDTO.ApplicantDesignationId);
                bool doesVerifierDesignationExists = await _designation.CheckIdIfExists(x => x.DesignationId == financialStatementDTO.VerifierDesignationId);

                if (!month || !fiscalYear || !doesApplicationDesignationExists || !doesVerifierDesignationExists)
                {
                    var errors = new List<string>();
                    if (!month) errors.Add("महिना फेला परेन");
                    if (!fiscalYear) errors.Add("आर्थिक वर्ष फेला परेन");
                    if (!doesApplicationDesignationExists) errors.Add("पद भेटिएन !");
                    if (!doesVerifierDesignationExists) errors.Add("पद भेटिएन !");

                    return new ServiceResult<FinancialStatementDTO>(false, errors: errors.ToArray());

                }

                financialStatementDTO.ApplicationEnglishDate = _nepaliDateHelpher.ReturnNullOrDate(financialStatementDTO.ApplicationNepaliDate);
                financialStatementDTO.VerifiedEnglishDate = _nepaliDateHelpher.ReturnNullOrDate(financialStatementDTO.VerifiedNepaliDate);
                financialStatementDTO.BranchId = _branchId;
                financialStatementDTO.EnglishDate = DateTime.Now;
                financialStatementDTO.NepaliDate = _nepaliDateHelpher.EngToNep(financialStatementDTO.EnglishDate.Value).ToString();
                financialStatementDTO.CreatedBy = _username;
                financialStatementDTO.IsActive = true;
                financialStatementDTO.PreviousMonthIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.PreviousMonthIncome);
                financialStatementDTO.CurrentMonthIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.CurrentMonthIncome);
                financialStatementDTO.TotalIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.TotalIncome);
                financialStatementDTO.PreviousMonthExpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.PreviousMonthExpenses);
                financialStatementDTO.CurrentMonthExpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.CurrentMonthExpenses);
                financialStatementDTO.Totalexpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.Totalexpenses);
                financialStatementDTO.RemainingCashAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.RemainingCashAmount);
                financialStatementDTO.RemainingBankAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.RemainingBankAmount);
                financialStatementDTO.TotalRemaining = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.TotalRemaining);
                financialStatementDTO.ActualRemainingAmountInBank = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.ActualRemainingAmountInBank);
                financialStatementDTO.DifferenceBetweenActualAmountAndRemainingAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.DifferenceBetweenActualAmountAndRemainingAmount);


                var finanicialStatement = await _financialStatement.AddAsync(new FinancialStatementDTO().ToFinanicailStatement(financialStatementDTO));
                return new ServiceResult<FinancialStatementDTO>(true, new FinancialStatementDTO().ToFinancialStatementDTO(finanicialStatement));

            }
            catch (Exception ex)
            {
                return new ServiceResult<FinancialStatementDTO>(false, errors: new[] { ex.Message });
            }
        }

        public Task<ServiceResult<FinancialStatementDTO>> DeleteFinancialStatement(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<FinancialStatementDTO>>> GetAllFinancialStatement(string filter)
        {
            try
            {
                if (filter != null)
                {
                    List<FinancialStatement> financialStatements = new();
                    int? fiscalYearId = await _fiscalYearHelpher.GetFiscalYearByEnglishDate(DateTime.Now);
                    if (filter.Equals("Active"))
                    {
                        financialStatements = await _financialStatement.WhereAsync(x => x.BranchId == _branchId && x.FiscalYearId == fiscalYearId);
                    }
                    else if(filter.Equals("Inactive"))
                    {
                        financialStatements = await _financialStatement.WhereAsync(x => x.BranchId == _branchId && !x.IsActive && x.FiscalYearId == fiscalYearId);

                    }
                    else
                    {
                        financialStatements = await _financialStatement.WhereAsync(x => x.BranchId == _branchId && x.FiscalYearId == fiscalYearId);
                    }

                    List<FinancialStatementDTO> financialStatementDTOs = new();
                    if(financialStatements.Count > 0)
                    {
                        for(int i=0; i<financialStatements.Count; i++)
                        {
                            financialStatementDTOs.Add(new FinancialStatementDTO()
                            {
                                FinancialStatementId = financialStatements[i].FinancialStatementId,
                                PreviousMonthIncome = financialStatements[i].PreviousMonthIncome,
                                CurrentMonthIncome = financialStatements[i].CurrentMonthIncome,
                                TotalIncome = financialStatements[i].TotalIncome,
                                PreviousMonthExpenses = financialStatements[i].PreviousMonthExpenses,
                                CurrentMonthExpenses = financialStatements[i].CurrentMonthExpenses,
                                Totalexpenses = financialStatements[i].Totalexpenses,
                                RemainingCashAmount = financialStatements[i].RemainingCashAmount,
                                RemainingBankAmount = financialStatements[i].RemainingBankAmount,
                                TotalRemaining = financialStatements[i].TotalRemaining,
                                ActualRemainingAmountInBank = financialStatements[i].ActualRemainingAmountInBank,
                                DifferenceBetweenActualAmountAndRemainingAmount = financialStatements[i].DifferenceBetweenActualAmountAndRemainingAmount,
                                BranchId = financialStatements[i].BranchId,
                                MonthId = financialStatements[i].MonthId,
                                Month = (await _month.GetByIdAsync(financialStatements[i].MonthId))?.MonthName ?? null,
                                FiscalYearId = financialStatements[i].FiscalYearId,
                                FiscalYear = (await _fiscalYear.GetByIdAsync(financialStatements[i].FiscalYearId))?.FyName ?? null,
                                CreatedBy = financialStatements[i].CreatedBy,
                                UpdatedBy = financialStatements[i].UpdatedBy,
                                IsActive = financialStatements[i].IsActive,
                                ReasaonForDifference = financialStatements[i].ReasaonForDifference,
                                ApplicantName = financialStatements[i].ApplicantName,
                                ApplicationEnglishDate = financialStatements[i].ApplicationEnglishDate,
                                ApplicationNepaliDate = financialStatements[i].ApplicationNepaliDate,
                                EnglishDate = financialStatements[i].EnglishDate,
                                NepaliDate = financialStatements[i].NepaliDate,
                                VerifiedEnglishDate = financialStatements[i].VerifiedEnglishDate,
                                VerifiedNepaliDate = financialStatements[i].VerifiedNepaliDate,
                                VerifierName = financialStatements[i].VerifierName,
                                ApplicantDesignationId = financialStatements[i].ApplicantDesignationId,
                                ApplicantDesignationName = (await _designation.GetByIdAsync(financialStatements[i].ApplicantDesignationId.Value))?.DesignationName ?? null,
                                VerifierDesignationId = financialStatements[i].VerifierDesignationId,
                                VerifierDesignationName = (await _designation.GetByIdAsync(financialStatements[i].VerifierDesignationId.Value))?.DesignationName ?? null
                           

                            });
                        }
                        return new ServiceResult<List<FinancialStatementDTO>>(true, financialStatementDTOs);
                    }

                    return new ServiceResult<List<FinancialStatementDTO>>(false, errors: new[] { "कुनै डाटा फेला परेन !" });
                }

                return new ServiceResult<List<FinancialStatementDTO>>(false, errors: new[] { "Something Went wrong" });

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<FinancialStatementDTO>>(true, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<FinancialStatementDTO>> GetFinancialStatementById(int Id)
        {
            try
            {
                var financialStatement = await _financialStatement.GetByIdAsync(Id);
                if(financialStatement != null)
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
                        Month = (await _month.GetByIdAsync(financialStatement.MonthId))?.MonthName ?? null,
                        FiscalYearId = financialStatement.FiscalYearId,
                        FiscalYear = (await _fiscalYear.GetByIdAsync(financialStatement.FiscalYearId))?.FyName ?? null,
                        CreatedBy = financialStatement.CreatedBy,
                        UpdatedBy = financialStatement.UpdatedBy,
                        IsActive = financialStatement.IsActive,
                        ReasaonForDifference = financialStatement.ReasaonForDifference,
                        ApplicantName = financialStatement.ApplicantName,
                        ApplicationEnglishDate = financialStatement.ApplicationEnglishDate,
                        ApplicationNepaliDate = financialStatement.ApplicationNepaliDate,
                        EnglishDate = financialStatement.EnglishDate,
                        NepaliDate = financialStatement.NepaliDate,
                        VerifiedEnglishDate = financialStatement.VerifiedEnglishDate,
                        VerifiedNepaliDate = financialStatement.VerifiedNepaliDate,
                        VerifierName = financialStatement.VerifierName,
                        ApplicantDesignationId = financialStatement.ApplicantDesignationId,
                        ApplicantDesignationName = (await _designation.GetByIdAsync(financialStatement.ApplicantDesignationId.Value))?.DesignationName ?? null,
                        VerifierDesignationId = financialStatement.VerifierDesignationId,
                        VerifierDesignationName = (await _designation.GetByIdAsync(financialStatement.VerifierDesignationId.Value))?.DesignationName ?? null,

                    };

                    return new ServiceResult<FinancialStatementDTO>(true, financialStatementDTO);
                }
                return new ServiceResult<FinancialStatementDTO>(false, errors: new[] { "डाटा भेटिएन " });

            }
            catch(Exception ex)
            {
                return new ServiceResult<FinancialStatementDTO>(false, errors: new[] { ex.Message });
            }
        }

        public async Task<ServiceResult<FinancialStatementDTO>> UpdateFinancialStatement(FinancialStatementDTO financialStatementDTO)
        {
            try
            {
                if(await _financialStatement.CheckIdIfExists(x=>x.FinancialStatementId == financialStatementDTO.FinancialStatementId))
                {
                    var month = await _month.CheckIdIfExists(x=>x.MonthId == financialStatementDTO.MonthId);
                    var fiscalYear = await _fiscalYear.CheckIdIfExists(x=>x.FiscalYearId == financialStatementDTO.FiscalYearId);
                    bool doesApplicantDesignationExists = await _designation.CheckIdIfExists(x => x.DesignationId == financialStatementDTO.ApplicantDesignationId);
                    bool doesVerifierDesignationExists = await _designation.CheckIdIfExists(x => x.DesignationId == financialStatementDTO.VerifierDesignationId);


                    if(!month || !fiscalYear || !doesApplicantDesignationExists || !doesVerifierDesignationExists)
                    {
                        var errors = new List<string>();
                        if (!month) errors.Add("महिना फेला परेन");
                        if (!fiscalYear) errors.Add("आर्थिक वर्ष फेला परेन");
                        if (!doesApplicantDesignationExists) errors.Add("पद भेटिएन !");
                        if (!doesVerifierDesignationExists) errors.Add("पद भेटिएन !");
                        return new ServiceResult<FinancialStatementDTO>(false, errors: errors.ToArray());
                    }

                    financialStatementDTO.UpdatedBy = _username;
                    financialStatementDTO.ApplicationEnglishDate = _nepaliDateHelpher.ReturnNullOrDate(financialStatementDTO.ApplicationNepaliDate);
                    financialStatementDTO.VerifiedEnglishDate = _nepaliDateHelpher.ReturnNullOrDate(financialStatementDTO.VerifiedNepaliDate);
                    financialStatementDTO.PreviousMonthIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.PreviousMonthIncome);
                    financialStatementDTO.CurrentMonthIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.CurrentMonthIncome);
                    financialStatementDTO.TotalIncome = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.TotalIncome);
                    financialStatementDTO.PreviousMonthExpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.PreviousMonthExpenses);
                    financialStatementDTO.CurrentMonthExpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.CurrentMonthExpenses);
                    financialStatementDTO.Totalexpenses = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.Totalexpenses);
                    financialStatementDTO.RemainingCashAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.RemainingCashAmount);
                    financialStatementDTO.RemainingBankAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.RemainingBankAmount);
                    financialStatementDTO.TotalRemaining = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.TotalRemaining);
                    financialStatementDTO.ActualRemainingAmountInBank = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.ActualRemainingAmountInBank);
                    financialStatementDTO.DifferenceBetweenActualAmountAndRemainingAmount = _uniCodeHelpher.ConvertNepaliToEnglishAndViceVersa(financialStatementDTO.DifferenceBetweenActualAmountAndRemainingAmount);


                    var financialStatement = await _financialStatement.UpdateAsync(new FinancialStatementDTO().ToFinanicailStatement(financialStatementDTO));
                    return new ServiceResult<FinancialStatementDTO>(true, new FinancialStatementDTO().ToFinancialStatementDTO(financialStatement));

                }
                return new ServiceResult<FinancialStatementDTO>(false, errors: new[] { "खराब अनुरोध" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<FinancialStatementDTO>(false, errors: new[] { ex.Message });
            }
        }
    }
}
