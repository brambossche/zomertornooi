using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentNHibernate.Cfg.Db;
using Marb.Bindinglist;
using NHibernate;
using NhibernateIntf;
using structures;
using Views;

namespace Factory
{
    
    public class StructureSetup
    {

        private DataAccessLayer _DataAccessLayer = null;
        private ISessionFactory _SessionFactory = null;

        private ActiveBindingList<Ploeg> _PloegList = null;
        private ActiveBindingList<Persoon> _PersoonList = null;        
        private ActiveBindingList<Reeks> _ReeksList = null;
        private ActiveBindingList<Wedstrijd> _WedstrijdList = null;
        private ActiveBindingList<Terrein> _TerreinList = null;

        private ISession _Session = null;

        public delegate void del_QueItems();
        public event del_QueItems Que_HasItems;
        public event del_QueItems Que_IsEmpty;



        public ActiveBindingList<Terrein> TerreinList
        {
            get { return _TerreinList; }
            set { _TerreinList = value; }
        }

        public ActiveBindingList<Persoon> PersoonList
        {
            get { return _PersoonList; }
            set { _PersoonList = value; }
        }


        public ActiveBindingList<Ploeg> PloegList
        {
            get { return _PloegList; }
            set { _PloegList = value; }
        }

        public ActiveBindingList<Reeks> ReeksList
        {
            get { return _ReeksList; }
            set { _ReeksList = value; }
        }

        public ActiveBindingList<Wedstrijd> WedstrijdList
        {
            get { return _WedstrijdList; }
            set { _WedstrijdList = value; }
        }



        public StructureSetup(ISessionFactory SessionFactory)
        {

            _SessionFactory = SessionFactory;
            _DataAccessLayer = new DataAccessLayer();
            _DataAccessLayer.Que_HasItems += _DataAccessLayer_Que_HasItems;
            _DataAccessLayer.Que_IsEmpty += _DataAccessLayer_Que_IsEmpty;
            LoadData();
            
        }

        void _DataAccessLayer_Que_IsEmpty()
        {
            if (Que_IsEmpty != null)
            {
                Que_IsEmpty.Invoke();
            }
        }

        void _DataAccessLayer_Que_HasItems()
        {
            if (Que_HasItems != null)
            {
                Que_HasItems.Invoke();
            }
        }



        public bool LoadData()
        {
            try
            {
                ISession NewSessioncreator = _SessionFactory.OpenSession();
                _DataAccessLayer.Session = NewSessioncreator;
                _TerreinList = new ActiveBindingList<Terrein>(_DataAccessLayer, NewSessioncreator);


                NewSessioncreator = _SessionFactory.OpenSession();
                _DataAccessLayer.Session = NewSessioncreator;
                _PersoonList = new ActiveBindingList<Persoon>(_DataAccessLayer, NewSessioncreator);

                NewSessioncreator = _SessionFactory.OpenSession();
                _DataAccessLayer.Session = NewSessioncreator;
                _PloegList = new ActiveBindingList<Ploeg>(_DataAccessLayer, NewSessioncreator);

                NewSessioncreator = _SessionFactory.OpenSession();
                _DataAccessLayer.Session = NewSessioncreator;
                _WedstrijdList = new ActiveBindingList<Wedstrijd>(_DataAccessLayer, NewSessioncreator);

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }

}
