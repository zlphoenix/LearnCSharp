using System;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    internal class OrExpression : Expression
    {
        public OrExpression(Expression expression, 
            Expression[] rightExpressions)
        {
            if (rightExpressions == null || rightExpressions.Any())
                throw new ArgumentException("Or Expression Create Arg Error!", "rightExpressions");
            Operands.Add(expression);
            Operands.AddRange(rightExpressions);
            ReturnType = typeof(bool);
        }

        public override string ToString()
        {
            var queryStr = new StringBuilder();
            for (var i = 0; i < Operands.Count; i++)
            {
                queryStr.Append(Operands[i]);
                if (i != Operands.Count - 1)
                {
                    queryStr.Append(" or ");
                }
            }
            return queryStr.ToString();
        }
    }
}