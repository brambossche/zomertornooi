using System;
using System.ComponentModel;
using System.Windows.Forms;
using Factory;
using log4net;
using structures.Views.ListRefreshEngine;
using Marb.Extender.Datgridview;

namespace structures.Views
{
    public partial class UC_wedstrijdViewer : UserControl
    {
        private ActiveBindingList<Wedstrijd> _WedstrijdList;
        protected static readonly ILog logger = LogManager.GetLogger(typeof(UC_wedstrijdViewer));
        protected BindingListRefresh<Wedstrijd> _BindingListRefresh;
        protected BindingList<Wedstrijd> _inputlist;

        public UC_wedstrijdViewer(ActiveBindingList<Wedstrijd> WedstrijdList)
        {
            InitializeComponent();
            _WedstrijdList = WedstrijdList;
            _WedstrijdList.ListChanged += _WedstrijdList_ListChanged;

            dgv_wedstrijden.DoubleBuffered(true);
            dgv_wedstrijden.DataSource = _WedstrijdList;
            dgv_wedstrijden.Refresh();
            this.Leave += UC_Leave;
            this.Enter += UC_Enter;
        }

        void _WedstrijdList_ListChanged(object sender, ListChangedEventArgs e)
        {
            dgv_wedstrijden.DataSource = _WedstrijdList;
            dgv_wedstrijden.Refresh();
        }

        private void UC_wedstrijdViewer_Load(object sender, EventArgs e)
        {
            dgv_wedstrijden.DoubleBuffered(true);
            dgv_wedstrijden.DataSource = _WedstrijdList;
            dgv_wedstrijden.Refresh();
            this.Leave += UC_Leave;
            this.Enter += UC_Enter;
        }





        private void UC_Enter(object sender, EventArgs e)
        {
            if (_BindingListRefresh != null)
            {
                if (_BindingListRefresh.AllowDataRefresh)
                {
                    _BindingListRefresh.StartRefreshing();
                }
            }
        }

        private void UC_Leave(object sender, EventArgs e)
        {
            if (_BindingListRefresh != null)
            {
                _BindingListRefresh.StopRefreshing();
            }
        }


    }
}
