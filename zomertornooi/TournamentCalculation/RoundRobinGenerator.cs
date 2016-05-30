using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using structures;
using ProgramDefinitions;
using System.Data;
using System.Collections.ObjectModel;

namespace TournamentCalculation
{
    public class RoundRobinGenerator
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RoundRobinGenerator));


        #region Calculate RoundRobin

        //Bereken Round Robin Wedstrijden
        public List<Wedstrijd> CalculateRoundRobinGames(Reeks r)
        {
            #region Initialize
            List<Ploeg> PloegHulp = r.Ploegen.ToList();
            List<Wedstrijd> RoundRobinGames = new List<Wedstrijd>();
            int aantalRondes = r.WedstrijdDefinition.AantalRoundRobin;

            int initialN = PloegHulp.Count;
            int n = PloegHulp.Count;
            //If number of teams is odd ==> odd one dummy team
            if (n % 2 == 1)
            {
                PloegHulp.Add(new Ploeg() { Ploegnaam = "Bye" });
                n = n + 1;
            }
            int[,] indices = new int[n / 2, 2];
            Wedstrijd[,] Roundrobin = new Wedstrijd[(n - 1) * aantalRondes, n / 2];
            #endregion
            
            # region First round
            for (int i = 0; i < n / 2; i++)
            {
                //Store team indices
                indices[i, 0] = i;
                indices[i, 1] = (n - 1) - i;

                //Find teams for next games
                Ploeg Home = PloegHulp[indices[i, 0]];
                Ploeg Away = PloegHulp[indices[i, 1]];

                //add Games to this round
                Roundrobin[0, i] = new Wedstrijd() { Home = Home, Away = Away ,Reeks = r, ReeksNaam = r.ReeksNaam};
                

            }

            # endregion

            # region Next Rounds
            //Next Rounds
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    Ploeg Home;
                    Ploeg Away;
                    //Adjust Indices
                    if (indices[j, 0] + (n / 2) < n - 1) { indices[j, 0] = indices[j, 0] + (n / 2); }
                    else { indices[j, 0] = indices[j, 0] - ((n / 2) - 1); }

                    if (indices[j, 1] + (n / 2) < n - 1) { indices[j, 1] = indices[j, 1] + (n / 2); }
                    else { indices[j, 1] = indices[j, 1] - ((n / 2) - 1); }

                    //Find teams for next games
                    Home = PloegHulp[indices[j, 0]];
                    Away = PloegHulp[indices[j, 1]];

                    if (j == 0 && i % 2 == 0)
                    {
                        Home = PloegHulp[n - 1];

                    }
                    else if (j == 0 && i % 2 == 1)
                    {
                        Away = PloegHulp[n - 1];
                    }

                    //Store games from this round in list
                    Roundrobin[i, j] = new Wedstrijd() { Home = Home, Away = Away, Reeks = r, ReeksNaam = r.ReeksNaam };
                    
                }
                //r.RoundRobin = Roundrobin;

            }



            if (initialN % 2 == 1)
            {
                //Eerste kolom in de roundrobintabel bevat wedstrijden tegen Bye, dus ook verwijderen
                int row = Roundrobin.GetLength(0);
                int column = Roundrobin.GetLength(1);
                Wedstrijd[,] NewRoundrobin = new Wedstrijd[row, column - 1];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 1; j < column; j++)
                    {
                        NewRoundrobin[i, j - 1] = Roundrobin[i, j];
                    }
                }

                Roundrobin = NewRoundrobin;

                //Verwijder 'bye' ploeg
                PloegHulp.Remove(PloegHulp[initialN]);


            }
            # endregion

            #region Multiplier
            int aantalR = Roundrobin.GetLength(0) / aantalRondes;
            int aantalC = Roundrobin.GetLength(1);

            if (aantalRondes > 1)
            {
                for (int i = 1; i < aantalRondes; i++)
                {
                    for (int row = 0; row < aantalR; row++)
                    {
                        for (int c = 0; c < aantalC; c++)
                        {
                            if (i % 2 == 0)
                            {
                                Ploeg Home = Roundrobin[row, c].Home;
                                Ploeg Away = Roundrobin[row, c].Away;
                                Roundrobin[row + i * aantalR, c] = new Wedstrijd() { Home = Home, Away = Away, Reeks = r, ReeksNaam = r.ReeksNaam };


                            }
                            else
                            {
                                Ploeg Home = Roundrobin[row, c].Away;
                                Ploeg Away = Roundrobin[row, c].Home;
                                Roundrobin[row + i * aantalR, c] = new Wedstrijd() { Home = Home, Away = Away, Reeks = r, ReeksNaam = r.ReeksNaam };

                            }

                        }

                    }

                }

            }


            #endregion


            //Array to List
            int nrRows = Roundrobin.GetLength(0);
            int nrCols = Roundrobin.GetLength(1);



            for (int row = 0; row < nrRows; row++)
            {
                for (int column = 0; column < nrCols; column++)
                {
                    Wedstrijd w = Roundrobin[row, column];
                    RoundRobinGames.Add(w);
                }
            }


            return RoundRobinGames;

        }

        #endregion

        #region Calculate Timeschedule

        //New algorithm to calculate Timeschedule
        public void CalculateTimeSchedule(List<Reeks> Reeksen)
        {
            //Haal alle ploegen uit de reeksen en stop ze in 1 list
            List<Ploeg> FreeTeams = GetFreeTeamsFromReeks(Reeksen);
            List<Wedstrijd> RoundGames = new List<Wedstrijd>();
            PrepareLists(Reeksen);
            //Aantal te spelen wedstrijden
            int AantalWedstrijden = FindNumberOfGames(Reeksen);
            int CurrentRound = 0;

            //Generate TimeSlots 
            GenerateTimeslots(Reeksen);

            while (AantalWedstrijden > 0)
            {
                for (int k = 0; k < Reeksen.Count; k++)
                {
                    RoundGames = RoundGames.Concat(CalculateRoundGames(Reeksen[k], CurrentRound)).ToList();
                    if (Reeksen[k].FreeTeams != null) { Reeksen[k].AantalRondes = CurrentRound + 1; }
                }

                //Vind scheidsrechters voor de rondewedstrijden
                Findreferees(Reeksen, RoundGames);

                //Vind de ploegen die rust kregen de vorige ronde
                FindRestTeams(Reeksen);

                //Reset Freeteams 
                ResetFreeTeams(Reeksen);

                //Update counter of the current round being calculated
                RoundGames.Clear();
                CurrentRound++;

                //Update de statusCounters van alle ploegen
                UpdatePloegCounters(Reeksen, CurrentRound);

                //Update aantal te spelen wedstrijden
                AantalWedstrijden = FindNumberOfGames(Reeksen);
            }

        }

        #region Methods

        //----------------//
        //Private methods //
        //----------------//

        private List<Ploeg> GetFreeTeamsFromReeks(List<Reeks> Reeksen)
        {
            List<Ploeg> Teams = new List<Ploeg>();

            //Stop alle vrije ploegen uit de reeksen in 1 List. 
            for (int k = 0; k < Reeksen.Count; k++)
            {
                if (Reeksen[k].FreeTeams != null)
                {
                    Teams = Teams.Concat(Reeksen[k].FreeTeams).ToList();
                }

            }
            return Teams;


        }

        // Reset FreeTeams
        private void ResetFreeTeams(List<Reeks> Reeksen)
        {
            for (int k = 0; k < Reeksen.Count; k++)
            {
                if (Reeksen[k].FreeTeams != null)
                {
                    Reeksen[k].FreeTeams = Reeksen[k].Ploegen.ToList();
                }

            }
        }

        public void PrepareLists(List<Reeks> Reeksen)
        {
            for (int k = 0; k < Reeksen.Count; k++)
            {
                Reeksen[k].TimeScheduleHulp = Reeksen[k].RoundRobin.ToList();
                Reeksen[k].TimeSchedule.Clear();
                Reeksen[k].FreeTeams = Reeksen[k].Ploegen.ToList();
                for (int p = 0; p < Reeksen[k].Ploegen.Count; p++)
                {
                    Reeksen[k].Ploegen[p].AantalxGespeeld = 0;
                    Reeksen[k].Ploegen[p].AantalxNaElkaarGespeeld = 0;
                    Reeksen[k].Ploegen[p].AantalxNaElkaarRust = 0;
                    Reeksen[k].Ploegen[p].AantalxNaElkaarScheids = 0;
                    Reeksen[k].Ploegen[p].AantalxRust = 0;
                    Reeksen[k].Ploegen[p].AantalxScheids = 0;
                    Reeksen[k].Ploegen[p].StatusList.Clear();
                }


                for (int t = 0; t < Reeksen[k].Terreinen.Count; t++)
                    {
                        if (Reeksen[k].Terreinen[t] != null && Reeksen[k].Terreinen[t].wedstrijden != null)
                        {
                            Reeksen[k].Terreinen[t].wedstrijden.Clear();
                        }
                    }
                




                //Reset optimization results
                Reeksen[k].AantalRondes = 0;
                Reeksen[k].MaxScheids = 0;
                Reeksen[k].MaxRust = 0;
                Reeksen[k].DubbelScheids = false;
                Reeksen[k].Vrijescheids = false;
                Reeksen[k].WedstrijdDefinition.StartingTimes.Clear();
            }

        }

        //Generate Timeslots
        private void GenerateTimeslots(List<Reeks> Reeksen)
        {
            

            for (int k = 0; k < Reeksen.Count; k++)
            {
                int mins = Reeksen[k].WedstrijdDefinition.Wedstrijdduur;
                int AantalRondesZon = Reeksen[k].WedstrijdDefinition.AantalrondesZaterdag + Reeksen[k].RoundRobin.Count;
                DateTime zaterdag = Reeksen[k].WedstrijdDefinition.AanvangsuurZat;
                DateTime zondag = Reeksen[k].WedstrijdDefinition.AanvangsuurZon;

                //Aanvangsuren aanmaken
                //Zaterdag
                for (int hh = 0; hh < Reeksen[k].WedstrijdDefinition.AantalrondesZaterdag; hh++)
                {
                    Reeksen[k].WedstrijdDefinition.StartingTimes.Add(zaterdag.AddMinutes(hh * mins));
                }
                //Zondag
                for (int hh = 0; hh < AantalRondesZon; hh++)
                {
                    Reeksen[k].WedstrijdDefinition.StartingTimes.Add(zondag.AddMinutes(hh * mins));
                }
            }

        }

        //Vind het totaal aantal wedstrijden in de reeksen
        private int FindNumberOfGames(List<Reeks> Reeksen)
        {
            int aantalWedstrijden = 0;
            for (int k = 0; k < Reeksen.Count; k++)
            {
                aantalWedstrijden += Reeksen[k].TimeScheduleHulp.Count;
            }

            return aantalWedstrijden;

        }

        //Update de status van de ploegen die rust kregen
        private void FindRestTeams(List<Reeks> Reeksen)
        {
            for (int k = 0; k < Reeksen.Count; k++)
            {
                if (Reeksen[k].FreeTeams != null)
                {
                    for (int p = 0; p < Reeksen[k].FreeTeams.Count; p++)
                    {
                        Reeksen[k].FreeTeams[p].StatusList.Add(Status.Rust);
                    }
                    //Alle statussen zijn aangepast, dus nu mogen alle FreeTeams lijsten gecleared worden
                    Reeksen[k].FreeTeams.Clear();
                }

            }


            




        }

        //Bereken de counters van alle ploegen 
        private void UpdatePloegCounters(List<Reeks> Reeksen, int cr)
        {
            //Reset all counters. 
            resetPloegCounters(Reeksen);

            for (int k = 0; k < Reeksen.Count; k++)
            {
                for (int p = 0; p < Reeksen[k].Ploegen.Count; p++)
                {
                    Ploeg ploeg = Reeksen[k].Ploegen[p];
                    for (int s = 0; s < cr; s++)
                    {
                        if (s < ploeg.StatusList.Count)
                        {
                            //Check eerste ronde
                            if (s == 0)
                            {
                                if (ploeg.StatusList[s] == Status.Gespeeld) { ploeg.AantalxGespeeld++; ploeg.AantalxNaElkaarGespeeld++; }
                                if (ploeg.StatusList[s] == Status.Scheidsrechter) { ploeg.AantalxScheids++; ploeg.AantalxNaElkaarScheids++; }
                                if (ploeg.StatusList[s] == Status.Rust) { ploeg.AantalxRust++; ploeg.AantalxNaElkaarRust++; }
                            }

                            //Vanaf tweede ronde ook status van vorige ronde bijhouden!
                            if (s > 0)
                            {
                                if (ploeg.StatusList[s] == Status.Gespeeld)
                                {
                                    ploeg.AantalxGespeeld++;
                                    if (ploeg.StatusList[s] == Status.Gespeeld && ploeg.StatusList[s - 1] == Status.Gespeeld) { ploeg.AantalxNaElkaarGespeeld++; }
                                    else { ploeg.AantalxNaElkaarGespeeld = 1; ploeg.AantalxNaElkaarScheids = 0; ploeg.AantalxNaElkaarRust = 0; }
                                }


                                if (ploeg.StatusList[s] == Status.Scheidsrechter)
                                {
                                    ploeg.AantalxScheids++;
                                    if (ploeg.StatusList[s] == Status.Scheidsrechter && ploeg.StatusList[s - 1] == Status.Scheidsrechter) { ploeg.AantalxNaElkaarScheids++; }
                                    else { ploeg.AantalxNaElkaarScheids = 1; ploeg.AantalxNaElkaarGespeeld = 0; ploeg.AantalxNaElkaarRust = 0; }
                                }


                                if (ploeg.StatusList[s] == Status.Rust)
                                {
                                    ploeg.AantalxRust++;
                                    if (ploeg.StatusList[s] == Status.Rust && ploeg.StatusList[s - 1] == Status.Rust) { ploeg.AantalxNaElkaarRust++; }
                                    else { ploeg.AantalxNaElkaarRust = 1; ploeg.AantalxNaElkaarGespeeld = 0; ploeg.AantalxNaElkaarScheids = 0; }
                                }
                                
                            }

                        }
                    }

                    //Update optimization results
                    Reeksen[k].MaxScheids = Math.Max(Reeksen[k].MaxScheids, ploeg.AantalxScheids);
                    Reeksen[k].MaxRust = Math.Max(Reeksen[k].MaxRust, ploeg.AantalxRust);
                }
            }

        }

        //Reset Counter 
        private void resetPloegCounters(List<Reeks> Reeksen)
        {
            for (int k = 0; k < Reeksen.Count; k++)
            {
                for (int p = 0; p < Reeksen[k].Ploegen.Count; p++)
                {
                    Ploeg ploeg = Reeksen[k].Ploegen[p];
                    ploeg.AantalxGespeeld = 0;
                    ploeg.AantalxNaElkaarGespeeld = 0;
                    ploeg.AantalxScheids = 0;
                    ploeg.AantalxNaElkaarScheids = 0;
                    ploeg.AantalxRust = 0;
                    ploeg.AantalxNaElkaarRust = 0;
                }
            }


        }

        //Vind scheidsrechters voor de rondewedstrijden binnen alle reeksen
        private void Findreferees(List<Reeks> Reeksen, List<Wedstrijd> RoundGames)
        {
            List<Ploeg> Scheidsrechters = GetFreeTeamsFromReeks(Reeksen);
            Scheidsrechters = Scheidsrechters.Where(ploeg => ploeg.AantalxNaElkaarScheids < 1).ToList();
            //Scheidsrechters = Scheidsrechters.Where(ploeg => ploeg.AantalxNaElkaarRust < 1).ToList();

            Scheidsrechters = Scheidsrechters.OrderBy(p => p.AantalxScheids).ToList();
            Scheidsrechters = Scheidsrechters.Concat(Scheidsrechters).ToList();

            for (int rg = 0; rg < RoundGames.Count; rg++)
            {
                if (Scheidsrechters.Count > 0)
                {
                    //Verwijder Team uit de Freeteam List
                    for (int k = 0; k < Reeksen.Count; k++)
                    {
                        if (Reeksen[k].FreeTeams != null && Reeksen[k].FreeTeams.Contains(Scheidsrechters[0]))
                        {
                            RoundGames[rg].Scheidsrechter = Scheidsrechters[0];
                            Scheidsrechters[0].StatusList.Add(Status.Scheidsrechter);
                            Reeksen[k].FreeTeams.Remove(Scheidsrechters[0]);
                        }
                        //Zoek van welke reeks de (dubbele) scheids afkomstig is
                        else if (Reeksen[k].Ploegen.Contains(Scheidsrechters[0]))
                        {
                            RoundGames[rg].Scheidsrechter = Scheidsrechters[0];
                            Reeksen[k].DubbelScheids = true;
                        }
                    }
                    //Verwijder ook uit scheidsrechter list
                    Scheidsrechters.RemoveAt(0);
                }
                else
                {
                    RoundGames[rg].Scheidsrechter = null;
                    //Vind uit welke reeks deze wedstrijd afstamt
                    RoundGames[rg].Reeks.Vrijescheids = true;
                }






            }






        }

        //Check if there are timeslote whith limited number of games
        public bool CheckTimeschedule(List<Reeks> Reeksen)
        {
            bool isFull = true;
            for (int k = 0; k < Reeksen.Count; k++)
            {
                for (int r = 0; r < Reeksen[k].AantalRondes - 1; r++)
                {
                    List<Wedstrijd> games = Reeksen[k].TimeSchedule.Where(w => w.Aanvangsuur.Equals(Reeksen[k].WedstrijdDefinition.StartingTimes[r])).ToList();

                    if (games.Count < Reeksen[k].Terreinen.Count)
                    {
                        isFull = false;
                    }



                }
            }

            return isFull;
        }



        //Bereken wedstrijd van 1 ronde/reeks

        private List<Wedstrijd> CalculateRoundGames(Reeks r, int ronde)
        {
            List<Wedstrijd> RoundGames = new List<Wedstrijd>();

            //Als alle wedstrijden gespeeld zijn in reeks ==> Geen scheidsrechters meer afleveren
            if (r.TimeScheduleHulp.Count == 0) { r.FreeTeams = null; }

            //Bereken de wedstrijden in 1 ronde.
            for (int t = 0; t < r.Terreinen.Count; t++)
            {
                //Bereken wedstrijden 
                if (r.TimeScheduleHulp.Count > 0)
                {
                    Wedstrijd NextGame = FindOptimalGame(r);
                    if (NextGame != null)
                    {
                        //Remove Home & Away from FreeTeams
                        r.FreeTeams.Remove(NextGame.Home);
                        r.FreeTeams.Remove(NextGame.Away);

                        //Add status for both teams
                        NextGame.Home.StatusList.Add(Status.Gespeeld);
                        NextGame.Away.StatusList.Add(Status.Gespeeld);

                        //Add Terrain to game 
                        NextGame.Terrein = r.Terreinen[t];
                        NextGame.Terrein.wedstrijden.Add(NextGame);

                        //Add starting time to game
                        NextGame.Aanvangsuur = r.WedstrijdDefinition.StartingTimes[ronde];

                        //Remove Game from list with games to be played
                        r.TimeScheduleHulp.Remove(NextGame);

                        //Add game to final timeschedule 
                        r.TimeSchedule.Add(NextGame);

                        //Add to Roundgames
                        RoundGames.Add(NextGame);
                    }
                }
            }

            //Geef de teams terug die eventueel scheidsrechter kunnen zijn.
            return RoundGames;
        }

        //Vind best passende wedstrijden
        private Wedstrijd FindOptimalGame(Reeks r)
        {
            int WedNaElkaar = r.WedstrijdDefinition.MaxNaElkaarSpelen;

            List<Wedstrijd> Wedstrijden = r.TimeScheduleHulp;
            List<Ploeg> Ploegen = r.FreeTeams;

            List<Ploeg> Teams = Ploegen.ToList();

            //Remove Teams which have played x times after each other
            Teams = Teams.Where(w => w.AantalxNaElkaarGespeeld < WedNaElkaar).ToList();

            //Get list of possible games
            List<Wedstrijd> MogelijkeWedstrijden = Wedstrijden.Where(w => (Teams.Contains(w.Home) && Teams.Contains(w.Away))).ToList();

            //Wedstrijd aanmaken om nadien in te vullen
            Wedstrijd OptimalGame = null;

            string a;
            string b;

            //Check of er mogelijke wedstrijden zijn en sorteer op beste manier
            if (MogelijkeWedstrijden.Count > 0)
            {
                MogelijkeWedstrijden = MogelijkeWedstrijden.OrderByDescending(w => w.Home.AantalxNaElkaarRust)
                    .ThenByDescending(w => w.Away.AantalxNaElkaarRust)
                    .ThenByDescending(w => w.Home.AantalxNaElkaarScheids)
                    .ThenByDescending(w => w.Away.AantalxNaElkaarScheids)
                    .ThenBy(w => w.Home.AantalxNaElkaarGespeeld)
                    .ThenBy(w => w.Away.AantalxNaElkaarGespeeld)
                    .ThenBy(w => w.Home.AantalxGespeeld)
                    .ThenBy(w => w.Away.AantalxGespeeld).ToList();

                //Random game selection
                //Random rand = new Random();
                //OptimalGame = MogelijkeWedstrijden[rand.Next(0, MogelijkeWedstrijden.Count)];

                //Best matching
                OptimalGame = MogelijkeWedstrijden[0];

                a = OptimalGame.Home.Ploegnaam;
                b = OptimalGame.Away.Ploegnaam;
            }
            return OptimalGame;
        }

        //---------------//
        //Public methods //
        //---------------//

        //Bereken optimaal aantal terreinen
        public List<Terrein> CalculateNrTerrains(Reeks r)
        {
            List<Terrein> terreinen = new List<Terrein>();
            int nrTeams = r.Ploegen.Count();
            int NrTerrains = (int)Math.Floor(Convert.ToDouble(nrTeams) / 3);

            for (int t = 0; t < NrTerrains; t++)
            {
                terreinen.Add(new Terrein() { TerreinNr = t, ReeksNaam = r.ReeksNaam });
            }
            return terreinen;
        }

        //Geef zelf een aantal terreinen in
        public void SetNrTerrains(Reeks r, int Aantal)
        {
            //List<Terrein> terreinen = new List<Terrein>();
            if (r.Terreinen.Count == 0)
            {
                for (int t = 0; t < Aantal; t++)
                {
                    r.Terreinen.Add(new Terrein() { TerreinNr = t, ReeksNaam = r.ReeksNaam });
                }
            }
            else if (Aantal >= r.Terreinen.Count)
            {
                for (int t = 0; t < Aantal - r.Terreinen.Count; t++)
                {
                    r.Terreinen.Add(new Terrein() { TerreinNr = r.Terreinen.Last().TerreinNr + 1, ReeksNaam = r.ReeksNaam });
                }
            }
            else if (Aantal < r.Terreinen.Count)
            {
                for (int t = 0; t < r.Terreinen.Count-Aantal; t++)
                {
                    r.Terreinen.Remove(r.Terreinen.Last());
                }
            }


            //r.Terreinen.Clear();

            //return terreinen;
        }

        public List<Wedstrijd> GetAllGames(List<Reeks> Reeksen)
        {
            List<Wedstrijd> Allewedstrijden = new List<Wedstrijd>();

            for (int k = 0; k < Reeksen.Count; k++)
            {
                Allewedstrijden = Allewedstrijden.Concat(Reeksen[k].TimeSchedule).ToList();
            }


                return Allewedstrijden;
        }

        public List<Terrein> GetAllTerrains(List<Reeks> Reeksen)
        {
            List<Terrein> AlleTerreinen = new List<Terrein>();

            for (int k = 0; k < Reeksen.Count; k++)
            {
                AlleTerreinen = AlleTerreinen.Concat(Reeksen[k].Terreinen).ToList();
            }

            return AlleTerreinen;
        } 





        #endregion


        #region Logging

        public void logRoundRobin(Reeks r)
        {
            string ConsoleDebug = "";
            int index = 0;

           
            for (int j = 0; j < r.Ploegen.Count - 1; j++)
            {
                ConsoleDebug = "";
                for (int i = 0; i < Math.Floor(Convert.ToDouble(r.Ploegen.Count) / Convert.ToDouble(2)); i++)
                {
                    ConsoleDebug += r.RoundRobin[index].Home.Ploegnaam + " - " + r.RoundRobin[index].Away.Ploegnaam + "\t";
                    index++;
                }
                logger.Debug(ConsoleDebug);
            }





        }

        public void LogTimeSchedule(Reeks r)
        {
            string ConsoleDebug = "";

            int index = 0;
            for (int ts = 0; ts < r.AantalRondes; ts++)
            {

                ConsoleDebug += r.WedstrijdDefinition.StartingTimes[ts].ToShortTimeString() + "\t";
                for (int t = 0; t < r.Terreinen.Count(); t++)
                {
                    if (index < r.TimeSchedule.Count)
                    {
                        if (r.TimeSchedule[index].Terrein == r.Terreinen[t] && r.TimeSchedule[index].Aanvangsuur == r.WedstrijdDefinition.StartingTimes[ts])
                        {
                            if (r.TimeSchedule[index].Scheidsrechter != null)
                            {
                                ConsoleDebug += r.TimeSchedule[index].Home.Ploegnaam + " - " + r.TimeSchedule[index].Away.Ploegnaam + "\t" + r.TimeSchedule[index].Scheidsrechter.Ploegnaam + "\t";
                            }
                            else
                            {
                                ConsoleDebug += r.TimeSchedule[index].Home.Ploegnaam + " - " + r.TimeSchedule[index].Away.Ploegnaam + "\t" + "Vrije Scheidsrechter" + "\t";
                            }
                            index++;
                        }
                    }
                    
                }

                logger.Debug(ConsoleDebug);
                ConsoleDebug = "";
            }








        }

        public void LogStatusList(Reeks r)
        {
            string ConsoleDebug = "";

            for (int p = 0; p < r.Ploegen.Count; p++)
            {
                ConsoleDebug += r.Ploegen[p].Ploegnaam + "\t";
            }
            logger.Debug(ConsoleDebug);
           

            for (int s = 0; s < r.Ploegen[0].StatusList.Count; s++)
            {
                ConsoleDebug = "";
                for (int p = 0; p < r.Ploegen.Count; p++)
                {
                    ConsoleDebug += r.Ploegen[p].StatusList[s].ToString() + "\t";
                }
                logger.Debug(ConsoleDebug);
            }



        }



        #endregion


