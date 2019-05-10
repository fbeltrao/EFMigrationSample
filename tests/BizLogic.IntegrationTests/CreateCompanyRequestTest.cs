using BizLogic.Data;
using BizLogic.Handlers;
using BizLogic.Requests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class CreateCompanyRequestTest
    {
        private readonly string connectionString;

        public CreateCompanyRequestTest()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }
        
        CompanyDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<CompanyDbContext>();
            builder.UseCompanySqlServer(connectionString);

            return new CompanyDbContext(builder.Options);
        }

        [OneTimeSetUp]
        public void SetupDatabase()
        {
            Console.WriteLine($"Starting database setup for {connectionString}...");
            var builder = new DbContextOptionsBuilder<CompanyDbContext>();
            builder.UseCompanySqlServer(connectionString);

            var dbContext = new CompanyDbContext(builder.Options);
            Console.WriteLine($"Starting migration for {connectionString}...");
            dbContext.Database.Migrate();
            Console.WriteLine($"Migration for {connectionString} done!");
        }

        [Test]
        public async Task When_Creating_Should_Write_To_Db_And_Return()
        {            
            var request = new CreateCompanyRequest()
            {
                Name = "Company-Test-01"
            };

            var handler = new CreateCompanyRequestHandler(CreateDbContext());
            var response = await handler.Handle(request, default);
            Assert.NotNull(response);
            Assert.AreEqual("Company-Test-01", response.Name);
            Assert.AreNotEqual(Guid.Empty, response.CompanyId);

            // ensure it is available in db
            // create a new DbContext to really read from store
            var dbContext = CreateDbContext();
            var createdCompany = await dbContext.Companies.FindAsync(response.CompanyId);
            Assert.NotNull(createdCompany);
            Assert.AreEqual("Company-Test-01", createdCompany.Name);
            Assert.AreEqual(response.CompanyId, createdCompany.CompanyId);
        }
    }
}