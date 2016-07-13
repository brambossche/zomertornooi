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
using TournamentCalculation;
using structures.Views.Final_Rounds;
using structures.TournamentCalculation;
using structures.Views.ListRefreshEngine;

namespace structures.Views
{
    public partial class UC_FinalRounds : UserControl
    {
        private ActiveBindingList<Wedstrijd> _WedstrijdList;
        private ActiveBindingList<Terrein> _TerreinList;
        private List<AdministratieReeks> _ReeksList = new List<AdministratieReeks>();
        private List<ComboBox> _ComboBoxes = new List<ComboBox>();
        private RoundRobinGenerator rrg = new RoundRobinGenerator();
        private List<Wedstrijd> _FinaleWedstrijden = new List<Wedstrijd>();

        //Bindinglists for update
        protected BindingListRefresh<Wedstrijd> _BindingListRefreshWedstrijd;
        protected BindingListRefresh<Terrein> _BindingListRefreshTerrein;


        public UC_FinalRounds(ActiveBindingList<Wedstrijd> wedstrijdlist, ActiveBindingList<Terrein> terreinList)
        {
            //Input lists
            _WedstrijdList = wedstrijdlist;
            _TerreinList = terreinList;

            //Refresh lists
            _BindingListRefreshWedstrijd = new BindingListRefresh<Wedstrijd>(_WedstrijdList);
            _BindingListRefreshTerrein = new BindingListRefresh<Terrein>(_TerreinList);

            _WedstrijdList.ListChanged += _wedstrijdlist_ListChanged;
            InitializeComponent();
            GetReeksen();
            PopulateCombo();
            _ComboBoxes.Clear();
            _ComboBoxes.Add(cmb_Reeks1);
            _ComboBoxes.Add(cmb_Reeks2);
            _ComboBoxes.Add(cmb_Reeks3);
            _ComboBoxes.Add(cmb_Reeks4);
        }

        private void RefreshList()
        {
            if (_BindingListRefreshWedstrijd != null)
            {
                _BindingListRefreshWedstrijd.RefreshList();
            }

            if (_BindingListRefreshTerrein != null)
            {
                _BindingListRefreshTerrein.RefreshList();
            }
            GetReeksen();
            PopulateCombo();
            nc_aantalReeksen.Value = 1;
        }

        private void PopulateCombo()
        {
            cmb_Reeks1.Items.Clear();
            cmb_Reeks1.Text = "";
            cmb_Reeks1.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks2.Items.Clear();
            cmb_Reeks2.Text = "";
            cmb_Reeks2.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks3.Items.Clear();
            cmb_Reeks3.Text = "";
            cmb_Reeks3.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks4.Items.Clear();
            cmb_Reeks4.Text = "";
            cmb_Reeks4.Items.AddRange(_ReeksList.ToArray());
        }


        void _wedstrijdlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            CreateReeksList();
            PopulateCombo();
        }

        private void nc_aantalReeksen_ValueChanged(object sender, EventArgs e)
        {
            switch ((int)nc_aantalReeksen.Value)
            {
                case 1:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = false;
                    cmb_Reeks3.Enabled = false;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 2:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = false;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 3:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = true;
                    cmb_Reeks4.Enabled = false;
                    break;
                case 4:
                    cmb_Reeks1.Enabled = true;
                    cmb_Reeks2.Enabled = true;
                    cmb_Reeks3.Enabled = true;
                    cmb_Reeks4.Enabled = true;
                    break;
            }

            CreateReeksList();



        }

        private void btn_SimulateFinals_Click(object sender, EventArgs e)
        {
            PanelWedstrijden.Controls.Clear();
        //CalculateFinalGames(_ReeksList, _terreinList);
            //PanelWedstrijden.Controls.Add(new UC_AllBrackets(2, 8) { Dock = DockStyle.Fill});
            FinalGamesGenerator fgr = new FinalGamesGenerator(_ReeksList,_TerreinList,dtp_Finals.Value, (int)nc_WedstrijdDuur.Value, txt_reeksnaam.Text);
            _FinaleWedstrijden = fgr.CalculateFinalGames();
            dgv_wedstrijden.DataSource = _FinaleWedstrijden;
            UC_AllBrackets FinalBrackets = new UC_AllBrackets(1, _FinaleWedstrijden.Count);
            PanelWedstrijden.Controls.Add(FinalBrackets);
            for (int i = 0; i < _FinaleWedstrijden.Count; i++)
            {
                FinalBrackets.FinalGames[i].Lbl_Home.Text = _FinaleWedstrijden[i].Home.ToString();
                FinalBrackets.FinalGames[i].Lbl_Away.Text = _FinaleWedstrijden[i].Away.ToString();
                FinalBrackets.FinalGames[i].Lbl_Winner.Text = "";

            }



        }

