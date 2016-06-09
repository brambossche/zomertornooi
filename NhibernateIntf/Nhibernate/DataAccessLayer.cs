using System;
using System.Collections.Generic;
using System.Threading;
using NHibernate;
using NHibernate.Criterion;
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
        private QueueManager _QueueManager;
        private static Semaphore Sem_session = new Semaphore(1,1);
        private Thread QueThreading;

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
            QueThreading.IsBackground = true;
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

        public void  AddRecord<T>(T objectdata, ISession Session)
        {
            _QueueManager.Add(new Action(() => Que_AddRecord(objectdata, Session)));
        }

        private void Que_AddRecord<T>(T objectdata, ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction transaction = Session.BeginTransaction())
                {
                    try
                    {
                        int reference = (int)Session.Save((T)objectdata);
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
        public void Delete<T>(T item, ISession Session)
        {
             _QueueManager.Add(new Action(() =>Que_Delete(item, Session)));
        }

        private void Que_Delete<T>(T item, ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = Session.BeginTransaction())
                {
                    try
                    {

                        if (Session.Contains(item))
                        {
                            Session.Delete(item);
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
        public void DeleteList<T>(IList<T> itemsToDelete, ISession Session)
        {
            _QueueManager.Add(() => Que_DeleteList<T>(itemsToDelete,Session));
        }
        private void Que_DeleteList<T>(IList<T> itemsToDelete, ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = Session.BeginTransaction())
                {
                    try
                    {
                        foreach (T item in itemsToDelete)
                        {
                            Session.Delete(item);
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
        public IList<T> RetrieveAll<T>(ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                Session.Flush();
                Session.Clear();
                // Retrieve all objects of the type passed in
                ICriteria targetObjects = Session.CreateCriteria(typeof(T));
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

        public IList<T> RefreshAll<T> ( ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                Session.Clear();                
                // Retrieve all objects of the type passed in
                ICriteria targetObjects = Session.CreateCriteria(typeof(T));
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
        public IList<T> RetrieveEquals<T>(string propertyName, object propertyValue,  ISession Session)
        {
            try
            {

                Sem_session.WaitOne();
                // Create a criteria object with the specified criteria
                ICriteria criteria = Session.CreateCriteria(typeof(T));
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
        public void Save<T>(T item, ISession Session)
        {
            _QueueManager.Add(new Action(() => Que_Save(item, Session)));
        }
        private void Que_Save<T>(T item, ISession Session)
        {
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = Session.BeginTransaction())
                {
                    try
                    {
                        if (Session.Contains(item))
                        {
                            Session.SaveOrUpdate(item);
                        }
                        else
                        {
                            Session.Save(item);                            
                        }
                        Session.Flush();
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

        public void SaveList<T>(IList<T> items, ISession Session)
        {
            _QueueManager.Add(new Action(() => Que_SaveList(items, Session)));
        }
        private void Que_SaveList<T>(IList<T> items, ISession Session)
        {
            Semaphore sem_items = new Semaphore(1, 1);
            try
            {
                Sem_session.WaitOne();
                using (ITransaction Transaction = Session.BeginTransaction())
                {                    
                    try
                    {
                        //Session.Clear();
                        //_session.FlushMode = FlushMode.Commit;
                        foreach (T item in items)
                        {
                            sem_items.WaitOne();
                            if (Session.Contains(item))
                            {
                                Session.SaveOrUpdate(item);                                
                            }
                            else
                            {
                                Session.Save(item);
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

        public void CleanUpTable<T>( ISession Session) 
        {
            _QueueManager.Add(new Action(() => Que_CleanUpTable<T>(Session)));
        }
        private void Que_CleanUpTable<T>( ISession Session)
        {
            try
            {
                //Sem_session.WaitOne();

                using (ITransaction Transaction = Session.BeginTransaction())
                {
                    try
                    {
                        Session.Clear();
                        Sem_session.WaitOne();
                        var metadata = Session.SessionFactory.GetClassMetadata(typeof(T)) as NHibernate.Persister.Entity.AbstractEntityPersister;
                        string table = metadata.TableName.Replace("[", "").Replace("]", "");
                            string deleteAll = "DELETE FROM " + table;
                        Session.CreateSQLQuery(deleteAll).ExecuteUpdate();
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
