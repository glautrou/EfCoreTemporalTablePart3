﻿// <auto-generated />
using System;
using EfCoreTemporalTablePart3.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EfCoreTemporalTablePart3.Migrations
{
    [DbContext(typeof(DemoTemporelleContext))]
    partial class DemoTemporelleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EfCoreTemporalTablePart3.Model.Employe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EntrepriseId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("PeriodEnd")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodEnd");

                    b.Property<DateTime>("PeriodStart")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("PeriodStart");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("EntrepriseId");

                    b.ToTable("Employe", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb
                                .HasPeriodStart("PeriodStart")
                                .HasColumnName("PeriodStart");
                            ttb
                                .HasPeriodEnd("PeriodEnd")
                                .HasColumnName("PeriodEnd");
                        }
                    ));
                });

            modelBuilder.Entity("EfCoreTemporalTablePart3.Model.Entreprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("SysEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SysStartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValideAu")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValideAu");

                    b.Property<DateTime>("ValideDu")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("ValideDu");

                    b.HasKey("Id");

                    b.ToTable("Entreprise", (string)null);

                    b.ToTable(tb => tb.IsTemporal(ttb =>
                        {
                            ttb.UseHistoryTable("EntrepriseHistorique");
                            ttb
                                .HasPeriodStart("ValideDu")
                                .HasColumnName("ValideDu");
                            ttb
                                .HasPeriodEnd("ValideAu")
                                .HasColumnName("ValideAu");
                        }
                    ));
                });

            modelBuilder.Entity("EfCoreTemporalTablePart3.Model.Employe", b =>
                {
                    b.HasOne("EfCoreTemporalTablePart3.Model.Entreprise", "Entreprise")
                        .WithMany("Employe")
                        .HasForeignKey("EntrepriseId")
                        .IsRequired()
                        .HasConstraintName("FK_Employe_Entreprise");

                    b.Navigation("Entreprise");
                });

            modelBuilder.Entity("EfCoreTemporalTablePart3.Model.Entreprise", b =>
                {
                    b.Navigation("Employe");
                });
#pragma warning restore 612, 618
        }
    }
}
