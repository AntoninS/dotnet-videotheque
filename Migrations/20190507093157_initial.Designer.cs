﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using videotheque.DataAccess;

namespace videotheque.Migrations
{
    [DbContext(typeof(VideothequeDbContext))]
    [Migration("20190507093157_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("videotheque.classes.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateDiffusion");

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("Duree");

                    b.Property<int>("IdMedia");

                    b.Property<int>("NumEpisode");

                    b.Property<int>("NumSaison");

                    b.Property<string>("Titre");

                    b.HasKey("Id");

                    b.HasIndex("IdMedia");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("videotheque.classes.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Libelle");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("videotheque.classes.GenreMedia", b =>
                {
                    b.Property<int>("IdGenre");

                    b.Property<int>("IdMedia");

                    b.HasKey("IdGenre", "IdMedia");

                    b.HasIndex("IdMedia");

                    b.ToTable("GenreMedias");
                });

            modelBuilder.Entity("videotheque.classes.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgeMinimum");

                    b.Property<string>("Commentaire");

                    b.Property<DateTime>("DateSortie");

                    b.Property<TimeSpan>("Duree");

                    b.Property<string>("Image");

                    b.Property<int>("LangueMedia");

                    b.Property<int>("LangueVO");

                    b.Property<int>("Note");

                    b.Property<int>("SousTitres");

                    b.Property<bool>("SupportNumerique");

                    b.Property<bool>("SupportPhysique");

                    b.Property<string>("Synopsis");

                    b.Property<string>("Titre");

                    b.Property<int>("TypeMedia");

                    b.Property<bool>("Vu");

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("videotheque.classes.Personne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Civilite");

                    b.Property<DateTime>("DateNaissance");

                    b.Property<string>("Nationalite");

                    b.Property<string>("Nom");

                    b.Property<string>("Photo");

                    b.Property<string>("Prenom");

                    b.HasKey("Id");

                    b.ToTable("Personnes");
                });

            modelBuilder.Entity("videotheque.classes.PersonneMedia", b =>
                {
                    b.Property<int>("IdPersonne");

                    b.Property<int>("IdMedia");

                    b.Property<int>("Fonction");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Role");

                    b.HasKey("IdPersonne", "IdMedia");

                    b.HasAlternateKey("Id");

                    b.HasIndex("IdMedia");

                    b.ToTable("PersonneMedias");
                });

            modelBuilder.Entity("videotheque.classes.Episode", b =>
                {
                    b.HasOne("videotheque.classes.Media", "Media")
                        .WithMany("Episodes")
                        .HasForeignKey("IdMedia")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("videotheque.classes.GenreMedia", b =>
                {
                    b.HasOne("videotheque.classes.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("videotheque.classes.Media", "Media")
                        .WithMany("Genres")
                        .HasForeignKey("IdMedia")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("videotheque.classes.PersonneMedia", b =>
                {
                    b.HasOne("videotheque.classes.Media", "Media")
                        .WithMany("Personnes")
                        .HasForeignKey("IdMedia")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("videotheque.classes.Personne", "Personne")
                        .WithMany("Medias")
                        .HasForeignKey("IdPersonne")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
