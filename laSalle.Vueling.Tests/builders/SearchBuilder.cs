using laSalle.Vueling.domain;
using laSalle.Vueling.Tests.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.builders
{
   public class SearchBuilder 
   {
    private Guid id;
    private String from;
    private String to;
    private TimePeriod on;

    private SearchBuilder() {
    }

    public static SearchBuilder AReservation() {
        return new SearchBuilder();
    }

    public SearchBuilder WithId(Guid id) {
        this.id = id;
        return this;
    }

    public SearchBuilder FromLocation(String from) {
        this.from = from;
        return this;
    }

    public SearchBuilder ToLocation(String to) {
        this.to = to;
        return this;
    }


    public SearchBuilder OnDate(TimePeriod date) {
        this.on = date;
        return this;
    }

    public Search Build() {
        Search search = new Search();
        search.Id = id;
        search.FromLocation = from;
        search.ToLocation = to;
        search.OnDate = on;
        return search;
    }
}

}
