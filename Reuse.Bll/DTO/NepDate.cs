using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.DTO
{
    public class NepDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        private string _weekDayName { get; set; }

        public string WeekDayName
        {
            get { return _weekDayName;}
            set { _weekDayName = value; }
        }

        public string MonthName { get; set; }
        public int WeekDay { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}/{2}", Day, Month, Year);
        }

        public string ToLongDateString()
        {
            return string.Format("{0}/{1}{2},{3}", WeekDayName, MonthName, Day, Year);
        }
    }
}
