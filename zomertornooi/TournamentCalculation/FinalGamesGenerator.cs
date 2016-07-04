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
        private List<PloegHulp> _ploegenHulp = new List<PloegHulp>();
        private List<Wedstrijd> _wedstrijden = new List<Wedstrijd>();
        private WedstrijdFormule _WedstrijdFormule = WedstrijdFormule.PlacementGames;
        private string _ReeksNaam;

        public FinalGamesGenerator(List<AdministratieReeks> reeksen, ActiveBindingList<Terrein> terreinen, DateTime Aanvangsuur, int Interval, string Reeksnaam)
        {
            _Reeksen = reeksen;
            _Terreinen = terreinen;
            _Aanvangsuur = Aanvangsuur;
            _Interval = Interval;
            _ReeksNaam = Reeksnaam;
        }

        public List<Wedstrijd> CalculateFinalGames()
        {
            switch (_Reeksen.Count)
            {
                case 1:
                    _WedstrijdFormule = WedstrijdFormule.PlacementGames;
                    CalculateGames();
                    break;
                case 2:
                    _WedstrijdFormule = WedstrijdFormule.CrossFinals;
                    CalculateGames();
                    break;
                case 3:
                    _WedstrijdFormule = WedstrijdFormule.CrossFinals;
                    CalculateGames();
                    break;
                case 4:
                    _WedstrijdFormule = WedstrijdFormule.CrossFinals;
                    CalculateGames();
                    break;
            }




            //_wedstrijden.Reverse();
            return _wedstrijden;
        }


        private void CalculateGames()
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

                    //zet de juiste reeksnaam
                    w.ReeksNaam = _ReeksNaam;

                    //Zet de juiste tornooiformule vast
                    w.WedstrijdFormule = _WedstrijdFormule;

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
            //Check of er ploegen zijn die enkel op zaterdag hebben gespeeld? 
            for (int j = 0; j < _Reeksen.Count; j++)
            {
                DataTable dt = _Reeksen[j].Klassement.Ranking;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        DataRow dr = dt.Rows[i];
                        Ploeg p = (Ploeg)dr[0];
                        if (p.OnlyOnSaterday)
                        {
                            dt.Rows.Remove(dr);
                        }
                }


            }



            List<Ploeg> _FreeTeams = new List<Ploeg>();
            _ploegen.Clear();

            //Get max nr of teams in reeksen
            int maxP = 0;
            for (int i = 0; i < _Reeksen.Count; i++)
            {
                maxP = Math.Max(_Reeksen[i].ReeksPloegen.Count, maxP);

            }
            //Store ploegen in logical order in list
            List<PloegHulp> pH_hulp = new List<PloegHulp>();

            for (int i = 0; i < maxP; i++)
            {
                for (int j = 0; j < _Reeksen.Count; j++)
                {
                    DataTable dt = _Reeksen[j].Klassement.Ranking;
                    if (i < dt.Rows.Count)
                    {
                        DataRow dr = dt.Rows[i];
                        Ploeg p = (Ploeg)dr[0];
                        int punten = (int)dr[8];
                        int GS = (int)dr[6];
                        int VS = (int)dr[7];
                        int AW = (int)dr[1];
                        PloegHulp PH = new PloegHulp(p, punten, GS, VS, AW);
                        pH_hulp.Add(PH);
                        
                        //_ploegen.Add(p);
                        //_FreeTeams.Add(p);
                    }
                }


                pH_hulp = pH_hulp.OrderByDescending(p => p.Setcoeff).ThenByDescending(p => p.Punten).ToList();
                if (i % 2 == 0)
                {
                    pH_hulp.Reverse();
                    _ploegenHulp.AddRange(pH_hulp);
                }
                else
                {
                    _ploegenHulp.AddRange(pH_hulp);
                }
                pH_hulp.Clear();
            }

            //Indien oneven, verwijder laatste ploeg! 
            if (_ploegenHulp.Count % 2 == 1)
            {
                //_ploegen.Remove(_ploegen.Last());
                //_FreeTeams.Remove(_FreeTeams.Last());
                _ploegenHulp.Remove(_ploegenHulp.Last());
            }


            //Populate ploegen en freeteams in this order: 
            _ploegen.Clear();
            _FreeTeams.Clear();
            foreach (PloegHulp ph in _ploegenHulp)
            {
                _ploegen.Add(ph.Ploeg);
                _FreeTeams.Add(ph.Ploeg);
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
            for (int i = 0; i < _ploegen.Count; i+=2)
            {
                _HulpWedstrijden.Add(new Wedstrijd() { Home = _ploegen[i], Away = _ploegen[i+1], WedstrijdFormule = ProgramDefinitions.WedstrijdFormule.PlacementGames});
            }

            return _HulpWedstrijden;
        }

        private List<Wedstrijd> CalculateCrossFinals()
        {
            List<Wedstrijd> _HulpWedstrijden = new List<Wedstrijd>();
            List<Wedstrijd> _EindWedstrijden = new List<Wedstrijd>();
            for (int i = 0; i < _ploegen.Count; i += 2)
            {
                _HulpWedstrijden.Add(new Wedstrijd() { Home = _ploegen[i], Away = _ploegen[i + 1], WedstrijdFormule = ProgramDefinitions.WedstrijdFormule.PlacementGames });
            }

            return _HulpWedstrijden;
        }

    }


    public class PloegHulp
    {
        private Ploeg _Ploeg;

        public Ploeg Ploeg
        {
            get { return _Ploeg; }
            set { _Ploeg = value; }
        }

        private int _AantalWedstrijden;

        public int AantalWedstrijden
        {
            get { return _AantalWedstrijden; }
            set { _AantalWedstrijden = value; }
        }



        private int _Punten;

        public int Punten
        {
            get { return _Punten; }
            set { _Punten = value; }
        }
        private int _GewonnenSets;

        public int GewonnenSets
        {
            get { return _GewonnenSets; }
            set { _GewonnenSets = value; }
        }
        private int _VerlorenSets;

        public int VerlorenSets
        {
            get { return _VerlorenSets; }
            set { _VerlorenSets = value; }
        }


        private double _Setcoeff;

        public double Setcoeff
        {
            get { return _Setcoeff; }
            set { _Setcoeff = value; }
        }


        public PloegHulp(Ploeg Ploeg, int Punten, int GewonnenSets, int VerlorenSets, int AantalWedstrijden)
        {
            _Ploeg = Ploeg;
            _AantalWedstrijden = AantalWedstrijden;
            _Punten = Punten;
            _GewonnenSets = GewonnenSets;
            _VerlorenSets = VerlorenSets;

            _Setcoeff = (Convert.ToDouble(_GewonnenSets) - Convert.ToDouble(_VerlorenSets)) / Convert.ToDouble(_AantalWedstrijden);

        }
    }




}
