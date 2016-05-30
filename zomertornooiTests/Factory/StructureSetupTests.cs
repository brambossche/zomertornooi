using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using structures.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateHelper;
using Views;
using FluentNHibernate.Cfg.Db;
using Factory;
using ProgramDefinitions;
using NhibernateIntf;
using NHibernate;
using zomertornooiTests.Dummies;



namespace structures.Factory.Tests
{
    
    [TestClass()]
    public class StructureSetupTests
    {
        IPersistenceConfigurer connection = Databaseconfig.DB_UnitHibernateTest;
        DataAccessLayer _DataAccessLayer = null;

        [TestInitialize()]
        public void setup()
        {
            ISession _session = null;
            try
            {

                NHibernateSessionManager.Connection = connection;
                _session = NHibernateSessionManager<BaseForm>.OpenSession();
                Assert.AreNotEqual(null, _session);
                _DataAccessLayer = new DataAccessLayer(_session);
                Assert.AreNotEqual(null, _DataAccessLayer);
                //_DataAccessLayer.CleanUpTable<Terrein>();
                
                //_DataAccessLayer.CleanUpTable<Persoon>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }





        }


        [TestMethod]
        public void CreateDummyPersonList()
        {

           /* try
            {
                List<Terrein> TerreinList = new List<Terrein>()
                {
                    new Terrein(){  TerreinNr = 1, Status = false}, 
                    new Terrein(){  TerreinNr = 2, Status = true}, 
                };
                _DataAccessLayer.SaveList<Terrein>(TerreinList);


            }
            catch (Exception e)
            {
                Assert.Fail("Exception : " + e.ToString() + "\r\n");
            }
            */

            _DataAccessLayer.CleanUpTable<Ploeg>();
            _DataAccessLayer.CleanUpTable<Persoon>();
            try
            {
                DummyData dummy = new DummyData();
                List<Ploeg> ploeglist = dummy.CreateDummyPloegen(100);
                _DataAccessLayer.SaveList<Ploeg>(ploeglist);
            }
            catch (Exception e)
            {
                Assert.Fail("Exception : " + e.ToString() + "\r\n");
            }
        }

        [TestMethod()]
        public void LoadDataTest()
        {
            try
            {
                IList<Ploeg> ploeglist = _DataAccessLayer.RetrieveAll<Ploeg>().ToList<Ploeg>();
                Console.WriteLine("Nr of ploegen : " + ploeglist.Count);
                if (ploeglist.Count > 0)
                {
                    Console.WriteLine(ploeglist[1].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
        }
    }
}
