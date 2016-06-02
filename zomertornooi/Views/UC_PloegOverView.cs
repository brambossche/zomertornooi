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
        private DataGridView _Lstbox_overview;
        private UC_categoryChanges _UC_categoryChanges;

        public UC_PloegOverView(ActiveBindingList<Ploeg> PloegList)
        {
            _ploeglist = PloegList;
            InitializeComponent();

            _ploegview = new Userview<Ploeg>(_ploeglist,true) { Name = "Ploegview" };
            _ploegview.Dock = DockStyle.Fill;
            _ploegview.ListRefreshed += _ploegview_ListRefreshed;
            splitContainer1.Panel1.Controls.Add(_ploegview);
            
            _Lstbox_overview = new DataGridView();
            _Lstbox_overview.Dock = DockStyle.Left;

            tstcmb_categoryfilter.Items.AddRange ( Category.Categories.ToArray());

            _UC_categoryChanges = new UC_categoryChanges();
        }

        void _ploegview_ListRefreshed()
        {
            //BindingList<Ploeg> _fileteredlist = new BindingList<Ploeg>(_ploeglist.Where(x => x.Category.Categorynaam == tstcmb_categoryfilter.Text).ToList());
            //_ploegview.DataSource = _fileteredlist;
        }


        private void tstbtn_overview_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(_Lstbox_overview);
            splitContainer1.Panel2Collapsed = true;
            _Lstbox_overview.DataSource =  Ploegoverview();
            _Lstbox_overview.Columns[0].HeaderText = "Category";
            _Lstbox_overview.Columns[1].HeaderText = "Aantal";
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void tstbtn_Filter_Click(object sender, EventArgs e)
        {
            if (tstbtn_Filter.CheckState == CheckState.Checked)
            {
                BindingList<Ploeg> _fileteredlist = new BindingList<Ploeg>(_ploeglist.Where(x => x.Category.Categorynaam == tstcmb_categoryfilter.Text).ToList());
                _ploegview.DataSource = _fileteredlist;
                //_ploegview.DataSource = _ploeglist;

                //CurrencyManager cm = (CurrencyManager)BindingContext[_ploegview.DataSource];
                //cm.SuspendBinding();
                //_ploegview.extendDataGridView1.Rows[0].Visible = false;
                //cm.ResumeBinding();


                tstbtn_nofilter.CheckState = CheckState.Unchecked;
            }            
        }


        private void tstbtn_nofilter_Click(object sender, EventArgs e)
        {
            if (tstbtn_nofilter.CheckState == CheckState.Checked)
            {
                _ploegview.DataSource = _ploeglist;
                tstbtn_Filter.CheckState = CheckState.Unchecked;
            }
        }


        private List<Tuple<string, int>> Ploegoverview()
        {
            List<Tuple<string, int>> overview = new List<Tuple<string, int>>();
            foreach (Category cat in Category.Categories)
            {             
                overview.Add(new Tuple<string, int>(cat.Categorynaam, _ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam).Count()));                
            }
            return overview;
        }

        private void tstcmb_categoryfilter_Click(object sender, EventArgs e)
        {
            
        }

        private void tstbtn_changecategory_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(_UC_categoryChanges);
            splitContainer1.Panel2Collapsed = false;
            _UC_categoryChanges.categorychanged += _UC_categoryChanges_categorychanged;
            _UC_categoryChanges.setbackcategory += _UC_categoryChanges_setbackcategory;
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
        }


    }

    
}
