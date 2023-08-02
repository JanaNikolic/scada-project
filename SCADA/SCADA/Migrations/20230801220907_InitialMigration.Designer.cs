﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCADA.Data;

#nullable disable

namespace SCADA.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230801220907_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SCADA.Model.Alarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<double>("Threshold")
                        .HasColumnType("float");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("SCADA.Model.AlarmActivated", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlarmId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AlarmId");

                    b.ToTable("AlarmsActivated");
                });

            modelBuilder.Entity("SCADA.Model.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IOAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tag");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SCADA.Model.TagRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IOAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("TagRecords");
                });

            modelBuilder.Entity("SCADA.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SCADA.Model.AnalogInput", b =>
                {
                    b.HasBaseType("SCADA.Model.Tag");

                    b.Property<string>("Driver")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("HighLimit")
                        .HasColumnType("float");

                    b.Property<bool>("IsScanOn")
                        .HasColumnType("bit");

                    b.Property<double>("LowLimit")
                        .HasColumnType("float");

                    b.Property<double>("ScanTime")
                        .HasColumnType("float");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Tag", t =>
                        {
                            t.Property("Driver")
                                .HasColumnName("AnalogInput_Driver");

                            t.Property("HighLimit")
                                .HasColumnName("AnalogInput_HighLimit");

                            t.Property("IsScanOn")
                                .HasColumnName("AnalogInput_IsScanOn");

                            t.Property("LowLimit")
                                .HasColumnName("AnalogInput_LowLimit");

                            t.Property("ScanTime")
                                .HasColumnName("AnalogInput_ScanTime");

                            t.Property("Units")
                                .HasColumnName("AnalogInput_Units");
                        });

                    b.HasDiscriminator().HasValue("AnalogInput");
                });

            modelBuilder.Entity("SCADA.Model.AnalogOutput", b =>
                {
                    b.HasBaseType("SCADA.Model.Tag");

                    b.Property<double>("HighLimit")
                        .HasColumnType("float");

                    b.Property<double>("InitialValue")
                        .HasColumnType("float");

                    b.Property<double>("LowLimit")
                        .HasColumnType("float");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Tag", t =>
                        {
                            t.Property("InitialValue")
                                .HasColumnName("AnalogOutput_InitialValue");
                        });

                    b.HasDiscriminator().HasValue("AnalogOutput");
                });

            modelBuilder.Entity("SCADA.Model.DigitalInput", b =>
                {
                    b.HasBaseType("SCADA.Model.Tag");

                    b.Property<string>("Driver")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsScanOn")
                        .HasColumnType("bit");

                    b.Property<double>("ScanTime")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("DigitalInput");
                });

            modelBuilder.Entity("SCADA.Model.DigitalOutput", b =>
                {
                    b.HasBaseType("SCADA.Model.Tag");

                    b.Property<bool>("InitialValue")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("DigitalOutput");
                });

            modelBuilder.Entity("SCADA.Model.Alarm", b =>
                {
                    b.HasOne("SCADA.Model.AnalogInput", "AnalogInput")
                        .WithMany("Alarms")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnalogInput");
                });

            modelBuilder.Entity("SCADA.Model.AlarmActivated", b =>
                {
                    b.HasOne("SCADA.Model.Alarm", "Alarm")
                        .WithMany()
                        .HasForeignKey("AlarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alarm");
                });

            modelBuilder.Entity("SCADA.Model.TagRecord", b =>
                {
                    b.HasOne("SCADA.Model.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("SCADA.Model.AnalogInput", b =>
                {
                    b.Navigation("Alarms");
                });
#pragma warning restore 612, 618
        }
    }
}
