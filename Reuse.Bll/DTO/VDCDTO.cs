using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class VDCDTO
    {
        public int VDCId { get; set; }
        public string VDCNameInNepali { get; set; } = string.Empty;

        public VDCDTO ToVDCDTO(VDC vdc) 
        {
            VDCDTO vDCDTO = new VDCDTO()
            {
                VDCId = vdc.VDCId,
                VDCNameInNepali = vdc.VDCNameInNepali
            };
            return vDCDTO;
        }

        public List<VDCDTO> ToVDCDTOList(List<VDC> vDCs)
        {
            List<VDCDTO> vDCDTOs = new List<VDCDTO>();
            for(int i=0; i<vDCs.Count(); i++)
            {
                vDCDTOs.Add(new VDCDTO().ToVDCDTO(vDCs[i]));

            }

            return vDCDTOs;
        }




    }
}
