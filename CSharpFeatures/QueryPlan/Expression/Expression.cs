using System;
using System.Collections.Generic;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    /// <summary>
    ///     表达式
    /// </summary>
    public abstract class Expression
    {
        private readonly List<Expression> _operands;

        protected Expression()
        {
            _operands = new List<Expression>();
        }

        /// <summary>
        ///     操作数
        /// </summary>
        public List<Expression> Operands
        {
            get { return _operands; }
        }

        public Type ReturnType { get; set; }
        public string Alia { get; set; }

        //public abstract System.Linq.Expressions.Expression ToExpressionTree();

        public Expression And(params Expression[] rightExpressions)
        {
            if (rightExpressions == null)
            {
                throw new ArgumentNullException("rightExpressions");
            }
            return new AndExpression(this, rightExpressions);
        }

        public Expression Or(params Expression[] rightExpressions)
        {
            if (rightExpressions == null)
            {
                throw new ArgumentNullException("rightExpressions");
            }
            return new OrExpression(this, rightExpressions);
        }

        public Expression Not()
        {
            return new NotExpression(this);
        }

        public Expression Member(string paramName, Type ofType = null)
        {
            var member = new MemberExpress(this, paramName, ofType);
            return member;
        }

        /// <summary>
        ///     大于
        /// </summary>
        /// <returns></returns>
        public Expression GT(Expression right)
        {
            return new GreaterThanExpression(this, right);
        }

        /// <summary>
        ///     大于
        /// </summary>
        /// <returns></returns>
        public Expression Equal(Expression right)
        {
            return new EqualExpression(this, right);
        }

        public Expression Join(Expression dest, Expression onCriteria)
        {
            return new JoinExpression(this, dest, onCriteria);
        }
    }
}