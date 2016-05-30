using System;
using System.Windows.Forms;
using log4net;
using structures;
using System.ComponentModel;
using structures.Views.ListRefreshEngine;
using Marb.Extender.Datgridview;

namespace Views
{

    /// <summary>
    /// A specific user control with Extendeddatagridview and refresh mechanism for updating binding list
    /// Contains an interface to log info - errors
    /// Has a binding refresh mechanism
    /// Needs a binding list as input
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Userview<T> : UserControl
    {
        protected static readonly ILog logger = LogManager.GetLogger(typeof(Userview<T>));
        protected BindingListRefresh<T> _BindingListRefresh;
        protected BindingList<T> _inputlist;
        public Userview(BindingList<T> list, bool AllowUserToAddRows = true) 
        {
            try
            {
                _inputlist = list;
                _BindingListRefresh = new BindingListRefresh<T>(_inputlist);
                _BindingListRefresh.ListRefreshed += _BindingListRefresh_ListRefreshed;
                InitializeComponent();                
            }
            catch (Exception e)
            {
                logger.Error(this.Name, e);
            }

            logger.Info(this.Name + " loaded");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                extendDataGridView1.DataSource = _inputlist;
                extendDataGridView1.AllowUserToAddRows = AllowUserToAddRows;
                extendDataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                extendDataGridView1.MultiSelect = false;
                extendDataGridView1.Dock = DockStyle.Fill;
                extendDataGridView1.AutoSize = true;
                extendDataGridView1.DoubleBuffered(true);
                //adding drop down list items for every column which is of type category
                extendDataGridView1.AddDropDownBox(new Category());
                extendDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                //attach events
                //extendDataGridView1.RowsAdded += extendDataGridView1_RowsAdded;
                //extendDataGridView1.CellClick += _Grid_CellClick;
                extendDataGridView1.CellBeginEdit += extendDataGridView1_CellBeginEdit;
                extendDataGridView1.CellEndEdit += extendDataGridView1_CellEndEdit;
                _inputlist.ListChanged += _inputlist_ListChanged;
                _BindingListRefresh.StartRefreshing();



            }
            catch (Exception ee)
            {
                logger.Error(this.Name, ee);
            }
        }


        public bool AllowUserToAddRows
        {
            get { return extendDataGridView1.AllowUserToAddRows; }
            set { extendDataGridView1.AllowUserToAddRows = value; }
        }

        /// <summary>
        /// property which allows data refresh on the datagridview
        /// </summary>
        public bool AllowDataRefresh
        {
            get { return _BindingListRefresh.AllowDataRefresh; }
            set { _BindingListRefresh.AllowDataRefresh = value; }
        }

        private void _inputlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            extendDataGridView1.Refresh();
        }

        /// <summary>
        /// attach a binding list to the datasource of the datagridview
        /// </summary>
        public BindingList<T> DataSource
        {
            get { return _inputlist; }
            set
            {
                _inputlist = value;
                extendDataGridView1.DataSource = null;
                extendDataGridView1.DataSource = _inputlist;
                _inputlist.ResetBindings();
                extendDataGridView1.Refresh();
                extendDataGridView1.Update();             
            }
        }




        /*void _Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool attributefound = false;
                //get the sinle selected object
                T obj = _inputlist[e.RowIndex];
                //get the selected propertyname from the column
                string propname = ((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName;
                //get all properties 
                PropertyInfo _PropertyInfo = obj.GetType().GetProperties().ToList().Where(x => x.Name == propname).First();

                foreach (CustomAttributeData attr in _PropertyInfo.CustomAttributes)
                {
                    if (attr.AttributeType == typeof(PropertyGridViewer))
                    {
                        attributefound = true;
                        if ((bool)attr.NamedArguments.ToList().Where(x => x.MemberName == "ShowInPropertyGrid").First().TypedValue.Value)
                        {
                            _BindingListRefresh.StopRefreshing();
                            PG_details.SelectedObject = ((DataGridView)sender).CurrentCell.Value;
                            PG_details.Visible = true;
                            PG_details.ExpandAllGridItems();
                            break;
                        }
                        else
                        {
                            PG_details.Visible = false;
                        }
                    }
                }
                if (!attributefound)
                {
                    PG_details.SelectedObject = ((DataGridView)sender).CurrentCell.Value;
                    PG_details.Visible = false;
                }
            }
            catch (Exception ee)
            {
                logger.Error(this.Name, ee);
            }
        }*/


        #region refresh data        

        private void extendDataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefresh.StopRefreshing();
        }

        private void extendDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefresh.StartRefreshing();
        }

        private void _BindingListRefresh_ListRefreshed()
        {
            RefreshList();
        }

        private void btn_refreshlist_Click(object sender, EventArgs e)
        {
            RefreshList();

        }
        private void RefreshList()
        {
            if (_BindingListRefresh != null)
            {
                _BindingListRefresh.RefreshList();
                extendDataGridView1.Refresh();
                extendDataGridView1.Update();
            }
        }

        private void Userview_Enter(object sender, EventArgs e)
        {
            if (_BindingListRefresh != null)
            {
                _BindingListRefresh.StartRefreshing();
            }
        }

        private void Userview_Leave(object sender, EventArgs e)
        {
            if (_BindingListRefresh != null)
            {
                _BindingListRefresh.StopRefreshing();
            }
        }
        #endregion
    }
}
