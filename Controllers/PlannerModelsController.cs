using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Data;
using Automated_Wedding_Application.Models;

namespace Automated_Wedding_Application.Views.Planner
{
    public class PlannerModelsController : Controller
    {
        private readonly Automated_Wedding_ApplicationContext _context;

        public PlannerModelsController(Automated_Wedding_ApplicationContext context)
        {
            _context = context;
        }

        // GET: PlannerModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlannerModel.ToListAsync());
        }

        // GET: PlannerModels/Details/5
       

        // GET: PlannerModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlannerModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlannerId,PlannerName,About,Location,ApplicationUserId")] PlannerModel plannerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plannerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plannerModel);
        }

        // GET: PlannerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plannerModel = await _context.PlannerModel.FindAsync(id);
            if (plannerModel == null)
            {
                return NotFound();
            }
            return View(plannerModel);
        }

        // POST: PlannerModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlannerId,PlannerName,About,Location,ApplicationUserId")] PlannerModel plannerModel)
        {
            if (id != plannerModel.PlannerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plannerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlannerModelExists(plannerModel.PlannerId))
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
            return View(plannerModel);
        }

        // GET: PlannerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plannerModel = await _context.PlannerModel
                .FirstOrDefaultAsync(m => m.PlannerId == id);
            if (plannerModel == null)
            {
                return NotFound();
            }

            return View(plannerModel);
        }

        // POST: PlannerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plannerModel = await _context.PlannerModel.FindAsync(id);
            _context.PlannerModel.Remove(plannerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlannerModelExists(int id)
        {
            return _context.PlannerModel.Any(e => e.PlannerId == id);
        }
    }
}
