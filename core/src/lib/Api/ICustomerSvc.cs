using System.Collections.Generic;

namespace Allen.Core.Api
{
    public interface ICustomerSvc
    {
        IList<Customer> Get();
    }

    public class Customer
    {
    }
}