using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IBranchServices<T> where T : class
    {
        Task<ServiceResult<T>> AddBranch(T entity, string? token);
        Task<ServiceResult<T>> AddInitialBranch(T entity);
        Task<ServiceResult<T>> CheckInitialization();
        Task<ServiceResult<List<T>>> GetAllBranch();
        Task<ServiceResult<List<T>>> GetBranchesToCreateAdmin();
        Task<ServiceResult<List<T>>> GetBranchesByBranchType(int Id);
        Task<ServiceResult<List<T>>> GetHeadBranches(int ParentBranchId);
        Task<ServiceResult<List<T>>> GetBranchesById(int Id);
        Task<ServiceResult<T>> GetBranchById(int Id);
        Task<ServiceResult<T>> UpdateBranch(T entity);
        Task<ServiceResult<T>> DeleteBranch(int Id);
    }
}
