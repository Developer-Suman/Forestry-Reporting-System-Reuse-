using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IFinancialStatementServices
    {
        Task<ServiceResult<FinancialStatementDTO>> AddFinancialStatement(FinancialStatementDTO financialStatementDTO);
        Task<ServiceResult<List<FinancialStatementDTO>>> GetAllFinancialStatement(string filter);
        Task<ServiceResult<FinancialStatementDTO>> GetFinancialStatementById(int Id);
        Task<ServiceResult<FinancialStatementDTO>> UpdateFinancialStatement(FinancialStatementDTO financialStatementDTO);
        Task<ServiceResult<FinancialStatementDTO>> DeleteFinancialStatement(int Id);
    }
}
