﻿using BizLogic.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizLogic.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Vehicles { get; set; }

        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfigurator());
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfigurator());

            base.OnModelCreating(modelBuilder);
        }
    }

    internal class CompanyConfigurator : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(f => f.Employees);
            builder.HasIndex(f => f.Name).HasName("ix_company_name");
        }
    }

    internal class EmployeeConfigurator : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsOne(v => v.HomeAddress).ToTable("EmployeeAddress");
        }
    }
}
