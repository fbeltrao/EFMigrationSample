using BizLogic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DbUpdate
{
    /// <summary>
    /// Add-Migration 0001Initial -StartupProject DbUpdate -Project DbUpdate -Context CompanyDbContext
    /// dotnet ef migrations add 0001Initial --context CompanyDbContext
    /// Update-Database -StartupProject DbUpdate -Context CompanyDbContext
    /// 
    /// Tool to manage local sql: sqllocaldb info
    /// 
    /// Db connection string: (localdb)\mssqllocaldb
    /// </summary>
    public class CompanyDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=Companies;Trusted_Connection=True;";
            if (args?.Length > 0)
            {
                connectionString = args[1];
            }
            var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
            optionsBuilder.UseCompanySqlServer(connectionString);

            return new CompanyDbContext(optionsBuilder.Options);
        }
    }
}