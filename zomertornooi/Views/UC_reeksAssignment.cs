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
using Marb.Draganddrop;
using Marb.Bindinglist;

namespace structures.Views
{
    public partial class UC_reeksAssignment : UserControl
    {        
        /// <summary>
        /// binding to  all teams
        /// </summary>
        private ActiveBindingList<Ploeg> _ploeglist;
        /// <summary>
        /// List with different series 
        /// </summary>
        private List<ReeksAssignment> _reeksAssignmentlist = new List<ReeksAssignment>();
        //list of all user controls to assign
        private List<UC_ListAllocation> List_UC_ListAllocation = new List<UC_ListAllocation>();
        private bool ListChanged = false;

        private UC_ListAllocation Selected_uc_ListAllocation;

        public UC_reeksAssignment(ActiveBindingList<Ploeg> ploeglist)
        {
            _ploeglist = ploeglist;
            _ploeglist.ListChanged += _ploeglist_ListChanged;
            InitializeComponent();
            CreateOverview();
            dataGridView1.DataSource = _reeksAssignmentlist;
            //ploeglist.ListChanged += ploeglist_ListChanged;
            
        }

        void _ploeglist_ListChanged(object sender, ListChangedEventArgs e)
        {
            ListChanged = true;
            
        }

        /*void ploeglist_ListChanged(object sender, ListChangedEventArgs e)
        {
            
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                ReeksAssignment reeks = _reeksAssignmentlist.Where(x => x.Category.Categorynaam == _ploeglist[e.NewIndex].Category.Categorynaam).First();
                reeks.AangemeldePloegen = _ploeglist.Where(x => x.Category.Categorynaam == reeks.Category.Categorynaam).Where(x => x.Aangemeld == true).Count();
                reeks.AantalPloegen = _ploeglist.Where(x => x.Category.Categorynaam == reeks.Category.Categorynaam).Count();

                UC_ListAllocation _UC_ListAllocation = List_UC_ListAllocation.Where(x => x.Name == reeks.Category.Categorynaam).First();                

                if (_ploeglist[e.NewIndex].Aangemeld)
                {
                    _UC_ListAllocation.InputList.Add(_ploeglist[e.NewIndex]);
                }
                else
                {
                    if (_UC_ListAllocation.InputList.Contains (_ploeglist[e.NewIndex]))
                    {
                        _UC_ListAllocation.InputList.Remove (_ploeglist[e.NewIndex]);
                    }
                    else
                    {
                        foreach (BindingList<Ploeg> output in _UC_ListAllocation.Outputlist)
                        {
                            if (output.Contains(_ploeglist[e.NewIndex]))
                            {
                                output.Remove(_ploeglist[e.NewIndex]);
                            }
                        }
                    }
                }
            }
        }*/

        private void CreateOverview()
        {
            try
            {
                foreach (Category cat in Category.Categories)
                {
                    _reeksAssignmentlist.Add(new ReeksAssignment()
                    {
                        Category = cat,
                        AantalPloegen = _ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam).Count(),
                        AangemeldePloegen = _ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam).Where(x => x.Aangemeld == true).Count(),
                    });

                    ExtBindingList<Ploeg> PloegList = new ExtBindingList<Ploeg>(_ploeglist.Where(x => x.Category.Categorynaam == cat.Categorynaam).Where(x => x.Aangemeld == true).ToList())
                    {
                        Name = _reeksAssignmentlist.Last<ReeksAssignment>().Category.Categorynaam
                    };

                    SortedDictionary<string, int> _differentSeries = new SortedDictionary<string, int>();

                    foreach (Ploeg pl in PloegList.ToList())
                    {
                        if (pl.Reeksnaam == null)
                        {
                            pl.Reeksnaam = "";
                        }

                        if (pl.Reeksnaam != "")
                        {
                            if (_differentSeries.ContainsKey(pl.Reeksnaam))
                            {
                                _differentSeries[pl.Reeksnaam]++;
                            }
                            else
                            {
                                _differentSeries.Add(pl.Reeksnaam, 1);
                            }
                        }
                    }
                    _differentSeries.OrderBy(key => key.Key);
                    _reeksAssignmentlist.Last<ReeksAssignment>().NrOfReeksen = _differentSeries.Count();
                    //populate the control list to keep the current states*/
                    List_UC_ListAllocation.Add(new UC_ListAllocation(PloegList) { Name = PloegList.Name });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        void Outputlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            
        }
        private void UpdateReeksView(int rowindex)
        {
            try
            {
                ReeksAssignment reeksAssignment = (ReeksAssignment)dataGridView1.Rows[rowindex].DataBoundItem;
                Selected_uc_ListAllocation = List_UC_ListAllocation.Where(x => x.Name == reeksAssignment.Category.Categorynaam).First();

                if (reeksAssignment.NrOfReeksen > 0)
                {
                    Selected_uc_ListAllocation.NrOfOuutputLists = reeksAssignment.NrOfReeksen;

                    Selected_uc_ListAllocation.Dock = DockStyle.Fill;
                    pnl_listview.Controls.Clear();
                    pnl_listview.Controls.Add(Selected_uc_ListAllocation);
                    splitContainer1.Panel2Collapsed = false;
                }
                else
                {
                    splitContainer1.Panel2Collapsed = true;
                    Selected_uc_ListAllocation.SetItemsToInputList();
                }

                this.AllowDrop = true;
            }
            catch (Exception ee)
            {
                Console.WriteLine("ecept" + ee);
            }
        }


        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateReeksView(dataGridView1.CurrentCell.RowIndex);
        }

        private void btn_assigntolists_Click(object sender, EventArgs e)
        {
            if (Selected_uc_ListAllocation != null)
            {
                Selected_uc_ListAllocation.DistributesInputOverOutput();
            }
        }

        private void btn_allbacktoinputlist_Click(object sender, EventArgs e)
        {
            if (Selected_uc_ListAllocation != null)
            {
                Selected_uc_ListAllocation.SetItemsToInputList();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateReeksView(e.RowIndex);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            UpdateReeksView(e.RowIndex);
            //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];





        }


        private void UC_reeksAssignment_Enter(object sender, EventArgs e)
        {
                List_UC_ListAllocation.Clear();
                _reeksAssignmentlist.Clear();
                CreateOverview();
                dataGridView1.Refresh();
                dataGridView1.Update(); 
        }



        public class CustomGrid : DataGridView
        {
            protected override bool ProcessDialogKey(Keys keyData)
            {
                if (keyData == Keys.Enter)
                {
                    EndEdit();
                    return true;
                }
                return base.ProcessDialogKey(keyData);
            }
        }






    }
}

