using System;
using System.ComponentModel;
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
        protected bool _AllowDataRefresh = true;
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



        #region refresh-data region
        /// <summary>
        /// Check if the refresh is active
        /// </summary>
        public bool AllowDataRefresh
        {
            get { return _AllowDataRefresh; }
            set { _AllowDataRefresh = value; }
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
        /// start the refresh-mechanism if AllowDataRefresh is true
        /// </summary>
        public void StartRefreshing()
        {
            if (_AllowDataRefresh)
            {
                _RefreshTimer.Start();
            }
        }

        /// <summary>
        /// stop the refresh mechanism
        /// </summary>
        public void StopRefreshing()
        {
            _RefreshTimer.Stop();
        }

        private void _RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (_AllowDataRefresh)
            {
                RefreshList();
                if (ListRefreshed != null)
                {
                    ListRefreshed.Invoke();
                }
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
