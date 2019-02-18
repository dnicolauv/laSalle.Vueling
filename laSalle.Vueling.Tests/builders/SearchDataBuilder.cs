using laSalle.Vueling.Tests.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.builders
{
    public class SearchDataBuilder
    {
        public SearchBuilder DefaultReservation(Guid id)
        {
            return SearchBuilder
                    .AReservation()
                    .WithId(id)                
                    .OnDate(new TODAY());
        }
    }
}
