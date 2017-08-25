using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workforce.Models;
using Workforce.Models.ViewModels;

namespace Workforce.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly WorkforceContext _context;

        public EmployeeController(WorkforceContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var workforceContext = _context.Employee.Include(e => e.Department);
            return View(await workforceContext.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .Include(e => e.TrainingSessions)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Set<Department>(), "DepartmentId", "Name");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Set<Department>(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EmployeeEditViewModel model = new EmployeeEditViewModel(_context, id);

            // Get the employee record and assing to model
            model.Employee = await _context.Employee
                    .SingleOrDefaultAsync(m => m.EmployeeId == id);

            // Return 404 if the employee was not found
            if (model.Employee == null)
            {
                return NotFound();
            }

            // Build a SelectList for displaying departments
            ViewData["DepartmentId"] = new SelectList(_context.Set<Department>(), "DepartmentId", "Name", model.Employee.DepartmentId);

            return View(model);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel model)
        {
            if (id != model.Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    // Update employee information
                    _context.Update(model.Employee);

                    // Remove all employee training sessions first
                    List<EmployeeTraining> sessions = await _context.EmployeeTraining
                        .Where(t => t.EmployeeId == id).ToListAsync();

                    foreach (var session in sessions)
                    {
                        _context.Remove(session);
                    }

                    // Add selected training sessions
                    if (model.SelectedSessions.Count > 0)
                    {
                        foreach (int sessionId in model.SelectedSessions)
                        {
                            EmployeeTraining session = new EmployeeTraining(){
                                EmployeeId = id,
                                TrainingId = sessionId
                            };
                             await _context.AddAsync(session);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(model.Employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Set<Department>(), "DepartmentId", "Name", model.Employee.DepartmentId);
            return View(model.Employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.SingleOrDefaultAsync(m => m.EmployeeId == id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
