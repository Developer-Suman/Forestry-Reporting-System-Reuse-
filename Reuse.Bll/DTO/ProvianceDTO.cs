using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class ProvianceDTO
    {
        public int ProvianceId { get; set; }
        public string ProvianceNameInNepali { get; set; }

        public ProvianceDTO ToProvainceDTO(Province province )
        {
            ProvianceDTO provianceDTO = new()
            {
                ProvianceId = province.ProvinceId,
                ProvianceNameInNepali = province.ProvinceNameInNepali
            };

            return provianceDTO;
        }

        public List<ProvianceDTO> ToProvianceDTOList(List<Province> provinceList)
        {
            List<ProvianceDTO> provianceDTOs = new List<ProvianceDTO>();
            for(int i=0;  i<provinceList.Count; i++)
            {
                provianceDTOs.Add(new ProvianceDTO().ToProvainceDTO(provinceList[i]));
            }
            return provianceDTOs;
        }
    }
}
