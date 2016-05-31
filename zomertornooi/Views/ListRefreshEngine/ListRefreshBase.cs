using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory;

namespace structures.Views.ListRefreshEngine
{
    /// <summary>
    /// class to enable automatic refreshing of a binding list
    /// </summary>
    public class BindingListRefresh<T>
    {
        private BindingList<T> _inputlist;

        protected Timer _RefreshTimer;
        protected bool _RefreshEnabled = true;
        protected int _RefreshTime = 5;

        public delegate void del_Listrefreshed();
        /// <summary>
        /// event from timer tick when the list has been refreshed
        /// </summary>
        public event del_Listrefreshed ListRefreshed;
        
        public BindingListRefresh()
        {
            _RefreshTimer = new Timer();
            _RefreshTimer.Interval = _RefreshTime * 1000;
            _RefreshTimer.Tick += _RefreshTimer_Tick;
        }
        
        public BindingListRefresh(BindingList<T> Inputlist)
            :this()
        {
            _inputlist = Inputlist;
        }

        public BindingList<T> BindingList
        {
            get { return _inputlist; }
            set { _inputlist = value; }
        }



        #region refreshdata region
        /// <summary>
        /// enable or disable the refresh of tables from the database
        /// Only needed one multi users are accessing the same database
        /// This does not start the refresh mechanism
        /// use StartRefreshing to start the engine
        /// </summary>
        public bool RefreshEnabled
        {
            get { return _RefreshEnabled; }
            set
            {
                _RefreshEnabled = value;
                if (_RefreshEnabled)
                {
                    StartRefreshing();
                }
                else
                {
                    StopRefreshing();
                }
            }
        }


        /// <summary>
        /// Set the refresh rate in seconds
        /// This will cause traffic towards the database
        /// Units = seconds
        /// </summary>
        public int RefreshTime
        {
            get { return _RefreshTime; }
            set { _RefreshTime = value; }
        }

        /// <summary>
        /// start the refreshmechanism
        /// </summary>
        public bool StartRefreshing()
        {
            if (_RefreshEnabled)
            {
                                
                _RefreshTimer.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// stop the refresh mechanism
        /// </summary>
        public void StopRefreshing()
        {
            if (_RefreshTimer.Enabled)
            {
                _RefreshTimer.Stop();
            }
        }
        private void _RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshList();
            if (ListRefreshed != null)
            {
                ListRefreshed.Invoke();
            }
        }

        /// <summary>
        /// Function updating the list
        /// </summary>
        public virtual void RefreshList()
        {
            ((ActiveBindingList<T>) _inputlist).Refresh();
        }
        #endregion
    }
        
}
