
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [TestMethod]
        public void CreateEmployeeCompensation_Returns_Created()
        {
	        // Arrange
			// Give John Lennon some cash
			var compensation = new Compensation()
	        {
				EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
				Salary = 130000.50F,
				EffectiveDate = DateTime.Now
	        };

	        var requestContent = new JsonSerialization().ToJson(compensation);

	        // Execute
	        var postRequestTask = _httpClient.PostAsync("api/compensation",
		        new StringContent(requestContent, Encoding.UTF8, "application/json"));
	        var response = postRequestTask.Result;

	        // Assert
	        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

	        var newCompensation = response.DeserializeContent<Compensation>();
	        Assert.IsNotNull(newCompensation.EmployeeId);
        }

		[ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
    }
}
