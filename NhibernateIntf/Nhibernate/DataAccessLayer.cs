using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NhibernateIntf;
using Marb.Quemanager;


namespace NhibernateIntf
{
    /// <summary>
    /// Implementation of CRUD on a defined session
    /// this is a generic interface and does not depend on any type
    /// It works on a given session
    /// </summary>
    public class DataAccessLayer
    {
        public bool SessionRunning = true;
        private ISession _session = null;
        private QueueManager _QueueManager;
        private static Semaphore Sem_session = new Semaphore(1,1);
        private Thread QueThreading;
        public ISession Session
        {
            set
            {
                try
                {
                    _session = value;
                    
                }
                catch (HibernateException e)
                {
                    throw new Exception("DataAccesLayer session could not be made", e);
                }
            }
        }


        public delegate void del_QueItems();
        public event del_QueItems Que_HasItems;
        public event del_QueItems Que_IsEmpty;
        
        public DataAccessLayer()
        {
            bool QueWasEmpty = true;
            _QueueManager = new QueueManager();
          
            QueThreading = new Thread(() =>
            {
                while (SessionRunning)
                {
                    if (!(_QueueManager.IsEmpty))
                    {
                        if (QueWasEmpty == true)
                        {
                            _QueueManager_Que_HasItems();
                            QueWasEmpty = false;
                        }

                        object temp = _QueueManager.Pop();

                        if (temp.GetType() == typeof(Action))
                        {
                            ((Action)temp).Invoke();
                        }
                        else
                        {
                            ((Func<object>)temp).Invoke();
                        }                   
                    }
                    else
                    {
                        if (QueWasEmpty == false)
                        {
                            _QueueManager_Que_IsEmpty();
                        }
                        QueWasEmpty = true;
                        Thread.Sleep(200);
                    }
                }

            });
            QueThreading.Start();

        }

        void _QueueManager_Que_IsEmpty()
        {
            if (Que_IsEmpty != null)
            {
                Que_IsEmpty.Invoke();
            }
        }

        void _QueueManager_Que_HasItems()
        {
            if (Que_HasItems != null)
            {
                Que_HasItems.Invoke();
            }
        }

        public void  AddRecord<T>(T objectdata)
        {
            _QueueManager.Add(new Action(() => Que_AddRecord(objectdata)));
        }

