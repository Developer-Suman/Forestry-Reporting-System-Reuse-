using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int? MunicipalityId { get; set; }
        public int? VDCId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchAddress { get; set; } = string.Empty;

        public int? ParentBranchId { get; set; }

        public string? PhoneNo { get; set; }
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual BranchType? BranchType { get; set; }
       
        public int BranchTypeId { get; set; }
    }
}
