using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification
{
    /// <summary>
    /// 表达式
    /// </summary>
    public abstract class Expression
    {
        protected Expression()
        {
            _operands = new List<Expression>();
        }

        private readonly List<Expression> _operands;

        /// <summary>
        ///  操作数
        /// </summary>
        public List<Expression> Operands
        {
            get { return _operands; }
        }

        public Type ReturnType { get; set; }

        //public abstract System.Linq.Expressions.Expression ToExpressionTree();

        internal Expression And(IList<Expression> rightExpressions)
        {
            if (rightExpressions == null || !rightExpressions.Any())
            {
                throw new ArgumentNullException("rightExpressions");
            }
            return new AndExpression(this, rightExpressions);
        }
    }

    internal class AndExpression : Expression
    {
        public AndExpression(Expression expression
            , IList<Expression> rightExpressions)
        {

            if (rightExpressions == null || rightExpressions.Any())
                throw new ArgumentException("And Expression Create Arg Error!", "rightExpressions");
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
                    queryStr.Append(" and");
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

    internal class OrExpression : Expression
    {
        public OrExpression(Expression expression
            , params Specification[] rightSideSpecifications)
        {
            if (rightSideSpecifications == null || rightSideSpecifications.Length <= 0)
                throw new ArgumentException("And Expression Create Arg Error!", "rightSideSpecifications");
            Operands.Add(expression);
            Operands.AddRange(rightSideSpecifications.Select(s => s.SatisfiedBy()));
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
                    queryStr.Append(" and");
                }
            }
            return queryStr.ToString();
        }
    }
}
