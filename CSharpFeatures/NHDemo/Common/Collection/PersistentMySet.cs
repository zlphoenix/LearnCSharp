using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Collection;

namespace NHDemo.Common.Collection
{
    public class PersistentMySet<T> : IPersistentCollection
    {
        private NHibernate.Engine.ISessionImplementor session;
        private IMySet<T> iMySet;

        public PersistentMySet(NHibernate.Engine.ISessionImplementor session, IMySet<T> iMySet)
        {
            // TODO: Complete member initialization
            this.session = session;
            this.iMySet = iMySet;
        }

        public PersistentMySet(NHibernate.Engine.ISessionImplementor session)
        {
            // TODO: Complete member initialization
            this.session = session;
        }

        public object Owner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object GetValue()
        {
            throw new NotImplementedException();
        }

        public bool RowUpdatePossible
        {
            get { throw new NotImplementedException(); }
        }

        public object Key
        {
            get { throw new NotImplementedException(); }
        }

        public string Role
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsUnreferenced
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDirty
        {
            get { throw new NotImplementedException(); }
        }

        public object StoredSnapshot
        {
            get { throw new NotImplementedException(); }
        }

        public bool Empty
        {
            get { throw new NotImplementedException(); }
        }

        public void SetSnapshot(object key, string role, object snapshot)
        {
            throw new NotImplementedException();
        }

        public void PostAction()
        {
            throw new NotImplementedException();
        }

        public void BeginRead()
        {
            throw new NotImplementedException();
        }

        public bool EndRead(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public bool AfterInitialize(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public bool IsDirectlyAccessible
        {
            get { throw new NotImplementedException(); }
        }

        public bool UnsetSession(NHibernate.Engine.ISessionImplementor currentSession)
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentSession(NHibernate.Engine.ISessionImplementor session)
        {
            throw new NotImplementedException();
        }

        public void InitializeFromCache(NHibernate.Persister.Collection.ICollectionPersister persister, object disassembled, object owner)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable Entries(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public object ReadFrom(System.Data.IDataReader reader, NHibernate.Persister.Collection.ICollectionPersister role, NHibernate.Loader.ICollectionAliases descriptor, object owner)
        {
            throw new NotImplementedException();
        }

        public object GetIdentifier(object entry, int i)
        {
            throw new NotImplementedException();
        }

        public object GetIndex(object entry, int i, NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public object GetElement(object entry)
        {
            throw new NotImplementedException();
        }

        public object GetSnapshotElement(object entry, int i)
        {
            throw new NotImplementedException();
        }

        public void BeforeInitialize(NHibernate.Persister.Collection.ICollectionPersister persister, int anticipatedSize)
        {
            throw new NotImplementedException();
        }

        public bool EqualsSnapshot(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public bool IsSnapshotEmpty(object snapshot)
        {
            throw new NotImplementedException();
        }

        public object Disassemble(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public bool NeedsRecreate(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ICollection GetSnapshot(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public void ForceInitialization()
        {
            throw new NotImplementedException();
        }

        public bool EntryExists(object entry, int i)
        {
            throw new NotImplementedException();
        }

        public bool NeedsInserting(object entry, int i, NHibernate.Type.IType elemType)
        {
            throw new NotImplementedException();
        }

        public bool NeedsUpdating(object entry, int i, NHibernate.Type.IType elemType)
        {
            throw new NotImplementedException();
        }

        public System.Collections.IEnumerable GetDeletes(NHibernate.Persister.Collection.ICollectionPersister persister, bool indexIsFormula)
        {
            throw new NotImplementedException();
        }

        public bool IsWrapper(object collection)
        {
            throw new NotImplementedException();
        }

        public bool WasInitialized
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasQueuedOperations
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerable QueuedAdditionIterator
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.ICollection GetQueuedOrphans(string entityName)
        {
            throw new NotImplementedException();
        }

        public void ClearDirty()
        {
            throw new NotImplementedException();
        }

        public void Dirty()
        {
            throw new NotImplementedException();
        }

        public void PreInsert(NHibernate.Persister.Collection.ICollectionPersister persister)
        {
            throw new NotImplementedException();
        }

        public void AfterRowInsert(NHibernate.Persister.Collection.ICollectionPersister persister, object entry, int i, object id)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ICollection GetOrphans(object snapshot, string entityName)
        {
            throw new NotImplementedException();
        }
    }
}
