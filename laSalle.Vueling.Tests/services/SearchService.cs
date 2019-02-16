using laSalle.Vueling.domain;
using laSalle.Vueling.Tests.builders;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laSalle.Vueling.Tests.services
{
    public class SearchService
    {
        private static ILog LOGGER = LogManager.GetLogger(typeof(SearchService));

        private Search GenerateSearchs(Guid i) {
            return new SearchDataBuilder().DefaultReservation(i).Build();
        }

        public void Clean() {
            LOGGER.Debug("Clean");
        }
    }
}
