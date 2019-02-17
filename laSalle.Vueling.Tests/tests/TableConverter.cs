using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace laSalle.Vueling.Tests.tests
{
    public class SpecFlowTableReader
    {
        private Table table;

        public SpecFlowTableReader(Table table)
        {
            this.table = table;
        }

        public int GetRowCount()
        {
            return table.RowCount;
        }

        public string GetValue(string key)
        {
            foreach(var dr in table.Rows)
            {
                string value = string.Empty;
                dr.TryGetValue(key, out value);
                return value;
            }
            return string.Empty;
        }
    }
}
