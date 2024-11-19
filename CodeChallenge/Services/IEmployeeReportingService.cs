using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
	public interface IEmployeeReportingService
	{
		/// <summary>
		/// Get the employee reporting structure for the given employee id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Reporting structure for the employee</returns>
		ReportingStructure GetEmployeeReportStructureById(String id);
	}
}
