using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Branch id is Required")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "RoleId is Required")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "PhonenUmber is Required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Password must be atleast 5 characters")]
        public string? Password { get; set; }
    }
}
