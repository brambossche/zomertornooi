using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhibernateIntf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Cfg.Db;
using Views;
using structures;
using NHibernate;
using ProgramDefinitions;


namespace NHibernateHelper.Tests
{
    [TestClass()]
    public class NHibernateIntfTests
    {

        IPersistenceConfigurer connection = Databaseconfig.DB_UnitHibernateTest;
        DataAccessLayer _DataAccessLayer = null;

        [TestInitialize()]
        public void OpenConnection()
        {
            ISession _session = null;
            try
            {

                NHibernateSessionManager<BaseForm>.Connection = connection;
                _session = NHibernateSessionManager<BaseForm>.OpenSession();
                Assert.AreNotEqual(null, _session);
                _DataAccessLayer = new DataAccessLayer(_session);
                Assert.AreNotEqual(null, _DataAccessLayer);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
            try
            {

                for (int i = 0; i < 4; i++)
                {
                    Persoon pers = new Persoon()
                    {
                        Naam = "VDB",
                        Voornaam = "Bram" + i.ToString(),

                            Straat = "Brugsestraat",
                            Nr = "113 - bus 3.01",
                            Postcode = "8020",
                            Woonplaats = "Oostkamp",
                            Land = "Belgie",
                        

   
                            Email = "bramvdbossche@gmail.com",
                            GSMNr = "0000",
                            TelNr = "000001"
                        
                    };

                    
                    _DataAccessLayer.AddRecord<Persoon>(pers);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
        }

        [TestMethod()]
        public void CreateRecordTest()
        {
            try
            {
                Persoon pers = new Persoon()
                {
                    Naam = "Van Den Bossche",
                    Voornaam = "Bram",
                    Straat = "Brugsestraat",
                    Nr = "113 - bus 3.01",
                    Postcode = "8020",
                    Woonplaats = "Oostkamp",
                    Land = "Belgie",
                    Email = "bramvdbossche@gmail.com",
                    GSMNr = "0000",
                    TelNr = "000001"
                    
                };

                _DataAccessLayer.AddRecord<Persoon>(pers);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
        }


        [TestMethod()]
        public void RetrieveAllTest()
        {
            try
            {
                IList<Persoon> persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                Console.WriteLine("Nr of persons : " + persoonlist.Count);
                if (persoonlist.Count > 0)
                {
                    Console.WriteLine( persoonlist[1].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }            
        }


        [TestMethod()]
        public void DeleteTest()
        {

            try
            {

                IList<Persoon> persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                int initialcount = persoonlist.Count();

                if (persoonlist.Count > 1)
                {
                    _DataAccessLayer.Delete<Persoon>(persoonlist[1]);
                }

                persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                
                Assert.AreNotEqual(initialcount, persoonlist.Count());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());

                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
        }

        [TestMethod()]
        public void DeleteListTest()
        {
            try
            {
                IList<Persoon> persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                _DataAccessLayer.DeleteList<Persoon>(persoonlist);
            }
            catch (Exception e)
            {                
                Assert.Fail("Exception : " + e.ToString() + "\r\n" + e.InnerException.ToString());
            }
        }
        
        [TestMethod()]
        public void CleanUpTableTest()
        {
            try
            {

                _DataAccessLayer.CleanUpTable<Ploeg>();

                IList<Persoon> persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                int persoonlistcount = persoonlist.Count();
                if (persoonlistcount > 0)
                {
                    _DataAccessLayer.CleanUpTable<Persoon>();
                    persoonlist = _DataAccessLayer.RetrieveAll<Persoon>().ToList<Persoon>();
                    Assert.AreNotEqual(persoonlist.Count, persoonlistcount);
                }
                else
                {
                    Assert.Fail("No data to be cleaned - please add data to the tables");
                }
                
            }
            catch (Exception e)
            {
                Assert.Fail("Exception : " + e.ToString() + "\r\n");
            }
        }




    }
}
