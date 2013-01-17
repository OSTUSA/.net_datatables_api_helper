namespace OST.DataTablesHelper.Mvc.Models
{
    public class DataTableColumn
    {
        public string Name { get; set; }
        public string SearchPhrase { get; set; }
        public bool Sortable { get; set; }
        public string SortDirection { get; set; }
    }
}