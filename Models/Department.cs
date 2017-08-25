using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workforce.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}