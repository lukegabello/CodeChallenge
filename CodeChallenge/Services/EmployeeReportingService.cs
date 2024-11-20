using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{   
	/// <summary>
	/// Service Class providing employee reporting information
	/// </summary>
	public class EmployeeReportingService : IEmployeeReportingService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ILogger<EmployeeReportingService> _logger;

		public EmployeeReportingService(ILogger<EmployeeReportingService> logger, IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
			_logger = logger;
		}

		/// <summary>
		/// Get the reporting structure for the requested employee id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Filled out reporting structure for the employee</returns>
		public ReportingStructure GetEmployeeReportStructureById(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				ReportingStructure reportingStructure = new();

				reportingStructure.Employee = _employeeRepository.GetById(id);

				// Verify that the employee exists
				if (reportingStructure.Employee != null)
				{
					// determine the number of direct reports for the employee
					reportingStructure.NumberOfReports = ComputeNumberEmployeeReports(id);
				}

				return reportingStructure;

			}

			return null;
		}

		/// <summary>
		///	Recursive method to determine the number of employees under the given employee ID
		/// </summary>
		/// <param name="employeeId"></param>
		/// <param name="numberOfDirectReports"></param>
		/// <returns>Number of direct reports for the employees</returns>
		private int ComputeNumberEmployeeReports(string employeeId, int numberOfDirectReports = 0)
		{
			var employee = _employeeRepository.GetById(employeeId);

			if (employee != null && employee.DirectReports != null)
			{
				for (var index = 0; index < employee.DirectReports.Count; index++)
				{
					numberOfDirectReports += employee.DirectReports.Count;
					return ComputeNumberEmployeeReports(employee.DirectReports[index].EmployeeId, numberOfDirectReports);
				}
			}

			return numberOfDirectReports;
		}
	}
}
