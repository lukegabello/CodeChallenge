using System;
using System.Collections.Generic;

namespace CodeChallenge.Models
{
	public class Compensation
	{
		public string CompensationId { get; set; }
		public string EmployeeId { get; set; }
		public float Salary { get; set; }
		public DateTime EffectiveDate { get; set; }
	}
}
