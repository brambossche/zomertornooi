using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory;
using System.ComponentModel;
using structures.structures;

namespace structures
{
    public class AdministratieReeks
    {
        private string _ReeksNaam = "";

        public string ReeksNaam
        {
            get { return _ReeksNaam; }
            set { _ReeksNaam = value; }
        }

        private Klassement _Klassement = null;

        public Klassement Klassement
        {
            get { return _Klassement; }
            set { _Klassement = value; }
        }



        private BindingList<Wedstrijd> _ReeksWedstrijden = new BindingList<Wedstrijd>();

        public BindingList<Wedstrijd> ReeksWedstrijden
        {
            get { return _ReeksWedstrijden; }
            set { _ReeksWedstrijden = value; }
        }

        private List<Ploeg> _ReeksPloegen = new List<Ploeg>();

        public List<Ploeg> ReeksPloegen
        {
            get { return _ReeksPloegen; }
            set { _ReeksPloegen = value; }
        }

        private List<Terrein> _ReeksTerreinen = new List<Terrein>();

        public List<Terrein> ReeksTerreinen
        {
            get { return _ReeksTerreinen; }
            set { _ReeksTerreinen = value; }
        }


        public AdministratieReeks(string rn, BindingList<Wedstrijd> w)
        {
            ReeksNaam = rn;
            _ReeksWedstrijden = w;
            Klassement = new Klassement() { ReeksNaam = rn };
            CalculateTeams();
        }


        public void CalculateTeams()
        {
            foreach (Wedstrijd w in _ReeksWedstrijden)
            {
                if (!_ReeksPloegen.Contains(w.Home))
                {
                    _ReeksPloegen.Add(w.Home);
                }
                if (!_ReeksPloegen.Contains(w.Away))
                {
                    _ReeksPloegen.Add(w.Away);
                }
            }
        }


