using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marb.Bindinglist;
using NHibernate;
using NhibernateIntf;
using structures.Views.ListRefreshEngine;

namespace Factory
{

    public class ActiveBindingList<T> : ExtBindingList<T>
    {
        private DataAccessLayer _DataAccessLayer = null;
        private ISession _BindingSession = null;

        public ActiveBindingList(DataAccessLayer DataAcces, ISession BindingSession)
            : base(DataAcces.RetrieveAll<T>(BindingSession))
        {
            _BindingSession = BindingSession;
            _DataAccessLayer = DataAcces;
            ListChanged += ListChangedEventHandler;
            
        }

        /// <summary>
        /// Remove the old list from db and set the new table
        /// </summary>
        /// <param name="NewList"></param>
        public void SetList (IList<T> NewList)        
        {
            RaiseListChangedEvents = false;
            while (Count > 0)
            {
                Remove(this.Last()) ;
            }
            foreach (T item in NewList)
            {
                Add(item);
            }

           
            _DataAccessLayer.CleanUpTable<T>(_BindingSession);
            _DataAccessLayer.SaveList<T>(NewList, _BindingSession);
            RaiseListChangedEvents = true;
            ResetBindings();
        }

        public void Refresh()
        {

            RaiseListChangedEvents = false;
            while (Count > 0)
            {
                Remove(this.Last());
            }
            
            List<T> temp = new List<T>(_DataAccessLayer.RetrieveAll<T>(_BindingSession));
            //List<T> temp = new List<T>(_DataAccessLayer.RefreshAll<T>());
            foreach (T item in temp)
            {
                Add(item);
            }
            ResetBindings();
            RaiseListChangedEvents = true;            
        }

        private void ListChangedEventHandler(object sender, ListChangedEventArgs e)
        {
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.Reset:
                        {
                            try
                            {
                               
                                _DataAccessLayer.CleanUpTable<T>(_BindingSession);                                
                                if (((ActiveBindingList<T>)sender).Count > 0)
                                {
                                    _DataAccessLayer.SaveList<T>(((ActiveBindingList<T>)sender).ToList<T>(),_BindingSession);
                                }
                            }
                            catch (Exception ee)
                            {

                            }
                        }
                        break;
                    case ListChangedType.ItemAdded:
                        {
                            
                            _DataAccessLayer.Save<T>(this[(int)e.NewIndex], _BindingSession);
                        }break;

                    case ListChangedType.ItemChanged:
                        {
                            
                            _DataAccessLayer.Save<T>(this[(int)e.OldIndex], _BindingSession);
                        }break;
                    case ListChangedType.ItemDeleted:
                        {
                            //if (((ActiveBindingList<T>)sender).Count > 0)
                            {
                                
                                _DataAccessLayer.Delete<T>(_Itemwhichwillberemoved, _BindingSession);
                            }
                        }break;
                }                
            }
            catch
            { }
        }
    }

}
