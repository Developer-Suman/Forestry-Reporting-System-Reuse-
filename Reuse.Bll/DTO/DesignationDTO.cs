using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class DesignationDTO
    {
        public int DesignationId { get;set; }
        [Required]
        public string DesignationName { get;set; } = string.Empty;
        public bool IsActive { get; set; }


        public DesignationDTO ToDesignationDTO(Designation designation)
        {
            DesignationDTO designationDTO = new()
            {
                DesignationId = designation.DesignationId,
                DesignationName = designation.DesignationName,
                IsActive = designation.IsActive,
            };
            return designationDTO;
        }


        public Designation ToDesignation(DesignationDTO designationDTO)
        {
            Designation designation = new()
            {
                DesignationId = designationDTO.DesignationId,
                DesignationName = designationDTO.DesignationName,
                IsActive = designationDTO.IsActive,
            };
            return designation;
        }

        public List<DesignationDTO> ToDesignationDTOList(List<Designation> designations)
        {
            List<DesignationDTO> designationDTOs = new();
            for(int i=0; i<designations.Count; i++)
            {
                designationDTOs.Add(new DesignationDTO().ToDesignationDTO(designations[i]));
            }
            return designationDTOs;
        }
    }
}
