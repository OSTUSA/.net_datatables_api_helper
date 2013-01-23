using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using OST.DataTablesHelper.Mvc.Framework.ModelBinders;
using OST.DataTablesHelper.Mvc.Models;

namespace OST.DataTablesHelper.Mvc.Controllers
{
    public class SampleCustomApiController : ApiController
    {
        public DataTableModel Get([ModelBinder(typeof (DataTableModelBinder))] DataTableProperties dt)
        {
            // Populate your DataTableModel object here
            // -- use this method when you want to implement searching, paging & sorting yourself
            return new DataTableModel() { };
        }
    }
}
