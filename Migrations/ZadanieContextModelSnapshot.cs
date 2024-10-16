﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoListApp.Models;

#nullable disable

namespace ToDoListApp.Migrations
{
    [DbContext(typeof(ZadanieContext))]
    partial class ZadanieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToDoListApp.Models.Kategoria", b =>
                {
                    b.Property<string>("IDKategoria")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDKategoria");

                    b.ToTable("Kategorie");

                    b.HasData(
                        new
                        {
                            IDKategoria = "pro",
                            Nazwa = "Projekt"
                        },
                        new
                        {
                            IDKategoria = "dom",
                            Nazwa = "Dom"
                        },
                        new
                        {
                            IDKategoria = "spo",
                            Nazwa = "Sport"
                        },
                        new
                        {
                            IDKategoria = "pra",
                            Nazwa = "Praca"
                        });
                });

            modelBuilder.Entity("ToDoListApp.Models.Status", b =>
                {
                    b.Property<string>("IDStatus")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDStatus");

                    b.ToTable("Statusy");

                    b.HasData(
                        new
                        {
                            IDStatus = "zam",
                            Nazwa = "Zrobione"
                        },
                        new
                        {
                            IDStatus = "otw",
                            Nazwa = "W trakcie"
                        });
                });

            modelBuilder.Entity("ToDoListApp.Models.Zadanie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("KategoriaIDKategoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusIDStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("KategoriaIDKategoria");

                    b.HasIndex("StatusIDStatus");

                    b.ToTable("Zadania");
                });

            modelBuilder.Entity("ToDoListApp.Models.Zadanie", b =>
                {
                    b.HasOne("ToDoListApp.Models.Kategoria", "Kategoria")
                        .WithMany()
                        .HasForeignKey("KategoriaIDKategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoListApp.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusIDStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategoria");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
