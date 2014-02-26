using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    internal class AndExpression : Expression
    {
        public AndExpression(Expression expression
            , IList<Expression> rightExpressions)
        {
            if (rightExpressions == null || !rightExpressions.Any())
                throw new ArgumentException("And Expression Create Arg Error!", "rightExpressions");
            Operands.Add(expression);
            Operands.AddRange(rightExpressions);
            ReturnType = typeof (bool);
        }

        public override string ToString()
        {
            var queryStr = new StringBuilder();
            for (int i = 0; i < Operands.Count; i++)
            {
                queryStr.Append(Operands[i]);
                if (i != Operands.Count - 1)
                {
                    queryStr.Append(" and ");
                }
            }
            return queryStr.ToString();
        }

        //public override System.Linq.Expressions.Expression ToExpressionTree()
        //{
        //    System.Linq.Expressions.Expression result;
        //    foreach (var operand in Operands)
        //    {
        //        result =  operand.ToExpressionTree().and
        //    }
        //}
    }
}