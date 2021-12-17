﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Musical_Instrument_Store.Data;

namespace Musical_Instrument_Store.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20211208210018_EditOrder")]
    partial class EditOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.CartLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("musicalInstrumentId")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("musicalInstrumentId");

                    b.ToTable("cartLines");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.MICategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("categoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("mICategories");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.MusicalInstrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MICategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("available")
                        .HasColumnType("bit");

                    b.Property<string>("descMI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nameMI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MICategoryId");

                    b.ToTable("MusicalInstruments");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("addressClient")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("emailClient")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("nameClient")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("phoneClient")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("statusOrderId")
                        .HasColumnType("int");

                    b.Property<string>("surnameClient")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("statusOrderId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("musicalInstrumentId")
                        .HasColumnType("int");

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<long>("price")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("musicalInstrumentId");

                    b.HasIndex("orderId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.StatusOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("statusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StatusOrders");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.CartLine", b =>
                {
                    b.HasOne("Musical_Instrument_Store.Data.Models.MusicalInstrument", "musicalInstrument")
                        .WithMany()
                        .HasForeignKey("musicalInstrumentId");

                    b.Navigation("musicalInstrument");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.MusicalInstrument", b =>
                {
                    b.HasOne("Musical_Instrument_Store.Data.Models.MICategory", "MICategory")
                        .WithMany("musicalInstruments")
                        .HasForeignKey("MICategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MICategory");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.Order", b =>
                {
                    b.HasOne("Musical_Instrument_Store.Data.Models.StatusOrder", "StatusOrder")
                        .WithMany("orders")
                        .HasForeignKey("statusOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusOrder");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("Musical_Instrument_Store.Data.Models.MusicalInstrument", "musicalInstrument")
                        .WithMany()
                        .HasForeignKey("musicalInstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Musical_Instrument_Store.Data.Models.Order", "order")
                        .WithMany("orderDetails")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("musicalInstrument");

                    b.Navigation("order");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.MICategory", b =>
                {
                    b.Navigation("musicalInstruments");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.Order", b =>
                {
                    b.Navigation("orderDetails");
                });

            modelBuilder.Entity("Musical_Instrument_Store.Data.Models.StatusOrder", b =>
                {
                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
