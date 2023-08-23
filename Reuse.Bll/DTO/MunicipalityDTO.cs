using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class MunicipalityDTO
    {
        public int MunicapalityId { get; set; }
        public string MunicaplaitynameInNepali { get; set; } = string.Empty;


        public MunicipalityDTO ToMunicipalityDTO(Municipality municipality)
        {
            MunicipalityDTO municipalityDTO = new MunicipalityDTO()
            {
                MunicapalityId = municipality.MunicipalityId,
                MunicaplaitynameInNepali = municipality.MunicipalityNameInNepali

            };
            return municipalityDTO;
        }


        public List<MunicipalityDTO> ToMunicipalityDTOList(List<Municipality> municipalities)
        {
            List<MunicipalityDTO> municipalityDTOs = new List<MunicipalityDTO>();
            for(int i=0; i<municipalities.Count(); i++)
            {
                municipalityDTOs.Add(new MunicipalityDTO().ToMunicipalityDTO(municipalities[i]));
            }

            return municipalityDTOs;
        }



    }
}