        public void CalculateFinalGames(List<AdministratieReeks> reeksList, ActiveBindingList<Terrein> Terreinen)
        {
            //Bepaal hoeveel rondes er gaan gespeeld worden



            //Verdeel voor het aantal reeksen alles onder in eerste plek, tweede plek, ... van de round robin
            foreach (AdministratieReeks adr in reeksList)
            {




            }








            switch (reeksList.Count)
            {
                case 1:
                    /*
                    List<Wedstrijd> _wedstrijden = new List<Wedstrijd>();
                    List<Ploeg> _ploegen = new List<Ploeg>();
                    List<string> Reeksnamen = new List<string>();
                    DataTable dt = reeksList[0].Klassement.Ranking;
                    foreach (DataRow dr in dt.Rows)
                    {
                        Ploeg p = (Ploeg)dr[0];
                        _ploegen.Add(p);
                        if (!Reeksnamen.Contains(p.Reeksnaam))
                        {
                            Reeksnamen.Add(p.Reeksnaam);
                        }
                    }

                    //Haal het aantal terreinen op
                    List<Terrein> _terreinlist = new List<Terrein>();
                    foreach (Terrein t in Terreinen)
                    {
                        if (Reeksnamen.Contains(t.ReeksNaam) && !_terreinlist.Contains(t))
                        {
                            _terreinlist.Add(t);
                        }
                    }

                    //Indien oneven, verwijder laatste ploeg! 
                    if (_ploegen.Count % 2 == 1)
                    {
                        _ploegen.Remove(_ploegen.Last());
                    }

                    //Bepaal het aantal wedstrijden
                    int AantalW = _ploegen.Count / 2;

                    //Volgorde van wedstrijden ligt deze keer vast, eerst laagste finales dan hoogste
                    for (int i = _ploegen.Count-1; i > 0; i -= 2)
                    {
                        Ploeg h = _ploegen[i];
                        Ploeg a = _ploegen[i-1];


                        Wedstrijd w = new Wedstrijd() {Away = _ploegen[i], Home = _ploegen[i-1],ReeksNaam = txt_reeksnaam.Text};
                        _wedstrijden.Add(w);
                    }

                    //Bepaal aantal terreinen
                    int nrTeams = _ploegen.Count();
                    int NrTerrains = 0;

                    if (nrTeams > 2)
                    {
                        NrTerrains = (int)Math.Floor(Convert.ToDouble(nrTeams) / 3);
                    }
                    else
                    {
                        NrTerrains = 1;
                    }

                    //Bepaal scheidsrechters

                    */




                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }



        public void GetKlassement()
        {




        }


        public void GetReeksen()
        {
            
            List<string> _ReeksListString = new List<string>();
            _ReeksList.Clear();
            _ReeksListString.Clear();
            foreach (Wedstrijd w in _WedstrijdList)
            {
                if (!_ReeksListString.Contains(w.ReeksNaam) && w.WedstrijdFormule == ProgramDefinitions.WedstrijdFormule.RoundRobin)
                {
                    _ReeksListString.Add(w.ReeksNaam);
                }
            }

            foreach (string s in _ReeksListString)
            {
                AdministratieReeks adr = new AdministratieReeks(s, _WedstrijdList);
                adr.CalculateRankings();
                _ReeksList.Add(adr);
            }
        }

        public void CreateReeksList()
        {
            _ReeksList.Clear();
            for (int i = 0; i < nc_aantalReeksen.Value; i++)
            {
                if (_ComboBoxes[i].SelectedIndex >= 0)
                {
                    AdministratieReeks adr = new AdministratieReeks(_ComboBoxes[i].Text, _WedstrijdList);
                    adr.CalculateRankings();
                    _ReeksList.Add(adr);
                }
            }
        }

        private void cmb_Reeks1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateReeksList();

        }

        private void cmb_Reeks2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateReeksList();
        }

        private void cmb_Reeks3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateReeksList();
        }

        private void cmb_Reeks4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateReeksList();
        }

        private void btn_AddFinals_Click(object sender, EventArgs e)
        {
            PanelWedstrijden.Controls.Clear();
            //CalculateFinalGames(_ReeksList, _terreinList);
            //PanelWedstrijden.Controls.Add(new UC_AllBrackets(2, 8) { Dock = DockStyle.Fill});
            FinalGamesGenerator fgr = new FinalGamesGenerator(_ReeksList, _TerreinList, dtp_Finals.Value, (int)nc_WedstrijdDuur.Value, txt_reeksnaam.Text);
            _FinaleWedstrijden = fgr.CalculateFinalGames();
            dgv_wedstrijden.DataSource = _FinaleWedstrijden;
            UC_AllBrackets FinalBrackets = new UC_AllBrackets(1, _FinaleWedstrijden.Count);
            PanelWedstrijden.Controls.Add(FinalBrackets);
            for (int i = 0; i < _FinaleWedstrijden.Count; i++)
            {
                FinalBrackets.FinalGames[i].Lbl_Home.Text = _FinaleWedstrijden[i].Home.ToString();
                FinalBrackets.FinalGames[i].Lbl_Away.Text = _FinaleWedstrijden[i].Away.ToString();
                FinalBrackets.FinalGames[i].Lbl_Winner.Text = "";

            }

            foreach (Wedstrijd w in _FinaleWedstrijden)
            {
                _WedstrijdList.Add(w);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshList();
        }













    }
}
