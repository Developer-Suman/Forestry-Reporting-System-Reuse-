using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class WoodBusinessDTO
    {
        public int WoodBusinessId { get; set; }
        public string WoodBusinessCode { get; set; } = string.Empty;
        public string WoodBusinessName { get; set; } = string.Empty;
        public bool IsActive { get; set; }


        public WoodBusinessDTO ToWoodBusinessDTO(WoodBusiness woodBusiness)
        {
            WoodBusinessDTO woodBusinessDTO = new WoodBusinessDTO()
            {
                WoodBusinessId = woodBusiness.WoodBusinessId,
                WoodBusinessName = woodBusiness.WoodBusinessName,
                WoodBusinessCode = woodBusiness.WoodBusinessCode,
                IsActive = woodBusiness.IsActive,

            };

            return woodBusinessDTO;
        }

        public WoodBusiness ToWoodBusiness(WoodBusinessDTO woodBusinessDTO)
        {
            WoodBusiness woodBusiness = new WoodBusiness()
            {
                WoodBusinessId = woodBusinessDTO.WoodBusinessId,
                WoodBusinessCode = woodBusinessDTO.WoodBusinessCode,
                WoodBusinessName = woodBusinessDTO.WoodBusinessName,
                IsActive = woodBusinessDTO.IsActive,
            };

            return woodBusiness;

        }


        public List<WoodBusinessDTO> ToWoodBusinessDTOList(List<WoodBusiness> woodBusinesses)
        {
            List<WoodBusinessDTO> woodBusinessDTOs = new();
            for(int i=0; i<woodBusinesses.Count(); i++)
            {
                woodBusinessDTOs.Add(new WoodBusinessDTO().ToWoodBusinessDTO(woodBusinesses[i]));
            } 
            return woodBusinessDTOs;
        }

    }
}
