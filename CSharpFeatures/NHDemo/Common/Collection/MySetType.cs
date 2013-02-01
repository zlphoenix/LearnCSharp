using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Engine;
using NHibernate.UserTypes;
using NHibernate.Collection;

namespace NHDemo.Common.Collection
{
    public class MySetType<T> : IUserCollectionType
    {
        #region IUserCollectionType Members

        public bool Contains(object collection, object entity)
        {
            return ((IMySet<T>)collection).Contains((T)entity);
        }

        public IEnumerable GetElements(object collection)
        {
            return (IEnumerable)collection;
        }

        public object IndexOf(object collection, object entity)
        {
            throw new NotImplementedException(); // 作为Set不需要这个方法
        }

        public object Instantiate(int anticipatedSize)
        {
            return new MySet<T>();
        }

        public IPersistentCollection Instantiate(ISessionImplementor session, NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            return new PersistentMySet<T>(session);
        }
        public object ReplaceElements(object original, object target, NHibernate.Persister.Collection.ICollectionPersister persister, object owner, IDictionary copyCache, ISessionImplementor session)
        {
            var result = (MySet<T>)target;

            result.Clear();
            foreach (var item in (IEnumerable<T>)original)
            {
                result.Add(item);
            }
            return result;
        }
        public IPersistentCollection Wrap(ISessionImplementor session, object collection)
        {
            return new PersistentMySet<T>(session, (IMySet<T>)collection);
        }

        #endregion
    }
}
