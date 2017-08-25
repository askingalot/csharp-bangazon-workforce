using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Workforce.Models;

namespace Workforce.Controllers
{
    public class TrainingController : Controller
    {
        private readonly WorkforceContext _context;

        public TrainingController(WorkforceContext context)
        {
            _context = context;
        }

        // GET: Training
        public async Task<IActionResult> Index()
        {
            List<Training> results = await _context.Training.Include("Attendees").ToListAsync();
            return View(results);
        }

        // GET: Training/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .SingleOrDefaultAsync(m => m.TrainingId == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Training/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Training/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingId,Title,Description,StartDate,EndDate")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(training);
        }

        // GET: Training/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (training == null)
            {
                return NotFound();
            }
            return View(training);
        }

        // POST: Training/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingId,Title,Description,StartDate,EndDate")] Training training)
        {
            if (id != training.TrainingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.TrainingId))
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
            return View(training);
        }

        // GET: Training/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Training
                .SingleOrDefaultAsync(m => m.TrainingId == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Training/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Training.SingleOrDefaultAsync(m => m.TrainingId == id);
            _context.Training.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.TrainingId == id);
        }
    }
}
