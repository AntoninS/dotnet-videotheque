using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace videotheque.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Libelle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateSortie = table.Column<DateTime>(nullable: false),
                    Duree = table.Column<TimeSpan>(nullable: false),
                    Titre = table.Column<string>(nullable: true),
                    Vu = table.Column<bool>(nullable: false),
                    Note = table.Column<int>(nullable: false),
                    Commentaire = table.Column<string>(nullable: true),
                    Synopsis = table.Column<string>(nullable: true),
                    TypeMedia = table.Column<int>(nullable: false),
                    AgeMinimum = table.Column<int>(nullable: false),
                    SupportPhysique = table.Column<bool>(nullable: false),
                    SupportNumerique = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    LangueVO = table.Column<int>(nullable: false),
                    LangueMedia = table.Column<int>(nullable: false),
                    SousTitres = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Civilite = table.Column<int>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Nationalite = table.Column<string>(nullable: true),
                    DateNaissance = table.Column<DateTime>(nullable: false),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdMedia = table.Column<int>(nullable: false),
                    NumSaison = table.Column<int>(nullable: false),
                    NumEpisode = table.Column<int>(nullable: false),
                    Duree = table.Column<TimeSpan>(nullable: false),
                    Titre = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateDiffusion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Medias_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMedias",
                columns: table => new
                {
                    IdGenre = table.Column<int>(nullable: false),
                    IdMedia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMedias", x => new { x.IdGenre, x.IdMedia });
                    table.ForeignKey(
                        name: "FK_GenreMedias_Genres_IdGenre",
                        column: x => x.IdGenre,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMedias_Medias_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonneMedias",
                columns: table => new
                {
                    IdMedia = table.Column<int>(nullable: false),
                    IdPersonne = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fonction = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonneMedias", x => new { x.IdPersonne, x.IdMedia });
                    table.UniqueConstraint("AK_PersonneMedias_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonneMedias_Medias_IdMedia",
                        column: x => x.IdMedia,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonneMedias_Personnes_IdPersonne",
                        column: x => x.IdPersonne,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_IdMedia",
                table: "Episodes",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMedias_IdMedia",
                table: "GenreMedias",
                column: "IdMedia");

            migrationBuilder.CreateIndex(
                name: "IX_PersonneMedias_IdMedia",
                table: "PersonneMedias",
                column: "IdMedia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "GenreMedias");

            migrationBuilder.DropTable(
                name: "PersonneMedias");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Personnes");
        }
    }
}
