using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface IBlackListServices
    {
        bool IsBlacklisted(string jwt);
        void Blacklist(string jwt);
    }
}