        private void Que_AddRecord<T>(T objectdata)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    try
                    {
                        int reference = (int)_session.Save((T)objectdata);
                        //_session.Flush();
                        transaction.Commit();
                        Sem_session.Release(1);                        
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        transaction.Rollback();
                        Sem_session.Release(1);
                        throw new Exception("DataAccesLayer AddRecord failed", e);
                    }
                }                
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer AddRecord failed", e);
            }
        }

        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void Delete<T>(T item)
        {
             _QueueManager.Add(new Action(() =>Que_Delete(item)));
        }

        private void Que_Delete<T>(T item)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = _session.BeginTransaction())
                {
                    try
                    {

                        if (_session.Contains(item))
                        {
                            _session.Delete(item);
                        }
                        
                        Transaction.Commit();
                        Sem_session.Release(1);
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        Transaction.Rollback();
                        Sem_session.Release(1);
                        throw new Exception("DataAccesLayer Delete failed", e);
                    }
                }
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer AddRecord failed", e);
            }
        }

        /// <summary>
        /// Deletes objects of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void DeleteList<T>(IList<T> itemsToDelete)
        {
            _QueueManager.Add(() => Que_DeleteList<T>(itemsToDelete));
        }
        private void Que_DeleteList<T>(IList<T> itemsToDelete)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = _session.BeginTransaction())
                {
                    try
                    {
                        foreach (T item in itemsToDelete)
                        {
                            _session.Delete(item);
                            //_session.Flush();
                        }
                        Transaction.Commit();
                        Sem_session.Release(1);
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        Transaction.Rollback();
                        Sem_session.Release(1);
                        throw new Exception("DataAccesLayer DeleteList failed", e);
                    }
                }
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer AddRecord failed", e);
            }
        }

        /// <summary>
        /// Retrieves all objects of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <returns>A list of all objects of the specified type.</returns>
        public IList<T> RetrieveAll<T>()
        {
            try
            {
                Sem_session.WaitOne();
                //_session.Clear();
                // Retrieve all objects of the type passed in
                ICriteria targetObjects = _session.CreateCriteria(typeof(T));
                IList<T> retrievelist = targetObjects.List<T>();

                Sem_session.Release(1);
                // Set return value  
                return retrievelist;
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer RetrieveAll failed", e);
            }
        }

        public IList<T> RefreshAll<T> ()
        {
            try
            {
                Sem_session.WaitOne();
                _session.Clear();                
                // Retrieve all objects of the type passed in
                ICriteria targetObjects = _session.CreateCriteria(typeof(T));
                IList<T> retrievelist = targetObjects.List<T>();
                
                Sem_session.Release(1);
                return retrievelist;

            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer RetrieveAll failed", e);
            }
        }



        /// <summary>
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<T> RetrieveEquals<T>(string propertyName, object propertyValue)
        {
            try
            {

                Sem_session.WaitOne();
                // Create a criteria object with the specified criteria
                ICriteria criteria = _session.CreateCriteria(typeof(T));
                criteria.Add(Expression.Eq(propertyName, propertyValue));

                // Get the matching objects
                IList<T> matchingObjects = criteria.List<T>();

                Sem_session.Release(1);
                // Set return value
                return matchingObjects;
            }

            catch (NHibernate.HibernateException e)
            {
               Sem_session.Release(1);
                throw new Exception("DataAccesLayer RetrieveEquals failed", e);
            }
        }


        
        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public void Save<T>(T item)
        {
            _QueueManager.Add(new Action(() => Que_Save(item)));
        }
        private void Que_Save<T>(T item)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = _session.BeginTransaction())
                {
                    try
                    {
                        if (_session.Contains(item))
                        {
                            _session.SaveOrUpdate(item);
                        }
                        else
                        {
                            _session.Save(item);                            
                        }
                        _session.Flush();
                        Transaction.Commit();
                        Sem_session.Release(1);
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        Transaction.Rollback();
                        Sem_session.Release(1);
                        throw new Exception("DataAccesLayer Save failed", e);
                    }
                }
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer AddRecord failed", e);
            }
        }

        public void SaveList<T>(IList<T> items)
        {
            _QueueManager.Add(new Action(() => Que_SaveList(items)));
        }
        private void Que_SaveList<T>(IList<T> items)
        {
            Semaphore sem_items = new Semaphore(1, 1);
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = _session.BeginTransaction())
                {                    
                    try
                    {
                        _session.Clear();
                        //_session.FlushMode = FlushMode.Commit;
                        foreach (T item in items)
                        {
                            sem_items.WaitOne();
                            if (_session.Contains(item))
                            {
                                _session.SaveOrUpdate(item);                                
                            }
                            else
                            {
                                _session.Save(item);
                            }                            
                            sem_items.Release(1);
                        }                        
                        Transaction.Commit();
                        //_session.FlushMode = FlushMode.Auto;
                        Sem_session.Release(1);
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        Transaction.Rollback();
                        Sem_session.Release(1);
                        throw new Exception("DataAccesLayer SaveList failed", e);
                    }
                }
            }
            catch (NHibernate.HibernateException e)
            {
                Sem_session.Release(1);
                throw new Exception("DataAccesLayer AddRecord failed", e);
            }
            
        }

        public void CleanUpTable<T>() 
        {
            _QueueManager.Add(new Action(() => Que_CleanUpTable<T>()));
        }
        private void Que_CleanUpTable<T>()
        {
            try
            {
                //Sem_session.WaitOne();

                using (ITransaction Transaction = _session.BeginTransaction())
                {
                    try
                    {
                        Sem_session.WaitOne();
                        var metadata = _session.SessionFactory.GetClassMetadata(typeof(T)) as NHibernate.Persister.Entity.AbstractEntityPersister;
                        string table = metadata.TableName.Replace("[", "").Replace("]", "");
                            string deleteAll = "DELETE FROM " + table;
                            _session.CreateSQLQuery(deleteAll).ExecuteUpdate();
                            Transaction.Commit();                            
                       
                        Sem_session.Release(1);
                    }
                    catch (NHibernate.HibernateException e)
                    {
                        //Transaction.Rollback();
                        //Sem_session.Release(1);
                        //throw new Exception("DataAccesLayer CleanUpTable failed", e);
                    }

                }
            }
            catch (Exception ee)
            {
                Sem_session.Release(1);
                //throw new Exception("DataAccesLayer CleanUpTable failed", ee);
            }
        }
    }
}
