using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Web.Data;
using JobPortal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace JobPortal.Web.Areas.Portal.Controllers

{
    [Authorize(Roles = "Admin")]
    [Area("Portal")]
    public class JobCategoriesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<JobCategoriesController> _logger;

        public JobCategoriesController(
            ApplicationDbContext context,
            ILogger<JobCategoriesController> logger)
        {
            _context = context;
            _logger = logger;


        }

        // GET: Portal/JobCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobCategories.ToListAsync());
        }
        // GET: Portal/JobCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // GET: Portal/JobCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Portal/JobCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCategoryId,JobCategoryName")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                // Sanitize the data before consumption
                jobCategory.JobCategoryName = jobCategory.JobCategoryName.Trim();

                // Check for Duplicate CategoryName
                bool isDuplicateFound
                    = _context.JobCategories.Any(c => c.JobCategoryName == jobCategory.JobCategoryName);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("CategoryName", "Duplicate! Another category with same name exists");
                }
                else
                {
                    // Save the changes 
                    _context.Add(jobCategory);
                    await _context.SaveChangesAsync();              // update the database
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(jobCategory);
        }
        // GET: Portal/JobCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        // POST: Portal/JobCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobCategoryId,JobCategoryName")] JobCategory jobCategory)
        {
            if (id != jobCategory.JobCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            { // Sanitize the data before consumption
                jobCategory.JobCategoryName = jobCategory.JobCategoryName.Trim();

                // Check for duplicate Category
                bool isDuplicateFound
                    = _context.JobCategories.Any(c => c.JobCategoryName == jobCategory.JobCategoryName
                                                   && c.JobCategoryId != jobCategory.JobCategoryId);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("CategoryName", "A Duplicate Category was found!");
                }
                else
                {
                    try
                    {
                        // Save the changes to the database.
                        _context.Update(jobCategory);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!JobCategoryExists(jobCategory.JobCategoryId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return View(jobCategory);
        }


        // GET: Portal/JobCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // POST: Portal/JobCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            _context.JobCategories.Remove(jobCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.JobCategoryId == id);
        }
    }
}
