using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CSharpFeatures.Lambda
{
    class ExpressionTreeFeature
    {
        public void LambdaExpressionParser()
        {
            var strList = new List<string>();
            var query = from str in strList
                        where str.Length > 0
                        select new { Str = str, Upper = str.ToUpper() };
           
        }
     

    }
}
