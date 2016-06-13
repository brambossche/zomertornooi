using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;
using Factory;
using structures.Views.ListRefreshEngine;

namespace structures.Views
{
    /// <summary>
    /// view of all teams
    /// </summary>
    public partial class UC_PloegOverView : UserControl
    {
        private Userview<Ploeg> _ploegview;
        private ActiveBindingList<Ploeg> _ploeglist;
        private ActiveBindingList<Persoon> _PersoonList;
        private DataGridView _Lstbox_overview;
        private UC_categoryChanges _UC_categoryChanges;
        private UC_AddTeams _UC_addTeams;
        private PropertyGrid _PropertiesPersoon = new PropertyGrid();
        private ComboBox _ComboPersonen = new ComboBox();

        public UC_PloegOverView(ActiveBindingList<Ploeg> PloegList, ActiveBindingList<Persoon> PersoonList)
        {
            _ploeglist = PloegList;
            _ploeglist.ListChanged += _ploeglist_ListChanged;
            _PersoonList = PersoonList;
            _PersoonList.ListChanged += _PersoonList_ListChanged;
            InitializeComponent();

            _ploegview = new Userview<Ploeg>(_ploeglist,false) { Name = "Ploegview" };
            _ploegview.Dock = DockStyle.Fill;
            _ploegview.extendDataGridView1.CellContentClick += extendDataGridView1_CellContentClick;
            _ploegview.ListRefreshed += _ploegview_ListRefreshed;
            _ploegview.extendDataGridView1.CellClick += extendDataGridView1_CellClick;
            _ploegview.extendDataGridView1.CellDoubleClick += extendDataGridView1_CellDoubleClick;
            splitContainer1.Panel1.Controls.Add(_ploegview);
            splitContainer1.IsSplitterFixed = false;

            
            _Lstbox_overview = new DataGridView();
            //_Lstbox_overview.Dock = DockStyle.Left;
            tstcmb_categoryfilter.Items.Add("Alle reeksen");
            tstcmb_categoryfilter.Items.AddRange ( Category.Categories.ToArray());

            _UC_categoryChanges = new UC_categoryChanges();
            _UC_addTeams = new UC_AddTeams(_ploeglist, _PersoonList);

            //Populate ComboBoxPersonen
            _PropertiesPersoon.PropertySort = PropertySort.NoSort;
            _ComboPersonen.Visible = false;
            PopulateComboPersonen();
            _ComboPersonen.SelectedValueChanged += _ComboPersonen_SelectedValueChanged;
            splitContainer1.Panel1.Controls.Add(_ComboPersonen);
        }

        void _PersoonList_ListChanged(object sender, ListChangedEventArgs e)
        {
            PopulateComboPersonen();
        }

