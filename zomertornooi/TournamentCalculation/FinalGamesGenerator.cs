using Factory;
using ProgramDefinitions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structures.TournamentCalculation
{
    public class FinalGamesGenerator
    {
        private List<AdministratieReeks> _Reeksen = new List<AdministratieReeks>();
        private ActiveBindingList<Terrein> _Terreinen;
        private DateTime _Aanvangsuur = new DateTime();
        private int _Interval = 0;
        private List<Ploeg> _ploegen = new List<Ploeg>();
        private List<Wedstrijd> _wedstrijden = new List<Wedstrijd>();

        public FinalGamesGenerator(List<AdministratieReeks> reeksen, ActiveBindingList<Terrein> terreinen, DateTime Aanvangsuur, int Interval)
        {
            _Reeksen = reeksen;
            _Terreinen = terreinen;
            _Aanvangsuur = Aanvangsuur;
            _Interval = Interval;
        }

        public List<Wedstrijd> CalculateFinalGames()
        {
            switch (_Reeksen.Count)
            {
                case 1:
                    CalculateFinalGames_1();
                    break;
                case 2:
                    CalculateFinalGames_2();
                    break;
                case 3:
                    CalculateFinalGames_3();
                    break;
                case 4:
                    CalculateFinalGames_4();
                    break;
            }


            return _wedstrijden;
        }


        private void CalculateFinalGames_1()
        {
            //Get ploegen from rankings, put them in the correct order in the list
            List<Ploeg> _Freeteams = GetTeams();

            //Bepaal het aantal terreinen
            List<Terrein> _TerreinList = GetTerrains();

            //Bereken de wedstrijden (zonder scheids, terrein)
            List<Wedstrijd> _WedstrijdList = CalculatePlacementGames();

            //Bepaal scheidsrechter, terrein en aanvangsuur
            int currentRound = 0;


            while (_WedstrijdList.Count > 0)
            {
                // haal de volgende wedstrijden op en zet ploegen op 'bezet'
                List<Wedstrijd> _NextGames = GetNextGames(_WedstrijdList, _TerreinList, _Freeteams, currentRound);

                //Vind scheidsrechters
                Findreferees(_NextGames, _Freeteams);

                //Zet alle ploegen terug op vrije status
                _Freeteams = _ploegen.ToList();

                //Voeg wedstrijden toe aan lijst 
                _wedstrijden.AddRange(_NextGames);

                //Update counter of the current round being calculated
                currentRound++;
            }


            //Laatste twee wedstrijden zonder scheidsrechter?!

        }

        private void Findreferees(List<Wedstrijd> _NextGames, List<Ploeg> _Freeteams)
        {
            List<Ploeg> Scheidsrechters = _Freeteams.ToList();
            //Sorteer de vrije teams naar aantal keer scheidsrechter 
            Scheidsrechters = Scheidsrechters.Where(ploeg => ploeg.AantalxNaElkaarScheids < 1).ToList();
            //Scheidsrechters = Scheidsrechters.Where(ploeg => ploeg.AantalxNaElkaarRust < 1).ToList();

            Scheidsrechters = Scheidsrechters.OrderBy(p => p.AantalxScheids).ThenBy(p => p.AantalxRust).ToList();
            Scheidsrechters = Scheidsrechters.Concat(Scheidsrechters).ToList();

            //Scheidsrechters toekennen
            for (int rg = 0; rg < _NextGames.Count; rg++)
            {
                if (Scheidsrechters.Count > 0)
                {
                    if (_Freeteams != null && _Freeteams.Contains(Scheidsrechters[0]))
                    {
                        _NextGames[rg].Scheidsrechter = Scheidsrechters[0];
                        Scheidsrechters[0].StatusList.Add(Status.Scheidsrechter);
                        _Freeteams.Remove(Scheidsrechters[0]);
                    }
                    //Verwijder ook uit scheidsrechter list
                    Scheidsrechters.RemoveAt(0);
                }
                else
                {
                    _NextGames[rg].Scheidsrechter = null;
                }

            }
        }


        private List<Wedstrijd> GetNextGames(List<Wedstrijd> _WedstrijdList, List<Terrein> _TerreinList, List<Ploeg> _Freeteams, int currentRound)
        {
            List<Wedstrijd> NextGames = new List<Wedstrijd>();

            for (int i = 0; i < _TerreinList.Count; i++)
            {
                if (_WedstrijdList.Count > 0)
                {
                    //Haal de wedstrijden op
                    Wedstrijd w = _WedstrijdList.Last();

                    //Geef terrein op
                    w.Terrein = _TerreinList[i];

                    //geef aanvangsuur op
                    w.Aanvangsuur = _Aanvangsuur.AddMinutes(_Interval * currentRound);

                    //Zet de teams in deze wedstrijd op bezet
                    _Freeteams.Remove(w.Home);
                    _Freeteams.Remove(w.Away);

                    //Voeg wedstrijd toe aan volgende wedstrijden 
                    NextGames.Add(w);

                    //Verwijder wedstrijd uit de volledige lijst
                    _WedstrijdList.Remove(_WedstrijdList.Last());
                }
            }




            return NextGames;
        }








        private void CalculateFinalGames_2()
        {
            //Get ploegen from rankings, put them in the correct order in the list
            List<Ploeg> _Freeteams = GetTeams();

            //Bepaal het aantal terreinen
            List<Terrein> _TerreinList = GetTerrains();

        }

        private void CalculateFinalGames_3()
        {
            //Get ploegen from rankings, put them in the correct order in the list
            List<Ploeg> _Freeteams = GetTeams();

            //Bepaal het aantal terreinen
            List<Terrein> _TerreinList = GetTerrains();
        }

        private void CalculateFinalGames_4()
        {
            //Get ploegen from rankings, put them in the correct order in the list
            List<Ploeg> _Freeteams = GetTeams();

            //Bepaal het aantal terreinen
            List<Terrein> _TerreinList = GetTerrains();
        }


        //Methods
        private List<Ploeg> GetTeams()
        {
            List<Ploeg> _FreeTeams = new List<Ploeg>();
            _ploegen.Clear();

            //Get max nr of teams in reeksen
            int maxP = 0;
            for (int i = 0; i < _Reeksen.Count; i++)
            {
                maxP = Math.Max(_Reeksen[i].ReeksPloegen.Count, maxP);

            }
            //Store ploegen in logical order in list

            for (int i = 0; i < maxP; i++)
            {
                for (int j = 0; j < _Reeksen.Count; j++)
                {
                    DataTable dt = _Reeksen[j].Klassement.Ranking;
                    if (i < dt.Rows.Count)
                    {
                        DataRow dr = dt.Rows[i];
                        Ploeg p = (Ploeg)dr[0];
                        _ploegen.Add(p);
                        _FreeTeams.Add(p);
                    }



                }
            }

            //Indien oneven, verwijder laatste ploeg! 
            if (_ploegen.Count % 2 == 1)
            {
                _ploegen.Remove(_ploegen.Last());
                _FreeTeams.Remove(_FreeTeams.Last());
            }

            return _FreeTeams;
        }

        private List<Terrein> GetTerrains()
        {
            List<Terrein> _terreinlist = new List<Terrein>();

            for (int i = 0; i < _Terreinen.Count; i++)
            {
                for (int j = 0; j < _Reeksen.Count; j++)
                {
                    if (!_terreinlist.Contains(_Terreinen[i]) && _Terreinen[i].ReeksNaam == _Reeksen[j].ReeksNaam)
                    {
                        _terreinlist.Add(_Terreinen[i]);
                    }
                }
            }

            //Bepaal optimaal aantal terreinen geef enkel dit aantal terreinen door
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

            //Gebruik enkel dit aantal terreinen
            for (int i = 0; i < _terreinlist.Count-NrTerrains; i++)
            {
                _terreinlist.Remove(_terreinlist.Last());
            }
            return _terreinlist;
        }

        private List<Wedstrijd> CalculateCrossFinalGames()
        {
            List<Wedstrijd> _EindWedstrijden = new List<Wedstrijd>();













            return _EindWedstrijden;
        }




        //1Reeks
        private List<Wedstrijd> CalculatePlacementGames()
        {   
            List<Wedstrijd> _HulpWedstrijden = new List<Wedstrijd>();
            List<Wedstrijd> _EindWedstrijden = new List<Wedstrijd>();
            for (int i = 0; i < _ploegen.Count; i+=2)
            {
                _HulpWedstrijden.Add(new Wedstrijd() { Home = _ploegen[i], Away = _ploegen[i+1], WedstrijdFormule = ProgramDefinitions.WedstrijdFormule.PlacementGames});
            }

            return _HulpWedstrijden;
        }

    }
}
