using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg.Db;

namespace ProgramDefinitions
{

    public enum DBDialects
    {
        MsSql2000Dialect,
        MsSql2005Dialect,
        MsSql2008Dialect,
        MsSql2012Dialect

    }


    public class DataBaseConfiguration
    {

        NHibernate.Dialect.Dialect _dialect;

        public NHibernate.Dialect.Dialect Dialect
        {
            get { return _dialect; }
            set { _dialect = value; }
        }


        private string _server = "";

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private string _database = "";

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        private string _username = "";

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password = "";

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


    }



    public class Databaseconfig
    {
        /// <summary>
        /// disable ShowSQL as it slows down enormous in debug mode
        /// Debug using the udpappender
        /// http://stackoverflow.com/questions/9946309/nhibernate-3-2-how-to-turn-off-show-sql
        /// </summary>
        /// 
       // Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\proj\zomertornooi\Databases\TestDB\UnitHibernateTest.mdf;Integrated Security = True; Connect Timeout = 30
        public static IPersistenceConfigurer DB_UnitHibernateTest = MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\proj\zomertornooi\Databases\TestDB\UnitHibernateTest.mdf;Integrated Security=True;Connect Timeout=30");
        //public static IPersistenceConfigurer DB_ZomerTornooi = MsSqlConfiguration.MsSql2012.ConnectionString(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\data\proj\zomertornooi\Databases\Tornooi\ZomerTornooi.mdf;Integrated Security=True;Connect Timeout=30");

        public Databaseconfig()
        {

            DataBaseConfiguration DB_ZomerTornooi = new DataBaseConfiguration()
            {
            };
        }


    }



    public enum Geslacht
    {
        Dames,
        Heren,
        Mixed
    }

    public enum Niveau
    {
        A,
        B,
        C
    }

    public enum Status
    {
        Gespeeld,
        Scheidsrechter,
        Rust
    }

    public enum TornooiFormule
    {
        RoundRobin,
        PlacementGames,
        CrossFinals
    }


}
