//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Dynamic;
//using System.Linq;
//using System.Linq.Dynamic;
//using System.Reflection;
//using System.Web.Http;
//using .Portal.Web.Models;

//namespace .Portal.Web.Framework
//{
//    public class DataTableApiController : ApiController
//    {
//        public DataTableModel ConstructDataTableModel(IQueryable<object> collection, int totalRecordCount, DataTableProperties dt)
//        {
//            // DEFAULT COL SEARCH
//            foreach (var col in dt.Columns) {
//                if (!string.IsNullOrEmpty(col.SearchPhrase) && !col.Name.Contains("_")) {
//                    collection = collection.Where(string.Format("{0}.Contains(@0)", col.Name.Replace("_", ".")), col.SearchPhrase.ToLower());
//                }
//            }

//            int matchingRecordCount = 0;
//            try { matchingRecordCount = collection.Count(); } catch (Exception) { }

//            // DEFAULT ORDER BY
//            if(!dt.SortColumn.Contains("_"))
//                collection = collection.OrderBy(string.Format("{0} {1}", dt.SortColumn.Replace("_", "."), dt.SortDirection));


//            // GET CORRECT PAGE
//            collection = collection.Skip(dt.DisplayStart).Take(dt.DisplayLength);

//            // RUN QUERY
//            var results = collection.ToList();

//            // TAILOR DATA MODEL TO ONLY REQUESTED FIELDS
//            var outputData = new List<ExpandoObject>();
//            foreach (var c in results)
//            {
//                dynamic data = new ExpandoObject();
//                foreach (var col in dt.Columns)
//                {
//                    var val = GetPropValue(col.Name, c);
//                    ((IDictionary<String, Object>)data).Add(col.Name, val);
//                }
//                ((IDictionary<String, Object>)data).Add("ID", c.GetType().GetProperty("ID").GetValue(c, null));
//                outputData.Add(data);
//            }

//            // POPULATE THE JSON MODEL FOR DATATABLES RESPONSE
//            var m = new DataTableModel();
//            m.sEcho = dt.Echo + 1;
//            m.iTotalDisplayRecords = matchingRecordCount;
//            m.iTotalRecords = totalRecordCount;
//            m.aaData = outputData.Distinct();

//            return m;
//        }

//        public Object GetPropValue(String name, Object obj)
//        {
//            foreach (String part in name.Split('_'))
//            {
//                if (obj == null) { return null; }

//                Type type = obj.GetType();
//                PropertyInfo info = type.GetProperty(part);
//                if (info == null) { return null; }

//                obj = info.GetValue(obj, null);
//            }
//            return obj;
//        }

//    }
//}