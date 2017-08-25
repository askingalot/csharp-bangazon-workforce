using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workforce.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required(ErrorMessage="You must provide a first name for this employee")]
        [DataType(DataType.Text)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="You must provide a last name for this employee")]
        [DataType(DataType.Text)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage="Please select which department this employee is assigned to")]
        [Display(Name="Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [Display(Name="Training Programs Attending")]
        public ICollection<EmployeeTraining> TrainingSessions { get; set; }

    }
}