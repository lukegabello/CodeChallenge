using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
	/// <summary>
	/// Interface for the Compensation Repository
	/// </summary>
    public interface ICompensationRepository
    {
		/// <summary>
		/// Gets the compensation for the given employee Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Compensation GetById(string id);

		/// <summary>
		/// Addes compensation for an existing employee
		/// </summary>
		/// <param name="compensation"></param>
		/// <returns></returns>
		Compensation Add(Compensation compensation);
		Task SaveAsync();
    }
}