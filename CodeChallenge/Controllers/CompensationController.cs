using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
	[ApiController]
	[Route("api/compensation")]
	public class CompensationController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly IEmployeeService _employeeService;

		/// <summary>
		/// Constructor for the compensation controller
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="compensationService"></param>
		public CompensationController(ILogger<CompensationController> logger, IEmployeeService employeeService)
		{
			_logger = logger;
			_employeeService = employeeService;
		}

		/// <summary>
		/// Creates a compensation entry for an existing employee
		/// </summary>
		/// <param name="compensation"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult CreateCompensation([FromBody] Compensation compensation)
		{
			_logger.LogDebug($"Received compensation create request for employee id '{compensation.EmployeeId}'");

			_employeeService.Create(compensation);

			//return CreatedAtRoute("getCompensationByEmployeeId", new { compensationId = compensation.CompensationId, employeeId = compensation.EmployeeId, salary = compensation.Salary, effectiveDate = compensation.EffectiveDate }, compensation);

			return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.EmployeeId }, compensation);
		}

		/// <summary>
		/// Get the compensation for a given employee id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}", Name = "getCompensationByEmployeeId")]
		public IActionResult GetCompensationByEmployeeId(String id)
		{
			_logger.LogDebug($"Received compensation get request for '{id}'");

			var compensation = _employeeService.GetCompensationById(id);

			if (compensation == null)
				return NotFound();

			return Ok(compensation);
		}
	}
}
