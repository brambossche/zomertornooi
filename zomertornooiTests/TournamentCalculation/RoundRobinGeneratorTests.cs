using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentCalculation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using structures;
using ProgramDefinitions;

namespace TournamentCalculation.Tests
{
    [TestClass()]
    public class RoundRobinGeneratorTests
    {
        
        private List<Reeks> ReeksTeams = new List<Reeks>();
        private const int maxreeks = 1;
        int[] AantalPloegen = new int[maxreeks] {9};

        private const int RoundRobins = 1;
        private const int NrOfTerreins = 2;
        private const bool OptimalTerrains = true;



        [TestInitialize()]
        public void setup()
        {
            ProgramLogger _ProgramLogger = new ProgramLogger();
            for (int i = 0; i < maxreeks; i++)
            {
                List<Ploeg> ploegen = new List<Ploeg>();
                for (int j = 0; j < AantalPloegen[i]; j++)
                {
                    ploegen.Add(new Ploeg() { Ploegnaam = "Reeks" + i.ToString() + "_Team" + j.ToString() });
                }
                ReeksTeams.Add(new Reeks() { Ploegen = ploegen,ReeksNaam = "Reeks"+i.ToString() });
            }
            
        }

        [TestMethod()]
        public void CalculateRoundRobinGamesTest()
        {
            RoundRobinGenerator robin = new RoundRobinGenerator();
            //Calculate standard Round Robin games - 
            try
            {

                for (int k = 0; k < maxreeks; k++)
                {
                    //Set parameters
                    ReeksTeams[k].WedstrijdDefinition.AantalRoundRobin = 1;
                    ReeksTeams[k].WedstrijdDefinition.MaxNaElkaarSpelen = 2;
                    ReeksTeams[k].WedstrijdDefinition.AantalrondesZaterdag = 10;
                    ReeksTeams[k].WedstrijdDefinition.Wedstrijdduur = 45;

                    //Bereken round robin games voor alle reeksen
                    ReeksTeams[k].RoundRobin = robin.CalculateRoundRobinGames(ReeksTeams[k]);

                    //Bereken optimaal aantal terreinen
                    if (OptimalTerrains)
                    {
                        ReeksTeams[k].Terreinen = robin.CalculateNrTerrains(ReeksTeams[k]);
                    }

                    //kies manueel het aantal terreinen indien nodig
                    //robin.SetNrTerrains(ReeksTeams[k], NrOfTerreins);
                }

                //Berekenen tijdschema van lijst van reeksen in dezelfde category & zelfde aantal teams
                robin.CalculateTimeSchedule(ReeksTeams);
                


                //Simuleer hoe de rondes eruit zien na afloop van round robin formule
                ReeksTeams[0].TornooiFormule = TornooiFormule.PlacementGames;
                //robin.SimulateFinalGames(ReeksTeams);

                //Log outputs
                robin.logRoundRobin(ReeksTeams[0]);
                robin.LogTimeSchedule(ReeksTeams[0]);
                robin.LogStatusList(ReeksTeams[0]);

                //robin.CalculateRankings(ReeksTeams[0]);

                //Get all games
                List<Wedstrijd> Wedstrijden = robin.GetAllGames(ReeksTeams);




            }
            catch (Exception e)
            {

                Assert.Fail("Exception " + e.Message);
            }

        }
        /*
        [TestMethod()]
        public void CalculateTimeScheduleTest()
        {
            Assert.Fail();
        }
        
        [TestMethod()]
        public void CalculateTimeSchedule2Test()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindOptimalGameTest()
        {
            Assert.Fail();
        }*/

    }
}
