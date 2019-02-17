using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.domain
{
    public class TimePeriod
    {
        public DateTime GetDateTime()
        { 
            return DateTime.MinValue;    
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        public static TimePeriod GetInstance(string name)
        {
            switch(name)
            {
                case "TODAY" : return new TODAY(); 
                case "TOMORROW" : return new TOMORROW();
                case "NEXT_WEEK" : return new NEXT_WEEK();
                case "NEXT_MONTH" : return new NEXT_MONTH();
            }
            return new TimePeriod();
        }
    }

    public class TODAY : TimePeriod
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }

    public class TOMORROW : TimePeriod
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now.AddDays(1);
        }
    }

    public class NEXT_WEEK : TimePeriod
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now.AddDays(7);
        }
    }

    public class NEXT_MONTH : TimePeriod
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now.AddMonths(1);
        }
    }
}
