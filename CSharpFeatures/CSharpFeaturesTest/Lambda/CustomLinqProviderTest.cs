using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.Lambda.LinqToTerraServerProvider;

namespace CSharpFeaturesTest.Lambda
{
    [TestClass]
    public class CustomLinqProviderTest
    {
        [TestMethod]
        public void LinqTest()
        {
            QueryableTerraServerData<Place> terraPlaces = new QueryableTerraServerData<Place>();

            var query = from place in terraPlaces
                        where place.Name.StartsWith("Lond")
                        select new { place.Name, place.State };

            foreach (var obj in query)
                Console.WriteLine(obj);
        }
    }
}
