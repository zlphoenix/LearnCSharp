using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFeatures.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Threading
{
    [TestClass]
    public class MutexTests
    {
        public static IPool Pool { get; private set; }


        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            //结论在同一台机器上测试，Single方式的执行效率要高出2个数量级
            //Pool = new SinglePool();  //请求数：11138545 req/5s
            Pool = new DualPool();      //         391904 req/5s
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("Free Count:{0}", Pool.FreeCount());
            var users = Pool.AllUser();
            users.ForEach(item => Console.WriteLine(item.ToString()));
            Console.WriteLine("total :{0}", users.Aggregate(0l, (l, info) => l += info.Count));
        }

        [TestMethod]
        public void ParallAllocate()
        {

            using (var source = new CancellationTokenSource())
            {

                var token = source.Token;

                ThreadPool.QueueUserWorkItem(state =>

                {
                    var s = state as CancellationTokenSource;
                    Thread.Sleep(5000);
                    s.Cancel();
                }, source);
                try
                {
                    Parallel.For(0, 10, new ParallelOptions() { CancellationToken = token }, (i) =>
                {
                    while (!token.IsCancellationRequested)
                    {

                        var u = Pool.GetFreeUser();
                        Pool.ReleaseUser(u);
                    }
                });
                }
                catch (OperationCanceledException ex)
                {
                    // ... handle the loop cancellation
                }


            }
        }
    }
}
