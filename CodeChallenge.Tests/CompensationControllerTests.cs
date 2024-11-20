
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

        [TestMethod]
        public void GetEmployeeCompensationById_Returns_Ok()
        {
	        // Arrange
	        // Give John Lennon some cash
	        var compensationJohn = new Compensation()
	        {
		        EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
		        Salary = 130000.50F,
		        EffectiveDate = DateTime.Now
	        };

	        var compensationPaul = new Compensation()
	        {
		        EmployeeId = "b7839309-3348-463b-a7e3-5de1c168beb3",
		        Salary = 430050.99F,
		        EffectiveDate = DateTime.Now
	        };

			var requestContentJohn = new JsonSerialization().ToJson(compensationJohn);
			var requestContentPaul = new JsonSerialization().ToJson(compensationPaul);

			// Execute
			var postRequestTaskJohn = _httpClient.PostAsync("api/compensation",
		        new StringContent(requestContentJohn, Encoding.UTF8, "application/json"));
	        var responseCreateJohn = postRequestTaskJohn.Result;

	        var postRequestTaskPaul = _httpClient.PostAsync("api/compensation",
		        new StringContent(requestContentPaul, Encoding.UTF8, "application/json"));
	        var responseCreatePaul = postRequestTaskPaul.Result;

			// Execute
			var getRequestTaskJohn = _httpClient.GetAsync($"api/compensation/{compensationJohn.EmployeeId}");
	        var responseGetJohn = getRequestTaskJohn.Result;

	        var getRequestTaskPaul = _httpClient.GetAsync($"api/compensation/{compensationPaul.EmployeeId}");
	        var responseGetPaul = getRequestTaskPaul.Result;

			// Assert
			Assert.AreEqual(HttpStatusCode.OK, responseGetJohn.StatusCode);
	        var compensationReturnedJohn = responseGetJohn.DeserializeContent<Compensation>();
	        Assert.AreEqual(compensationJohn.EmployeeId, compensationReturnedJohn.EmployeeId);
	        Assert.AreEqual(compensationJohn.Salary, compensationReturnedJohn.Salary);
	        Assert.AreEqual(compensationJohn.EffectiveDate, compensationReturnedJohn.EffectiveDate);

	        Assert.AreEqual(HttpStatusCode.OK, responseGetPaul.StatusCode);
	        var compensationReturnedPaul = responseGetPaul.DeserializeContent<Compensation>();
	        Assert.AreEqual(compensationPaul.EmployeeId, compensationReturnedPaul.EmployeeId);
	        Assert.AreEqual(compensationPaul.Salary, compensationReturnedPaul.Salary);
	        Assert.AreEqual(compensationPaul.EffectiveDate, compensationReturnedPaul.EffectiveDate);
        }

		[ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }
    }
}
