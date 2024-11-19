using System;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
	[ApiController]
	[Route("api/employeeReporting")]
	public class EmployeeReportingController : Controller
	{
		private readonly ILogger _logger;
		private readonly IEmployeeReportingService _employeeReportingServiceService;

		public EmployeeReportingController(ILogger<EmployeeReportingController> logger, IEmployeeReportingService employeeReportingService)
		{
			_logger = logger;
			_employeeReportingServiceService = employeeReportingService;
		}

		[HttpGet("{id}", Name = "getEmployeeReportStructureById")]
		public IActionResult GetEmployeeReportStructureById(String id)
		{
			_logger.LogDebug($"Received employee report structure get request for '{id}'");

			// Get the reporting structure for the associated employee id
			var reportingStructure = _employeeReportingServiceService.GetEmployeeReportStructureById(id);

			// Verify that the reporting structure has a valid employee
			if (reportingStructure.Employee == null)
				return NotFound();

			return Ok(reportingStructure);
		}
	}
}
