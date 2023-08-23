using Reuse.Bll.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IMunicipalityServices
    {
        Task<ServiceResult<List<MunicipalityDTO>>> GetAllMunicipalities();
        Task<ServiceResult<List<MunicipalityDTO>>> GetMunicipalityByDistrictId(int Id);
    }
}
