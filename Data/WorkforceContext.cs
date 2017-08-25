using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workforce.Models;

    public class WorkforceContext : DbContext
    {
        public WorkforceContext (DbContextOptions<WorkforceContext> options)
            : base(options)
        {
        }

        public DbSet<Workforce.Models.Employee> Employee { get; set; }

        public DbSet<Workforce.Models.Department> Department { get; set; }

        public DbSet<Workforce.Models.Training> Training { get; set; }
        
        public DbSet<Workforce.Models.EmployeeTraining> EmployeeTraining { get; set; }
    }
