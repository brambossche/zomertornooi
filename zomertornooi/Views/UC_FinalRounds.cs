﻿using System;
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

namespace structures.Views
{
    public partial class UC_FinalRounds : UserControl
    {
        private ActiveBindingList<Wedstrijd> _wedstrijdlist;
        private ActiveBindingList<Ploeg> _ploegList;
        private ActiveBindingList<Terrein> _terreinList;
        private List<AdministratieReeks> _ReeksList = new List<AdministratieReeks>();
        private List<ComboBox> _ComboBoxes = new List<ComboBox>();
        RoundRobinGenerator rrg = new RoundRobinGenerator();


        public UC_FinalRounds(ActiveBindingList<Wedstrijd> wedstrijdlist, ActiveBindingList<Ploeg> ploegList, ActiveBindingList<Terrein> terreinList)
        {
            _wedstrijdlist = wedstrijdlist;
            _ploegList = ploegList;
            _terreinList = terreinList;

            _wedstrijdlist.ListChanged += _wedstrijdlist_ListChanged;
            InitializeComponent();
            GetReeksen();
            PopulateCombo();
            _ComboBoxes.Clear();
            _ComboBoxes.Add(cmb_Reeks1);
            _ComboBoxes.Add(cmb_Reeks2);
            _ComboBoxes.Add(cmb_Reeks3);
            _ComboBoxes.Add(cmb_Reeks4);
        }

        private void PopulateCombo()
        {
            cmb_Reeks1.Items.Clear();
            cmb_Reeks1.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks2.Items.Clear();
            cmb_Reeks2.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks3.Items.Clear();
            cmb_Reeks3.Items.AddRange(_ReeksList.ToArray());
            cmb_Reeks4.Items.Clear();
            cmb_Reeks4.Items.AddRange(_ReeksList.ToArray());
        }


        void _wedstrijdlist_ListChanged(object sender, ListChangedEventArgs e)
        {
            CreateReeksList();
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
        //CalculateFinalGames(_ReeksList, _terreinList);
            //PanelWedstrijden.Controls.Add(new UC_AllBrackets(2, 8) { Dock = DockStyle.Fill});
            FinalGamesGenerator fgr = new FinalGamesGenerator(_ReeksList,_terreinList,dtp_Finals.Value, (int)nc_WedstrijdDuur.Value);
            List<Wedstrijd> games = fgr.CalculateFinalGames();
            dgv_wedstrijden.DataSource = games;
            UC_AllBrackets FinalBrackets = new UC_AllBrackets(1, games.Count) { Dock = DockStyle.Fill};
            PanelWedstrijden.Controls.Add(FinalBrackets);
            for (int i = 0; i < games.Count; i++)
            {
                FinalBrackets.FinalGames[i].Lbl_Home.Text = games[i].Home.ToString();
                FinalBrackets.FinalGames[i].Lbl_Away.Text = games[i].Away.ToString();
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
            _ReeksListString.Clear();
            foreach (Wedstrijd w in _wedstrijdlist)
            {
                if (!_ReeksListString.Contains(w.ReeksNaam))
                {
                    _ReeksListString.Add(w.ReeksNaam);
                }
            }

            foreach (string s in _ReeksListString)
            {
                AdministratieReeks adr = new AdministratieReeks(s, _wedstrijdlist);
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
                    AdministratieReeks adr = new AdministratieReeks(_ComboBoxes[i].Text, _wedstrijdlist);
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










    }
}
