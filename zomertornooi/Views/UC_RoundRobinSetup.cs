using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Marb.Bindinglist;
using TournamentCalculation;
using System.Threading;
using Factory;
using NhibernateIntf;
using NHibernate;

namespace structures.Views
{
    public partial class UC_RoundRobinSetup : UserControl
    {
        /// <summary>
        /// binding to to all teams
        /// </summary>
        private ActiveBindingList<Ploeg> _ploeglist;
        private ActiveBindingList<Wedstrijd> _wedstrijdlist;
        private ActiveBindingList<Terrein> _Terreinlist;
        private RoundRobinGenerator rrg = new RoundRobinGenerator();
        private bool isListChanged = false;

        public UC_RoundRobinSetup(ActiveBindingList<Ploeg> ploeglist, ActiveBindingList<Wedstrijd> wedstrijdlist, ActiveBindingList<Terrein> terreinlist)
        {
            InitializeComponent();
            _ploeglist = ploeglist;
            _wedstrijdlist = wedstrijdlist;
            _Terreinlist = terreinlist;
            _ploeglist.ListChanged += _ploeglist_ListChanged;             
        }

        void _ploeglist_ListChanged(object sender, ListChangedEventArgs e)
        {

            isListChanged = true;

        }



        # region create list of reeksen for round robin


        private List<Reeks> _ReeksList = new List<Reeks>();

        private void CreateReeksList()
        {

            for (int i = 0; i < _ReeksList.Count; i++ )
            {
                _ReeksList[i].Ploegen.Clear();
            }

                foreach (Ploeg pl in _ploeglist)
                {
                    if (pl.Reeksnaam != null && pl.Reeksnaam != "")
                    {
                        Reeks reeks;
                        if (_ReeksList
                            .Where(x => x.ReeksNaam == pl.Reeksnaam)
                            //.Where(x => x.ReeksNaam != "")
                            //.Where(x => x.ReeksNaam != null)
                            .Count() == 0)
                        {
                            reeks = new Reeks() { ReeksNaam = pl.Reeksnaam };
                            _ReeksList.Add(reeks);
                        }
                        else
                        {
                            reeks = _ReeksList.Where(x => x.ReeksNaam == pl.Reeksnaam).First();
                        }
                        reeks.Ploegen.Add(pl);
                    }

                }

            //Initiate terreinen
                for (int t = 0; t < _ReeksList.Count; t++)
                {
                    _ReeksList[t].Terreinen = rrg.CalculateNrTerrains(_ReeksList[t]);
                }

            //Check for empty reeksen
                List<Reeks> ReeksListHulp = _ReeksList.ToList();

                for (int i = 0; i < _ReeksList.Count; i++)
                {
                    if(_ReeksList[i].Ploegen.Count == 0)
                    {
                        ReeksListHulp.Remove(_ReeksList[i]);
                    }
                }

                _ReeksList = ReeksListHulp.ToList();



            comboBxReeksen.Items.Clear();

            foreach (Reeks r in _ReeksList)
            {
                comboBxReeksen.Items.Add(r.ReeksNaam);
            }

            if (_ReeksList.Count > 0)
            {
                btn_OptimizeTerrein.Enabled = true;
                btn_Simulate.Enabled = true;
                comboBxReeksen.SelectedIndex = 0;
            }
            else
            {
                comboBxReeksen.Text = "";
                btn_OptimizeTerrein.Enabled = false;
                btn_Simulate.Enabled = false;
                dtv_Wedstrijden.DataSource = null;
                dtv_optimisation.DataSource = null;
                dtv_StatusList.DataSource = null;
            }

            

            
        }

        private void UpdateReeksParameters()
        {
            if (_ReeksList.Count > 0)
            {
                nc_aantalRoundRobin.Value = _ReeksList[comboBxReeksen.SelectedIndex].WedstrijdDefinition.AantalRoundRobin;
                nc_MaxNaElkaar.Value = _ReeksList[comboBxReeksen.SelectedIndex].WedstrijdDefinition.MaxNaElkaarSpelen;
                nc_AantalRondesZaterdag.Value = _ReeksList[comboBxReeksen.SelectedIndex].WedstrijdDefinition.AantalrondesZaterdag;
                nc_WedstrijdDuur.Value = _ReeksList[comboBxReeksen.SelectedIndex].WedstrijdDefinition.Wedstrijdduur;
                nc_aantalTerreinen.Value = _ReeksList[comboBxReeksen.SelectedIndex].Terreinen.Count();

                createTerreinDtv();
            }


           /*for (int i = 0; i < _ReeksList[comboBxReeksen.SelectedIndex].Terreinen.Count; i++)
           {
               dtv_Terreinen.Rows[i].Cells[1].Value = _ReeksList[comboBxReeksen.SelectedIndex].Terreinen[i].TerreinNr;
           }*/

        }