        public void CalculateRankings()
        {
            BindingList<Wedstrijd> Wedstrijden = _ReeksWedstrijden;        

            DataTable Rankings = new DataTable();
            Rankings.Columns.Add("Ploeg", typeof(Ploeg));
            Rankings.Columns.Add("Aantal Wed.", typeof(int));
            Rankings.Columns.Add("Aantal Gew.", typeof(int));
            Rankings.Columns.Add("Aantal Gew. 2-1", typeof(int));
            Rankings.Columns.Add("Aantal Verl. 2-1", typeof(int));
            Rankings.Columns.Add("Aantal Verl.", typeof(int));
            Rankings.Columns.Add("Aantal Gew. Sets", typeof(int));
            Rankings.Columns.Add("Aantal Verl. Sets", typeof(int));
            Rankings.Columns.Add("Totale Punten", typeof(int));

            for (int p = 0; p < _ReeksPloegen.Count; p++)
            {
                Ploeg ploeg = _ReeksPloegen[p];
                int aantalWed = 0;
                int AantalGew = 0;
                int AantalGew21 = 0;
                int AantalVerl21 = 0;
                int AantalVerl = 0;
                int AantalGewSets = 0;
                int AantalVerlSets = 0;
                int TotalePunten = 0;




                for (int i = 0; i < Wedstrijden.Count; i++)
                {
                    int SetsG = 0;
                    int SetsV = 0;
                    int PuntenH = 0;
                    int PuntenA = 0;

                    //Indien thuisploeg
                    if (Wedstrijden[i].Home.Equals(ploeg))
                    {
                        
                        if (Wedstrijden[i].Set1Home != 0 || Wedstrijden[i].Set1Away != 0)
                        {
                            aantalWed++;
                            if (Wedstrijden[i].Set1Home < Wedstrijden[i].Set1Away)
                            {
                                SetsV++;
                            }
                            else
                            {
                                SetsG++;
                            }

                        }

                        if (Wedstrijden[i].Set2Home != 0 || Wedstrijden[i].Set2Away != 0)
                        {
                            if (Wedstrijden[i].Set2Home < Wedstrijden[i].Set2Away)
                            {
                                SetsV++;
                            }
                            else
                            {
                                SetsG++;
                            }
                        }

                        if (Wedstrijden[i].Set3Home != 0 || Wedstrijden[i].Set3Away != 0)
                        {
                            if (Wedstrijden[i].Set3Home < Wedstrijden[i].Set3Away)
                            {
                                SetsV++;
                            }
                            else
                            {
                                SetsG++;
                            }

                        }
                        PuntenH = Wedstrijden[i].Set1Home + Wedstrijden[i].Set2Home + Wedstrijden[i].Set3Home;
                        PuntenA = Wedstrijden[i].Set1Away + Wedstrijden[i].Set2Away + Wedstrijden[i].Set3Away;
                        //Update de counters voor de ploeg
                        AantalGewSets += SetsG;
                        AantalVerlSets += SetsV;

                        if (SetsG < SetsV)
                        {
                            AantalVerl++;
                        }
                        else if (SetsG > SetsV)
                        {
                            AantalGew++;
                            TotalePunten += 3; 
                        }
                        else if (SetsG == SetsV && SetsG!=0 && SetsV !=0)
                        {
                            if (PuntenH < PuntenA)
                            {
                                AantalVerl21++;
                                TotalePunten += 1;
                            }
                            else if (PuntenH > PuntenA)
                            {
                                AantalGew21++;
                                TotalePunten += 2;
                            }
                            else if (PuntenH == PuntenA)
                            {
                                TotalePunten += 1;
                            }

                        }


                    }

                    //Indien uitploeg
                    if (Wedstrijden[i].Away.Equals(ploeg))
                    {

                        if (Wedstrijden[i].Set1Home != 0 || Wedstrijden[i].Set1Away != 0)
                        {
                            aantalWed++;
                            if (Wedstrijden[i].Set1Home < Wedstrijden[i].Set1Away)
                            {
                                SetsG++;
                            }
                            else
                            {
                                SetsV++;
                            }

                        }

                        if (Wedstrijden[i].Set2Home != 0 || Wedstrijden[i].Set2Away != 0)
                        {
                            if (Wedstrijden[i].Set2Home < Wedstrijden[i].Set2Away)
                            {
                                SetsG++;
                            }
                            else
                            {
                                SetsV++;
                            }

                        }

                        if (Wedstrijden[i].Set3Home != 0 || Wedstrijden[i].Set3Away != 0)
                        {
                            if (Wedstrijden[i].Set3Home < Wedstrijden[i].Set3Away)
                            {
                                SetsG++;
                            }
                            else
                            {
                                SetsV++;
                            }

                        }

                        PuntenH = Wedstrijden[i].Set1Home + Wedstrijden[i].Set2Home + Wedstrijden[i].Set3Home;
                        PuntenA = Wedstrijden[i].Set1Away + Wedstrijden[i].Set2Away + Wedstrijden[i].Set3Away;
                        //Update de counters voor de ploeg
                        AantalGewSets += SetsG;
                        AantalVerlSets += SetsV;

                        if (SetsG < SetsV)
                        {
                            AantalVerl++;
                        }
                        else if (SetsG > SetsV)
                        {
                            AantalGew++;
                            TotalePunten += 3;
                        }
                        else if (SetsG == SetsV && SetsG != 0 && SetsV != 0)
                        {
                            if (PuntenH < PuntenA)
                            {
                                AantalGew21++;
                                TotalePunten += 2;
                            }
                            else if (PuntenH > PuntenA)
                            {
                                AantalVerl21++;
                                TotalePunten += 1;
                            }
                            else if (PuntenH > PuntenA)
                            {
                                TotalePunten += 1;
                            }

                        }

                    }

                }

                //Add to rankings table
                Rankings.Rows.Add(ploeg, aantalWed, AantalGew, AantalGew21, AantalVerl21, AantalVerl, AantalGewSets, AantalVerlSets, TotalePunten);
            }

            DataView dt = Rankings.DefaultView;
            dt.Sort = "Totale Punten DESC, Aantal Gew. DESC, Aantal Verl. ASC ";
            Rankings = dt.ToTable();

            _Klassement.Ranking = Rankings;
        }













    }


    



















}
