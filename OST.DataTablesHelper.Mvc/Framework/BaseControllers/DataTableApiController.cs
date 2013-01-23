using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Web.Http;
using OST.DataTablesHelper.Mvc.Models;

namespace OST.DataTablesHelper.Mvc.Framework.BaseControllers
{
    public class DataTableApiController : ApiController
    {
        public DataTableModel ConstructDataTableModel(IQueryable<object> collection, int totalRecordCount, DataTableProperties dt)
        {
            return ConstructDataTableModel(collection, totalRecordCount, dt, false);
        }

        public DataTableModel ConstructDataTableModel(IQueryable<object> collection, int totalRecordCount, DataTableProperties dt, bool performGlobalSearch)
        {
            var output = new List<object>();

            // PERFORM GLOBAL SEARCH
            if (performGlobalSearch)
            {
                if (!string.IsNullOrEmpty(dt.GlobalSearchPhrase))
                {
                    foreach (var col in dt.Columns)
                    {
                        try {
                            output.AddRange(collection.Where(string.Format("{0}.Contains(@0)".Replace("_", "."), col.Name.Replace("_", ".")), dt.GlobalSearchPhrase));
                        } catch (Exception) {
                            try {
                                output.AddRange(collection.Where(string.Format("{0}.Value == @0", col.Name.Replace("_", ".")), Decimal.Parse(dt.GlobalSearchPhrase)));
                            } catch {}
                        }
                    }
                    collection = output.Distinct().AsQueryable();
                }
            }

            // DEFAULT COL SEARCH
            foreach (var col in dt.Columns)
            {
                if (!string.IsNullOrEmpty(col.SearchPhrase) && !col.Name.StartsWith("*"))
                {
                    try { // STRING
                        
                        collection = collection.Where(string.Format("{0}.Contains(@0)", col.Name.Replace("_", ".")), col.SearchPhrase.ToLower());
                    } catch {
                        try { // DECIMAL
                            collection = collection.Where(string.Format("{0}.Value == @0", col.Name.Replace("_", ".")), Decimal.Parse(col.SearchPhrase));
                        } catch {
                            try { // DATE
                                var beginDate = DateTime.Parse(col.SearchPhrase);
                                var endDate = beginDate.AddDays(1);
                                collection = collection.Where(string.Format("{0} > @0 && {0} < @1", col.Name.Replace("_", ".")), beginDate, endDate);
                            } catch { }
                        }
                    }
                }
            }

            int matchingRecordCount = 0;
            try { matchingRecordCount = collection.Count(); } catch (Exception) { }

            // DEFAULT ORDER BY
            //if(!dt.SortColumn.Contains("_"))
            try {
                collection = collection.OrderBy(string.Format("{0} {1}", dt.SortColumn.Replace("_", "."), dt.SortDirection));
            } catch { }


            // GET CORRECT PAGE
            collection = collection.Skip(dt.DisplayStart).Take(dt.DisplayLength);

            // RUN QUERY
            var results = collection.ToList();

            // TAILOR DATA MODEL TO ONLY REQUESTED FIELDS
            var outputData = new List<ExpandoObject>();
            foreach (var c in results)
            {
                dynamic data = new ExpandoObject();
                foreach (var col in dt.Columns)
                {
                    var val = GetPropValue(col.Name, c);
                    if(col.Name.ToLower().Contains("date"))
                        ((IDictionary<String, Object>)data).Add(col.Name, ((val as DateTime?).HasValue) ? (val as DateTime?).Value.ToShortDateString() : val);
                    else
                        ((IDictionary<String, Object>)data).Add(col.Name, val);
                }
                if(dt.Columns.FirstOrDefault(x => x.Name == "ID") == null) ((IDictionary<String, Object>)data).Add("ID", c.GetType().GetProperty("ID").GetValue(c, null));
                outputData.Add(data);
            }

            // POPULATE THE JSON MODEL FOR DATATABLES RESPONSE
            var m = new DataTableModel();
            m.sEcho = dt.Echo + 1;
            m.iTotalDisplayRecords = matchingRecordCount;
            m.iTotalRecords = totalRecordCount;
            m.aaData = outputData.Distinct();

            return m;
        }

        public Object GetPropValue(String name, Object obj)
        {
            foreach (String part in name.Replace("*","").Split('_'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

    }
}