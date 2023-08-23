using Reuse.Bll.Repository.Interface;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Helpher
{
    public class FiscalYearHelpher
    {
        private readonly IRepository<FiscalYear> _fiscalYear;

        public FiscalYearHelpher(IRepository<FiscalYear> fiscalYear)
        {
            _fiscalYear = fiscalYear;

        }

        public async Task<int?> GetCurrentFiscalYearId()
        {
            DateTime todaysDate = DateTime.Now;
            return (await _fiscalYear.GetSingle(x => x.StartDt <= todaysDate && x.EndDt >= todaysDate))?.FiscalYearId ?? null;
        }

        public async Task<int?> GetFiscalYearByEnglishDate(DateTime? englishDate)
        {
            return (await _fiscalYear.GetSingle(x => x.StartDt <= englishDate && x.EndDt <= englishDate))?.FiscalYearId ?? null;
        }
    }
}
