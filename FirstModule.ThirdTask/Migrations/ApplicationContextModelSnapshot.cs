﻿// <auto-generated />
using System;
using FirstModule.ThirdTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FirstModule.ThirdTask.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FirstModule.ThirdTask.Entities.GameEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("HomeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScoreId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitorsId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("FirstModule.ThirdTask.Entities.ScoreEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("HomeScore")
                        .HasColumnType("integer");

                    b.Property<int?>("VisitorsScore")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("FirstModule.ThirdTask.Entities.TeamsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
