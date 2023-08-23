using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IProvinceServices
    {
        Task<ServiceResult<List<ProvianceDTO>>> GetAllProviance();
        Task<ServiceResult<ProvianceDTO>> GetProvianceById(int Id);
    }
}
