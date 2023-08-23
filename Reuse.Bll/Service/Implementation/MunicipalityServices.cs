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
    public class MunicipalityServices : IMunicipalityServices
    {
        private readonly IRepository<Municipality> _repository;
        public MunicipalityServices(IRepository<Municipality> repository)
        {
            _repository = repository;
            
        }
        public async Task<ServiceResult<List<MunicipalityDTO>>> GetAllMunicipalities()
        {
            try
            {
                var municapality = await _repository.GetAllAsync();
                return new ServiceResult<List<MunicipalityDTO>>(true, new MunicipalityDTO().ToMunicipalityDTOList(municapality));
           

            }
            catch (Exception ex)
            {
                return new ServiceResult<List<MunicipalityDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<MunicipalityDTO>>> GetMunicipalityByDistrictId(int Id)
        {
            try
            {
                var municipality = await _repository.WhereAsync(x=>x.DistrictId == Id);
                if(municipality.Count()>0)
                {
                    return new ServiceResult<List<MunicipalityDTO>>(true, new MunicipalityDTO().ToMunicipalityDTOList(municipality));
                }
                return new ServiceResult<List<MunicipalityDTO>>(false, errors: new[] { "Municiapality not found" });


            }
            catch(Exception ex)
            {
                return new ServiceResult<List<MunicipalityDTO>>(false, errors: new[] {ex.Message});
            }
        }
    }
}
