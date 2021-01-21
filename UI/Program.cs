using System;
using Model.Entities;
using Model.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace UI
{
    class Program
    {
        private static readonly bool DarkMode = false;

        // . . . . 

        private static Persoon Account;
        private static string LoginGegevens => $"{(Account == null ? "Niet ingelogd" : (Account is Profiel ? "PROFIEL: " : "MEDEWERKER: ") + "Nr: " + Account.PersoonId + " - Naam: " + Account.LoginNaam)}";

        

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("========================");
            Console.WriteLine("G E M E E N T E B O E K ");
            Console.WriteLine("========================");

            KiesHoofdmenu();

            Console.WriteLine("\nWij danken u voor uw medewerking. Tot de volgend keer....");
            Console.ReadKey();
        }

        public static void KiesHoofdmenu()
        {
            char? keuze = null;

            while (keuze != 'X')
            {
                string input;

                if (Account == null)
                    input = "AX";
                else    // Profiel
                    if (Account is Profiel)
                    input = "AXNR";
                else    // Medewerker
                    input = "AXGBD";

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine($"=================");
                Console.WriteLine($"H O O F D M E N U - {LoginGegevens}");
                Console.WriteLine($"=================");
                Console.WriteLine("<A>ccount");

                if (Account is Medewerker)
                {
                    Console.WriteLine("<G>oedkeuring nieuw profiel");
                    Console.WriteLine("<B>lokkeren van een profiel");
                    Console.WriteLine("<D>eblokkeren van een profiel");
                }

                if (Account is Profiel)
                {
                    Console.WriteLine("<N>ieuw bericht");
                    Console.WriteLine($"<R>aadplegen berichten van uw hoofdgemeente {(Account.Adres.Straat.Gemeente.HoofdGemeente == null ? Account.Adres.Straat.Gemeente.GemeenteNaam : Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam)}");
                }

                Console.WriteLine("e<X>it");
                Console.WriteLine();

                keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }

                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Black;

                switch (keuze)
                {
                    case 'A':
                        KiesAccountMenu();
                        break;

                    case 'B':
                        BlokkerenProfiel();
                        break;

                    case 'D':
                        DeblokkerenProfiel();
                        break;

                    case 'G':
                        GoedkeurenNieuwProfiel();
                        break;

                    case 'N':
                        InvoerenNieuwBericht(Account);
                        break;

                    case 'R':
                        RaadplegenBerichten(Account);
                        break;
                }
            }
        }

        public static void KiesAccountMenu()
        {
            string input;

            char? keuze = null;

            while (keuze != 'X')
            {
                if (Account == null)
                    input = "IRX";
                else
                    if (Account is Profiel)
                    input = "UTWVX";
                else
                    input = "UTX";

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine($"===================");
                Console.WriteLine($"A C C O U N T M E N U - {LoginGegevens}");
                Console.WriteLine($"===================");

                if (Account == null)
                {
                    Console.WriteLine("<I>nloggen");
                    Console.WriteLine("<R>egistreren");
                }
                else
                {
                    Console.WriteLine("<U>itloggen");
                    Console.WriteLine("<T>oon profielgegevens");

                    if (Account is Profiel)
                    {
                        Console.WriteLine("<W>ijzig profielgegevens");
                        Console.WriteLine("<V>erwijder profiel");
                    }
                }

                Console.WriteLine("e<X>it");
                Console.WriteLine();

                keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }

                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;

                switch (keuze)
                {
                    case 'I':
                        Inloggen();
                        break;

                    case 'U':
                        Uitloggen();
                        break;

                    case 'R':
                        Registeren();
                        break;

                    case 'T':
                        ToonGegegevens(Account);
                        break;

                    case 'W':
                        WijzigGegevens(Account);
                        break;

                    case 'V':
                        VerwijderGegevens(Account);
                        break;
                }
                if (keuze == 'X')
                {
                    KiesHoofdmenu();
                }
                
            }
        }

        public static void GoedkeurenNieuwProfiel()
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.WriteLine("-------------------");
            Console.WriteLine("Goedkeuring Profiel");
            Console.WriteLine("-------------------");

            using var context = new EFEindproefContext();
            DateTime d1 = new DateTime(2008, 1, 1);
            var goedkeurenprofiel = (from profiel in context.Profielen
                                     where profiel.GoedgekeurdTijdstip <d1
                                     select profiel).FirstOrDefault();
            if (goedkeurenprofiel!=null)
            {
                Console.Write($"Naam van het profiel dat goedgekeurd moet worden <Enter>=terug: {goedkeurenprofiel.LoginNaam}");
                var test = Console.ReadLine();
                goedkeurenprofiel.GoedgekeurdTijdstip = DateTime.Now;
                context.SaveChanges();
                Console.WriteLine("Profiel is goedgekeurd...");
                KiesHoofdmenu();
            }
            else
            {
                Console.WriteLine("Geen goed te keuren profiel meer.");
                KiesHoofdmenu();
            }
           
        }

        public static void BlokkerenProfiel()
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.WriteLine("-------------------------");
            Console.WriteLine("Blokkeren van een Profiel");
            Console.WriteLine("-------------------------");

            using var context = new EFEindproefContext();
            Console.Write("Geef de naam van het te blokkeren profiel <Enter>=terug: ");

            var profiel = (from p in context.Profielen
                           where p.LoginNaam == Console.ReadLine()
                           select p).FirstOrDefault();
            if (profiel!=null)
            {
                profiel.Geblokkeerd = true;
                context.SaveChanges();
                Console.WriteLine("Profiel werd geblokkeerd...");
                Console.WriteLine("");
                KiesHoofdmenu();

            }
            else
            {
                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("Profiel bestaat niet!");
                Console.WriteLine("");
                BlokkerenProfiel();
            }
            
        }

        public static void DeblokkerenProfiel()
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.WriteLine("---------------------------");
            Console.WriteLine("Deblokkeren van een Profiel");
            Console.WriteLine("---------------------------");

            using var context = new EFEindproefContext();
            Console.Write("Geef de naam van het te deblokkeren profiel <Enter>=terug: ");

            var profiel = (from p in context.Profielen
                           where p.LoginNaam == Console.ReadLine()
                           select p).FirstOrDefault();
            if (profiel != null)
            {
                profiel.Geblokkeerd = false;
                context.SaveChanges();
                Console.WriteLine("Profiel werd gedeblokkeerd...");
                Console.WriteLine("");
                KiesHoofdmenu();

            }
            else
            {
                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("Profiel bestaat niet!");
                Console.WriteLine("");
                DeblokkerenProfiel();
            }
        }

        public static void Inloggen()

        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.Write("Gebruikersnaam <Enter>=Terug: ");
            string naam = Console.ReadLine();
            Console.Write("Wachtwoord: ");
            string paswoord = Console.ReadLine();

            using var context = new EFEindproefContext();
            Persoon persoon = context.Personen.Where(k => k.LoginNaam.ToUpper() == naam.ToUpper() &&
            paswoord.Equals(k.LoginPaswoord)).FirstOrDefault();
            if (persoon!= null)
            {
                if (persoon is Profiel)
                {
                    var profiel = persoon as Profiel;
                    DateTime d1 = new DateTime(2008, 1, 1);
                    if (profiel.Geblokkeerd==false && profiel.GoedgekeurdTijdstip>d1)
                    {
                        Console.WriteLine("Inloggen met succes voltooid");
                        profiel.LoginAantal += 1;
                        context.SaveChanges();
                        Account = profiel;

                        KiesAccountMenu();
                    }
                    else
                    {
                        if (profiel.GoedgekeurdTijdstip<d1)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Profiel nog niet goedgekeurd!");
                            Console.WriteLine("");
                            Account = null;
                            KiesHoofdmenu();
                        }
                        if (profiel.Geblokkeerd==true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Profiel is geblokkeerd!");
                            Console.WriteLine("");
                            Account = null;
                            KiesHoofdmenu();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Inloggen met succes voltooid");
                    persoon.LoginAantal += 1;
                    context.SaveChanges();
                    Account = persoon;

                    KiesAccountMenu();

                }
            }
            else
            {
                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("Foute aanmelding.");
                Console.WriteLine("");
                Account = null;
                Inloggen();
            }


        }

        public static void Uitloggen()
        {
            Account = null;
            KiesHoofdmenu();

        }

        public static Persoon Registeren()
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Profiel persoon = new Profiel();
            Console.WriteLine("---------------------------");
            Console.WriteLine("R E G I S T R E R E N");
            Console.WriteLine("---------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("--> Ingave Profiel");
            
            persoon.VoorNaam = ConsoleHelper.LeesString("Voornaam (< Enter >= Terug)", 20, OptionMode.Mandatory);
            persoon.FamilieNaam = ConsoleHelper.LeesString("Familienaam", 30, OptionMode.Mandatory);
            do
            {
                Console.Write("Geboortedatum (JJJJ-MM--DD)* : ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    persoon.GeboorteDatum = date;
                    break;
                }
                Console.WriteLine("Geen correcte datum");

            } while (true);

            persoon.TelefoonNr = ConsoleHelper.LeesTelefoonNummer("Telefoonnummer", OptionMode.Optional);

            persoon.KenningsmakingTekst = ConsoleHelper.LeesString("Kennismaking Tekst", 250, OptionMode.Mandatory);
            
            persoon.EmailAdres = ConsoleHelper.LeesEmailAdres("Emailadres", OptionMode.Mandatory);
           
            persoon.BeroepTekst = ConsoleHelper.LeesString("Beroep",30,OptionMode.Optional);
            
            persoon.FirmaNaam = ConsoleHelper.LeesString("Firma",30,OptionMode.Optional);
            
            persoon.FacebookNaam = ConsoleHelper.LeesString("Facebooknaam",50,OptionMode.Optional);
            
            persoon.WebsiteAdres = ConsoleHelper.LeesWebsiteUrl("Website URL", OptionMode.Optional);

            Console.Write("Geslacht (M/V)* : ");
            string testGeslacht = Console.ReadLine().ToUpper();
            while (testGeslacht != "M" && testGeslacht != "V")
            {
                Console.WriteLine("Verplicht veld!");
                Console.Write("Geslacht (M/V)* : ");
                testGeslacht = Console.ReadLine().ToUpper();
            }
            if (testGeslacht == "M")
            {
                persoon.GeslachtType = Persoon.Geslacht.Man;
            }
            else
            {
                persoon.GeslachtType = Persoon.Geslacht.Vrouw;
            }

            
            do
            {
                Console.Write("Woont hier sinds (JJJJ-MM-DD)* : ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    persoon.WoontHierSindsDatum = date;
                    break;
                }
                Console.WriteLine("Geen correcte datum");

            } while (true);

            using var context = new EFEindproefContext();
            var lijstTaal = context.Talen.OrderBy(m => m.TaalId).ToList();
            Console.WriteLine("");
            Console.WriteLine("Kies taal*");
            Console.WriteLine("-----------");
            foreach (var item in lijstTaal)
            {
                Console.WriteLine($"{item.TaalId}           {item.TaalNaam}");
            }
            int keuzeTaal;
            do
            {
                Console.Write("Geef het volgnummer uit de lijst: ");
                if (int.TryParse(Console.ReadLine(), out int keuze))
                {
                    if (keuze > 0 && keuze < 4)
                    {
                        keuzeTaal = keuze;
                        break;
                    }

                }
                Console.WriteLine("Geen geldige ingave");
            } while (true);
            persoon.TaalId = keuzeTaal;

            var gekozenTaal = context.Talen.Where(t => t.TaalId == persoon.TaalId).FirstOrDefault();
            Console.WriteLine($"Gekozen taal is {gekozenTaal.TaalCode} - {gekozenTaal.TaalNaam}");
            Console.WriteLine("");
            Console.WriteLine("Kies Geboorteplaats");
            Console.WriteLine("-------------------");
            Console.Write("Geef een aantal letters in van de gemeente: ");
            string zoekGGemeente = Console.ReadLine();
            
            var lijstGGemeentes = context.Gemeentes.Where(t => t.GemeenteNaam.Contains(zoekGGemeente));
            if (lijstGGemeentes==null)
            {
                persoon.GeboorteplaatsId = 0;
                Console.WriteLine("Geen gekozen gemeente");
            }
            else
            {
                foreach (var item in lijstGGemeentes)
                {
                    Console.WriteLine($"{item.GemeenteId}         {item.GemeenteNaam}");
                }
                Console.Write("Geef het volgnummer uit de lijst : ");
                persoon.GeboorteplaatsId = Convert.ToInt32(Console.ReadLine());
                var gekozenGGemeente = context.Gemeentes.Where(g => g.GemeenteId == persoon.GeboorteplaatsId).FirstOrDefault();
                Console.WriteLine($"Gekozen gemeente is {gekozenGGemeente.GemeenteNaam}");
            }
            Console.WriteLine("");
            Console.WriteLine("--> Ingave adres");
            Console.WriteLine("");
            Console.WriteLine("Kies Woonplaats*");
            Console.WriteLine("-----------------");
            Console.Write("Geef een aantal letters in van de gemeente: ");
            string zoekGemeente = Console.ReadLine();
            var lijstGemeentes = context.Gemeentes.Where(t => t.GemeenteNaam.Contains(zoekGemeente));
            while (lijstGemeentes == null)
            {
                Console.WriteLine("Gemeente niet gevonden; Verplicht veld!");
                Console.Write("Geef een aantal letters in van de gemeente: ");
                zoekGemeente = Console.ReadLine();
                lijstGemeentes = context.Gemeentes.Where(t => t.GemeenteNaam.Contains(zoekGemeente));

            }
            
                foreach (var item in lijstGemeentes)
                {
                    Console.WriteLine($"{item.GemeenteId}         {item.GemeenteNaam}");
                }
                Console.Write("Geef het volgnummer uit de lijst : ");
            int gemeenteID = Convert.ToInt16(Console.ReadLine());
            var gekozenGemeente = context.Gemeentes.Where(g => g.GemeenteId == gemeenteID).FirstOrDefault();
             
            Console.WriteLine($"Gekozen gemeente is {gekozenGemeente.GemeenteNaam}");
            Console.WriteLine("");
            Console.WriteLine("Kies Straat*");
            Console.WriteLine("-----------------");
            Console.Write("Geef een aantal letters in van de straat: ");
            var lijstStraten = context.Straten.Where(s => s.StraatNaam.Contains(Console.ReadLine()));
            while (lijstStraten==null)
            {
                Console.WriteLine("Straat niet gevonden; Verplicht veld!");
                Console.Write("Geef een aantal letters in van de straat: ");
                lijstStraten = context.Straten.Where(s => s.StraatNaam.Contains(Console.ReadLine()));

            }
            foreach (var item in lijstStraten)
            {
                Console.WriteLine($"{item.StraatId}      {item.StraatNaam}");
            }
            Console.Write("Geef het volgnummer uit de lijst : ");
            int straatId = Convert.ToInt32(Console.ReadLine());
            var gekozenStraat = context.Straten.Where(s => s.StraatId == straatId).FirstOrDefault();
            Console.WriteLine($"Gekozen straat is {gekozenStraat.StraatNaam}.");
            Console.WriteLine("");
            Console.Write($"Huisnummer* : ");
            string gekozenHuisNr = Console.ReadLine();
            Console.Write("Busnummer : ");
            string gekozenBusNr = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("-->Ingave Login");
            Console.Write("Login naam* : ");
            persoon.LoginNaam = Console.ReadLine();
            Console.Write("Wachtwoord* : ");
            string eersteWachtwoord = Console.ReadLine();
            Console.Write("Wachtwoord bevestigen* : ");
            string tweedeWachtwoord = Console.ReadLine();
            while (eersteWachtwoord!=tweedeWachtwoord)
            {
                Console.WriteLine("De wachtwoorden matchen niet. Probeer opnieuw.");
                Console.Write("Wachtwoord* : ");
                eersteWachtwoord = Console.ReadLine();
                Console.Write("Wachtwoord bevestigen* : ");
                tweedeWachtwoord = Console.ReadLine();
            }
            persoon.LoginPaswoord = eersteWachtwoord;
            //Console.WriteLine("");
            //Console.WriteLine("--> Ingave interesses");
            //Console.WriteLine("");

            var adresID = (from a in context.Adressen
                           where a.HuisNr == gekozenHuisNr && a.BusNr == gekozenBusNr && a.StraatId == straatId
                           select a).FirstOrDefault();
            if (adresID==null)
            {
                var adres = new Adres
                {
                    HuisNr = gekozenHuisNr,
                    BusNr = gekozenBusNr,
                    StraatId = straatId
                };
                context.Adressen.Add(adres);
                context.SaveChanges();
                persoon.AdresId = adres.AdresId;
            }
            else
            {
                persoon.AdresId = adresID.AdresId;
            }
            

            var profiel = new Profiel
            {
                VoorNaam= persoon.VoorNaam,
                FamilieNaam=persoon.FamilieNaam,
                GeslachtType= persoon.GeslachtType,
                GeboorteDatum= persoon.GeboorteDatum,
                AdresId= persoon.AdresId,
                GeboorteplaatsId= persoon.GeboorteplaatsId,
                TelefoonNr=persoon.TelefoonNr,
                LoginNaam=persoon.LoginNaam,
                LoginPaswoord= persoon.LoginPaswoord,
                TaalId=persoon.TaalId,
                KenningsmakingTekst=persoon.KenningsmakingTekst,
                WoontHierSindsDatum= persoon.WoontHierSindsDatum,
                BeroepTekst=persoon.BeroepTekst,
                FirmaNaam=persoon.FirmaNaam,
                WebsiteAdres= persoon.WebsiteAdres,
                EmailAdres= persoon.EmailAdres,
                FacebookNaam= persoon.FacebookNaam,
                CreatieTijdstip = DateTime.Now,
                LaatsteUpdateTijdstip = DateTime.Now

        };
            context.Profielen.Add(profiel);
            
            ToonGegegevens(profiel);
            bool? opslaan=ConsoleHelper.LeesBool("Bewaren OK ? ",OptionMode.Mandatory);
            if (opslaan == false)
            {
                KiesHoofdmenu();
            }
            else
            {
                context.SaveChanges();
                Console.WriteLine($"toegevoegd als gebruiker (id: {profiel.PersoonId}).");
                Console.WriteLine("Wacht nu op goedkeuring van een medewerker");
                KiesHoofdmenu();
            }
            return Account;
        }

        public static void ToonGegegevens(Persoon persoon)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            using var context = new EFEindproefContext();

            //var pers = context.Personen.Where(p => p.PersoonId == 1);
            //var a = persoon.Adres.Straat.StraatNaam

            var adres = (from a in context.Adressen.Include("Straat")
                         where a.AdresId == persoon.AdresId
                         select a).FirstOrDefault();
            var straat = (from s in context.Straten.Include("Gemeente")
                          where s.StraatId==adres.StraatId
                          select s).FirstOrDefault();
            var gemeente = (from g in context.Gemeentes.Include("Provincie")
                            where g.GemeenteId == straat.GemeenteId
                            select g).FirstOrDefault();
            var provincie = (from p in context.Provincies
                             where p.ProvincieId == gemeente.ProvincieId
                             select p).FirstOrDefault();
            var taal = (from t in context.Talen
                        where t.TaalId == persoon.TaalId
                        select t).FirstOrDefault();
            var hoofdgemeente = (from h in context.Gemeentes
                                 where h.HoofdGemeente != null && h.GemeenteId == gemeente.GemeenteId
                                 select h).FirstOrDefault();



            string geblokkeerd = "";
            if (!persoon.Geblokkeerd)
            {
                geblokkeerd = "Niet ";
            }
            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine("Overzicht");
            Console.WriteLine("---------");

            Console.WriteLine($"Naam:  { persoon.VoorNaam} {persoon.FamilieNaam} ");
            Console.WriteLine("");
            Console.WriteLine($"Adres :" );
            Console.WriteLine($"{straat.StraatNaam} {adres.HuisNr} {adres.BusNr}");
            if (hoofdgemeente ==null)
            {
                Console.WriteLine($"{gemeente.PostCode}  {gemeente.GemeenteNaam}");
            }
            else
            {
                Console.WriteLine($"{gemeente.PostCode}  {gemeente.GemeenteNaam} Hoofdgemeente: {hoofdgemeente.HoofdGemeente.GemeenteNaam}");
            }
            
            Console.WriteLine($"{provincie.ProvincieNaam}");
            Console.WriteLine("");
            Console.WriteLine($"Geboortedatum: { persoon.GeboorteDatum}");
            Console.WriteLine($"Geslacht: {persoon.GeslachtType}");
            Console.WriteLine($"Taal: {taal.TaalNaam}");
            Console.WriteLine("");
            Console.WriteLine($"Telefoon: {persoon.TelefoonNr}");
            Console.WriteLine("");
            Console.WriteLine($"Login: {persoon.VoorNaam}/{persoon.LoginPaswoord} {geblokkeerd}Geblokkeerd" );
            Console.WriteLine($"Aantal keer ingelogd: {persoon.LoginAantal}");
            if (persoon is Profiel)
            {
                var profiel = persoon as Profiel;
                Console.WriteLine($"Woont hier sinds: {profiel.WoontHierSindsDatum}");
                Console.WriteLine($"Emailadres: {profiel.EmailAdres}");
                Console.WriteLine($"Facebook: {profiel.FacebookNaam}");
                Console.WriteLine($"Website: {profiel.WebsiteAdres}");
                Console.WriteLine($"Beroep: {profiel.BeroepTekst}");
                Console.WriteLine($"Profiel goedgekeurd op:");
                Console.WriteLine($"Aangemaakt op: {profiel.CreatieTijdstip}");
                Console.WriteLine($"");
                Console.WriteLine($"Kennismakingstekst: {profiel.KenningsmakingTekst}");
             
            }

        }

        public static void WijzigGegevens(Persoon persoon)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;

            var profiel = persoon as Profiel;
            using var context = new EFEindproefContext();

            Console.WriteLine($"Uw profiel is: ID:{profiel.PersoonId} Naam:{profiel.LoginNaam}");
            Console.WriteLine("----------------");
            Console.WriteLine("Wijzigen Profiel");
            Console.WriteLine("----------------");
            
            Console.WriteLine("1.     Kennismakingstekst");
            Console.WriteLine("2.     Email");
            Console.WriteLine("3.     Firma");
            Console.WriteLine("4.     Taal");

            int goedekeuze;
            do
            {
                Console.Write("Geef het volgnummer uit de lijst: ");
                if (int.TryParse(Console.ReadLine(),out int keuze))
                {
                    if (keuze > 0 && keuze<5)
                    {
                        goedekeuze = keuze;
                        break;
                    }
                    
                }
                Console.WriteLine("Geen geldige ingave");
            } while (true);

            Console.WriteLine("");
            switch (goedekeuze)
            {
                case 1:
                    profiel.KenningsmakingTekst = ConsoleHelper.LeesString("Kennismaking Tekst", 250, OptionMode.Mandatory);
                    profiel.LaatsteUpdateTijdstip= DateTime.Now;
                    context.Update(profiel);
                    context.SaveChanges();
                    Console.WriteLine($"Uw tekst werd gewijzigd naar: {profiel.KenningsmakingTekst}");
                    
                    
                    break;
                case 2:
                    profiel.EmailAdres = ConsoleHelper.LeesEmailAdres("Emailadres", OptionMode.Mandatory);
                    profiel.LaatsteUpdateTijdstip = DateTime.Now;
                    context.Update(profiel);
                    context.SaveChanges();
                    Console.WriteLine($"Uw email werd gewijzigd naar: {profiel.EmailAdres}");
                    
                    break;
                case 3:
                    profiel.FirmaNaam = ConsoleHelper.LeesString("Firma", 30, OptionMode.Optional);
                    profiel.LaatsteUpdateTijdstip = DateTime.Now;
                    context.Update(profiel);
                    context.SaveChanges();
                    Console.WriteLine($"Uw firma werd gewijzigd naar: {profiel.FirmaNaam}");
                    
                    break;
                case 4:
                    int keuzeTaal;
                    var lijstTaal = context.Talen.OrderBy(m => m.TaalId).ToList();
                    Console.WriteLine("Kies taal*");
                    Console.WriteLine("-----------");
                    foreach (var item in lijstTaal)
                    {
                        Console.WriteLine($"{item.TaalId}           {item.TaalNaam}");
                    }
                    
                    do
                    {
                        Console.Write("Geef het volgnummer uit de lijst: ");
                        if (int.TryParse(Console.ReadLine(), out int keuze))
                        {
                            if (keuze > 0 && keuze < 4)
                            {
                                keuzeTaal = keuze;
                                break;
                            }

                        }
                        Console.WriteLine("Geen geldige ingave");
                    } while (true);
                    profiel.TaalId = keuzeTaal;
                    profiel.LaatsteUpdateTijdstip = DateTime.Now;
                    context.Update(profiel);
                    context.SaveChanges();
                    var gekozenTaal = context.Talen.Where(t => t.TaalId == profiel.TaalId).FirstOrDefault();
                    Console.WriteLine($"Gekozen taal is {gekozenTaal.TaalCode} - {gekozenTaal.TaalNaam}");
                    
                    break;

                default:
                    break;
            }
            Console.WriteLine("");
            Console.WriteLine($"Uw profiel werd gewijzigd!");
            Console.WriteLine("");
            KiesAccountMenu();

        }

        public static void VerwijderGegevens(Persoon persoon)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            using var context = new EFEindproefContext();
            bool? verwijderen = ConsoleHelper.LeesBool("Verwijderen OK ? ", OptionMode.Mandatory);
            if (verwijderen == false)
            {
                Console.WriteLine("U werd niet verwijderd als gebruiker.");
                KiesAccountMenu();
            }
            else
            {
                context.Personen.Remove(persoon);
                context.SaveChanges();
                Console.WriteLine("U werd verwijderd als gebruiker");
                Account = null;
                KiesHoofdmenu();
            }
        }

        public static void InvoerenNieuwBericht(Persoon persoon)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.WriteLine("Kies BerichtType*");
            Console.WriteLine("-----------------");
            using var context = new EFEindproefContext();
            var berichttype = context.BerichtTypes.OrderBy(b => b.BerichtTypeId).ToList();
            int berichtId;
            foreach (var item in berichttype)
            {
                Console.WriteLine($"{item.BerichtTypeId}       {item.BerichtTypeNaam}");
            }
            Console.WriteLine("");
            Console.WriteLine("Geef het volgnummer uit de lijst: ");
            berichtId = Convert.ToInt32(Console.ReadLine());
            var berichtgegevens = (from b in context.BerichtTypes
                                   where b.BerichtTypeId == berichtId
                                   select b).FirstOrDefault();
            Console.WriteLine($"Gekozen BerichtType is {berichtgegevens.BerichtTypeCode} - {berichtgegevens.BerichtTypeNaam}");
            Console.WriteLine("");
            string titelBericht = ConsoleHelper.LeesString("Titel bericht", 50, OptionMode.Mandatory);
            string berichtTekst = ConsoleHelper.LeesString("Bericht", 250, OptionMode.Mandatory);
            bool? opslaan = ConsoleHelper.LeesBool("Nieuw bericht toevoegen ", OptionMode.Mandatory);
            DateTime tijdstip = DateTime.Now;
            Console.WriteLine("");
            if (opslaan ==false)
            {
                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Red;
                
                Console.WriteLine("Bericht niet opgeslaan!");
                KiesHoofdmenu();
            }
            else
            {
                var bericht = new Bericht
                {
                    
                    GemeenteId = persoon.Adres.Straat.Gemeente.HoofdGemeente.GemeenteId,
                    PersoonId = persoon.PersoonId,
                    BerichtTypeId = berichtId,
                    BerichtTijdstip = tijdstip,
                    BerichtTitel = titelBericht,
                    BerichtTekst = berichtTekst
                };
                context.Berichten.Add(bericht);
                context.SaveChanges();
                Console.WriteLine($"Gemeente : {persoon.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam}");
                Console.WriteLine($"BerichtType : {berichtgegevens.BerichtTypeNaam}");
                Console.WriteLine($"Titel : {titelBericht}");
                Console.WriteLine($"Tekst : {berichtTekst}");
                Console.WriteLine($"Tijdstip : {tijdstip}");
                Console.WriteLine($"Profiel : {persoon.LoginNaam}");
                Console.WriteLine("Het bericht werd toegevoegd");
                Console.WriteLine("");
                KiesHoofdmenu();
            }
            
        }

        public static void RaadplegenBerichten(Persoon persoon)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            Console.WriteLine($"Kies berichten voor hoofdgemeente: {persoon.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam}");
            using var context = new EFEindproefContext();
            var berichten = from b in context.Berichten
                            where b.GemeenteId == persoon.Adres.Straat.Gemeente.HoofdGemeente.GemeenteId
                            select b;
            Console.WriteLine("");
            foreach (var item in berichten.ToList())

            {
                if (item.HoofdBerichtId==null)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"--{item.BerichtId}--    Van: {persoon.LoginNaam}  Op: {item.BerichtTijdstip}");
                    Console.WriteLine($"Type: {item.BerichtType.BerichtTypeNaam}");
                    Console.WriteLine($"Titel: {item.BerichtTitel}");
                    Console.WriteLine($"Tekst: {item.BerichtTekst}");
                    Console.WriteLine("---------------------------------------");
                }
                else
                {
                   
                    Console.WriteLine($"                 --{item.BerichtId}--    Van: {persoon.LoginNaam}  Op: {item.BerichtTijdstip}");
                     
                   Console.WriteLine($"                  Tekst: {item.BerichtTekst}");
                    Console.WriteLine("------------------------------------------------");
                }

            }
            Console.WriteLine("");
            int IntKeuze;
            do { 
            Console.Write("Geef het volgnummer uit de lijst: ");
            if (int.TryParse(Console.ReadLine(), out int keuze2))
            {
                if (keuze2 > 0 && keuze2 < 1000)
                {
                        IntKeuze = keuze2;
                    break;
                }

            }
            Console.WriteLine("Geen geldige ingave");
        } while (true);

            var bericht = (from b in context.Berichten
                           where b.BerichtId == IntKeuze
                           select b).FirstOrDefault();
            Console.WriteLine("");
            Console.WriteLine($"Gekozen bericht is {bericht.BerichtTijdstip} - {bericht.BerichtTitel} - {bericht.BerichtTekst}");
            Console.WriteLine("");
            string input;
            char? keuze = null;
            if (bericht.PersoonId!=persoon.PersoonId)
            {
                input = "AX";
                
                keuze = ConsoleHelper.LeesString($"Geef uw keuze ( e<X>it, <A>ntwoorden) ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }
                if (keuze == 'X')
                {
                    KiesHoofdmenu();
                }
                if (keuze == 'A')
                {
                    AntwoordBericht(bericht);
                }

            }
            else
            {
                input = "AXWV";
                
                keuze = ConsoleHelper.LeesString($"Geef uw keuze ( e<X>it, <A>ntwoorden , <W>ijzigen, <V>erwijderen) ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }
                if (keuze =='V')
                {
                    VerwijderBericht(bericht);
                }
                if (keuze=='W')
                {
                    WijzigBericht(bericht);
                }
                if (keuze == 'X')
                {
                    KiesHoofdmenu();
                }
                if (keuze == 'A')
                {
                    AntwoordBericht(bericht);
                }
            }
        }

        public static void AntwoordBericht(Bericht hoofdBericht)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            using var context = new EFEindproefContext();
            int hoofdBerichtId = hoofdBericht.BerichtId;
            string antwoord = ConsoleHelper.LeesString("Antwoord", 255, OptionMode.Mandatory);
            bool? opslaan = ConsoleHelper.LeesBool("Antwoord toevoegen", OptionMode.Mandatory);
            if (opslaan == false)
            {
                Console.WriteLine("");
                Console.WriteLine("Niet geantwoord.");
                Console.WriteLine("");
                RaadplegenBerichten(Account);
            }
            else
            {
                var bericht = new Bericht
                {
                    HoofdBerichtId = hoofdBerichtId,
                    GemeenteId = Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteId,
                    PersoonId = Account.PersoonId,
                    BerichtTypeId = hoofdBericht.BerichtTypeId,
                    BerichtTijdstip = DateTime.Now,
                    BerichtTitel = hoofdBericht.BerichtTitel,
                    BerichtTekst = antwoord
                };
                context.Berichten.Add(bericht);
                context.SaveChanges();
                Console.WriteLine($"Gemeente : {Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam}");
                //Console.WriteLine($"BerichtType : {bericht.BerichtType.BerichtTypeNaam}");
                Console.WriteLine($"Titel : {bericht.BerichtTitel}");
                Console.WriteLine($"Tekst : {bericht.BerichtTekst}");
                Console.WriteLine($"Tijdstip : {bericht.BerichtTijdstip}");
                Console.WriteLine($"Profiel : {Account.LoginNaam}");
                Console.WriteLine("Het bericht werd toegevoegd");
                Console.WriteLine("");
                KiesHoofdmenu();
            }
        }

        public static void WijzigBericht(Bericht bericht)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            using var context = new EFEindproefContext();
            
            string nieuweTekst = ConsoleHelper.LeesString("Wijzig berichttekst", 250, OptionMode.Mandatory);
            bool? wijzigen = ConsoleHelper.LeesBool("Bericht Wijzigen", OptionMode.Mandatory);
            if (wijzigen ==false)
            {
                Console.WriteLine("");
                Console.WriteLine("Bericht niet gewijzigd.");
                Console.WriteLine("");
                RaadplegenBerichten(Account);

            }
            else
            {
                bericht.BerichtTekst = nieuweTekst;
                bericht.BerichtTijdstip = DateTime.Now;
                context.Update(bericht);
                context.SaveChanges();
                Console.WriteLine("");
                Console.WriteLine($"Gemeente: {bericht.GemeenteId}");
                Console.WriteLine($"BerichtType: {bericht.BerichtType}");
                Console.WriteLine($"Titel: {bericht.BerichtTitel}");
                Console.WriteLine($"Tekst: {bericht.BerichtTekst}");
                Console.WriteLine($"Tijdstip: {bericht.BerichtTijdstip}");
                Console.WriteLine($"Profiel {Account.LoginNaam}");
                Console.WriteLine("");
                Console.WriteLine("Bericht is gewijzigd.");
                Console.WriteLine("");

            }
        }

        public static void VerwijderBericht(Bericht bericht)
        {
            Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Blue;
            using var context = new EFEindproefContext();
            bool? verwijderen = ConsoleHelper.LeesBool("Bericht verwijderen", OptionMode.Mandatory);
            if (verwijderen==false)
            {
                Console.WriteLine("");
                Console.WriteLine("Bericht niet verwijderd.");
                Console.WriteLine("");
                RaadplegenBerichten(Account);
            }
            else
            {
                context.Berichten.Remove(bericht);
                context.SaveChanges();
                Console.WriteLine("");
                Console.WriteLine("Bericht is verwijderd.");
                Console.WriteLine("");
                RaadplegenBerichten(Account);
            }
            

        }




        public static List<Bericht> GetBerichtenGemeente(Gemeente gemeente)
        {
            using var context = new EFEindproefContext();
            var sqlString =
                $@"WITH tree AS
(
                    SELECT  BerichtId,HoofdBerichtId,BerichtTypeId,BerichtTitel,BerichtTekst,BerichtTijdstip,b.GemeenteId,b.PersoonId,p.LoginNaam,1 AS Level1,
CAST(right('0000'+convert(varchar(4), BerichtId),4) as varchar(1000) Hierarchy
FROM Berichten b join Gemeenten g on b.GemeenteId=g.GemeenteId join Personen p on b.PersoonId = p.PersoonId
where HoofdBerichtId is null
and ((g.GemeenteId ={gemeente.GemeenteId}) or (HoofdGemeenteId={gemeente.GemeenteId}
or (g.GemeenteId =(select HoofdGemeenteId from Gemeenten where GemeenteId={gemeente.GemeenteId})
or HoofdGemeenteId = (select HoofdGemeenteId from Gemeenten where GemeenteId={gemeente.GemeenteId})))
UNION ALL
SELECT b.BerichtId,b.HoofdBerichtId,b.BerichtTypeId,b.BerichtTitel,b.BerichtTekst,b.BerichtTijdstip,b.GemeenteId,b.PersoonId,p.LoginNaam,t.Level1+1,
CAST (Hierarchy+':'+CAST(right('0000'+convert(varchar(4),b.BerichtId),4) as varchar(100)) as varchar(1000) Hierarchy
FROM Berichten b inner join tree t on b.HoofdBerichtId=t.BerichtId join Personen p on b.PersoonId = p.PersoonId)
SELECT *, Level=Level1 FROM tree t order by Hierarchy";
            var berichten = context.Berichten.FromSqlRaw(sqlString).ToList();
            foreach (var bericht in berichten)
            {
                bericht.Persoon = context.Profielen.Where(t => t.PersoonId == bericht.PersoonId).FirstOrDefault();
                bericht.BerichtType = context.BerichtTypes.Where(t => t.BerichtTypeId == bericht.BerichtTypeId).FirstOrDefault();
            }
            return berichten;
        }
    }
}
