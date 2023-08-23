using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class DistrictDTO
    {
        public int DistrictId { get; set; }
        public string DistrictNameInNepali { get; set; }


        public DistrictDTO ToDistrictDTO(District district)
        {
            DistrictDTO districtDTO = new DistrictDTO()
            {
                DistrictId = district.DistrictId,
                DistrictNameInNepali = district.DistrictNameNepali

            };

            return districtDTO;
        }

        public List<DistrictDTO> ToListDistrictDTO(List<District> districts)
        {
            List<DistrictDTO> districtDTOs = new List<DistrictDTO>();

            for(int i = 0; i < districts.Count(); i++)
            {
                districtDTOs.Add(new DistrictDTO().ToDistrictDTO(districts[i]));
            };

            return districtDTOs;
        }
    }
}
