using System.Collections.Specialized;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using OST.DataTablesHelper.Mvc.Models;

namespace OST.DataTablesHelper.Mvc.Framework.ModelBinders
{
    public class DataTableModelBinder : IModelBinder
    {
        public DataTableModelBinder()
        {
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext modelBindingContext)
        {
            
            var p = new DataTableProperties();

            var query = actionContext.ControllerContext.Request.RequestUri.Query;
            var parameters = query.Replace("?", "").Split('&');
            var qs = new NameValueCollection();

            // INITIALIZE QUERY STRING DICTIONARY
            foreach (var param in parameters)
            {
                var split = param.Split('=');
                qs.Add(split[0], split[1]);
            }
            p.QueryStrings = qs;

            // SORT COLUMN POSITION
            var sortColPosition = qs["iSortCol_0"];

            // SORT DIRECTION
            var sortDir = qs["sSortDir_0"];
            sortDir = (sortDir == "asc") ? "" : "descending";
            p.SortDirection = sortDir;

            foreach (var k in qs.AllKeys)
            {
                if (k.StartsWith("mData"))
                {
                    // ADD COLUMN PROPERTIES MODEL
                    var number = k.Split('_')[1].Trim();
                    var searchPhrase = qs[string.Format("sSearch_{0}", number)];
                    var sortable = qs[string.Format("bSortable_{0}", number)];
                    if (sortColPosition == number) p.SortColumn = qs[k];
                    var col = new DataTableColumn { Name = qs[k], SearchPhrase = searchPhrase, Sortable = bool.Parse(sortable) };
                    p.Columns.Add(col);
                }
                else if (k == "sSearch")
                {
                    // GLOBAL SEARCH PHRASE
                    p.GlobalSearchPhrase = qs[k].ToLower();
                }
                else if (k == "sEcho")
                {
                    // REQUEST COUNT 
                    // (must be incremented and returned with dataset for DataTable to recognize a new dataset)
                    p.Echo = int.Parse(qs[k]);
                }
                else if (k == "iDisplayStart")
                {
                    // STARTING RECORD POSITION FOR PAGING
                    p.DisplayStart = int.Parse(qs[k]);
                }
                else if (k == "iDisplayLength")
                {
                    // PAGE SIZE
                    p.DisplayLength = int.Parse(qs[k]);
                }
            }

            modelBindingContext.Model = p;

            return true;
        }

    }
}