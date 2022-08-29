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

namespace JobPortal.Web.Areas.Portal.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Portal")]
    public class JobDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Portal/JobDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobDetails.Include(j => j.JobCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Portal/JobDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDetail = await _context.JobDetails
                .Include(j => j.JobCategory)
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (jobDetail == null)
            {
                return NotFound();
            }

            return View(jobDetail);
        }

        // GET: Portal/JobDetails/Create
        public IActionResult Create()
        {
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName");
            return View();
        }

        // POST: Portal/JobDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,Experience,CTC,Location,JobDiscription,JobCategoryId")] JobDetail jobDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName", jobDetail.JobCategoryId);
            return View(jobDetail);
        }

        // GET: Portal/JobDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDetail = await _context.JobDetails.FindAsync(id);
            if (jobDetail == null)
            {
                return NotFound();
            }
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName", jobDetail.JobCategoryId);
            return View(jobDetail);
        }

        // POST: Portal/JobDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,Experience,CTC,Location,JobDiscription,JobCategoryId")] JobDetail jobDetail)
        {
            if (id != jobDetail.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobDetailExists(jobDetail.CompanyId))
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
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName", jobDetail.JobCategoryId);
            return View(jobDetail);
        }

        // GET: Portal/JobDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobDetail = await _context.JobDetails
                .Include(j => j.JobCategory)
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (jobDetail == null)
            {
                return NotFound();
            }

            return View(jobDetail);
        }

        // POST: Portal/JobDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobDetail = await _context.JobDetails.FindAsync(id);
            _context.JobDetails.Remove(jobDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobDetailExists(int id)
        {
            return _context.JobDetails.Any(e => e.CompanyId == id);
        }
    }
}
