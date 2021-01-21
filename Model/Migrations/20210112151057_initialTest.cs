﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class initialTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afdelingen",
                columns: table => new
                {
                    AfdelingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AfdelingCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    AfdelingNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AfdelingTekst = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afdelingen", x => x.AfdelingId);
                });

            migrationBuilder.CreateTable(
                name: "BerichtTypes",
                columns: table => new
                {
                    BerichtTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BerichtTypeCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BerichtTypeNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BerichtTypeTekst = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerichtTypes", x => x.BerichtTypeId);
                });

            migrationBuilder.CreateTable(
                name: "InteresseSoorten",
                columns: table => new
                {
                    IntersseSoortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InteresseSoortNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteresseSoorten", x => x.IntersseSoortId);
                });

            migrationBuilder.CreateTable(
                name: "Provincies",
                columns: table => new
                {
                    ProvincieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvincieCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ProvincieNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincies", x => x.ProvincieId);
                });

            migrationBuilder.CreateTable(
                name: "Talen",
                columns: table => new
                {
                    TaalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaalCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TaalNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talen", x => x.TaalId);
                });

            migrationBuilder.CreateTable(
                name: "Gemeentes",
                columns: table => new
                {
                    GemeenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GemeenteNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostCode = table.Column<int>(type: "int", nullable: false),
                    ProvincieId = table.Column<int>(type: "int", nullable: false),
                    TaalId = table.Column<int>(type: "int", nullable: false),
                    HoofdGemeenteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gemeentes", x => x.GemeenteId);
                    table.ForeignKey(
                        name: "FK_Gemeentes_Gemeentes_HoofdGemeenteId",
                        column: x => x.HoofdGemeenteId,
                        principalTable: "Gemeentes",
                        principalColumn: "GemeenteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gemeentes_Provincies_ProvincieId",
                        column: x => x.ProvincieId,
                        principalTable: "Provincies",
                        principalColumn: "ProvincieId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Gemeentes_Talen_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Talen",
                        principalColumn: "TaalId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Straten",
                columns: table => new
                {
                    StraatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StraatNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GemeenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Straten", x => x.StraatId);
                    table.ForeignKey(
                        name: "FK_Straten_Gemeentes_GemeenteId",
                        column: x => x.GemeenteId,
                        principalTable: "Gemeentes",
                        principalColumn: "GemeenteId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    AdresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HuisNr = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    BusNr = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    StraatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.AdresId);
                    table.ForeignKey(
                        name: "FK_Adressen_Straten_StraatId",
                        column: x => x.StraatId,
                        principalTable: "Straten",
                        principalColumn: "StraatId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Personen",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoorNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FamilieNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GeslachtType = table.Column<int>(type: "int", nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    GeboorteplaatsId = table.Column<int>(type: "int", nullable: false),
                    Geblokkeerd = table.Column<bool>(type: "bit", nullable: false),
                    TelefoonNr = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LoginNaam = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LoginPaswoord = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VerkeerdeLoginsAantal = table.Column<int>(type: "int", nullable: false),
                    LoginAantal = table.Column<int>(type: "int", nullable: false),
                    TaalId = table.Column<int>(type: "int", nullable: false),
                    PersoonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfdelingsId = table.Column<int>(type: "int", nullable: true),
                    AfdelingId = table.Column<int>(type: "int", nullable: true),
                    KenningsmakingTekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WoontHierSindsDatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeroepTekst = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FirmaNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WebsiteAdres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailAdres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacebookNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GoedgekeurdTijdstip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatieTijdstip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LaatsteUpdateTijdstip = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personen", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Personen_Adressen_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adressen",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Personen_Afdelingen_AfdelingId",
                        column: x => x.AfdelingId,
                        principalTable: "Afdelingen",
                        principalColumn: "AfdelingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personen_Gemeentes_GeboorteplaatsId",
                        column: x => x.GeboorteplaatsId,
                        principalTable: "Gemeentes",
                        principalColumn: "GemeenteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Personen_Talen_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Talen",
                        principalColumn: "TaalId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Berichten",
                columns: table => new
                {
                    BerichtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoofdBerichtId = table.Column<int>(type: "int", nullable: false),
                    GemeenteId = table.Column<int>(type: "int", nullable: false),
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    BerichtTypeId = table.Column<int>(type: "int", nullable: false),
                    BerichtTijdstip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BerichtTitel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BerichtTekst = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProfielPersoonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berichten", x => x.BerichtId);
                    table.ForeignKey(
                        name: "FK_Berichten_Berichten_HoofdBerichtId",
                        column: x => x.HoofdBerichtId,
                        principalTable: "Berichten",
                        principalColumn: "BerichtId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Berichten_BerichtTypes_BerichtTypeId",
                        column: x => x.BerichtTypeId,
                        principalTable: "BerichtTypes",
                        principalColumn: "BerichtTypeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Berichten_Personen_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Berichten_Personen_ProfielPersoonId",
                        column: x => x.ProfielPersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfielInteresses",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    InteresseSoortId = table.Column<int>(type: "int", nullable: false),
                    ProfielInteresseTekst = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProfielPersoonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfielInteresses", x => new { x.PersoonId, x.InteresseSoortId });
                    table.ForeignKey(
                        name: "FK_ProfielInteresses_InteresseSoorten_InteresseSoortId",
                        column: x => x.InteresseSoortId,
                        principalTable: "InteresseSoorten",
                        principalColumn: "IntersseSoortId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProfielInteresses_Personen_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProfielInteresses_Personen_ProfielPersoonId",
                        column: x => x.ProfielPersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adressen_StraatId_HuisNr_BusNr",
                table: "Adressen",
                columns: new[] { "StraatId", "HuisNr", "BusNr" },
                unique: true,
                filter: "[BusNr] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Afdelingen_AfdelingCode",
                table: "Afdelingen",
                column: "AfdelingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Afdelingen_AfdelingNaam",
                table: "Afdelingen",
                column: "AfdelingNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_BerichtTypeId",
                table: "Berichten",
                column: "BerichtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_GemeenteId",
                table: "Berichten",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_HoofdBerichtId",
                table: "Berichten",
                column: "HoofdBerichtId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_PersoonId",
                table: "Berichten",
                column: "PersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_ProfielPersoonId",
                table: "Berichten",
                column: "ProfielPersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_BerichtTypes_BerichtTypeCode",
                table: "BerichtTypes",
                column: "BerichtTypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gemeentes_GemeenteNaam",
                table: "Gemeentes",
                column: "GemeenteNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gemeentes_HoofdGemeenteId",
                table: "Gemeentes",
                column: "HoofdGemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Gemeentes_ProvincieId",
                table: "Gemeentes",
                column: "ProvincieId");

            migrationBuilder.CreateIndex(
                name: "IX_Gemeentes_TaalId",
                table: "Gemeentes",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_InteresseSoorten_InteresseSoortNaam",
                table: "InteresseSoorten",
                column: "InteresseSoortNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personen_AdresId",
                table: "Personen",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_AfdelingId",
                table: "Personen",
                column: "AfdelingId");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_GeboorteplaatsId",
                table: "Personen",
                column: "GeboorteplaatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_LoginNaam",
                table: "Personen",
                column: "LoginNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personen_TaalId",
                table: "Personen",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfielInteresses_InteresseSoortId",
                table: "ProfielInteresses",
                column: "InteresseSoortId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfielInteresses_ProfielPersoonId",
                table: "ProfielInteresses",
                column: "ProfielPersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Provincies_ProvincieCode",
                table: "Provincies",
                column: "ProvincieCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provincies_ProvincieNaam",
                table: "Provincies",
                column: "ProvincieNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Straten_GemeenteId",
                table: "Straten",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Straten_StraatNaam",
                table: "Straten",
                column: "StraatNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talen_TaalNaam",
                table: "Talen",
                column: "TaalNaam",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Berichten");

            migrationBuilder.DropTable(
                name: "ProfielInteresses");

            migrationBuilder.DropTable(
                name: "BerichtTypes");

            migrationBuilder.DropTable(
                name: "InteresseSoorten");

            migrationBuilder.DropTable(
                name: "Personen");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Afdelingen");

            migrationBuilder.DropTable(
                name: "Straten");

            migrationBuilder.DropTable(
                name: "Gemeentes");

            migrationBuilder.DropTable(
                name: "Provincies");

            migrationBuilder.DropTable(
                name: "Talen");
        }
    }
}
