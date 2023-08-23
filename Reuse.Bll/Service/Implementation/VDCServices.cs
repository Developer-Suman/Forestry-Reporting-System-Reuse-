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
    public class VDCServices : IVDCServices
    {
        private readonly IRepository<VDC> _repository;
        public VDCServices(IRepository<VDC> repository)
        {
            _repository = repository;
            
        }
        public async Task<ServiceResult<List<VDCDTO>>> GetAllVDCs()
        {
            try
            {
                var vdc = await _repository.GetAllAsync();
                return new ServiceResult<List<VDCDTO>>(true, new VDCDTO().ToVDCDTOList(vdc));

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<VDCDTO>>(false,errors: new[] {ex.Message});
            }
        }

        public async Task<ServiceResult<List<VDCDTO>>> GetVDCByDistrictId(int Id)
        {
            try
            {
                var vdcs = await _repository.WhereAsync(x => x.DistrictId == Id);
                if(vdcs.Count()>0)
                {
                    return new ServiceResult<List<VDCDTO>>(true,new VDCDTO().ToVDCDTOList(vdcs));
                }

                return new ServiceResult<List<VDCDTO>>(false, errors: new[] { "No VDC found...." });

            }
            catch(Exception ex)
            {
                return new ServiceResult<List<VDCDTO>>(false, errors: new[] {ex.Message});
            }
        }
    }
}
