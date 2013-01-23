using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OST.DataTablesHelper.Mvc.Framework.ModelBinders;

namespace OST.DataTablesHelper.Tests
{
    [TestClass]
    public class ModelBinderTest
    {
        [TestMethod]
        public void CanGeneratePropertiesModel()
        {
            var queryString = @"?sEcho=1&iColumns=3&sColumns=Name%2CPrimaryBillingAddress_City%2CPrimaryBillingAddress_State&iDisplayStart=0&iDisplayLength=10&mDataProp_0=Name&mDataProp_1=PrimaryBillingAddress_City&mDataProp_2=PrimaryBillingAddress_State&sSearch=&bRegex=false&sSearch_0=&bRegex_0=false&bSearchable_0=true&sSearch_1=&bRegex_1=false&bSearchable_1=true&sSearch_2=&bRegex_2=false&bSearchable_2=true&iSortCol_0=0&sSortDir_0=asc&iSortingCols=1&bSortable_0=true&bSortable_1=true&bSortable_2=true&_=1358913249672";
            var binder = new DataTableModelBinder();
            var properties = binder.GeneratePropertiesModel(queryString);
            Assert.IsNotNull(properties, "Somthing bad happened. There is no properties object!");
        }
    }
}
