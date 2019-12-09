﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TruckSystem.DAL.Context;

namespace TruckSystem.DAL.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20191209055226_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TruckSystem.Domain.Vehicles.Model.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("TB_TruckModel");
                });

            modelBuilder.Entity("TruckSystem.Domain.Vehicles.Model.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("IdModel");

                    b.Property<int>("ManufactureYear");

                    b.Property<int>("ModelYear");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("IdModel");

                    b.ToTable("TB_Truck");
                });

            modelBuilder.Entity("TruckSystem.Domain.Vehicles.Model.Truck", b =>
                {
                    b.HasOne("TruckSystem.Domain.Vehicles.Model.Model", "Model")
                        .WithMany()
                        .HasForeignKey("IdModel")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}