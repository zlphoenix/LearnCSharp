using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using Allen.Util.Logging;

namespace Allen.Util.TestUtil
{
    /// <summary>
    /// 计时器
    /// </summary>
    public static class CodeTimer
    {
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time("", 1, () => { });
        }
        /// <summary>
        /// 按照倍增系数批量执行循环测试
        /// </summary>
        /// <param name="name">测试名称</param>
        /// <param name="iterationMax">最大执行迭代数，如果小于min，直接返回，不执行</param>
        /// <param name="iterationMin">最第一轮轮执行迭代数</param>
        /// <param name="multiple">每轮倍增系数</param>
        /// <param name="action">需要测试执行的Action</param>
        public static void Time(string name, int iterationMax, int iterationMin, int multiple, Action action)
        {
            for (; iterationMin < iterationMax; iterationMin *= multiple)
            {
                Time(string.Format("action,{0},{1}", name, iterationMin), iterationMin, action);
            }
        }
        public static void Time(string name, int iteration, Action action)
        {
            if (String.IsNullOrEmpty(name)) return;
            ILogger logger = LogManager.GetLogger("CodeTimer");
            // 1.
            //ConsoleColor currentForeColor = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            logger.Debug(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.
            //Console.ForegroundColor = currentForeColor;
            logger.Debug("\tTime Elapsed,\t" + watch.ElapsedMilliseconds.ToString("D0") + "ms");
            logger.Debug("\tCPU Cycles,\t" + cpuCycles.ToString("D0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                logger.Debug("\tGen " + i + ", \t\t" + count.ToString("D0"));
            }

            //Console.WriteLine();
        }
        public static void Time<TParam, TResult>(string name, int iterationMax, int iterationMin, int multiple, Func<TParam, TResult> action, TParam param) where TResult : class
        {
            for (; iterationMin < iterationMax; iterationMin *= multiple)
            {
                Time(string.Format("action,{0},{1}", name, iterationMin), iterationMin, action, param);
            }
        }
        public static TResult Time<TParam, TResult>(string name, int iteration, Func<TParam, TResult> action, TParam param) where TResult : class
        {
            if (String.IsNullOrEmpty(name)) return null;
            ILogger logger = LogManager.GetLogger("CodeTimer");
            // 1.
            //ConsoleColor currentForeColor = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            logger.Debug(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TResult result = null;
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) result = action(param);
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.
            //Console.ForegroundColor = currentForeColor;
            logger.Debug("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            logger.Debug("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                logger.Debug("\tGen " + i + ": \t\t" + count);
            }
            return result;
            //Console.WriteLine();
        }


        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();


    }
}
