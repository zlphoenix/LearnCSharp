using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Threading
{
    public interface IPool
    {
        UserInfo GetFreeUser();
        void ReleaseUser(UserInfo user);

        int FreeCount();

        List<UserInfo> AllUser();
    }

    public class DualPool : IPool
    {
        private readonly ConcurrentDictionary<string, UserInfo> freePool = new ConcurrentDictionary<string, UserInfo>();
        private readonly ConcurrentDictionary<string, UserInfo> inUseInfo = new ConcurrentDictionary<string, UserInfo>();

        public DualPool()
        {
            for (var i = 0; i < 10; i++)
            {
                var user = new UserInfo()
                {
                    UserCode = "User" + i.ToString().PadLeft(3, '0'),
                    Pwd = "aaaaaa"
                };
                freePool.TryAdd(user.UserCode, user);
            }
        }

        public UserInfo GetFreeUser()
        {
            var count = 0;
            UserInfo user;
            do
            {
                count++;
                var item = freePool.FirstOrDefault();
                //if (string.IsNullOrEmpty(item.Key))
                //    throw new Exception("没有可用的用户");
                if (!freePool.TryRemove(item.Key, out user))
                    continue;


                inUseInfo.TryAdd(user.UserCode, user);
                user.Count++;
            } while (user == null);

            //Debug.WriteLine("User {0} is taken,try for {1} items", user.UserCode, count);
            return user;
        }

        public void ReleaseUser(UserInfo user)
        {
            Debug.Assert(inUseInfo.TryRemove(user.UserCode, out var innerUser));
            Debug.Assert(freePool.TryAdd(user.UserCode, user));
            Debug.WriteLine("User {0} is back", user.UserCode, "");
        }

        public int FreeCount()
        {
            return freePool.Count;
        }

        public List<UserInfo> AllUser()
        {
            return freePool.Values.ToList();
        }
    }

    public class SinglePool : IPool
    {
        public SinglePool()
        {
            for (var i = 0; i < 10; i++)
            {
                var user = new UserInfo()
                {
                    UserCode = "User" + i.ToString().PadLeft(3, '0'),
                    Pwd = "aaaaaa"
                };
                freePool.TryAdd(user.UserCode, user);
            }
        }
        private readonly ConcurrentDictionary<string, UserInfo> freePool = new ConcurrentDictionary<string, UserInfo>();

        public UserInfo GetFreeUser()
        {
            var count = 0;
            UserInfo user;
            do
            {
                //count++;
                var item = freePool.FirstOrDefault();
                //if (string.IsNullOrEmpty(item.Key))
                //    throw new Exception("没有可用的用户");
                if (!freePool.TryRemove(item.Key, out user))
                    continue;

                //Debug.WriteLine("User {0} is taken,try for {1} items", user.UserCode, count);
                user.Count++;
                return user;
            } while (user == null);
            return user;
        }

        public void ReleaseUser(UserInfo user)
        {
            Debug.Assert(freePool.TryAdd(user.UserCode, user));
        }

        public int FreeCount()
        {
            return freePool.Count;
        }

        public List<UserInfo> AllUser()
        {
            return freePool.Values.ToList();
        }
    }

    public class UserInfo
    {
        public string UserCode { get; set; }
        public string Pwd { get; set; }

        public long Count { get; set; }
        //public bool InUse { get; set; }

        public override string ToString()
        {
            return $"User:{UserCode}\t Count:{Count}";
        }
    }
}
