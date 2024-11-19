using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
	public class CompensationService : ICompensationService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ICompensationRepository _compensationRepository;
		private readonly ILogger<CompensationService> _logger;

		public CompensationService(ILogger<CompensationService> logger, IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
			_logger = logger;
		}
		public Compensation Create(Compensation compensation)
		{
			if (!string.IsNullOrEmpty(compensation.EmployeeId))
			{
				// Verify there is an employee to create compensation for
				// because a non-existent employee cannot have a compensation
				var employee = _employeeRepository.GetById(compensation.EmployeeId);
				if (employee != null)
				{
					_compensationRepository.Add(compensation);
					_compensationRepository.SaveAsync().Wait();

					return compensation;
				}
			}

			return null;
		}

		public Compensation GetById(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				return _compensationRepository.GetById(id);
			}

			return null;
		}
	}
}
