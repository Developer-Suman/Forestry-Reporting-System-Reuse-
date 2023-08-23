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
    public class ProvianceServices : IProvinceServices
    {
        private readonly IRepository<Province> _repository;
        public ProvianceServices(IRepository<Province> repository)
        {
            _repository = repository;
            
        }

        public async Task<ServiceResult<List<ProvianceDTO>>> GetAllProviance()
        {
            try
            {
                var proviance = await _repository.GetAllAsync();
                return new ServiceResult<List<ProvianceDTO>>(true, new ProvianceDTO().ToProvianceDTOList(proviance));

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<ProvianceDTO>>(false, errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<ProvianceDTO>> GetProvianceById(int Id)
        {
            try
            {
                var proviance = await _repository.GetByIdAsync(Id);
                if (proviance != null)
                {
                    return new ServiceResult<ProvianceDTO>(true, new ProvianceDTO().ToProvainceDTO(proviance));
                }
                return new ServiceResult<ProvianceDTO>(false, errors: new[] { "Proviance not found....." });

            }
            catch (Exception ex)
            {
                return new ServiceResult<ProvianceDTO>(false, errors: new[] { ex.Message });
            }
        }
    }
}
