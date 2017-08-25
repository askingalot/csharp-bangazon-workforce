using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Workforce.Models;

namespace Workforce.Models.ViewModels
{
    public class EmployeeEdit
    {
        public Employee Employee { get; set; }

        public int DeparmentId { get; set; }

        public MultiSelectList Sessions { get; private set; }

        public List<int> SelectedSessions { get; set; }

        public EmployeeEdit() {}
        public EmployeeEdit(WorkforceContext ctx)
        {
            this.Sessions = new MultiSelectList(ctx.Training, "TrainingId", "Title");
        }


    }
}