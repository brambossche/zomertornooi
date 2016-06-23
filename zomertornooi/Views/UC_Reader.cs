using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory;
using Marb.Extender.Datgridview;
using structures.Views.ListRefreshEngine;

namespace structures.Views
{
    public partial class UC_Reader : UserControl
    {
        private ActiveBindingList<Wedstrijd> _wedstrijdlist;
        //Bindinglists for update
        protected BindingListRefresh<Wedstrijd> _BindingListRefreshWedstrijd;

        public UC_Reader(ActiveBindingList<Wedstrijd> wedstrijdlist)
        {
            InitializeComponent();
            _wedstrijdlist = wedstrijdlist;
            _wedstrijdlist.onListSizeChanged += _wedstrijdlist_onListSizeChanged;
            //_wedstrijdlist.ListChanged += _wedstrijdlist_ListChanged;
            _BindingListRefreshWedstrijd = new BindingListRefresh<Wedstrijd>(_wedstrijdlist);
            _BindingListRefreshWedstrijd.ListRefreshed += _BindingListRefreshWedstrijd_ListRefreshed;
            dgv_Wedstrijden.DataSource = _wedstrijdlist;
            dgv_Wedstrijden.DoubleBuffered(true);
        }

        void _wedstrijdlist_onListSizeChanged()
        {
            dgv_Wedstrijden.DataSource = null;
            dgv_Wedstrijden.DataSource = _wedstrijdlist;
            UpdateWedstrijden();
        }




        void _wedstrijdlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (_wedstrijdlist.Count > 0 & _wedstrijdlist != null)
            {
                UpdateWedstrijden();
            }

        }

        private void dgv_Wedstrijden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _wedstrijdlist.ListChanged -= _wedstrijdlist_ListChanged;
                dgv_Wedstrijden.EndEdit();
                UpdateWedstrijden();
                _wedstrijdlist.ListChanged += _wedstrijdlist_ListChanged;
            }
            catch (Exception ee)
            {
                throw ee;
            }

          
        }

        public void UpdateWedstrijden()
        {
            

            try
            {
                CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
                WedstrijdManager.SuspendBinding();
                //_Wedstrijden = new BindingList<Wedstrijd>(_wedstrijdlist.Where(x => x.IsBusy == true && x.Isplayed == false).ToList());
                //dgv_Wedstrijden.DataSource = _Wedstrijden;
                for (int i = 0; i < dgv_Wedstrijden.Rows.Count; i++)
                {
                    Wedstrijd w = dgv_Wedstrijden.Rows[i].DataBoundItem as Wedstrijd;
                    if (w.IsBusy && !w.Isplayed)
                    {
                        dgv_Wedstrijden.Rows[i].Visible = true;
                        if (w.IsStarted)
                        {
                            dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        }
                        else
                        {
                            dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.Coral;
                        }

                    }
                    else
                    {
                        dgv_Wedstrijden.Rows[i].Visible = false;
                    }


                }

                dgv_Wedstrijden.Refresh();
                dgv_Wedstrijden.Update();
                
                    foreach (DataGridViewColumn column in dgv_Wedstrijden.Columns)
                    {
                        if (column.Index <= 5)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            column.ReadOnly = true;
                        }
                        else
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            column.ReadOnly = true;
                        }

                    }
                dgv_Wedstrijden.Columns["IsStarted"].ReadOnly = false;
                WedstrijdManager.ResumeBinding();
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        private void UC_Reader_Load(object sender, EventArgs e)
        {
            //dgv_Wedstrijden.DoubleBuffered(true);
            UpdateWedstrijden();
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        #region refresh data

        void _BindingListRefreshWedstrijd_ListRefreshed()
        {
            RefreshList();
            UpdateWedstrijden();
        }


        private void RefreshList()
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.RefreshList();
                dgv_Wedstrijden.Refresh();
                dgv_Wedstrijden.Update();
                Console.WriteLine("joepi");
            }
        }

        private void dgv_Wedstrijden_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _BindingListRefreshWedstrijd.StopRefreshing();
        }

        private void dgv_Wedstrijden_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _BindingListRefreshWedstrijd.StartRefreshing();
        }

        private void dgv_Wedstrijden_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_Wedstrijden_Leave(object sender, EventArgs e)
        {

        }
        #endregion

        private void UC_Reader_Enter(object sender, EventArgs e)
        {
            UpdateWedstrijden();
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.StartRefreshing();
            }
        }

        private void UC_Reader_Leave(object sender, EventArgs e)
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.StopRefreshing();
            }

        }


    }
}
