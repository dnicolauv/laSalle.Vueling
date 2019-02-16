using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace laSalle.Vueling.domain
{
    public class Search 
    {
        public Guid Id {get;set;}
        public String FromLocation {get;set;}
        public String ToLocation {get;set;}
        public DateTime OnDate {get;set;}

        public override string ToString()
        {
            return FromLocation + " - " + ToLocation + " - " + OnDate.ToShortDateString();
        }
    }
}