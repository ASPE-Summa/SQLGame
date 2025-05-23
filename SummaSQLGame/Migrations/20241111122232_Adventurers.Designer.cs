﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SummaSQLGame.Databases;

#nullable disable

namespace SummaSQLGame.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241111122232_Adventurers")]
    partial class Adventurers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SummaSQLGame.Models.Adventurer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("Charisma")
                        .HasColumnType("INTEGER")
                        .HasColumnName("charisma");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("klasse");

                    b.Property<int>("Constitution")
                        .HasColumnType("INTEGER")
                        .HasColumnName("constitutie");

                    b.Property<int>("Dexterity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("behendigheid");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER")
                        .HasColumnName("intelligentie");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER")
                        .HasColumnName("niveau");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER")
                        .HasColumnName("kracht");

                    b.Property<int>("Wisdom")
                        .HasColumnType("INTEGER")
                        .HasColumnName("wijsheid");

                    b.HasKey("Id");

                    b.ToTable("avonturier");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("engelsenaam");

                    b.Property<string>("Genres")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("genres");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.Property<decimal?>("Score")
                        .HasColumnType("decimal(2,1)")
                        .HasColumnName("score");

                    b.Property<string>("Studios")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("studios");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("omschrijving");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("anime");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<double>("AlcoholPercentage")
                        .HasColumnType("REAL")
                        .HasColumnName("alcoholpercentage");

                    b.Property<string>("Brewery")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("brouwerij");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("land");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("soort");

                    b.HasKey("Id");

                    b.ToTable("bieren");
                });

            modelBuilder.Entity("SummaSQLGame.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("CountryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("land");

                    b.Property<bool>("IsCapital")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_hoofdstad");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.Property<int?>("Population")
                        .HasColumnType("INTEGER")
                        .HasColumnName("inwoners");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("stad");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.HasKey("Id");

                    b.ToTable("land");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Dog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("naam");

                    b.Property<string>("Race")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("ras");

                    b.HasKey("Id");

                    b.ToTable("honden");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Songs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Artists")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("artiesten");

                    b.Property<int>("Bpm")
                        .HasColumnType("INTEGER")
                        .HasColumnName("bpm");

                    b.Property<int>("ReleasedYear")
                        .HasColumnType("INTEGER")
                        .HasColumnName("verschijningsjaar");

                    b.Property<long?>("Streams")
                        .HasColumnType("INTEGER")
                        .HasColumnName("streams");

                    b.Property<string>("Track")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("titel");

                    b.HasKey("Id");

                    b.ToTable("spotify");
                });

            modelBuilder.Entity("SummaSQLGame.Models.City", b =>
                {
                    b.HasOne("SummaSQLGame.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("SummaSQLGame.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
