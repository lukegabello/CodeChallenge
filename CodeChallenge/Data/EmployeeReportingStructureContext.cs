using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class EmployeeReportingStructureContext : DbContext
    {
        public EmployeeReportingStructureContext(DbContextOptions<EmployeeReportingStructureContext> options) : base(options)
        {

        }

        public DbSet<ReportingStructure> ReportingStructures { get; set; }
    }
}
