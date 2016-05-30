﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using structures;

namespace zomertornooiTests.Dummies
{
    public class DummyData
    {


        private List<int> RandomNrs(int low, int high)
        {
            //sequntial list
            List<int> sourcelist = new List<int>();
            for (int i = low; i < high; i++) { sourcelist.Add(i); }

            var rand = new Random();
            var rtnlist = new List<int>();

            for (int i = sourcelist.Count; i > 0 ; i--)
            {
                int randomindex = rand.Next(sourcelist.Count);
                rtnlist.Add(sourcelist[randomindex]);
                sourcelist.Remove(rtnlist.Last());
            }

            return rtnlist;
        }


        public  List<Ploeg> CreateDummyPloegen(int aantal)
        {
            List<int> indexen = RandomNrs(0, aantal);
            List<int> nameindexen = RandomNrs(0,Names.Count-1);
            List<Ploeg> teams = new List<Ploeg>();
            Random random = new Random();
            Array GeslachtValues = Enum.GetValues(typeof(ProgramDefinitions.Geslacht));
            Array NiveauValues = Enum.GetValues(typeof(ProgramDefinitions.Niveau));

            try
            {
                for (int i = 0; i < aantal; i++)
                {
                    Ploeg temp = new Ploeg()
                    {
                        Ploegnaam = GetItemFromList(Teams,indexen[i]),
                        Category = new  Category()
                        {
                             Geslacht = (ProgramDefinitions.Geslacht)GeslachtValues.GetValue(random.Next(GeslachtValues.Length)),
                             Niveau = (ProgramDefinitions.Niveau)NiveauValues.GetValue(random.Next(NiveauValues.Length))
                        },                        
                        Aangemeld = (random.Next(2) == 0),
                        Betaald = (random.Next(2) == 0),
                        Contactpersoon = new Persoon()
                        {
                            Naam = Names[nameindexen[i]].Split(' ')[0].TrimEnd(),
                            Voornaam = Names[nameindexen[i]].Split(' ')[1].TrimEnd(),                            
      
                                Land = "Belgie",
                                Nr = random.Next(0, 1000).ToString(),
                                Postcode = random.Next(1000, 9999).ToString(),
                                Straat = streets[random.Next(0, streets.Count - 1)],
                                Woonplaats = "gent",
   
                                GSMNr = random.Next(1000000, 99999999).ToString()
                            
                        }
                    };
                    temp.SubscribedCategory = temp.Category;
                    teams.Add(temp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exec" + e.ToString());
            }

            return teams;
        }


        private string GetItemFromList (List<string> items, int index)
        {
            int div = index /items.Count  ;
            int mod = index % items.Count;
            string temp = items[mod].TrimEnd();
            if (div > 0)
            {
                temp += "_" + div.ToString();
            }
            return temp;
        }


        public List<string> streets = new List<string>()
        {
            "A Sifferdock Kaai                              ",
"A. Casier De Ter Bekenlaan                     ",
"Aaigemstraat                                   ",
"Aannemerstraat                                 ",
"Abdijmolenstraat                               ",
"Abdisstraat                                    ",
"Achilles Musschestraat                         ",
"Achtenkouterstraat                             ",
"Adelaarsstraat                                 ",
"Adriaan Walckiersdreef                         ",
"Afrikalaan                                     ",
"Afsneedorp                                     ",
"Afsneekouter                                   ",
"Ajuinlei                                       ",
"Akkerhage                                      ",
"Akkerstraat                                    ",
"Albrecht Rodenbachstraat                       ",
"Alfons Bijnlaan                                ",
"Alfons Van Den Broeckstraat                    ",
"Alpacastraat                                   ",
"Alphonse Sifferlaan                            ",
"Andre De Bruynstraat                           ",
"Antonius Triestlaan                            ",
"Antoon Catriestraat                            ",
"Antwerpenplein                                 ",
"Antwerpsesteenweg                              ",
"Antwerpsestwg                                  ",
"Apostelhuizen                                  ",
"Appelstraat                                    ",
"Armand Bourdonlaan                             ",
"Arthur Verhaegenstraat                         ",
"Asselsstraat                                   ",
"Autoweg-noord                                  ",
"Avennesdreef                                   ",
"Baarleboslaan                                  ",
"Baarledorpstraat                               ",
"Bagattenstraat                                 ",
"Baliestraat                                    ",
"Balsamierenstraat                              ",
"Bardelare                                      ",
"Baudelokaai                                    ",
"Baudelostraat                                  ",
"Beekstraatkouter                               ",
"Beemdstraat                                    ",
"Begijnengracht                                 ",
"Begijnhoflaan                                  ",
"Belfortstraat                                  ",
"Bellevuestraat                                 ",
"Bennesteeg                                     ",
"Benninsbrugstraat                              ",
"Berouw                                         ",
"Beukelaarstraat                                ",
"Beukenlaan                                     ",
"Beverhoutplein                                 ",
"Bevrijdingslaan                                ",
"Bibliotheekstraat                              ",
"Biekorfstraat                                  ",
"Biezenstuk                                     ",
"Bij Sint-jacobs                                ",
"Bijde Inkomststraat                            ",
"Bijenstraat                                    ",
"Bijlokehof                                     ",
"Bijlokevest                                    ",
"Blankenbergestraat                             ",
"Blauwstraat                                    ",
"Blazoenstraat                                  ",
"Blekerijstraat                                 ",
"Blekersdijk                                    ",
"Blijde Inkomststraat                           ",
"Bloemistenstraat                               ",
"Boelenaar                                      ",
"Bollewerkstraat                                ",
"Bomastraat                                     ",
"Boomstraat                                     ",
"Borkelaarstraat                                ",
"Botermarkt                                     ",
"Botestraat                                     ",
"Brabantdam                                     ",
"Bredestraat                                    ",
"Breestraat                                     ",
"Broederlijk Weversplein                        ",
"Broederlijke-wevers                            ",
"Broekkantstraat                                ",
"Brugsepoortstraat                              ",
"Brugsesteenweg                                 ",
"Brugsevaart                                    ",
"Brugstraat                                     ",
"Bruiloftstraat                                 ",
"Brusselsepoortstraat                           ",
"Brusselsesteenweg                              ",
"Buchtenstraat                                  ",
"Buffelstraat                                   ",
"Burggravenlaan                                 ",
"Burgstraat                                     ",
"Busstraat                                      ",
"Casinoplein                                    ",
"Cataloniestraat                                ",
"Charles De Costerstraat                        ",
"Charles De Kerchovelaan                        ",
"Charles De Kerckhovelaan                       ",
"Chr. Van Der Heydenlaan                        ",
"Citadellaan                                    ",
"Citadelpark                                    ",
"Clarissenstraat                                ",
"Congreslaan                                    ",
"Corduwaniersstraat                             ",
"Coupure                                        ",
"Coupure Links                                  ",
"Coupure Rechts                                 ",
"Cyriel Buyssestraat                            ",
"D. Kinetstraat                                 ",
"Dampoortstraat                                 ",
"Davidtenierslaan                               ",
"De Pintelaan                                   ",
"Deinse Horsweg                                 ",
"Dendermondesteenweg                            ",
"Dendermondsesteenweg                           ",
"Derbystraat                                    ",
"Desire Van Monckhovenstraat                    ",
"Desteldonkdorp                                 ",
"Dijkweg                                        ",
"Dikkelindestraat                               ",
"Distelstraat                                   ",
"Dok                                            ",
"Dok Zuid                                       ",
"Dok-noord                                      ",
"Domien Geersstraat                             ",
"Domien Ingelsstraat                            ",
"Donkersteeg                                    ",
"Doornzelestraat                                ",
"Dracenastraat                                  ",
"Dreef                                          ",
"Driekoningenstraat                             ",
"Driemasterstraat                               ",
"Drieselstraat                                  ",
"Drongenplein                                   ",
"Drongensesteenweg                              ",
"Drongensteenweg                                ",
"Druifstraat                                    ",
"Duddegemstraat                                 ",
"Duivelstraat                                   ",
"Dukkeldamstraat                                ",
"E3-plein                                       ",
"Ebergiste De Deynestraat                       ",
"Eddastraat                                     ",
"Edm Blockstraat                                ",
"Edmond Helderweirdtstraat                      ",
"Edmond Van Beverenplein                        ",
"Edmond Van Beverenplein / Myosotisstraat       ",
"Eedverbondkaai                                 ",
"Eekhout                                        ",
"Eekhoutdriesstraat                             ",
"Eendrachtstraat                                ",
"Eikstraat                                      ",
"Einde Were                                     ",
"Ekkergemstraat                                 ",
"Eksaarderijweg                                 ",
"Eksaardserijweg                                ",
"Elfjulistraat                                  ",
"Elshout                                        ",
"Elyzeese Velden                                ",
"Elzeboomlaan                                   ",
"Emanuel Hielstraat                             ",
"Emiel Lossystraat                              ",
"Emiel Van Swedenlaan                           ",
"Emile Braunplein                               ",
"Emilius Seghersplein                           ",
"Eugeen Zetternamstraat                         ",
"Evergemsesteenweg                              ",
"F. Lousbergskaai                               ",
"Ferdinand Lousbergskaai                        ",
"Filips Van Arteveldestraat                     ",
"Flamingostraat                                 ",
"Fleurusstraat                                  ",
"Florbertusstraat                               ",
"Forelstraat                                    ",
"Fortlaan                                       ",
"Francisco Ferrerlaan                           ",
"Francois Laurentplein                          ",
"Franklin Rooseveltlaan                         ",
"Frans Gevaertstraat                            ",
"Frans Louwersstraat                            ",
"Frans Van Rijhoevelaan                         ",
"Frans Van Ryhovelaan                           ",
"Fratersplein                                   ",
"Frederik Burvenichstraat                       ",
"Fregatstraat                                   ",
"Frere Orbanlaan                                ",
"Fresiastraat                                   ",
"Fritz De Beulestraat                           ",
"Gaaipersstraat                                 ",
"Gaardeniersweg                                 ",
"Gagelstraat                                    ",
"Galgenberg                                     ",
"Galglaan                                       ",
"Gandastraat                                    ",
"Ganzendries                                    ",
"Gasmeterlaan                                   ",
"Gaspeldoorndreef                               ",
"Gaverlandstraat                                ",
"Gaverstraat                                    ",
"Gebroeders De Cockstraat                       ",
"Gebroeders Van Eyckstraat                      ",
"Gebroeders Vandeveldestraat                    ",
"Geelgorsstraat                                 ",
"Geldmunt                                       ",
"Gentbruggekouter                               ",
"Gentbruggeplein                                ",
"Gentsesteenweg                                 ",
"Gentstraat                                     ",
"Geo Verbancklaan                               ",
"Gestichtstraat                                 ",
"Gildestraat                                    ",
"Godshuizenlaan                                 ",
"Goedingenstraat                                ",
"Goedlevenstraat                                ",
"Gordunakaai                                    ",
"Goubaulaan                                     ",
"Goudenleeuwplein                               ",
"Gouvernemenstraat                              ",
"Gouvernementstraat                             ",
"Graaf Van Vlaanderenplein                      ",
"Graslei                                        ",
"Grensstraat                                    ",
"Griendijk                                      ",
"Groendreef                                     ",
"Groenespechtstraat                             ",
"Groenstraat                                    ",
"Groentenmarkt                                  ",
"Groenvinkstraat                                ",
"Grondwetlaan                                   ",
"Groot-brittannielaan                           ",
"Grootkannonplein                               ",
"Grote Huidevettershoek                         ",
"Grotesteenweg Zuid                             ",
"Guldenspoorstraat                              ",
"Gustaaf Callierlaan                            ",
"Hagelandkaai                                   ",
"Halewijnkouter                                 ",
"Ham                                            ",
"Handbalstraat                                  ",
"Harelbekestraat                                ",
"Harlekijnstraat                                ",
"Hazenlaan                                      ",
"Heemstraat                                     ",
"Heernislaan                                    ",
"Heerweg-noord                                  ",
"Heilig Kruisplein                              ",
"Heinakker                                      ",
"Hemelrijkstraat                                ",
"Henegouwenstraat                               ",
"Henleykaai                                     ",
"Henri Dunantlaan                               ",
"Henri Farmanstraat                             ",
"Henri Pirennelaan                              ",
"Henricus Bracqstraat                           ",
"Herderstraat                                   ",
"Herfststraat                                   ",
"Herman Teirlinckstraat                         ",
"Hertogsplein                                   ",
"Hieveldstraat                                  ",
"Hipp. Persoonsstraat                           ",
"Hippoliet Lammensstraat                        ",
"Hippoliet Lippensplein                         ",
"Hoefslagstraatje                               ",
"Hoek Voldersstraat                             ",
"Hof Ter Mere                                   ",
"Hofstraat                                      ",
"Hogeheerweg                                    ",
"Hogeweg                                        ",
"Holstraat                                      ",
"Hondelee                                       ",
"Hoogpoort                                      ",
"Hoogstraat                                     ",
"Hoornstraat                                    ",
"Houtdoklaan                                    ",
"Houtjen                                        ",
"Hoverniersstraat                               ",
"Hubert Frere-orbanlaan                         ",
"Hubert Frore-orbanlaan                         ",
"Hubert Frsre-orbanlaan                         ",
"Hundelgemsesteenweg                            ",
"Hyacinth Lippensstraat                         ",
"Ijskelderstraat                                ",
"Ijzerlaan                                      ",
"Imsakkerlaan                                   ",
"Industriepark                                  ",
"Industrieweg                                   ",
"Industrieweg 122 Ab                            ",
"Industrieweg 122 B E                           ",
"Industrieweg 122 B L                           ",
"Industrieweg 64-                               ",
"Industrieweg 74                                ",
"Industrieweg 78-                               ",
"Ingelandgat                                    ",
"Invalidenstraat                                ",
"Isegrimstraat                                  ",
"J. Van Crombrugghestraat                       ",
"Jacob Van Arteveldestraat                      ",
"Jacob Van Maerlantstraat                       ",
"Jacobijnstraat                                 ",
"Jakobijnenstraat                               ",
"Jan Samijnstraat                               ",
"Jan Baptist Guinardstraat                      ",
"Jan Breydelstraat                              ",
"Jan Delvinlaan                                 ",
"Jan Frans Willemsstraat                        ",
"Jan Palfijnstraat                              ",
"Jan Van Stopenberghestraat                     ",
"Jan Verspeyenstraat                            ",
"Jasmijnstraat                                  ",
"Jean-baptiste D'hanedreef                      ",
"Jean-baptiste De Gieylaan                      ",
"Jef Crickstraat                                ",
"Jeroom Dequesnoylaan                           ",
"Joachim Schayckstraat                          ",
"Johan Daisnestraat                             ",
"Johannes Schrantstraat                         ",
"John Kennedylaan                               ",
"Joremaai                                       ",
"Joremaaie                                      ",
"Joseph Gerardstraat                            ",
"Jozef Guislainstraat                           ",
"Jozef Ii-straat                                ",
"Jozef Kluyskensstraat                          ",
"Jozef Plateaustraat                            ",
"Jozef Vervaenestraat                           ",
"Jozef Wautersstraat                            ",
"Jubileumlaan                                   ",
"Julius De Vigneplein                           ",
"Justus De Harduwijnlaan                        ",
"Kaarderijstraat                                ",
"Kalandeberg                                    ",
"Kalandestraat                                  ",
"Kamerijkstraat                                 ",
"Kamerstraat                                    ",
"Kammerstraat                                   ",
"Kantienberg                                    ",
"Kapelledreef                                   ",
"Kapiteinstraat                                 ",
"Karel Van De Woestijnestraat                   ",
"Karperstraat                                   ",
"Kartuizerlaan                                  ",
"Kastanjestraat                                 ",
"Kasteellaan                                    ",
"Kasterbant                                     ",
"Kattenberg                                     ",
"Kazemattenstraat                               ",
"Keizer Karelstraat                             ",
"Keizer Leopoldstraat                           ",
"Keizervest                                     ",
"Kempstraat                                     ",
"Kennedy Industriepark                          ",
"Kerkhofstraat                                  ",
"Kerkstraat                                     ",
"Ketelpoort                                     ",
"Ketelvest                                      ",
"Kettingstraat                                  ",
"Keuze                                          ",
"Kiekenbosstraat                                ",
"Kiekenstraat                                   ",
"Kikvorsstraat                                  ",
"Klapeksterstraat                               ",
"Klein Turkije                                  ",
"Kleindokkaai                                   ",
"Kleine Gentstraat                              ",
"Kleine Vismarkt                                ",
"Kliniekstraat                                  ",
"Kluisweg                                       ",
"Kluizensesteenweg                              ",
"Knokkestraat                                   ",
"Koersenstraat                                  ",
"Koestraat                                      ",
"Kolegemstraat                                  ",
"Kollebloemstraat                               ",
"Kolveniersgang                                 ",
"Kongostraat                                    ",
"Koning Albertlaan                              ",
"Koning Boudewijnstraat                         ",
"Koning Leopold Ii-laan                         ",
"Koningin Astridlaan                            ",
"Koningin Elisabethlaan                         ",
"Koningin Fabiolalaan                           ",
"Koningin Maria Hendrikaplein                   ",
"Koninging Elisabethlaan                        ",
"Koningsdal                                     ",
"Koolkapperstraat                               ",
"Koophandelsplein                               ",
"Koopvaardijlaan                                ",
"Koraalwortelsraat                              ",
"Korenlei                                       ",
"Korenmarkt                                     ",
"Korianderstraat                                ",
"Korte Kruisstraat                              ",
"Korte Magerstraat                              ",
"Korte Meer                                     ",
"Kortedagsteeg                                  ",
"Kortemunt                                      ",
"Kortrijkse Steenweg                            ",
"Kortrijksepoortstraat                          ",
"Kortrijksesteenweg                             ",
"Kortrijksesteenweg H                           ",
"Kortrijksesteenweg 676-                        ",
"Kortrijksesteenweg B                           ",
"Kortrijksesteenweg C                           ",
"Kouter                                         ",
"Kouter 1 Bus                                   ",
"Kouterdreef                                    ",
"Kraanlei                                       ",
"Kramersplein                                   ",
"Krevelstraat                                   ",
"Kriekerijstraat                                ",
"Krijgsgasthuisstraat                           ",
"Krijgslaan                                     ",
"Krijtestraat                                   ",
"Krommehamlaan                                  ",
"Kruisstraat                                    ",
"Kruitmagazijnstraat                            ",
"Kuiperskaai                                    ",
"Kunstlaan                                      ",
"Kwartelstraat                                  ",
"Lammerstraat                                   ",
"Land En Waaslaan                               ",
"Land Van Waaslaan                              ",
"Landegemdorp                                   ",
"Lange Kruisstraat                              ",
"Lange Steenstraat                              ",
"Lange Violetstraat                             ",
"Lange Violettestaat                            ",
"Lange Violettestraat                           ",
"Langerbruggekaai                               ",
"Langerbruggestraat                             ",
"Langerbruggestraat 76-                         ",
"Langezoutstraat                                ",
"Lavendelstraat                                 ",
"Ledebergplein                                  ",
"Ledebergstraat                                 ",
"Ledergemstraat                                 ",
"Leebeekstraat                                  ",
"Leeuwerikstraat                                ",
"Leiekaai                                       ",
"Leieriggestraat                                ",
"Lentestraat                                    ",
"Leo Tertzweillaan                              ",
"Lieven Bauwensplein                            ",
"Lieven De Winnestraat                          ",
"Lijnmolenstraat                                ",
"Limburgstraat                                  ",
"Lindenlei                                      ",
"Lisdoddestraat                                 ",
"Lochtingstraat                                 ",
"Londenstraat                                   ",
"Loofblommestraat                               ",
"Loskaai                                        ",
"Lostraat                                       ",
"Lothariusstraat                                ",
"Louis Delebecquelaan                           ",
"Louise Deachestraat                            ",
"Lourdesstraat                                  ",
"Lucas Munichstraat                             ",
"Luiklaan                                       ",
"M Claeysplein                                  ",
"M St. Kristoffelstraat                         ",
"Maaltebruggestraat                             ",
"Maaltekouter                                   ",
"Mageleinstr                                    ",
"Mageleinstraat                                 ",
"Mageleinstraat En Sint-baafsplein              ",
"Mageleistraat                                  ",
"Magergoedhof                                   ",
"Mahatma Gandhistraat                           ",
"Mail                                           ",
"Maisstraat                                     ",
"Makelaarsstraat                                ",
"Manchesterstraat                               ",
"Marekstraat                                    ",
"Maria Theresiastraat                           ",
"Mariakerksesteenweg                            ",
"Markt                                          ",
"Marmotstraat                                   ",
"Martelaarslaan                                 ",
"Mechelsesteenweg                               ",
"Meerhem                                        ",
"Meerhoutstraat                                 ",
"Meerkoetlaan                                   ",
"Meersstraat                                    ",
"Meibloemstraat                                 ",
"Mellestraat                                    ",
"Mendonkdorp                                    ",
"Merelbekestationplein                          ",
"Meulestedekaai                                 ",
"Meulesteedsesteenweg							",
        };


        public List<string> Names = new List<string>()
        {
            "Freya Aelbrecht         ",
"Seppe Baetens           ",
"Jasmien Biebauw         ",
"Angie Bland             ",
"Manuel Callebert        ",
"Maud Catry              ",
"Lorena Cianci           ",
"Gertjan Claes           ",
"Martijn Colson          ",
"Pieter Coolman          ",
"Nina Coolman            ",
"Sarah Cools             ",
"Greet Coppe             ",
"Lienert Cosemans        ",
"Valérie Courtois        ",
"Jolan Cox               ",
"Stijn D'Hulst           ",
"Jan De Brandt           ",
"Virginie De Carne       ",
"Yana De Leeuw           ",
"Stijn Dejonckheere      ",
"Frank Depestele         ",
"Sander Depovere         ",
"Bert Derkoningen        ",
"Dennis Deroey           ",
"Sam Deroo               ",
"Frauke Dirickx          ",
"Aziliz Divoux           ",
"Lies Eykens             ",
"Lore Gillis             ",
"Karolina Goliat         ",
"Kaja Grobelna           ",
"Michel Haeevoets        ",
"Sofie Hawinkel          ",
"Dries Heyrman           ",
"Laura Heyrman           ",
"Kristof Hoho            ",
"Gwendoline Horemans     ",
"Niels Huysegoms         ",
"Lou Kindt               ",
"Kevin Klinkenberg       ",
"Thomas Konings          ",
"Yves Kruyner            ",
"Celine Laforge          ",
"François Lecat          ",
"Nathalie Lemmens        ",
"Charlotte Leys          ",
"Florian Malisse         ",
"Lisa Neyt               ",
"Simon Peeters           ",
"Berre Peters            ",
"Arne Poelman            ",
"Berto Poosen            ",
"Jimmy Prenen            ",
"Matias Raymaekers       ",
"Jelle Ribbens           ",
"Hélène Rousseaux        ",
"Tomas Rousseaux         ",
"Britt Ruysschaert       ",
"Dominika Sobolska       ",
"Kim Staelens            ",
"Dominika Strumilo       ",
"Lowie Stuer             ",
"Marta Szczygielska      ",
"Martha Szcygielska      ",
"Hendrik Tuerlinckx      ",
"Matthias Valkiers       ",
"Anja Van Damme          ",
"Christophe Van De Plas  ",
"Arno Van De Velde       ",
"Ilka Van De Vijver      ",
"Simon Van De Voorde     ",
"Ilka Van de Vyver       ",
"Jo Van Decraen          ",
"Bram Van Den Dries      ",
"Celine Van Gestel       ",
"Christof Van Goethem    ",
"Lise Van Hecke          ",
"Griet Van Vaerenbergh   ",
"Gert Van Walle          ",
"Els Vandesteene         ",
"Robbe Vandeweyer        ",
"Matthijs Verhanneman    ",
"Pieter Verhees          ",
"Wouter Verhelst         ",
"Tim Verschueren         ",
"Liesbet Vindevoghel     ",
"Wout Wijsmans           ",
"Jolien Wittock			 "
        };


        public List<string> Teams = new List<string>()
        {
            "Knack Randstad Roeselare           ",
"Noliko Maaseik                     ",
"Prefaxis Menen                     ",
"Topvolley Precura Antwerpen        ",
"VC Argex Duvel Puurs               ",
"VC Euphony Asse-Lennik             ",
"VBC Waremme                        ",
"Axis Shanks Guibertin              ",
"VDK Gent                           ",
"Avoc Achel                         ",
"Beivoc Humbeek                     ",
"Gea Happel Amigos Zoersel          ",
"TSV Vilvoorde                      ",
"VC Global Wineries Kapellen        ",
"VC Helios Zonhoven                 ",
"VC Herenthout                      ",
"VC Zoersel                         ",
"VDK Gent Heren                     ",
"Volley Haasrode Leuven             ",
"VT Optima Lendelede                ",
        };
    }
}
