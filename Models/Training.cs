using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Workforce.Models
{
    public class Training
    {
        [Key]
        public int TrainingId { get; set; }
        
        [Required(ErrorMessage="Please provide a title for training program")]
        [DataType(DataType.Text)]
        [Display(Name="Training Program Title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage="Please provide a short description of the training program")]
        [DataType(DataType.Text)]
        [Display(Name="Training Program Description")]
        public string Description { get; set; }

        [Required(ErrorMessage="You must specify when the training program starts")]
        [DataType(DataType.Date)]
        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage="You must specify when the training program ends")]
        [DataType(DataType.Date)]
        [Display(Name="End Date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<EmployeeTraining> Attendees { get; set; }
    }
}