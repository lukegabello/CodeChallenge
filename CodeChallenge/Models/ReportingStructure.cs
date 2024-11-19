namespace CodeChallenge.Models
{
	/// <summary>
	/// Reporting structure for a given employee
	/// </summary>
	public class ReportingStructure
	{
		/// <summary>
		/// Employee reporting structure is for
		/// </summary>
		public Employee Employee;

		/// <summary>
		/// Total number of reports under to the Employee.
		/// Ex:
		///
		///               John Lennon
		///	              /       \
		///      Paul McCartney   Ringo Starr
		///                        /      \
		///                  Pete Best   George Harrison
		/// John Lennon would have 4 reports
		/// </summary>
		public int NumberOfReports;
	}
}
