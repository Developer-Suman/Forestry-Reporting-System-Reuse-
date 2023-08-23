using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Helpher
{
    public class TrimesterHelpher
    {
        public int? GetTrimesterIdByDate(string nepaliDate)
        {
            int trimesterId;
            string[] splittedNepaliDate = nepaliDate.Split('/');

            if (splittedNepaliDate[1].Equals("4") || splittedNepaliDate[1].Equals("5") || splittedNepaliDate[6].Equals("6"))
            {
                trimesterId = 1;
            }
            else if (splittedNepaliDate[1].Equals("7") || splittedNepaliDate[1].Equals ("8") || splittedNepaliDate[1].Equals("9") )
            {
                trimesterId = 2;
            }
            else if (splittedNepaliDate[1].Equals("10") || splittedNepaliDate[1].Equals("11") || splittedNepaliDate[1].Equals("12"))
            {
                trimesterId = 3;
            }
            else
            {
                trimesterId = 4;
            }
            return trimesterId;
        }
    }
}
