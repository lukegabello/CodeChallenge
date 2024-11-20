
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
	public class EmployeeReportingControllerTests
	{
		private static HttpClient _httpClient;
		private static TestServer _testServer;

		[TestClass]
		public class EmployeeCompensationControllerTests
		{
			private static HttpClient _httpClient;
			private static TestServer _testServer;

			[ClassInitialize]
			// Attribute ClassInitialize requires this signature
			[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter",
				Justification = "<Pending>")]
			public static void InitializeClass(TestContext context)
			{
				_testServer = new TestServer();
				_httpClient = _testServer.NewClient();
			}

			[TestMethod]
			public void GetEmployeeReportingStructureById_Returns_Ok()
			{
				// Arrange
				var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
				var expectedFirstName = "John";
				var expectedLastName = "Lennon";

				// Execute
				var getRequestTask = _httpClient.GetAsync($"api/employeeReporting/{employeeId}");
				var response = getRequestTask.Result;

				// Assert
				Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
				var reportingStructure = response.DeserializeContent<ReportingStructure>();
				Assert.AreEqual(4, reportingStructure.NumberOfReports);
			}

			[ClassCleanup]
			public static void CleanUpTest()
			{
				_httpClient.Dispose();
				_testServer.Dispose();
			}
		}
	}
}
