using BizLogic.Model;
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

        public DbSet<Employee> Employees { get; set; }

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
            builder.HasMany(c => c.Employees);
            builder.HasIndex(c => c.Name).HasName("ix_company_name");
            builder.Property<string>(c => c.Description)
                .IsRequired(true)
                .HasDefaultValue("")
                .HasMaxLength(200);

            builder.Property(c => c.City)
                .IsRequired()
                .HasDefaultValue("")
                .HasMaxLength(100);
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
