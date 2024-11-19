using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class CompensationContext : DbContext
    {
        /// <summary>
        /// Constructor for the Compensation Context
        /// </summary>
        /// <param name="options"></param>
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }

        /// <summary>
        /// DB Set of Employee Compensations
        /// </summary>
        public DbSet<Compensation> Compensations { get; set; }
    }
}
