using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceExtention
{

    public class OrderValidationCallHandler : ICallHandler
    {
        private static IList<string> _legalSuppliers;
        public static IList<string> LegalSuppliers
        {
            get
            {
                if (_legalSuppliers == null)
                {
                    _legalSuppliers = new List<string>();
                    _legalSuppliers.Add("Company AAA");
                    _legalSuppliers.Add("Company BBB");
                    _legalSuppliers.Add("Company CCC");
                }

                return _legalSuppliers;
            }
        }
        public bool ValidateTotalPrice { get; set; }
        public bool ValidateSupplier { get; set; }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (input.Inputs.Count == 0) { return getNext()(input, getNext); }
            Order order = input.Inputs[0] as Order;
            if (order == null) { return getNext()(input, getNext); }
            if (order.OrderDate > DateTime.Today)
            {
                return input.CreateExceptionMethodReturn(new OrderValidationException("The order date is later than the current date!"));
            }

            if (order.Items.Count == 0)
            {
                return input.CreateExceptionMethodReturn(new OrderValidationException("There are not any items for the order!"));
            }

            if (this.ValidateSupplier)
            {
                if (!LegalSuppliers.Contains<string>(order.Supplier))
                {
                    return input.CreateExceptionMethodReturn(new OrderValidationException("The supplier is inllegal!"));
                }
            }

            if (this.ValidateTotalPrice)
            {
                double totalPrice = 0;
                foreach (OrderItem item in order.Items)
                {
                    totalPrice += item.Quantity * item.UnitPrice;
                }
                if (totalPrice != order.TotalPrice)
                {
                    return input.CreateExceptionMethodReturn(new OrderValidationException("The sum of the order item is not equal to the order total price!"));
                }
            }

            return getNext()(input, getNext);
        }
    }

}