        #endregion
        List<Wedstrijd> FinalGames;
        List<Terrein> FinalTerreinen;
        private void btn_calculateRoundRobin_Click(object sender, EventArgs e)
        {
            CalculateAllRoundRobinGames();
            _wedstrijdlist.Clear();
            _Terreinlist.Clear();
            _Terreinlist.SetList(FinalTerreinen);
            _wedstrijdlist.SetList(FinalGames);

            /*

            foreach (Terrein terrein in FinalTerreinen)
            {
                _Terreinlist.Add(terrein);
            }

            foreach (Wedstrijd wedstr in FinalGames)
            {
                _wedstrijdlist.Add(wedstr);
            }*/
        }




        private void CalculateAllRoundRobinGames()
        {
            FinalGames = new List<Wedstrijd>();

            for (int p = 0; p < _ReeksList.Count; p++)
            {
                List<Reeks> listOfReeksen = new List<Reeks>();
                _ReeksList[p].RoundRobin = rrg.CalculateRoundRobinGames(_ReeksList[p]);
                listOfReeksen.Add(_ReeksList[p]);
                rrg.CalculateTimeSchedule(listOfReeksen);

                //rrg.logRoundRobin(_ReeksList[p]);
                //rrg.LogTimeSchedule(_ReeksList[p]);
                //rrg.LogStatusList(_ReeksList[p]);
            }
            FinalGames = rrg.GetAllGames(_ReeksList).ToList();
            FinalTerreinen = rrg.GetAllTerrains(_ReeksList);
        
        }


