﻿// <auto-generated />
using System;
using BizLogic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbUpdate.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20190514063957_0002CompanyDescription")]
    partial class _0002CompanyDescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BizLogic.Model.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasDefaultValue("");

                    b.Property<string>("Name");

                    b.HasKey("CompanyId");

                    b.HasIndex("Name")
                        .HasName("ix_company_name");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("BizLogic.Model.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CompanyId");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BizLogic.Model.Employee", b =>
                {
                    b.HasOne("BizLogic.Model.Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.OwnsOne("BizLogic.Model.Address", "HomeAddress", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId");

                            b1.Property<string>("City");

                            b1.Property<string>("PostalCode");

                            b1.Property<string>("Street");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("EmployeeAddress");

                            b1.HasOne("BizLogic.Model.Employee")
                                .WithOne("HomeAddress")
                                .HasForeignKey("BizLogic.Model.Address", "EmployeeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
