using System.Collections.Generic;
using System.Collections.Specialized;

namespace OST.DataTablesHelper.Mvc.Models
{
    public class DataTableProperties
    {
        private List<DataTableColumn> _columns;
        public List<DataTableColumn> Columns {
            get { return _columns ?? (_columns = new List<DataTableColumn>()); }
        }
        public NameValueCollection QueryStrings { get; set; }
        public string GlobalSearchPhrase { get; set; }
        public int Echo { get; set; }
        public int DisplayLength { get; set; }
        public int DisplayStart { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
}