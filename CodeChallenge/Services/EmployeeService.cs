using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ILogger<EmployeeService> _logger;

		public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
			_logger = logger;
		}

		public Employee Create(Employee employee)
		{
			if (employee != null)
			{
				_employeeRepository.Add(employee);
				_employeeRepository.SaveAsync().Wait();
			}

			return employee;
		}

		public Employee GetById(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				return _employeeRepository.GetById(id);
			}

			return null;
		}

		public Employee Replace(Employee originalEmployee, Employee newEmployee)
		{
			if (originalEmployee != null)
			{
				_employeeRepository.Remove(originalEmployee);
				if (newEmployee != null)
				{
					// ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
					_employeeRepository.SaveAsync().Wait();

					_employeeRepository.Add(newEmployee);
					// overwrite the new id with previous employee id
					newEmployee.EmployeeId = originalEmployee.EmployeeId;
				}

				_employeeRepository.SaveAsync().Wait();
			}

			return newEmployee;
		}

		/// <summary>
		/// Gets the compensation information for an employee given their employee Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Compensation GetCompensationById(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				return _employeeRepository.GetCompensationById(id);
			}

			return null;
		}

		/// <summary>
		/// Creates a compensation entry for an existing employee
		/// </summary>
		/// <param name="compensation"></param>
		/// <returns></returns>
		public Compensation Create(Compensation compensation)
		{
			if (!string.IsNullOrEmpty(compensation.EmployeeId))
			{
				// Verify there is an employee to create compensation for
				// because a non-existent employee cannot have a compensation
				var employee = _employeeRepository.GetById(compensation.EmployeeId);
				if (employee != null)
				{
					_employeeRepository.Add(compensation);
					_employeeRepository.SaveAsync().Wait();

					return compensation;
				}
			}

			return null;
		}
	}
}
