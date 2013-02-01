using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.CallHandlers;
using Microsoft.Practices.EnterpriseLibrary.Validation.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace test
{
    public class BankAccount : MarshalByRefObject
    {
        private decimal balance;
        /// <summary>
        /// 获取当前余额
        /// </summary>
        public decimal GetCurrentBalance()
        {
            return balance;
        }

        /// <summary>
        /// 存款
        /// </summary>
        /// <param name="depositAmount">存款金额</param>
        [ValidationCallHandler]
        public void Deposit([RangeValidator(typeof(Decimal), "0.0", RangeBoundaryType.Exclusive, "0.0", RangeBoundaryType.Ignore, MessageTemplate = "存款金额必须大于零")] 
decimal depositAmount)
        {
            balance += depositAmount;
        }

        /// <summary>
        /// 取款
        /// </summary>
        /// <param name="withdrawAmount">取款金额</param>
        [ValidationCallHandler]
        public void Withdraw([RangeValidator(typeof(Decimal), "0.0", RangeBoundaryType.Exclusive, "1000.0", RangeBoundaryType.Inclusive, MessageTemplate = "取款金额必须介入0至1000之间.")]
decimal withdrawAmount)
        {
            if (withdrawAmount > balance)
            {
                throw new ArithmeticException();
            }
            balance -= withdrawAmount;
        }
    }
}
