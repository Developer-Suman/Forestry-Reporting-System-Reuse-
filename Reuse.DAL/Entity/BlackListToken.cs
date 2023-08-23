using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.DAL.Entity
{
    public class BlackListToken
    {
        [Key]
        public int BlackLisedtokensId { get; set; }
        public string Token { get; set; }
    }
}
