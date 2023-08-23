using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IAuthServices
    {
        Task<ServiceResult<List<UserDTO>>> GetUserByBranchId(int BranchId);
        Task<ServiceResult<List<UserDTO>>> GetAllUsers();
        Task<ServiceResult<UserDTO>> UpdateUser(UserDTO entity);
        Task<ServiceResult<UserDTO>> ChangePassword(ChangePasswordDTO entity);
        Task<ServiceResult<TokenDTO>> LoginService(LogInDTO entity);
        Task<ServiceResult<TokenDTO>> RegisterServices(RegisterDTO entity);
        Task<ServiceResult<TokenModel>> RefreshTokenServices(TokenModel entity);
        Task<ServiceResult<TokenDTO>> RegisterAdmin(RegisterDTO entity);
        Task<ServiceResult<RevokeUserDTO>> RevokeUser(string authorization);
        Task<ServiceResult<RevokeUserDTO>> RevokeAllUser();
        Task<ServiceResult<UserDTO>> ResetPassword(ChangePasswordDTO entity);
    }
}
