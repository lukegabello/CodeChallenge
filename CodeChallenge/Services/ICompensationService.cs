using System;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
	/// <summary>
	/// Interface class for the compensation service
	/// </summary>
	public interface ICompensationService
	{
		/// <summary>
		/// Gets the compensation for the given employee Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Compensation for the employee</returns>
		Compensation GetById(String id);

		/// <summary>
		/// Creates compensation for an existing employee
		/// </summary>
		/// <param name="compensation"></param>
		/// <returns>Compensation for the existing employee</returns>
		Compensation Create(Compensation compensation);
	}
}
