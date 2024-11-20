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
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
			return _employeeContext.Employees.Include(e => e.DirectReports).FirstOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        /// <summary>
        /// Get compensation for the given employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Compensation for the employee</returns>
        public Compensation GetCompensationById(string id)
        {
	        return _employeeContext.Compensations.SingleOrDefault(e => e.EmployeeId == id);
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
				var  added =  _employeeContext.Add(compensation);

                // Verify added
				if (added.State == EntityState.Added)
				{
					return compensation;
				}
	        }

	        return null;
        }
	}
}
