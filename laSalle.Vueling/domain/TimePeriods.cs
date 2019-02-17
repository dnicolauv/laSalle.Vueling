using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.domain
{
    public abstract class TimePeriod
    {
        public virtual DateTime GetDateTime()
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
                default: return new TODAY();
            }            
        }
    }

    public class TODAY : TimePeriod
    {
        public override DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }

    public class TOMORROW : TimePeriod
    {
        public override DateTime GetDateTime()
        {
            return DateTime.Now.AddDays(1);
        }
    }

    public class NEXT_WEEK : TimePeriod
    {
        public override DateTime GetDateTime()
        {
            return DateTime.Now.AddDays(7);
        }
    }

    public class NEXT_MONTH : TimePeriod
    {
        public override DateTime GetDateTime()
        {
            return DateTime.Now.AddMonths(1);
        }
    }
}
