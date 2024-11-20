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
		private readonly ICompensationService _compensationService;

		/// <summary>
		/// Constructor for the compensation controller
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="compensationService"></param>
		public CompensationController(ILogger<CompensationController> logger, ICompensationService  compensationService)
		{
			_logger = logger;
			_compensationService = compensationService;
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

			_compensationService.Create(compensation);

			return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.EmployeeId, salary = compensation.Salary, effectiveDate = compensation.EffectiveDate }, compensation);
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

			var compensation = _compensationService.GetById(id);

			if (compensation == null)
				return NotFound();

			return Ok(compensation);
		}
	}
}
