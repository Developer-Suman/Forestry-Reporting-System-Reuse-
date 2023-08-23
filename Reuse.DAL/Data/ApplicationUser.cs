using Microsoft.AspNetCore.Identity;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirtTime { get; set; }
        public virtual Branch Branch { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }
    }
}
