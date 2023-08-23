using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IDistrictServices
    {
        Task<ServiceResult<List<DistrictDTO>>> GetAllDistricts();
        Task<ServiceResult<List<DistrictDTO>>> GetDistrictByProvianceid(int Id);
    }
}
