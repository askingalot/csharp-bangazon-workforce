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

        public int DeparmentId { get; set; }

        [Display(Name="Training Sessions")]
        public MultiSelectList Sessions { get; private set; }

        public List<int> SelectedSessions { get; set; }

        public EmployeeEditViewModel() {}
        public EmployeeEditViewModel(WorkforceContext ctx, int employeeId)
        {
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

            this.Sessions = new MultiSelectList(availableSessions, "TrainingId", "Title", goingToList);
        }
    }
}