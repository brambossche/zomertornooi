using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;


namespace NhibernateIntf
{

    /// <summary>
    /// Class provides the connection and creates the sessionfactory 
    /// from the sessionfactory open session can be called
    /// </summary>

    public class NHibernateSessionManager<T>
    {

        private static IPersistenceConfigurer _connection = null;
        private static ISessionFactory _sessionFactory;

        public NHibernateSessionManager(IPersistenceConfigurer PersistenceConfigurer)

        {
            try
            {
                _connection = PersistenceConfigurer;
            }
            catch (Exception e)
            {
                throw new Exception("DatabaseSetup failed", e);

            }
        }

        private void CreateStaticSessionManager()
        {
            try
            {
                if (_sessionFactory != null)
                {
                    throw new Exception("NHibernateSessionManager trying to init SessionFactory twice!");
                }
                else
                {
                    if (Connection == null)
                    {
                        Configuration cfg = new Configuration();
                        cfg.SetProperty("default_batch_fetch_size", "15");
                        _sessionFactory = cfg.Configure().BuildSessionFactory();
                    }
                    else
                    {
                        //log4net.Config.XmlConfigurator.Configure();
                        _sessionFactory = Fluently.Configure()
                        .Database(Connection)
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))                        
                        .ExposeConfiguration(cfg => cfg.Properties.Add("use_proxy_validator", "false")) //otherwise issue with nofity property event changed                        
                        .BuildSessionFactory();
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("NHibernateSessionManager initialization failed", ex);
            }
        }


        public ISessionFactory SessionFactory
        {
            get
            {
                try
                {
                    if (_sessionFactory == null)
                    {
                        CreateStaticSessionManager();
                    };
                    return _sessionFactory;
                }
                catch (Exception e)
                {
                    throw new Exception("NHibernateSessionManager OpenSession failed", e);
                }
            }
        }

        public IPersistenceConfigurer Connection
        {
            get
            {
                return _connection;
            }

            set
            {
                _connection = value;
            }
        }
    }
}
