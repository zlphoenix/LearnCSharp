using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allen.Design.QueryPlan.NewSpecification.Expression;

namespace Allen.Design.QueryPlan.Test
{
    public class CreateExpression
    {
        /// <summary>
        /// Order.Qty > 100 and Order.Customer = "Zhang San" 
        /// </summary>
        /// <returns></returns>
        public ExpressionContext On()
        {
            var orderParam = new ParameterExpression() { Name = "Order", Type = typeof(Order) };

            var exp = orderParam.Member("Qty")
                    .GT(new ConstantExpression(100))
                .And(orderParam.Member("Customer")
                    .Equal(new ConstantExpression("Zhang San")));

            return new ExpressionContext
            {
                Parameters = new List<ParameterExpression> { orderParam },
                Expression = exp,
            };
        }

        public Expression Join()
        {
            var fromParam = new ParameterExpression()
            {
                Name = "SrcOrder",
                Alia = "Src",
                Type = typeof(Order)
            };
            var joinCtx = On();
            var join = fromParam.Join(joinCtx.Parameters[0], joinCtx.Expression);
            return join;
        }
    }


    public class ExpressionContext
    {
        public List<ParameterExpression> Parameters { get; set; }
        public Expression Expression { get; set; }
    }
    public class Order
    {
        public Order SourceOrder { get; set; }
        public string OrderNumber { get; set; }
        public decimal Qty { get; set; }
        public string Customer { get; set; }
    }
}
