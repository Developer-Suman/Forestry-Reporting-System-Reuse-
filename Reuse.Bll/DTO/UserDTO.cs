using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class UserDTO
    {
            public string? UserId { get; set; }
            public string? Username { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public string? Branch { get; set; }
            public string? Role { get; set; }

            public bool IsActive { get; set; }
    }
}