#endregion

        #region Calculate Post RoundRobin Games

        public void SimulateFinalGames(List<Reeks> Reeksen)
        {
            switch (Reeksen[0].TornooiFormule)
            {
                case TornooiFormule.RoundRobin:
                    break;

                case TornooiFormule.PlacementGames:
                    for (int k = 0; k < Reeksen.Count; k++)
                    {                        
                        //Aantal wedstrijden bepalen
                        List<Wedstrijd> Placementgames = new List<Wedstrijd>();
                        int n = Reeksen[k].Ploegen.Count;
                        if (n % 2 == 1)
                        {
                            n--;
                        }

                        //Haal aanvangsuur op van laatste gespeelde wedstrijd
                        DateTime tijd = Reeksen[k].TimeSchedule.Last().Aanvangsuur;

                        
                        // Dummy wedstrijden aanmaken
                        int terrein = 0;
                        int Aanvang = 1;
                        //Start placement games pas in volgende ronde: 
                        Reeksen[k].AantalRondes++;


                        for (int w = 0; w < n / 2; w++)
                        {
                            //Maak Dummy team
                            Ploeg DummyHome = new Ploeg() { Ploegnaam = Reeksen[k].ReeksNaam + "_Home" + w.ToString() };
                            Ploeg DummyAway = new Ploeg() { Ploegnaam = Reeksen[k].ReeksNaam + "_Away" + w.ToString() +"*"};
                            Ploeg Scheids = new Ploeg() { Ploegnaam = "Scheidsrechter"};
                            Reeksen[k].TimeSchedule.Add(new Wedstrijd() { Home = DummyHome, Away = DummyAway, Scheidsrechter = Scheids, Aanvangsuur = tijd.AddMinutes(Aanvang * Reeksen[k].WedstrijdDefinition.Wedstrijdduur), Terrein = Reeksen[k].Terreinen[terrein],Reeks = Reeksen[k], ReeksNaam = Reeksen[k].ReeksNaam });
                            terrein++;
                            //Als het aantal terreinen bereikt is, resetten van terrein counters + updaten van rondecounters 
                            if ((w + 1) % Reeksen[k].Terreinen.Count == 0)
                            {
                                //Voeg ronde toe aan timeschedule
                                Reeksen[k].AantalRondes++;
                                Aanvang++;
                                terrein = 0;
                            }
                        }
                    }
                    break;

                case TornooiFormule.CrossFinals:

                    switch (Reeksen.Count)
                    {
                        case 2:
                            break;
                        case 4:
                            break;

                    }






























                    break;
            }




        }


        public void CalculateFinalGames(List<Reeks> Reeksen)
        {
            switch (Reeksen[0].TornooiFormule)
            {
                case TornooiFormule.RoundRobin:
                    break;

                case TornooiFormule.PlacementGames:
                    break;

                case TornooiFormule.CrossFinals:
                    break;
            }



            for (int i = 0; i < Reeksen.Count; i++)
            {

            }
        }






        #endregion

        #region Calculate Rankings

        #endregion

        #region Tournament optimization

        //Calculate optimal number of terrains 
        public DataTable OptimizeTournament(List<Reeks> Reeksen)
        {
            List<Reeks> InitialReeksen = Reeksen.ToList();

            List<DataTable> Optimizationresults = new List<DataTable>();

            for (int k = 0; k < Reeksen.Count; k++)
            {
                List<Terrein> initialTerreinen = Reeksen[k].Terreinen.ToList();


                DataTable Optimizationresult = new DataTable();
                Optimizationresult.Columns.Add("Aantal Terreinen", typeof(int));
                Optimizationresult.Columns.Add("Aantal Rondes", typeof(int));
                Optimizationresult.Columns.Add("Aanvang", typeof(string));
                Optimizationresult.Columns.Add("Laatste wedstrijd", typeof(string));
                Optimizationresult.Columns.Add("MaxScheids", typeof(int));
                Optimizationresult.Columns.Add("MaxRust", typeof(int));
                Optimizationresult.Columns.Add("Dubbel scheids", typeof(bool));
                Optimizationresult.Columns.Add("Vrije scheids", typeof(bool));

                //Bereken optimaal aantal terreinen
                List<Terrein> terreinen = CalculateNrTerrains(Reeksen[k]);
                int aantalTerreinen = terreinen.Count;

                for (int i = 1; i < 2 * aantalTerreinen+1; i++)
                {
                    SetNrTerrains(Reeksen[k], i);
                    CalculateTimeSchedule(Reeksen);

                    int aantR = Reeksen[k].AantalRondes;
                    int maxS = Reeksen[k].MaxScheids;
                    int maxR = Reeksen[k].MaxRust;
                    bool dubbelS = Reeksen[k].DubbelScheids;
                    bool vrijeS = Reeksen[k].Vrijescheids;
                    string aanvang = Reeksen[k].WedstrijdDefinition.AanvangsuurZat.ToShortTimeString();
                    string einde = String.Format("{0:d/M/yy HH:mm}", Reeksen[k].WedstrijdDefinition.StartingTimes[aantR - 1]);
                    Optimizationresult.Rows.Add(i, aantR, aanvang, einde, maxS, maxR, dubbelS, vrijeS);
                }
                Optimizationresults.Add(Optimizationresult);

                //Restore initial information
                Reeksen[k].Terreinen = initialTerreinen;
                CalculateTimeSchedule(Reeksen);            }



            return Optimizationresults[0];
        }

        public void SimulateTournament(List<Reeks> Reeksen)
        {

        }

        #endregion




    }

}

