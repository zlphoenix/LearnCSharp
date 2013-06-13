using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using TelChina.AF.Util.Logging;

namespace TelChina.AF.Util.TestUtil
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
            logger.Debug("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            logger.Debug("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                logger.Debug("\tGen " + i + ": \t\t" + count);
            }

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
