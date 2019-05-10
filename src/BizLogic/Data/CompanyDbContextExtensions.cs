using Microsoft.EntityFrameworkCore;

namespace BizLogic.Data
{
    public static class CompaniesDbContextExtensions
    {
        private const string MigrationHistoryTableName = "CompanyMigrationHistory";
        private const string MigrationAssemblyName = "DbUpdate";

        public static DbContextOptionsBuilder<CompanyDbContext> UseCompanySqlServer(this DbContextOptionsBuilder<CompanyDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, o =>
            {
                o.MigrationsHistoryTable(MigrationHistoryTableName);
                o.MigrationsAssembly(MigrationAssemblyName);
            });

            return builder;
        }

        public static DbContextOptionsBuilder UseCompanySqlServer(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString, o =>
            {
                o.MigrationsHistoryTable(MigrationHistoryTableName);
                o.MigrationsAssembly(MigrationAssemblyName);
            });

            return builder;
        }
    }
}
