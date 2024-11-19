using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class ComponsationRespository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly CompensationContext _compensationContext;
		private readonly ILogger<ICompensationRepository> _logger;

        public ComponsationRespository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext, CompensationContext compensationContext)
        {
            _employeeContext = employeeContext;
            _compensationContext = compensationContext;
            _logger = logger;
        }

        /// <summary>
        /// Get compensation for the given employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Compensation for the employee</returns>
        public Compensation GetById(string id)
        {
	        return _compensationContext.Compensations.SingleOrDefault(e => e.EmployeeId == id);
        }

        /// <summary>
        /// Add compensation given employee id
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns>Compensation for the employee</returns>
		public Compensation Add(Compensation compensation)
		{
            // Only add compensation if an employee with the given employee id exists because you can not add
            // compensation for a non-existent employee
			var employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == compensation.EmployeeId);
			if (employee != null)
			{
				compensation.CompensationId = Guid.NewGuid().ToString();
				_compensationContext.Add(compensation);
				return compensation;
			}

            return null;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
	}
}