        void extendDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == _ploegview.extendDataGridView1.Columns["Contactpersoon"].Index)
            {
                _ComboPersonen.Location = _ploegview.extendDataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                _ComboPersonen.Width = _ploegview.extendDataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Width;
                _ComboPersonen.Visible = true;
                _ComboPersonen.BringToFront();
            }
        }




        void _ComboPersonen_SelectedValueChanged(object sender, EventArgs e)
        {
            Ploeg p = _ploegview.extendDataGridView1.CurrentRow.DataBoundItem as Ploeg;
            p.Contactpersoon = (Persoon)_ComboPersonen.SelectedItem;
            _ComboPersonen.Visible = false;
        }

        private void PopulateComboPersonen()
        {
            _ComboPersonen.Items.Clear();
            foreach (Persoon p in _PersoonList)
            {
                _ComboPersonen.Items.Add(p);
            }
        }

        void extendDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _PropertiesPersoon.SelectedObject = _ploeglist[_ploegview.extendDataGridView1.CurrentCell.RowIndex].Contactpersoon;
        }

        void extendDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _ploegview.extendDataGridView1.EndEdit();
        }

        void _ploeglist_ListChanged(object sender, ListChangedEventArgs e)
        {
            _Lstbox_overview.DataSource = Ploegoverview();
        }

        void _ploegview_ListRefreshed()
        {

        }


        private void tstbtn_overview_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Contains(_Lstbox_overview))
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Controls.Clear();
                _PropertiesPersoon.SelectedObject = _ploeglist[_ploegview.extendDataGridView1.CurrentCell.RowIndex].Contactpersoon;
                splitContainer1.Panel2.Controls.Add(_PropertiesPersoon);
                splitContainer1.SplitterDistance = splitContainer1.Width /4*3;
                _PropertiesPersoon.Dock = DockStyle.Fill;
                //_UC_categoryChanges.Dock = DockStyle.Fill;
                //splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_Lstbox_overview);
                splitContainer1.Panel2Collapsed = true;
                //splitContainer1.SplitterDistance = splitContainer1.Width - _Lstbox_overview.Width;
                _Lstbox_overview.Dock = DockStyle.Fill;
                _Lstbox_overview.DataSource = Ploegoverview();
                _Lstbox_overview.Columns[0].HeaderText = "Category";
                _Lstbox_overview.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _Lstbox_overview.Columns[1].HeaderText = "Aantal";
                _Lstbox_overview.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                _Lstbox_overview.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _Lstbox_overview.Columns[2].HeaderText = "Aangemeld";
                _Lstbox_overview.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                _Lstbox_overview.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            }
        }

        private void tstbtn_Filter_Click(object sender, EventArgs e)
        {
            if (tstbtn_Filter.CheckState == CheckState.Checked)
            {
                CurrencyManager currencyManager1 = (CurrencyManager)_ploegview.extendDataGridView1.BindingContext[_ploegview.extendDataGridView1.DataSource];
                currencyManager1.SuspendBinding();

                if (tstcmb_categoryfilter.SelectedIndex <= 0)
                {
                    for (int i = 0; i < _ploeglist.Count; i++)
                    {
                        _ploegview.extendDataGridView1.Rows[i].Visible = true;
                    }

                }
                else
                {

                    for (int i = 0; i < _ploeglist.Count; i++)
                    {
                        if (_ploeglist[i].Category.ToString().Equals(tstcmb_categoryfilter.Text))
                        {
                            _ploegview.extendDataGridView1.Rows[i].Visible = true;
                        }
                        else
                        {
                            _ploegview.extendDataGridView1.Rows[i].Visible = false;
                        }
                    }
                }
                currencyManager1.ResumeBinding();


                tstbtn_nofilter.CheckState = CheckState.Unchecked;
            }            
        }


        private void tstbtn_nofilter_Click(object sender, EventArgs e)
        {
            if (tstbtn_nofilter.CheckState == CheckState.Checked)
            {
                CurrencyManager currencyManager1 = (CurrencyManager)_ploegview.extendDataGridView1.BindingContext[_ploegview.extendDataGridView1.DataSource];
                currencyManager1.SuspendBinding();
                for (int i = 0; i < _ploeglist.Count; i++)
                {
                    _ploegview.extendDataGridView1.Rows[i].Visible = true;
                }
                currencyManager1.ResumeBinding();
                tstbtn_Filter.CheckState = CheckState.Unchecked;
            }
        }


        private List<Tuple<string, int, int>> Ploegoverview()
        {
            List<Tuple<string, int, int>> overview = new List<Tuple<string, int, int>>();
            foreach (Category cat in Category.Categories)
            {
                overview.Add(new Tuple<string, int, int>(cat.Categorynaam, _ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam).Count(), _ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam && x.Aangemeld == true).Count()));                
            }
            return overview;
        }

        private void tstcmb_categoryfilter_Click(object sender, EventArgs e)
        {
            
        }

        private void tstbtn_changecategory_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Contains(_UC_categoryChanges))
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Controls.Clear();
                _PropertiesPersoon.SelectedObject = _ploeglist[_ploegview.extendDataGridView1.CurrentCell.RowIndex].Contactpersoon;
                splitContainer1.Panel2.Controls.Add(_PropertiesPersoon);
                splitContainer1.SplitterDistance = splitContainer1.Width /4*3;
                _PropertiesPersoon.Dock = DockStyle.Fill;
                //_UC_categoryChanges.Dock = DockStyle.Fill;
                //splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_UC_categoryChanges);
                //_UC_categoryChanges.Dock = DockStyle.Fill;
                splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
                splitContainer1.Panel2Collapsed = false;
                _UC_categoryChanges.categorychanged += _UC_categoryChanges_categorychanged;
                _UC_categoryChanges.setbackcategory += _UC_categoryChanges_setbackcategory;
            }
        }

        void _UC_categoryChanges_setbackcategory()
        {
            foreach (Ploeg pl in _ploeglist)
            {
                pl.Category = pl.SubscribedCategory;
            }
        }

        void _UC_categoryChanges_categorychanged(Category oldcategory, Category newcategory)
        {
            foreach (Ploeg pl in _ploeglist.Where(x => x.Category.Categorynaam == oldcategory.Categorynaam))
            {
                pl.Category = newcategory;
            }
        }

        private void tstcmb_categoryfilter_TextChanged(object sender, EventArgs e)
        {
            tstbtn_nofilter.CheckState = CheckState.Unchecked;
            tstbtn_Filter.CheckState = CheckState.Checked;
            tstbtn_Filter_Click(null, null);
        }

        private void UC_PloegOverView_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Controls.Clear();
            _PropertiesPersoon.SelectedObject = _ploeglist[_ploegview.extendDataGridView1.CurrentCell.RowIndex].Contactpersoon;
            splitContainer1.Panel2.Controls.Add(_PropertiesPersoon);
            splitContainer1.SplitterDistance = splitContainer1.Width /4*3;
            _PropertiesPersoon.Dock = DockStyle.Fill;
            //_UC_categoryChanges.Dock = DockStyle.Fill;
            //splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
            splitContainer1.Panel2Collapsed = false;


        }

        private void tsb_AddTeam_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.Controls.Contains(_UC_addTeams))
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Controls.Clear();
                _PropertiesPersoon.SelectedObject = _ploeglist[_ploegview.extendDataGridView1.CurrentCell.RowIndex].Contactpersoon;
                splitContainer1.Panel2.Controls.Add(_PropertiesPersoon);
                splitContainer1.SplitterDistance = splitContainer1.Width /4*3;
                _PropertiesPersoon.Dock = DockStyle.Fill;
                //_UC_categoryChanges.Dock = DockStyle.Fill;
                //splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_UC_addTeams);
                splitContainer1.SplitterDistance = splitContainer1.Width - _UC_addTeams.Width;
                //_UC_categoryChanges.Dock = DockStyle.Fill;
                //splitContainer1.SplitterDistance = splitContainer1.Width - _UC_categoryChanges.Width;
                splitContainer1.Panel2Collapsed = false;
            }


        }



    }

    
}
