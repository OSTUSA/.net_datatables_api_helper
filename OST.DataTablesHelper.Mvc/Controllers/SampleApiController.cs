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
    public class SampleApiController : ApiController
    {
        public DataTableModel Get([ModelBinder(typeof (DataTableModelBinder))] DataTableProperties dt)
        {
            // populate your DataTableModel object here
            return new DataTableModel();
        }
    }
}
