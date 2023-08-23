using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class BranchTypeDTO
    {
        public int BranchTypeId { get; set; }
        public string BranchTypeNameInNepali { get; set; } = string.Empty;


        public BranchTypeDTO ToBranchTypeDTO(BranchType branchType)
        {

            BranchTypeDTO branchTypeDTO = new()
            {
                BranchTypeId = branchType.BranchTypeId,
                BranchTypeNameInNepali = branchType.BranchTypeNameInNepali
            };
            return branchTypeDTO;

        }

        public List<BranchTypeDTO> ToBranchTypeDTOList(List<BranchType> branchTypes)
        {
            List<BranchTypeDTO> branchTypeDTOs = new();
            for(int i=0; i<branchTypes.Count; i++)
            {
                branchTypeDTOs.Add(new BranchTypeDTO().ToBranchTypeDTO(branchTypes[i]));
            }
            return branchTypeDTOs;

        }
    }
}
