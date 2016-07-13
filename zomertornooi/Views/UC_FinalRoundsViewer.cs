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
using structures.Views.ListRefreshEngine;
using Marb.Extender.Datgridview;
using structures.Views.Final_Rounds;
using structures.TournamentCalculation;

namespace structures.Views
{
    public partial class UC_FinalRoundsViewer : UserControl
    {
        //Inputlist
        private ActiveBindingList<Wedstrijd> _WedstrijdList;

        private UC_AllBrackets _FinalBrackets = new UC_AllBrackets();

        //Bindinglists for update
        protected BindingListRefresh<Wedstrijd> _BindingListRefreshWedstrijd;

        //Private bool Listchanged
        private bool Listchanged = false;

        public UC_FinalRoundsViewer(ActiveBindingList<Wedstrijd> wedstrijdlist)
        {
            InitializeComponent();
            _WedstrijdList = wedstrijdlist;
            _WedstrijdList.ListChanged += _WedstrijdList_ListChanged;
            dgv_Wedstrijden.DoubleBuffered(true);
            dgv_Wedstrijden.DataSource = _WedstrijdList;
            InitWedstrijden();
            
        }

        void _WedstrijdList_ListChanged(object sender, ListChangedEventArgs e)
        {
            Listchanged = true;
        }



        private void PopulateReeksCombobox()
        {
            //Populate cmb_reeksnaam
            List<string> Reeksen = new List<string>();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if ((w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals || 
                    w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                    && !Reeksen.Contains(w.ReeksNaam))
                {
                    Reeksen.Add(w.ReeksNaam);
                }
            }
            cmb_ReeksNaam.Items.Clear();
            cmb_ReeksNaam.Items.AddRange(Reeksen.ToArray());
            cmb_ReeksNaam.Width = DropDownWidth(cmb_ReeksNaam);
            if (Reeksen.Count > 0)
            {
                cmb_ReeksNaam.SelectedIndex = 0;
            }
            else
            {
                CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
                WedstrijdManager.SuspendBinding();
                for (int i = 0; i < _WedstrijdList.Count; i++)
                {
                    if (_WedstrijdList[i].ReeksNaam == cmb_ReeksNaam.Text)
                    {
                        dgv_Wedstrijden.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Wedstrijden.Rows[i].Visible = false;
                    }
                }
                WedstrijdManager.ResumeBinding();

                panel1.Controls.Clear();


            }
            
        }



        private int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth + 20;
        }

        private void UC_FinalRoundsViewer_Load(object sender, EventArgs e)
        {
            PopulateReeksCombobox();
            
        }

        private void cmb_ReeksNaam_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
            WedstrijdManager.SuspendBinding();
            for (int i = 0; i < _WedstrijdList.Count; i++)
            {
                if (_WedstrijdList[i].ReeksNaam == cmb_ReeksNaam.Text)
                {
                    dgv_Wedstrijden.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Wedstrijden.Rows[i].Visible = false;
                }
            }
            WedstrijdManager.ResumeBinding();

            //Update Brackets
            CreateBrackets();