        private void AddDatabindings(int index)
        {

            nc_aantalRoundRobin.DataBindings.Clear();
            nc_MaxNaElkaar.DataBindings.Clear();
            nc_WedstrijdDuur.DataBindings.Clear();
            nc_AantalRondesZaterdag.DataBindings.Clear();

            dtp_Zaterdag.DataBindings.Clear();
            dtp_Zondag.DataBindings.Clear();


            if (_ReeksList.Count > 0)
            {
                nc_aantalRoundRobin.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "AantalRoundRobin");
                nc_MaxNaElkaar.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "MaxNaElkaarSpelen");
                nc_WedstrijdDuur.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "Wedstrijdduur");
                nc_AantalRondesZaterdag.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "AantalrondesZaterdag");

                dtp_Zaterdag.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "AanvangsuurZat");
                dtp_Zondag.DataBindings.Add("Value", _ReeksList[index].WedstrijdDefinition, "AanvangsuurZon");
            }


        }

        private void UC_RoundRobinSetup_Load(object sender, EventArgs e)
        {
            CreateReeksList();
            AddDatabindings(comboBxReeksen.SelectedIndex);
            UpdateReeksParameters();
        }



        private void comboBxReeksen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReeksParameters();
            AddDatabindings(comboBxReeksen.SelectedIndex);
            SimulateGames();
        }

        private void btn_OptimizeTerrein_Click(object sender, EventArgs e)
        {
            _ReeksList[comboBxReeksen.SelectedIndex].Terreinen = rrg.CalculateNrTerrains(_ReeksList[comboBxReeksen.SelectedIndex]);
            nc_aantalTerreinen.Value = _ReeksList[comboBxReeksen.SelectedIndex].Terreinen.Count();
            UpdateReeksParameters();

        }

        private void dtp_Zaterdag_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(""+ e.ToString());
        }

        private void UC_RoundRobinSetup_Enter(object sender, EventArgs e)
        {
            if (isListChanged)
            {
                isListChanged = false;
                CreateReeksList();
                AddDatabindings(comboBxReeksen.SelectedIndex);
                UpdateReeksParameters();
            }
        }

        private void nc_aantalTerreinen_ValueChanged(object sender, EventArgs e)
        {
            //NumericUpDown o = (NumericUpDown)sender;
            //int currentvalue = (int)o.Value;           
            int nrT = (int)nc_aantalTerreinen.Value;
            rrg.SetNrTerrains(_ReeksList[comboBxReeksen.SelectedIndex], nrT);
            nc_aantalTerreinen.Value = _ReeksList[comboBxReeksen.SelectedIndex].Terreinen.Count();
            createTerreinDtv();


        }

        private void createTerreinDtv()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Terrein", typeof(string));
            dt.Columns.Add("Nummer", typeof(int));

            for (int i = 0; i < _ReeksList[comboBxReeksen.SelectedIndex].Terreinen.Count; i++)
            {
                dt.Rows.Add("Terrein " + (i + 1).ToString(), _ReeksList[comboBxReeksen.SelectedIndex].Terreinen[i].TerreinNr);
            }

            dtv_Terreinen.DataSource = dt;
            dtv_Terreinen.Columns[0].ReadOnly = true;
                

        }

        private void btn_Simulate_Click(object sender, EventArgs e)
        {
            SimulateGames();
        }


        private void SimulateGames()
        {
            List<Reeks> listOfReeksen = new List<Reeks>();


            //Calculate Round Robin
            _ReeksList[comboBxReeksen.SelectedIndex].RoundRobin = rrg.CalculateRoundRobinGames(_ReeksList[comboBxReeksen.SelectedIndex]);
            listOfReeksen.Add(_ReeksList[comboBxReeksen.SelectedIndex]);
            //Calculate Timeschedule
            rrg.CalculateTimeSchedule(listOfReeksen);

            //rrg.logRoundRobin(_ReeksList[comboBxReeksen.SelectedIndex]);
            //rrg.LogTimeSchedule(_ReeksList[comboBxReeksen.SelectedIndex]);
            //rrg.LogStatusList(_ReeksList[comboBxReeksen.SelectedIndex]);

            FilldtvGames(_ReeksList[comboBxReeksen.SelectedIndex]);
            FilldtvStatuslist(_ReeksList[comboBxReeksen.SelectedIndex]);
            FilldtvOptimize(_ReeksList[comboBxReeksen.SelectedIndex]);

        }





        private void FilldtvStatuslist(Reeks r)
        {
            DataTable StatusTable = new DataTable();
            for (int i = 0; i < r.Ploegen.Count; i++)
            {
                StatusTable.Columns.Add(r.Ploegen[i].Ploegnaam, typeof(string));
            }

            for (int s = 0; s < r.Ploegen[0].StatusList.Count; s++)
            {
                object[] statusarray = new object[r.Ploegen.Count];
                for (int p = 0; p < r.Ploegen.Count; p++)
                {
                    statusarray[p] = r.Ploegen[p].StatusList[s].ToString();
                }
                StatusTable.Rows.Add(statusarray);
            }

            dtv_StatusList.DataSource = StatusTable;
            foreach (DataGridViewColumn column in dtv_StatusList.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtv_StatusList.ClearSelection();
        }

        private void FilldtvGames(Reeks r)
        {
            DataTable GameTable = new DataTable();
            GameTable.Columns.Add("Aanvangsuur", typeof(string));
            for (int i = 0; i < r.Terreinen.Count; i++)
            {
                GameTable.Columns.Add("Terrein " + _ReeksList[comboBxReeksen.SelectedIndex].Terreinen[i].TerreinNr.ToString(), typeof(string));
            }


            int index = 0;
            for (int ts = 0; ts < r.AantalRondes; ts++)
            {
                object[] Gamearray = new object[r.Terreinen.Count + 1];
                Gamearray[0] = r.WedstrijdDefinition.StartingTimes[ts].ToShortTimeString();
                for (int t = 0; t < r.Terreinen.Count(); t++)
                {
                    if (index < r.TimeSchedule.Count)
                    {
                        if (r.TimeSchedule[index].Terrein == r.Terreinen[t] && r.TimeSchedule[index].Aanvangsuur == r.WedstrijdDefinition.StartingTimes[ts])
                        {
                            if (r.TimeSchedule[index].Scheidsrechter != null)
                            {
                                Gamearray[t+1] += r.TimeSchedule[index].Home.Ploegnaam + " - " + r.TimeSchedule[index].Away.Ploegnaam;
                            }
                            else
                            {
                                Gamearray[t + 1] += r.TimeSchedule[index].Home.Ploegnaam + " - " + r.TimeSchedule[index].Away.Ploegnaam;
                            }
                            index++;
                        }
                    }

                }

                GameTable.Rows.Add(Gamearray);
            }


            dtv_Wedstrijden.DataSource = GameTable;
            foreach (DataGridViewColumn column in dtv_Wedstrijden.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dtv_Wedstrijden.ClearSelection();


        }

        private void FilldtvOptimize(Reeks r)
        {
            List<Reeks> reeksList = new List<Reeks>();
            reeksList.Add(r);
            DataTable dt = rrg.OptimizeTournament(reeksList);
            dtv_optimisation.DataSource = dt;
            foreach (DataGridViewColumn column in dtv_optimisation.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            //dtv_optimisation.DefaultCellStyle.SelectionBackColor = dtv_optimisation.DefaultCellStyle.BackColor;
            //dtv_optimisation.DefaultCellStyle.SelectionForeColor = dtv_optimisation.DefaultCellStyle.ForeColor;
            dtv_optimisation.ClearSelection();
        }

        private void dtv_StatusList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.Equals("Gespeeld"))
            {
                e.CellStyle.BackColor = Color.Green;

            }
            else if (e.Value.Equals("Scheidsrechter"))
            {
                e.CellStyle.BackColor = Color.OrangeRed;
            }
            else if (e.Value.Equals("Rust"))
            {
                e.CellStyle.BackColor = Color.Yellow;
            }
            




        }

        private void dtv_Terreinen_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            _ReeksList[comboBxReeksen.SelectedIndex].Terreinen[e.RowIndex].TerreinNr = (int) dtv_Terreinen.Rows[e.RowIndex].Cells[1].Value;

        }

        private void dtv_optimisation_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == nc_aantalTerreinen.Value - 1)
            {
                e.CellStyle.BackColor = Color.Plum;

            }

        }

        private void btn_DeleteAll_Click(object sender, EventArgs e)
        {
            _wedstrijdlist.Clear();
            _Terreinlist.Clear();
           

            //_DataAccessLayer.CleanUpTable<Wedstrijd>();
            //_DataAccessLayer.CleanUpTable<Terrein>();

        }

    }
}
