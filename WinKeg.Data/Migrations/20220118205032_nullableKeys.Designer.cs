﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WinKeg.Data;

#nullable disable

namespace WinKeg.Data.Migrations
{
    [DbContext(typeof(WinKegContext))]
    [Migration("20220118205032_nullableKeys")]
    partial class nullableKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("WinKeg.Data.Models.Beverage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ABV")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("IBU")
                        .HasColumnType("REAL");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRestricted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Beverages");
                });

            modelBuilder.Entity("WinKeg.Data.Models.BeverageImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("ImageBlob")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BeverageImages");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Hardware", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("KegId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("KegeratorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("KegId");

                    b.HasIndex("KegeratorId");

                    b.ToTable("Hardware");
                });

            modelBuilder.Entity("WinKeg.Data.Models.HistoryEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("KegHistoryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("KegHistoryID");

                    b.HasIndex("UserID");

                    b.ToTable("HistoryEvents");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Keg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BeverageId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("CurrentVolume")
                        .HasColumnType("REAL");

                    b.Property<int?>("FlowCalibration")
                        .HasColumnType("INTEGER");

                    b.Property<double>("InitialVolume")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BeverageId");

                    b.ToTable("Kegs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentVolume = 0.0,
                            InitialVolume = 0.0,
                            Name = "Left Keg"
                        },
                        new
                        {
                            Id = 2,
                            CurrentVolume = 0.0,
                            InitialVolume = 0.0,
                            Name = "Right Keg"
                        });
                });

            modelBuilder.Entity("WinKeg.Data.Models.Kegerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Kegerators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Highlands Ranch, CO",
                            Name = "The Ziehnert's Beverage Fountain",
                            Owner = "Nathan Ziehnert"
                        });
                });

            modelBuilder.Entity("WinKeg.Data.Models.KegeratorEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("KegeratorEvents");
                });

            modelBuilder.Entity("WinKeg.Data.Models.KegHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BeverageID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("KegID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BeverageID");

                    b.HasIndex("KegID")
                        .IsUnique();

                    b.ToTable("KegHistories");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("WinKeg.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EncryptedPasscode")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBeverageRestricted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PCSalt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Beverage", b =>
                {
                    b.HasOne("WinKeg.Data.Models.BeverageImage", "Image")
                        .WithMany("Beverages")
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Hardware", b =>
                {
                    b.HasOne("WinKeg.Data.Models.Keg", "Keg")
                        .WithMany("KegHardware")
                        .HasForeignKey("KegId");

                    b.HasOne("WinKeg.Data.Models.Kegerator", "Kegerator")
                        .WithMany("KegeratorHardware")
                        .HasForeignKey("KegeratorId");

                    b.Navigation("Keg");

                    b.Navigation("Kegerator");
                });

            modelBuilder.Entity("WinKeg.Data.Models.HistoryEvent", b =>
                {
                    b.HasOne("WinKeg.Data.Models.KegHistory", "KegHistory")
                        .WithMany("HistoryEvents")
                        .HasForeignKey("KegHistoryID");

                    b.HasOne("WinKeg.Data.Models.User", "User")
                        .WithMany("HistoryEvents")
                        .HasForeignKey("UserID");

                    b.Navigation("KegHistory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Keg", b =>
                {
                    b.HasOne("WinKeg.Data.Models.Beverage", "Beverage")
                        .WithMany()
                        .HasForeignKey("BeverageId");

                    b.Navigation("Beverage");
                });

            modelBuilder.Entity("WinKeg.Data.Models.KegHistory", b =>
                {
                    b.HasOne("WinKeg.Data.Models.Beverage", "Beverage")
                        .WithMany("KegHistories")
                        .HasForeignKey("BeverageID");

                    b.HasOne("WinKeg.Data.Models.Keg", "Keg")
                        .WithOne("CurrentHistory")
                        .HasForeignKey("WinKeg.Data.Models.KegHistory", "KegID");

                    b.Navigation("Beverage");

                    b.Navigation("Keg");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Beverage", b =>
                {
                    b.Navigation("KegHistories");
                });

            modelBuilder.Entity("WinKeg.Data.Models.BeverageImage", b =>
                {
                    b.Navigation("Beverages");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Keg", b =>
                {
                    b.Navigation("CurrentHistory");

                    b.Navigation("KegHardware");
                });

            modelBuilder.Entity("WinKeg.Data.Models.Kegerator", b =>
                {
                    b.Navigation("KegeratorHardware");
                });

            modelBuilder.Entity("WinKeg.Data.Models.KegHistory", b =>
                {
                    b.Navigation("HistoryEvents");
                });

            modelBuilder.Entity("WinKeg.Data.Models.User", b =>
                {
                    b.Navigation("HistoryEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
