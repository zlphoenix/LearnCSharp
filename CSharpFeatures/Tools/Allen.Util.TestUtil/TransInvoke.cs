using System;
using System.Transactions;
using TelChina.AF.Util.Logging;

namespace TelChina.AF.Util.TestUtil
{
    public class TransInvoke
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(TransInvoke).FullName);

        public static T InvokTransFunction<T>(Func<T> func, bool needCommit = true,
         bool reThrowExceptions = true, Action<Transaction> assert = null,
            TransactionScopeOption tOption = TransactionScopeOption.Required)
        {
            T result = default(T);

            using (var trans = new TransactionScope(tOption, new TimeSpan(1, 1, 1, 0)))
            {
                if (Transaction.Current != null)
                {
                    Transaction.Current.TransactionCompleted +=
                        new TransactionCompletedEventHandler((obj, args) =>
                                                                 {
                                                                     Logger.Debug("外部事务结束:");
                                                                     LogTransactionInfo(args.Transaction);
                                                                 });
                }

                Logger.Debug("执行前的外部事务属性:");
                LogTransactionInfo(Transaction.Current);
                try
                {
                    result = func();
                }
                catch (Exception e)
                {
                    if (reThrowExceptions)
                        throw;
                    else
                    {
                        Logger.Error(e);
                    }
                }

                Logger.Debug("执行后事务提交前的外部事务属性:");
                LogTransactionInfo(Transaction.Current);

                if (assert != null)
                {
                    assert(Transaction.Current);
                }
                if (needCommit)
                {
                    try
                    {
                        trans.Complete();
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug("事务已经在内部方法中释放");
                        Logger.Error(ex);
                    }
                }

            }
            return result;
        }
        private static void LogTransactionInfo(Transaction trans)
        {
            if (trans != null)
            {
                Logger.Debug(string.Format("事务属性:\t事务状态:{0}\t创建时间{1}\t分布式标识{2}\t本地标识{3}",
                                                trans.TransactionInformation.Status,
                                                trans.TransactionInformation.CreationTime,
                                                trans.TransactionInformation.DistributedIdentifier,
                                                trans.TransactionInformation.LocalIdentifier));
            }
            else
            {
                Logger.Debug("当前位置工作在无事务状态");
            }
        }
    }
}
