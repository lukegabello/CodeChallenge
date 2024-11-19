﻿using System;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
	public class CompensationController : Controller
	{
		private readonly ILogger _logger;
		private readonly ICompensationService _compensationService;

		public CompensationController(ILogger<CompensationController> logger, ICompensationService  compensationService)
		{
			_logger = logger;
			_compensationService = compensationService;
		}

		[HttpPost]
		public IActionResult CreateCompensation([FromBody] Compensation compensation)
		{
			_logger.LogDebug($"Received compensation create request for employee id '{compensation.EmployeeId}'");

			_compensationService.Create(compensation);

			return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.EmployeeId, salary = compensation.Salary, effectiveDate = compensation.EffectiveDate }, compensation);
		}

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