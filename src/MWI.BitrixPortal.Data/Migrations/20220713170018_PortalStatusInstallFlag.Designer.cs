﻿// <auto-generated />
using System;
using MWI.BitrixPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MWI.BitrixPortal.Data.Migrations
{
    [DbContext(typeof(BitrixPortalContext))]
    [Migration("20220713170018_PortalStatusInstallFlag")]
    partial class PortalStatusInstallFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MWI.BitrixPortal.Domain.Entities.Portal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AdminUserName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ApplicationToken")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("BitrixAccountStatus")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("InstallStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<string>("PortalStatus")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("varchar(40)");

                    b.Property<bool>("WizardMode")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Portals", (string)null);
                });

            modelBuilder.Entity("MWI.BitrixPortal.Domain.Entities.Portal", b =>
                {
                    b.OwnsOne("MWI.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PortalId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email");

                            b1.HasKey("PortalId");

                            b1.ToTable("Portals");

                            b1.WithOwner()
                                .HasForeignKey("PortalId");
                        });

                    b.Navigation("Email");
                });
#pragma warning restore 612, 618
        }
    }
}
