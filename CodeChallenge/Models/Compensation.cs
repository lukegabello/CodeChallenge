using System;
using System.Collections.Generic;

namespace CodeChallenge.Models
{
	/// <summary>
	/// Compensation information for an employee
	/// </summary>
	public class Compensation
	{
		/// <summary>
		/// Unique compensation Id
		/// </summary>
		public string CompensationId { get; set; }
		
		/// <summary>
		/// Employee Id associated with the compensation
		/// </summary>
		public string EmployeeId { get; set; }

		/// <summary>
		/// Employee comensation abount
		/// </summary>
		public float Salary { get; set; }

		/// <summary>
		/// Effective compensation date
		/// </summary>
		public DateTime EffectiveDate { get; set; }
	}
}
