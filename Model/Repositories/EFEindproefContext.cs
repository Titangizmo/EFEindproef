
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Model.Entities;

namespace Model.Repositories
{
    public class EFEindproefContext : DbContext
    {
        public static IConfigurationRoot configuration;
        private bool testMode = false;
        // ------
        // DbSets
        // ------
        public DbSet<Provincie> Provincies { get; set; }
        public DbSet<Gemeente> Gemeentes { get; set; }
        public DbSet<Straat> Straten { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Taal> Talen { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Profiel> Profielen { get; set; }
        public DbSet<Afdeling> Afdelingen { get; set; }
        public DbSet<InteresseSoort> InteresseSoorten { get; set; }
        public DbSet<ProfielInteresse> ProfielInteresses { get; set; }
        public DbSet<Bericht> Berichten { get; set; }
        public DbSet<Bericht> HoofdBerichten { get; set; }
        public DbSet<BerichtType> BerichtTypes { get; set; }

        // ------------
        // Constructors
        // ------------
        public EFEindproefContext() { }
        public EFEindproefContext(DbContextOptions<EFEindproefContext> options) : base(options) { }

        // -------
        // Logging
        // -------
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging
            (builder => builder.AddConsole()
            .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
            );
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

                var connectionString = configuration.GetConnectionString("efeindproef");

                if (connectionString != null)
                {
                    optionsBuilder.UseSqlServer(
                    connectionString
                    , options => options.MaxBatchSize(150)).UseLazyLoadingProxies();
                    //.EnableSensitiveDataLogging(true) 

                }
            }
            else
            {
                testMode = true;
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ProfielInteresse key
            modelBuilder.Entity<ProfielInteresse>().HasKey(p => new { p.PersoonId, p.InteresseSoortId });
            //Index & Unique
            modelBuilder.Entity<Provincie>(entity =>{entity.HasIndex(e => e.ProvincieCode).IsUnique();});
            modelBuilder.Entity<Provincie>(entity =>{entity.HasIndex(e => e.ProvincieNaam).IsUnique();});
            modelBuilder.Entity<Gemeente>(entity => { entity.HasIndex(e => e.GemeenteNaam).IsUnique(); });
            modelBuilder.Entity<Provincie>(entity => { entity.HasIndex(e => e.ProvincieCode).IsUnique(); });
            modelBuilder.Entity<Straat>(entity => { entity.HasIndex(e => e.StraatNaam).IsUnique();});
            modelBuilder.Entity<Adres>(entity => { entity.HasIndex(e =>new { e.StraatId, e.HuisNr, e.BusNr }).IsUnique(); });
            modelBuilder.Entity<Taal>(entity => { entity.HasIndex(e => e.TaalNaam).IsUnique(); });
            modelBuilder.Entity<Persoon>(entity => { entity.HasIndex(e => e.LoginNaam).IsUnique(); });
            modelBuilder.Entity<Afdeling>(entity => { entity.HasIndex(e => e.AfdelingCode).IsUnique(); });
            modelBuilder.Entity<Afdeling>(entity => { entity.HasIndex(e => e.AfdelingNaam).IsUnique(); });
            modelBuilder.Entity<InteresseSoort>(entity => { entity.HasIndex(e => e.InteresseSoortNaam).IsUnique(); });
            modelBuilder.Entity<BerichtType>(entity => { entity.HasIndex(e => e.BerichtTypeCode).IsUnique(); });
            modelBuilder.Entity<Bericht>(entity => { entity.HasIndex(e => e.GemeenteId); });
            modelBuilder.Entity<Straat>(entity => { entity.HasIndex(e => new { e.StraatId, e.StraatNaam }).IsUnique(); });
            // ----------------
            // Inheritance: Persoon
            // ----------------
            modelBuilder.Entity<Persoon>()
            .ToTable("Personen") 
            .HasDiscriminator<string>("PersoonType") 
            .HasValue<Medewerker>("M")
            .HasValue<Profiel>("P");

            if (!testMode)
            {
                //Seeding
                modelBuilder.Entity<Provincie>().HasData
                (
                new Provincie { ProvincieId = 1, ProvincieCode = "ANT", ProvincieNaam = "Antwerpen" },
                new Provincie { ProvincieId = 2, ProvincieCode = "LIM", ProvincieNaam = "Limburg" },
                new Provincie { ProvincieId = 3, ProvincieCode = "OVL", ProvincieNaam = "Oost-Vlaanderen" },
                new Provincie { ProvincieId = 4, ProvincieCode = "VBR", ProvincieNaam = "Vlaams-Brabant" },
                new Provincie { ProvincieId = 5, ProvincieCode = "WVL", ProvincieNaam = "West-Vlaanderen" },
                new Provincie { ProvincieId = 6, ProvincieCode = "WBR", ProvincieNaam = "Waals-Brabant" },
                new Provincie { ProvincieId = 7, ProvincieCode = "HEN", ProvincieNaam = "Henegouwen" },
                new Provincie { ProvincieId = 8, ProvincieCode = "LUI", ProvincieNaam = "Luik" },
                new Provincie { ProvincieId = 9, ProvincieCode = "LUX", ProvincieNaam = "Luxemburg" },
                new Provincie { ProvincieId = 10, ProvincieCode = "NAM", ProvincieNaam = "Naman" },
                new Provincie { ProvincieId = 11, ProvincieCode = "BRU", ProvincieNaam = "Brussel" }
                );

                modelBuilder.Entity<Gemeente>().HasData
                    (
                    new Gemeente { GemeenteId = 1730, GemeenteNaam = "Beernem", PostCode = 8730, ProvincieId = 5, HoofdGemeenteId = null, TaalId = 1 },
                    new Gemeente { GemeenteId = 1731, GemeenteNaam = "Oedelem", PostCode = 8730, ProvincieId = 5, HoofdGemeenteId = 1730, TaalId = 1 },
                    new Gemeente { GemeenteId = 1732, GemeenteNaam = "Sint-Joris", PostCode = 8730, ProvincieId = 5, HoofdGemeenteId = 1730, TaalId = 1 },
                    new Gemeente { GemeenteId = 1790, GemeenteNaam = "Oostkamp", PostCode = 8020, ProvincieId = 5, HoofdGemeenteId = null, TaalId = 1 },
                    new Gemeente { GemeenteId = 1791, GemeenteNaam = "Herstberge", PostCode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 },
                    new Gemeente { GemeenteId = 1792, GemeenteNaam = "Ruddervoorde", PostCode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 },
                    new Gemeente { GemeenteId = 1793, GemeenteNaam = "Waardamme", PostCode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 }

                    );

                modelBuilder.Entity<Straat>().HasData
                    (
                    new Straat { StraatId = 1, StraatNaam = "Abelenstraat", GemeenteId = 1730 },
                    new Straat { StraatId = 2, StraatNaam = "Dorpstraat", GemeenteId = 1730 },
                    new Straat { StraatId = 3, StraatNaam = "Marktplaats", GemeenteId = 1730 },
                    new Straat { StraatId = 4, StraatNaam = "Anjerstraat", GemeenteId = 1731 },
                    new Straat { StraatId = 5, StraatNaam = "Bloemenstraat", GemeenteId = 1731 },
                    new Straat { StraatId = 6, StraatNaam = "Alexstraat", GemeenteId = 1732 },
                    new Straat { StraatId = 7, StraatNaam = "Bartstraat", GemeenteId = 1732 },
                    new Straat { StraatId = 8, StraatNaam = "Corneelstraat", GemeenteId = 1790 },
                    new Straat { StraatId = 9, StraatNaam = "Davidstraat", GemeenteId = 1790 },
                    new Straat { StraatId = 10, StraatNaam = "Eikenstraat", GemeenteId = 1790 },
                    new Straat { StraatId = 11, StraatNaam = "Fanfarestraat", GemeenteId = 1791 },
                    new Straat { StraatId = 12, StraatNaam = "Geelstraat", GemeenteId = 1791 },
                    new Straat { StraatId = 13, StraatNaam = "Hamstraat", GemeenteId = 1792 },
                    new Straat { StraatId = 14, StraatNaam = "Imkerstraat", GemeenteId = 1792 },
                    new Straat { StraatId = 15, StraatNaam = "Jurgenstraat", GemeenteId = 1793 },
                    new Straat { StraatId = 16, StraatNaam = "Kimstraat", GemeenteId = 1793 },
                    new Straat { StraatId = 17, StraatNaam = "Langestraat", GemeenteId = 1793 }

                    );

                modelBuilder.Entity<Adres>().HasData
                    (
                    new Adres { AdresId = 1, StraatId = 1, HuisNr = "1", BusNr = null },
                    new Adres { AdresId = 2, StraatId = 2, HuisNr = "2", BusNr = null },
                    new Adres { AdresId = 3, StraatId = 3, HuisNr = "3a", BusNr = "1" },
                    new Adres { AdresId = 4, StraatId = 4, HuisNr = "4", BusNr = "2" },
                    new Adres { AdresId = 5, StraatId = 5, HuisNr = "5b", BusNr = "3" },
                    new Adres { AdresId = 6, StraatId = 6, HuisNr = "6", BusNr = null }
                    );

                modelBuilder.Entity<Taal>().HasData
                    (
                    new Taal { TaalId = 1, TaalCode = "nl", TaalNaam = "Nederlands" },
                    new Taal { TaalId = 2, TaalCode = "fr", TaalNaam = "Frans" },
                    new Taal { TaalId = 3, TaalCode = "en", TaalNaam = "Engels" }

                    );

                modelBuilder.Entity<Medewerker>().HasData
                    (
                    new Medewerker { PersoonId = 1, VoorNaam = "Jan", FamilieNaam = "Jansen", GeslachtType = Persoon.Geslacht.Man, GeboorteDatum = new DateTime(2000, 01, 18), AdresId = 1, GeboorteplaatsId = 1790, TelefoonNr = "011/111111", LoginNaam = "Jan", LoginPaswoord = "Baarden", VerkeerdeLoginsAantal = 0, LoginAantal = 0, TaalId = 1, Geblokkeerd = false, AfdelingsId = 1 },
                    new Medewerker { PersoonId = 2, VoorNaam = "Piet", FamilieNaam = "Pieters", GeslachtType = Persoon.Geslacht.Man, GeboorteDatum = new DateTime(2000, 11, 18), AdresId = 1, GeboorteplaatsId = 1790, TelefoonNr = "012/222222", LoginNaam = "Piet", LoginPaswoord = "Baarden", VerkeerdeLoginsAantal = 0, LoginAantal = 0, TaalId = 1, Geblokkeerd = false,  AfdelingsId = 1 },
                    new Medewerker { PersoonId = 3, VoorNaam = "Joris", FamilieNaam = "Jorissen", GeslachtType = Persoon.Geslacht.Man, GeboorteDatum = new DateTime(2001, 1, 18), AdresId = 1, GeboorteplaatsId = 1790, TelefoonNr = "013/333333", LoginNaam = "Joris", LoginPaswoord = "Baarden", VerkeerdeLoginsAantal = 0, LoginAantal = 0, TaalId = 1, Geblokkeerd = false,  AfdelingsId = 1 },
                    new Medewerker { PersoonId = 4, VoorNaam = "Korneel", FamilieNaam = "Korneels", GeslachtType = Persoon.Geslacht.Man, GeboorteDatum = new DateTime(2002, 6, 18), AdresId = 1, GeboorteplaatsId = 1790, TelefoonNr = "014/444444", LoginNaam = "Korneel", LoginPaswoord = "Baarden", VerkeerdeLoginsAantal = 0, LoginAantal = 0, TaalId = 1, Geblokkeerd = false,  AfdelingsId = 1 }
                    );

                modelBuilder.Entity<Afdeling>().HasData
                    (
                    new Afdeling { AfdelingId = 1, AfdelingCode = "VERK", AfdelingNaam = "Verkoop", AfdelingTekst = null },
                    new Afdeling { AfdelingId = 2, AfdelingCode = "BOEK", AfdelingNaam = "Boekhouding", AfdelingTekst = null },
                    new Afdeling { AfdelingId = 3, AfdelingCode = "AANK", AfdelingNaam = "Aankoop", AfdelingTekst = null }
                    );

                modelBuilder.Entity<InteresseSoort>().HasData
                    (
                    new InteresseSoort { IntersseSoortId = 1, InteresseSoortNaam = "Fietsen" },
                    new InteresseSoort { IntersseSoortId = 2, InteresseSoortNaam = "ICT" },
                    new InteresseSoort { IntersseSoortId = 3, InteresseSoortNaam = "Klussen" },
                    new InteresseSoort { IntersseSoortId = 4, InteresseSoortNaam = "Muziek" },
                    new InteresseSoort { IntersseSoortId = 5, InteresseSoortNaam = "Muziek spelen" },
                    new InteresseSoort { IntersseSoortId = 6, InteresseSoortNaam = "Natuur" },
                    new InteresseSoort { IntersseSoortId = 7, InteresseSoortNaam = "TV" },
                    new InteresseSoort { IntersseSoortId = 8, InteresseSoortNaam = "Vrijwilliegrswerk" },
                    new InteresseSoort { IntersseSoortId = 9, InteresseSoortNaam = "Wandelen" },
                    new InteresseSoort { IntersseSoortId = 10, InteresseSoortNaam = "Zwemmen" }
                    );

                modelBuilder.Entity<BerichtType>().HasData
                    (
                    new BerichtType { BerichtTypeId = 1, BerichtTypeCode = "AL", BerichtTypeNaam = "Algemeen" },
                    new BerichtType { BerichtTypeId = 2, BerichtTypeCode = "TK", BerichtTypeNaam = "Te Koop" },
                    new BerichtType { BerichtTypeId = 3, BerichtTypeCode = "IZ", BerichtTypeNaam = "Ik zoek" },
                    new BerichtType { BerichtTypeId = 4, BerichtTypeCode = "ID", BerichtTypeNaam = "Idee" },
                    new BerichtType { BerichtTypeId = 5, BerichtTypeCode = "LN", BerichtTypeNaam = "Lenen" },
                    new BerichtType { BerichtTypeId = 6, BerichtTypeCode = "WG", BerichtTypeNaam = "Weggeven" },
                    new BerichtType { BerichtTypeId = 7, BerichtTypeCode = "AC", BerichtTypeNaam = "Activiteit" },
                    new BerichtType { BerichtTypeId = 8, BerichtTypeCode = "MD", BerichtTypeNaam = "Melding" },
                    new BerichtType { BerichtTypeId = 9, BerichtTypeCode = "BS", BerichtTypeNaam = "Babysit" },
                    new BerichtType { BerichtTypeId = 10, BerichtTypeCode = "HD", BerichtTypeNaam = "Huishouden" },
                    new BerichtType { BerichtTypeId = 11, BerichtTypeCode = "GH", BerichtTypeNaam = "Gezondheid" }
                    );
            }


        }

    }
}
