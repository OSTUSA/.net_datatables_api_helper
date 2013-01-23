using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using OST.DataTablesHelper.Mvc.Framework.BaseControllers;
using OST.DataTablesHelper.Mvc.Framework.ModelBinders;
using OST.DataTablesHelper.Mvc.Models;

namespace OST.DataTablesHelper.Mvc.Controllers
{
    public class SampleApiController : DataTableApiController
    {
        public DataTableModel Get([ModelBinder(typeof (DataTableModelBinder))] DataTableProperties dt)
        {
            // Retreive your IQueryable collection
            var queryableCollection = new List<Object>().AsQueryable();
            // Let the Base Controller do the rest (i.e. Searching, Paging, Sort Order, Data Trimming)
            return ConstructDataTableModel(queryableCollection, queryableCollection.Count(), dt);
        }
    }
}