            //
            checkbuttonStatus();

        }

        private void CreateBrackets()
        {
            panel1.Controls.Clear();
            int count = _WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals)
                    && w.ReeksNaam == cmb_ReeksNaam.Text).ToList().Count;

            if (count == 0)
            {
                count = _WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                                && w.ReeksNaam == cmb_ReeksNaam.Text).ToList().Count;
            }


            if(_WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals ||
                    w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                    && w.ReeksNaam == cmb_ReeksNaam.Text).ToList()[0].WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
            {
                //Generate brackets
                _FinalBrackets = new UC_AllBrackets(1, count);
            }
            else if(_WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals ||
                    w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                    && w.ReeksNaam == cmb_ReeksNaam.Text).ToList()[0].WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals)
            {
                //Generate brackets
                _FinalBrackets = new UC_AllBrackets(2, count);
            }



            int index = 0;
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if ((w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals ||
                    w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                    && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    _FinalBrackets.FinalGames[index].Lbl_Home.DataBindings.Add("Text", w.Home, "Ploegnaam");
                    _FinalBrackets.FinalGames[index].Lbl_Away.DataBindings.Add("Text", w.Away, "Ploegnaam");
                    _FinalBrackets.FinalGames[index].Lbl_Winner.DataBindings.Add("Text", w, "Winner");

                    //_FinalBrackets.FinalGames[index].Lbl_Home.Text = w.Home.Ploegnaam;
                    //_FinalBrackets.FinalGames[index].Lbl_Away.Text = w.Away.Ploegnaam;
                    index++;
                }
            }
            panel1.Controls.Add(_FinalBrackets);



        }

        private void UpdateBrackets()
        {
            int index = 0;
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if ((w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals ||
                    w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                    && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    //_FinalBrackets.FinalGames[index].Lbl_Home.DataBindings.Add("Text", w.Home, "Ploegnaam");
                    //_FinalBrackets.FinalGames[index].Lbl_Away.DataBindings.Add("Text", w.Away, "Ploegnaam");
                    //_FinalBrackets.FinalGames[index].Lbl_Winner.DataBindings.Add("Text", w, "Winner");

                    _FinalBrackets.FinalGames[index].Lbl_Home.Text = w.Home.Ploegnaam;
                    _FinalBrackets.FinalGames[index].Lbl_Away.Text = w.Away.Ploegnaam;
                    _FinalBrackets.FinalGames[index].Lbl_Winner.Text = w.Winner;
                    index++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = _FinalBrackets.AantalWedstrijdenPr;
            List<Ploeg> _winnaars = new List<Ploeg>();
            List<Ploeg> _verliezers = new List<Ploeg>();
            List<Terrein> _terreinen = new List<Terrein>();
            DateTime Finals = new DateTime();

            foreach (Wedstrijd w in _WedstrijdList)
            {
                if ((w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals)
                    && w.ReeksNaam == cmb_ReeksNaam.Text)
                {
                    if (w.Home.Ploegnaam == w.Winner)
                    {
                        _winnaars.Add(w.Home);
                        _verliezers.Add(w.Away);
                    }
                    else
                    {
                        _winnaars.Add(w.Away);
                        _verliezers.Add(w.Home);
                    }
                    Finals = w.Aanvangsuur;

                    //Voeg terreinen toe aan list 
                    if (!_terreinen.Contains(w.Terrein))
                    {
                        _terreinen.Add(w.Terrein);
                    }
                }
            }

            FinalGamesGenerator fgg = new FinalGamesGenerator(Finals.AddMinutes(50), 50, cmb_ReeksNaam.Text);
            List<Ploeg> _teams = fgg.ArrangeWinnersLosers(_winnaars, _verliezers);
            //Reverse order of teams
            _teams.Reverse();
            List<Wedstrijd> NextGames = fgg.CalculateNextRound(_teams, _terreinen);
            //Check of de knop weg kan
            checkbuttonStatus();




            foreach (Wedstrijd w in NextGames)
            {
                _WedstrijdList.Add(w);
            }


            UpdateBrackets();




        }

        private void UC_FinalRoundsViewer_Enter(object sender, EventArgs e)
        {
            if (Listchanged)
            {
                Listchanged = false;
                PopulateReeksCombobox();
                UpdateBrackets();
            }
        }

        private void dgv_wedstrijden_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateBrackets();
            checkbuttonStatus();
        }


        private void checkbuttonStatus()
        {
            int count = _WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals)
            && w.ReeksNaam == cmb_ReeksNaam.Text).ToList().Count;

            int countP = _WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
            && w.ReeksNaam == cmb_ReeksNaam.Text).ToList().Count;

            if (count == 0)
            {
                count = _WedstrijdList.Where(w => (w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.PlacementGames)
                                && w.ReeksNaam == cmb_ReeksNaam.Text).ToList().Count;
            }

            int index = 0;
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if ((w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.CrossFinals)
                    && w.ReeksNaam == cmb_ReeksNaam.Text && w.Winner != "")
                {
                    index++;
                }
            }

            if (count == index)
            {
                button1.Visible = true;
                button1.Enabled = true;
            }
            else
            {
                button1.Visible = true;
                button1.Enabled = false;
            }


            if (count + countP > count)
            {
                button1.Visible = false;
            }
            else
            {
                button1.Visible = true;
            }

        }

        private void InitWedstrijden()
        {
            CurrencyManager WedstrijdManager = (CurrencyManager)dgv_Wedstrijden.BindingContext[dgv_Wedstrijden.DataSource];
            WedstrijdManager.SuspendBinding();
            foreach (DataGridViewColumn column in dgv_Wedstrijden.Columns)
            {
                if (column.Index <= 5)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            dgv_Wedstrijden.Columns["IsStarted"].ReadOnly = true;
            WedstrijdManager.ResumeBinding();
        }

    }
}
