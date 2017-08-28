using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workforce.Models;

namespace Workforce.Models.ViewModels
{
    public class EmployeeEditViewModel
    {
        public Employee Employee { get; set; }

        // Property to hold all training sessions for selection on edit form
        [Display(Name="Training Sessions")]
        public MultiSelectList Sessions { get; private set; }

        [Display(Name="Current Department")]
        public List<SelectListItem> Departments { get; private set; }

        // This will accept the selected training sessions on form POST
        public List<int> SelectedSessions { get; set; }

        public EmployeeEditViewModel() {}
        public EmployeeEditViewModel(WorkforceContext ctx, int employeeId)
        {
            // Select list for departments
            this.Departments = ctx.Department
                .OrderBy(l => l.Name)
                .AsEnumerable()
                .Select(li => new SelectListItem { 
                    Text = li.Name,
                    Value = li.DepartmentId.ToString()
                }).ToList();

            // Add a prompt so that the <select> element isn't blank for a new employee
            this.Departments.Insert(0, new SelectListItem { 
                Text = "Choose department...",
                Value = "0"
            }); 

            // Build a list of training sessions that begin in the future
            List<Training> availableSessions = ctx.Training
                .Where(t => t.StartDate > DateTime.Now)
                .ToList();

            // Build a list of training program ids to pre-select in the multiselect element
            var goingToList = (
              from t in ctx.Training
              join et in ctx.EmployeeTraining on t.TrainingId equals et.TrainingId
              where et.EmployeeId == employeeId
              select t.TrainingId
            ).ToList();

            /*
                This MultiSelectList constructor takes 4 arguments. Here's what they all mean.
                    1. The collection that store all items I want in the <select> element
                    2. The column to use for the `value` attribute
                    3. The column to use for display text
                    4. A list of integers for ones to be pre-selected
             */
            this.Sessions = new MultiSelectList(availableSessions, "TrainingId", "Title", goingToList);
        }
    }
}
