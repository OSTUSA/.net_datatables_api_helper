using System.Collections.Generic;

namespace OST.DataTablesHelper.Mvc.Models
{
    public class DataTableModel
    {
        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public IEnumerable<dynamic> aaData { get; set; }
    }
}