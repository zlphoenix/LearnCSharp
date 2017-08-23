using System;
using System.Collections.Generic;
using Inspur.Gsp.CSharpIntroduction.DemoLib;
using Inspur.Gsp.CSharpIntroduction.DemoLib.StubLib;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Stub
{
    /// <summary>
    /// #.3.4 测试桩： 被测试类:酒吧
    /// </summary>
    public class Pub
    {

        private readonly ICheckInFee checkInFee;
        private decimal inCome;

        /// <summary>
        /// #.3.4.2 依赖模块Lib 中的ICheckInFee 
        /// </summary>
        /// <param name="checkInFee"></param>
        public Pub(ICheckInFee checkInFee)
        {
            this.checkInFee = checkInFee;
        }

        /// <summary>
        /// #.3.4.1 被测方法：入场
        /// </summary>
        /// <param name="customers">一大波顾客</param>
        /// <returns></returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = customer.Gender != Gender.Male;
                var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;


                //女生免費入場
                if (isFemale)
                    //#.3.4.4 Shim 
                    if (isLadyNight)
                        continue;


                //for stub, validate status: income value
                //for mock, validate only male
                inCome += checkInFee.GetFee(customer);

                result++;
            }

            //for stub, validate return value
            return result;
        }
        /// <summary>
        /// 获得总收入
        /// </summary>
        /// <returns></returns>
        public decimal GetInCome()
        {
            return inCome;
        }
    }
}