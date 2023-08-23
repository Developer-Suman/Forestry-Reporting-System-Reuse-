using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Service.Interface
{
    public interface ISeedServices
    {
        Task SeedUser(int BranchId);
        Task SeedData();
    }
}
