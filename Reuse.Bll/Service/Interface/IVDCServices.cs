using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IVDCServices
    {
        Task<ServiceResult<List<VDCDTO>>> GetAllVDCs();
        Task<ServiceResult<List<VDCDTO>>> GetVDCByDistrictId(int Id);
    }
}
