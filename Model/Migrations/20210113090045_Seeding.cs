using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Afdelingen",
                columns: new[] { "AfdelingId", "AfdelingCode", "AfdelingNaam", "AfdelingTekst" },
                values: new object[,]
                {
                    { 1, "VERK", "Verkoop", null },
                    { 2, "BOEK", "Boekhouding", null },
                    { 3, "AANK", "Aankoop", null }
                });

            migrationBuilder.InsertData(
                table: "BerichtTypes",
                columns: new[] { "BerichtTypeId", "BerichtTypeCode", "BerichtTypeNaam", "BerichtTypeTekst" },
                values: new object[,]
                {
                    { 11, "GH", "Gezondheid", null },
                    { 10, "HD", "Huishouden", null },
                    { 9, "BS", "Babysit", null },
                    { 8, "MD", "Melding", null },
                    { 6, "WG", "Weggeven", null },
                    { 7, "AC", "Activiteit", null },
                    { 4, "ID", "Idee", null },
                    { 3, "IZ", "Ik zoek", null },
                    { 2, "TK", "Te Koop", null },
                    { 1, "AL", "Algemeen", null },
                    { 5, "LN", "Lenen", null }
                });

            migrationBuilder.InsertData(
                table: "InteresseSoorten",
                columns: new[] { "IntersseSoortId", "InteresseSoortNaam" },
                values: new object[,]
                {
                    { 7, "TV" },
                    { 10, "Zwemmen" },
                    { 9, "Wandelen" },
                    { 8, "Vrijwilliegrswerk" },
                    { 6, "Natuur" },
                    { 5, "Muziek spelen" },
                    { 4, "Muziek" },
                    { 3, "Klussen" },
                    { 2, "ICT" },
                    { 1, "Fietsen" }
                });

            migrationBuilder.InsertData(
                table: "Provincies",
                columns: new[] { "ProvincieId", "ProvincieCode", "ProvincieNaam" },
                values: new object[,]
                {
                    { 11, "BRU", "Brussel" },
                    { 10, "NAM", "Naman" },
                    { 9, "LUX", "Luxemburg" },
                    { 8, "LUI", "Luik" },
                    { 7, "HEN", "Henegouwen" },
                    { 5, "WVL", "West-Vlaanderen" },
                    { 4, "VBR", "Vlaams-Brabant" },
                    { 3, "OVL", "Oost-Vlaanderen" },
                    { 2, "LIM", "Limburg" },
                    { 1, "ANT", "Antwerpen" },
                    { 6, "WBR", "Waals-Brabant" }
                });

            migrationBuilder.InsertData(
                table: "Talen",
                columns: new[] { "TaalId", "TaalCode", "TaalNaam" },
                values: new object[,]
                {
                    { 2, "fr", "Frans" },
                    { 1, "nl", "Nederlands" },
                    { 3, "en", "Engels" }
                });

            migrationBuilder.InsertData(
                table: "Gemeentes",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "PostCode", "ProvincieId", "TaalId" },
                values: new object[] { 1730, "Beernem", null, 8730, 5, 1 });

            migrationBuilder.InsertData(
                table: "Gemeentes",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "PostCode", "ProvincieId", "TaalId" },
                values: new object[] { 1790, "Oostkamp", null, 8020, 5, 1 });

            migrationBuilder.InsertData(
                table: "Gemeentes",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "PostCode", "ProvincieId", "TaalId" },
                values: new object[,]
                {
                    { 1731, "Oedelem", 1730, 8730, 5, 1 },
                    { 1732, "Sint-Joris", 1730, 8730, 5, 1 },
                    { 1791, "Herstberge", 1790, 8020, 5, 1 },
                    { 1792, "Ruddervoorde", 1790, 8020, 5, 1 },
                    { 1793, "Waardamme", 1790, 8020, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Straten",
                columns: new[] { "StraatId", "GemeenteId", "StraatNaam" },
                values: new object[,]
                {
                    { 1, 1730, "Abelenstraat" },
                    { 2, 1730, "Dorpstraat" },
                    { 3, 1730, "Marktplaats" },
                    { 8, 1790, "Corneelstraat" },
                    { 9, 1790, "Davidstraat" },
                    { 10, 1790, "Eikenstraat" }
                });

            migrationBuilder.InsertData(
                table: "Adressen",
                columns: new[] { "AdresId", "BusNr", "HuisNr", "StraatId" },
                values: new object[,]
                {
                    { 1, null, "1", 1 },
                    { 2, null, "2", 2 },
                    { 3, "1", "3a", 3 }
                });

            migrationBuilder.InsertData(
                table: "Straten",
                columns: new[] { "StraatId", "GemeenteId", "StraatNaam" },
                values: new object[,]
                {
                    { 4, 1731, "Anjerstraat" },
                    { 5, 1731, "Bloemenstraat" },
                    { 6, 1732, "Alexstraat" },
                    { 7, 1732, "Bartstraat" },
                    { 11, 1791, "Fanfarestraat" },
                    { 12, 1791, "Geelstraat" },
                    { 13, 1792, "Hamstraat" },
                    { 14, 1792, "Imkerstraat" },
                    { 15, 1793, "Jurgenstraat" },
                    { 16, 1793, "Kimstraat" },
                    { 17, 1793, "Langestraat" }
                });

            migrationBuilder.InsertData(
                table: "Adressen",
                columns: new[] { "AdresId", "BusNr", "HuisNr", "StraatId" },
                values: new object[,]
                {
                    { 4, "2", "4", 4 },
                    { 5, "3", "5b", 5 },
                    { 6, null, "6", 6 }
                });

            migrationBuilder.InsertData(
                table: "Personen",
                columns: new[] { "PersoonId", "AdresId", "AfdelingId", "AfdelingsId", "FamilieNaam", "Geblokkeerd", "GeboorteDatum", "GeboorteplaatsId", "GeslachtType", "LoginAantal", "LoginNaam", "LoginPaswoord", "PersoonType", "TaalId", "TelefoonNr", "VerkeerdeLoginsAantal", "VoorNaam" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "Jansen", false, new DateTime(2000, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1790, 0, 0, "Jan", "Baarden", "M", 1, "011/111111", 0, "Jan" },
                    { 2, 1, null, 1, "Pieters", false, new DateTime(2000, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1790, 0, 0, "Piet", "Baarden", "M", 1, "012/222222", 0, "Piet" },
                    { 3, 1, null, 1, "Jorissen", false, new DateTime(2001, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1790, 0, 0, "Joris", "Baarden", "M", 1, "013/333333", 0, "Joris" },
                    { 4, 1, null, 1, "Korneels", false, new DateTime(2002, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1790, 0, 0, "Korneel", "Baarden", "M", 1, "014/444444", 0, "Korneel" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Afdelingen",
                keyColumn: "AfdelingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Afdelingen",
                keyColumn: "AfdelingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Afdelingen",
                keyColumn: "AfdelingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BerichtTypes",
                keyColumn: "BerichtTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "InteresseSoorten",
                keyColumn: "IntersseSoortId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Personen",
                keyColumn: "PersoonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Personen",
                keyColumn: "PersoonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Personen",
                keyColumn: "PersoonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Personen",
                keyColumn: "PersoonId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Talen",
                keyColumn: "TaalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Talen",
                keyColumn: "TaalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Adressen",
                keyColumn: "AdresId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1791);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1792);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1793);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1731);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1732);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1790);

            migrationBuilder.DeleteData(
                table: "Straten",
                keyColumn: "StraatId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gemeentes",
                keyColumn: "GemeenteId",
                keyValue: 1730);

            migrationBuilder.DeleteData(
                table: "Provincies",
                keyColumn: "ProvincieId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Talen",
                keyColumn: "TaalId",
                keyValue: 1);
        }
    }
}
