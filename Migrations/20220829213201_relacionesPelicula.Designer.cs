﻿// <auto-generated />
using System;
using Cine.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cine.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220829213201_relacionesPelicula")]
    partial class relacionesPelicula
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cine.Domain.Objects.OActor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaNacimientoA")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreA")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("TActor");
                });

            modelBuilder.Entity("Cine.Domain.Objects.OGenero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("TGenero");
                });

            modelBuilder.Entity("Cine.Domain.Objects.OPelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Encine")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstrenoP")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreP")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TPelicula");
                });

            modelBuilder.Entity("Cine.Domain.Objects.OReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("TReview");
                });

            modelBuilder.Entity("Cine.Domain.Objects.OSala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("TSala");
                });

            modelBuilder.Entity("Cine.Domain.Objects.OUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("TUsuario");
                });

            modelBuilder.Entity("Cine.Domain.ObjectsR.PeliculaActor", b =>
                {
                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaID")
                        .HasColumnType("int");

                    b.Property<int?>("ActorEId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculaEId")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorID", "PeliculaID");

                    b.HasIndex("ActorEId");

                    b.HasIndex("PeliculaEId");

                    b.ToTable("TPeliculaActor");
                });

            modelBuilder.Entity("Cine.Domain.ObjectsR.PeliculaGenero", b =>
                {
                    b.Property<int>("GeneroID")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaID")
                        .HasColumnType("int");

                    b.Property<int?>("GeneroEId")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculaEId")
                        .HasColumnType("int");

                    b.HasKey("GeneroID", "PeliculaID");

                    b.HasIndex("GeneroEId");

                    b.HasIndex("PeliculaEId");

                    b.ToTable("TPeliculaGenero");
                });

            modelBuilder.Entity("Cine.Domain.ObjectsR.PeliculaActor", b =>
                {
                    b.HasOne("Cine.Domain.Objects.OActor", "ActorE")
                        .WithMany()
                        .HasForeignKey("ActorEId");

                    b.HasOne("Cine.Domain.Objects.OPelicula", "PeliculaE")
                        .WithMany()
                        .HasForeignKey("PeliculaEId");

                    b.Navigation("ActorE");

                    b.Navigation("PeliculaE");
                });

            modelBuilder.Entity("Cine.Domain.ObjectsR.PeliculaGenero", b =>
                {
                    b.HasOne("Cine.Domain.Objects.OGenero", "GeneroE")
                        .WithMany()
                        .HasForeignKey("GeneroEId");

                    b.HasOne("Cine.Domain.Objects.OPelicula", "PeliculaE")
                        .WithMany()
                        .HasForeignKey("PeliculaEId");

                    b.Navigation("GeneroE");

                    b.Navigation("PeliculaE");
                });
#pragma warning restore 612, 618
        }
    }
}
