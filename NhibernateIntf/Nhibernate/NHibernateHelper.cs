using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateHelper
{





    /// <summary>
    /// Specifies whether to begin a new session, continue an existing session, or end an existing session.
    /// </summary>
    public enum SessionAction { Begin, Continue, End, BeginAndEnd }

    public class NHibernateIntf<T>
    {
        private static ISessionFactory _sessionFactory = null;
        private ISession _Session = null;
        private IPersistenceConfigurer _Connection = null;

        public IPersistenceConfigurer Connectionn
        {
            get { return _Connection; }
            //set { _Connectionn = value; }
        }


        public NHibernateIntf(IPersistenceConfigurer Connectionn)
        {
            _Connection = Connectionn;
        }

        public bool OpenConnection()
        {
            try
            {
                if (_sessionFactory == null)
                {
                    InitializeSessionFactory(_Connection);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception("NHibernateHelper.OpenConnection", e);
            }
            return false;
        }



        private static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory(IPersistenceConfigurer connection)
        {
            //IPersistenceConfigurer SQLitePersistenceConfigurer = SQLiteConfiguration.Standard.UsingFile("Testproject.db");
            //IPersistenceConfigurer MsSql2008PersistenceConfigurer = MsSqlConfiguration.MsSql2008.ConnectionString(@"Server=(local);initial catalog=TestDB;user=sa;password=sa2008;").ShowSql();
            //IPersistenceConfigurer connection = MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Bram.vandenBossche\Documents\HibernateTest.mdf;Integrated Security=True;Connect Timeout=30").ShowSql();


            /*_sessionFactory = Fluently.Configure()
                .Database(connection)
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<T>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true, true))
                .BuildSessionFactory();*/

            _sessionFactory = Fluently.Configure()
                                        .Database(connection)
                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                                        .BuildSessionFactory();



            /*
            _sessionFactory = Fluently.Configure()
                .Database(connection)
                .Mappings(m =>
                          m.AutoMappings.Add(
                              AutoMap.AssemblyOf<Userview>()))
                .BuildSessionFactory();*/


        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }


        public bool CreateRecord<T>(T objectdata)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var reference = session.Save((T)objectdata);
                        transaction.Commit();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void Delete<T>(T item)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    using (session.BeginTransaction())
                    {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.Delete", e);
            }
        }

        /// <summary>
        /// Deletes objects of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void DeleteList<T>(IList<T> itemsToDelete)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    foreach (T item in itemsToDelete)
                    {
                        using (session.BeginTransaction())
                        {
                            session.Delete(item);
                            session.Transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.DeleteList", e);
            }
        }

        /// <summary>
        /// Retrieves all objects of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <returns>A list of all objects of the specified type.</returns>
        public IList<T> RetrieveAll<T>(SessionAction sessionAction)
        {
            /* Note that NHibernate guarantees that two object references will point to the
             * same object only if the references are set in the same session. For example,
             * Order #123 under the Customer object Able Inc and Order #123 in the Orders
             * list will point to the same object only if we load Customers and Orders in 
             * the same session. If we load them in different sessions, then changes that
             * we make to Able Inc's Order #123 will not be reflected in Order #123 in the
             * Orders list, since the references point to different objects. That's why we
             * maintain a session as a member variable, instead of as a local variable. */


            try
            {
                // Open a new session if specified
                if ((sessionAction == SessionAction.Begin) || (sessionAction == SessionAction.BeginAndEnd))
                {
                    _Session = _sessionFactory.OpenSession();
                }

                // Retrieve all objects of the type passed in
                ICriteria targetObjects = _Session.CreateCriteria(typeof(T));
                IList<T> itemList = targetObjects.List<T>();

                // Close the session if specified
                if ((sessionAction == SessionAction.End) || (sessionAction == SessionAction.BeginAndEnd))
                {
                    _Session.Close();
                    _Session.Dispose();
                }

                // Set return value
                return itemList;
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.RetrieveAll", e);
            }
        }

        /// <summary>
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<T> RetrieveEquals(string propertyName, object propertyValue)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    // Create a criteria object with the specified criteria
                    ICriteria criteria = session.CreateCriteria(typeof(T));
                    criteria.Add(Expression.Eq(propertyName, propertyValue));

                    // Get the matching objects
                    IList<T> matchingObjects = criteria.List<T>();

                    // Set return value
                    return matchingObjects;
                }
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.RetrieveEquals", e);
            }
        }



        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public void Save<T>(T item)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    using (session.BeginTransaction())
                    {
                        session.SaveOrUpdate(item);
                        session.Transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.RetrieveEquals", e);
            }
        }

        public void SaveList<T>(IList<T> items)
        {
            try
            {
                using (ISession session = _sessionFactory.OpenSession())
                {
                    foreach (T item in items)
                    {
                        using (session.BeginTransaction())
                        {
                            session.SaveOrUpdate(item);
                            session.Transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("NHibernateHelper.RetrieveEquals", e);
            }
        }
        public void CleanUpTable<T>()
        {
            var metadata = _sessionFactory.GetClassMetadata(typeof(T)) as NHibernate.Persister.Entity.AbstractEntityPersister;
            string table = metadata.TableName;

            using (ISession session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    string deleteAll = string.Format("DELETE FROM \"{0}\"", table);
                    session.CreateSQLQuery(deleteAll).ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }
    }
}

