using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class BranchDTO
    {
        public int BranchId { get; set; }
        //[Required]
        public string BranchName { get; set; } = string.Empty;
        //[Required]
        public string BranchCode { get; set;} = string.Empty;

        public string BranchAddress { get; set; } = string.Empty;
        public string? PhoneNo { get; set; } 
        public string? Email { get; set; } 
        public int? ParentBranchId { get;set; }
        public string? ParentBranch { get; set; }
        public bool IsActive { get; set; } = true;
        public int BranchTypeId { get; set;}
        public string? BranchType { get; set; }

        public int ProvinceId { get; set; }
        public string? Province { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string? District { get; set; }
        public int? MunicipalityId { get; set; }
        public string? Municipality { get; set; }
        public int? VDCId { get; set; }
        public string? VDC { get; set; }


        public Branch ToBranch(BranchDTO branchDTO)
        {
            Branch branch = new Branch()
            {
                BranchId = branchDTO.BranchId,
                BranchName = branchDTO.BranchName,
                BranchCode = branchDTO.BranchCode,
                ProvinceId = branchDTO.ProvinceId,
                DistrictId = branchDTO.DistrictId,
                MunicipalityId = branchDTO.MunicipalityId,
                VDCId = branchDTO.VDCId,
                BranchAddress = branchDTO.BranchAddress,
                ParentBranchId = branchDTO.ParentBranchId,
                IsActive = branchDTO.IsActive,
                PhoneNo = branchDTO.PhoneNo,
                Email = branchDTO.Email,
                BranchTypeId = branchDTO.BranchTypeId,
            };
            return branch;
        }


        public BranchDTO ToBranchDTO(Branch branch)
        {
            BranchDTO persondto = new()
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                ProvinceId = branch.ProvinceId,
                DistrictId = branch.DistrictId,
                MunicipalityId = branch.MunicipalityId,
                VDCId = branch.VDCId,
                BranchCode = branch.BranchCode,
                BranchAddress = branch.BranchAddress,
                PhoneNo = branch.PhoneNo,
                Email = branch.Email,
                ParentBranchId = branch.ParentBranchId,
                IsActive = branch.IsActive,
                BranchTypeId = branch.BranchTypeId

            };

            return persondto;
        }


        public List<BranchDTO> ToBranchDTOList(List<Branch> branches)
        {
            List<BranchDTO> branchDTOs = new();
            for(int i = 0; i < branches.Count; i++)
            {
                branchDTOs.Add(new BranchDTO().ToBranchDTO(branches[i]));

            }

            return branchDTOs;
        }


        public Branch ToUpdatedBranch(Branch branch, BranchDTO branchDTO)
        {
            branch.BranchId = branchDTO.BranchId;
            branch.BranchName = branchDTO.BranchName;
            branch.BranchCode = branchDTO.BranchCode;
            branch.BranchAddress = branchDTO.BranchAddress;
            branch.PhoneNo = branchDTO.PhoneNo;
            branch.Email = branchDTO.Email;
            branch.ParentBranchId = branchDTO.ParentBranchId;
            branch.IsActive = branchDTO.IsActive;
            return branch;
        }

        

    }
}
