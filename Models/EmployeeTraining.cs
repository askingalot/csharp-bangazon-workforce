using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workforce.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingId { get; set; }
        
        [Required]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        
        [Required]
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}