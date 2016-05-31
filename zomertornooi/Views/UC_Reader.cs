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

namespace structures.Views
{
    public partial class UC_Reader : UserControl
    {
        private ActiveBindingList<Wedstrijd> _wedstrijdlist;
        private BindingList<Wedstrijd> _Wedstrijden;

        public UC_Reader(ActiveBindingList<Wedstrijd> wedstrijdlist)
        {
            InitializeComponent();
            _wedstrijdlist = wedstrijdlist;
            _wedstrijdlist.ListChanged += _wedstrijdlist_ListChanged;
            _Wedstrijden = new BindingList<Wedstrijd>(_wedstrijdlist.Where(x => x.IsBusy == true && x.Isplayed == false).ToList());
            UpdateWedstrijden();
        }

        void _wedstrijdlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateWedstrijden();
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
                _Wedstrijden = new BindingList<Wedstrijd>(_wedstrijdlist.Where(x => x.IsBusy == true && x.Isplayed == false).ToList());
                dgv_Wedstrijden.DataSource = _Wedstrijden;

                for (int i = 0; i < _Wedstrijden.Count; i++)
                {
                    if (_Wedstrijden[i].IsStarted == true)
                    {
                        dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                    else
                    {
                        dgv_Wedstrijden.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
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
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }

        private void UC_Reader_Load(object sender, EventArgs e)
        {
            dgv_Wedstrijden.DoubleBuffered(true);
            UpdateWedstrijden();
        }


    }
}
