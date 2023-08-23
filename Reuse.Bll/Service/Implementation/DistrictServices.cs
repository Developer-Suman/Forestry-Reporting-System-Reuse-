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
    public class DistrictServices : IDistrictServices
    {
        private readonly IRepository<District> _repository;

        public DistrictServices(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<List<DistrictDTO>>> GetAllDistricts()
        {
            try
            {
                var districts = await _repository.GetAllAsync();
                return new ServiceResult<List<DistrictDTO>>(true, new DistrictDTO().ToListDistrictDTO(districts));

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<DistrictDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<DistrictDTO>>> GetDistrictByProvianceid(int Id)
        {
            try
            {
                var district = await _repository.WhereAsync(x=>x.ProvinceId == Id);
                if(district.Count() > 0)
                {
                    return new ServiceResult<List<DistrictDTO>>(true, new DistrictDTO().ToListDistrictDTO(district));
                }
                return new ServiceResult<List<DistrictDTO>>(false, errors: new[] { "District not found!!" });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<DistrictDTO>>(false, errors: new[] { ex.Message });
            }
        }
    }
}
