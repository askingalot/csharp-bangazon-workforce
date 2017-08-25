using System;
using System.ComponentModel.DataAnnotations;

namespace Workforce.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage="You must provide a first name for this employee")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="You must provide a last name for this employee")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage="Please select which department this employee is assigned to")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

    }
}