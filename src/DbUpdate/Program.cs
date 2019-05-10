using BizLogic.Data;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DbUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = Parser.Default.ParseArguments<DbUpdateOptions>(args)
                .WithParsed<DbUpdateOptions>(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(DbUpdateOptions opts)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>()
                   .UseCompanySqlServer(opts.ConnectionString);

                using (var sc = new CompanyDbContext(optionsBuilder.Options))
                {
                    var pendingMigrations = sc.Database.GetPendingMigrations();

                    if (pendingMigrations.Any())
                    {
                        Console.WriteLine("Found following pending migrations");
                        var previousColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        foreach (var migration in pendingMigrations)
                        {
                            Console.WriteLine($"  {migration}");
                        }
                        Console.ForegroundColor = previousColor;

                        Console.WriteLine("Starting migration...");
                        sc.Database.Migrate();
                        Console.WriteLine("Migration completed.");
                    }
                    else
                    {
                        Console.WriteLine("No migration required.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.ToString()}");
                Environment.ExitCode = 1;
                return;
            }
        }
    }
}
