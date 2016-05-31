using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhibernateIntf;
using Views;
using ProgramDefinitions;
using NHibernate;
using FluentNHibernate.Cfg.Db;
namespace Factory
{

    /// <summary>
    /// setup the connection to the database
    /// Control the session
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DatabaseSetup<T>
    {
        public ISession session = null;
        public DatabaseSetup(IPersistenceConfigurer PersistenceConfigurer)
        {
            try
            {
                //connect to the wished database
                NHibernateSessionManager<T>.Connection = PersistenceConfigurer;
                //open a session
                session = NHibernateSessionManager<T>.OpenSession();
            }
            catch (Exception e)
            {
                throw new Exception("DatabaseSetup failed", e);
            }
        }
    }
}
