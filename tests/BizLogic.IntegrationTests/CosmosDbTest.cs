using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.IntegrationTests
{
    [TestFixture]
    public class CosmosDbTest
    {
        const string DatabaseId = "CompaniesIntegrationTest";
        const string CompaniesCollectionId = "Companies";

        private DocumentClient documentClient;

        [OneTimeSetUp]
        public async Task SetupDatabase()
        {
            var endpoint = Environment.GetEnvironmentVariable("CosmosDbEmulatorEndpoint");
            Console.WriteLine($"Checking Configuration: CosmosDbEmulatorEndpoint='{endpoint}'");
            if (string.IsNullOrWhiteSpace(endpoint))
                endpoint = ConfigurationManager.AppSettings["CosmosDbEndpoint"];

            // var authKey = Environment.GetEnvironmentVariable("CosmosDbEmulator.Authkey");
            // if (string.IsNullOrWhiteSpace(authKey))
            var authKey = ConfigurationManager.AppSettings["CosmosDbAuthKey"];

            try
            {

                this.documentClient = new DocumentClient(new Uri(endpoint), authKey, new ConnectionPolicy()
                {
                    ConnectionProtocol = Protocol.Tcp,
                    ConnectionMode = ConnectionMode.Direct,
                });

                await documentClient.CreateDatabaseIfNotExistsAsync(new Database()
                {
                    Id = DatabaseId,
                });

                await documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection
                {
                    Id = CompaniesCollectionId,
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to setup CosmosDb for tests. Endpoint: '{endpoint}', AuthKey: '{authKey}'", ex);
            }
        }

        [Test]
        public async Task Create_And_Read_Document()
        {
            var newDocumentId = Guid.NewGuid().ToString();
            var newDocument = new { id = newDocumentId, Name = "TestIntegration" };
            var response = await this.documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CompaniesCollectionId), newDocument);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var doc = await this.documentClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CompaniesCollectionId, newDocumentId));
            Assert.AreEqual(newDocument.Name, ((dynamic)(doc.Resource)).Name);
        }
    }
}
