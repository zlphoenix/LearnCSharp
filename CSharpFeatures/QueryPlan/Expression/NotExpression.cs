using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    class NotExpression : Expression
    {
        private readonly Expression _innerExpression;

        public NotExpression(Expression expression)
        {
            this._innerExpression = expression;
            ReturnType = typeof (bool);
        }

        public Expression InnerExpression
        {
            get { return _innerExpression; }
        }

        public override string ToString()
        {
            return string.Format(" not {0}", InnerExpression.ToString());
        }
    }
}
