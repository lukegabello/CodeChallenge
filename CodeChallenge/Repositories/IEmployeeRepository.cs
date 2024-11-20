using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
		Employee Remove(Employee employee);

		/// <summary>
		/// Gets the compensation for the given employee Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Compensation GetCompensationById(string id);

		/// <summary>
		/// Adds compensation for an existing employee
		/// </summary>
		/// <param name="compensation"></param>
		/// <returns></returns>
		Compensation Add(Compensation compensation);

		Task SaveAsync();
    }
}