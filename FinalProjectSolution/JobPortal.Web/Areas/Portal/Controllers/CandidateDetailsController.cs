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
    [Area("Portal")]
    [Authorize]
    public class CandidateDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CandidateDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Portal/CandidateDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CandidateDetails.Include(c => c.JobDetail);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
            var applicationDbContext = _context.CandidateDetails.Include(c => c.JobDetail);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Portal/CandidateDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails
                .Include(c => c.JobDetail)
                .FirstOrDefaultAsync(m => m.CandidateDetailId == id);
            if (candidateDetail == null)
            {
                return NotFound();
            }



            return View(candidateDetail);
        }

        // GET: Portal/CandidateDetails/Create
        public IActionResult Create()
        {
            ViewData["JobDetailId"] = new SelectList(_context.JobDetails, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Portal/CandidateDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidateDetailId,CandidateFirstName,CandidateLastName,CandidateEmailId,DateofBirth,CandidateExperience,JobDetailId")] CandidateDetail candidateDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidateDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = candidateDetail.CandidateDetailId });
            }
            ViewData["JobDetailId"] = new SelectList(_context.JobDetails, "CompanyId", "CompanyName", candidateDetail.JobDetailId);
            return View(candidateDetail);



        }

        // GET: Portal/CandidateDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails.FindAsync(id);
            if (candidateDetail == null)
            {
                return NotFound();
            }
            ViewData["JobDetailId"] = new SelectList(_context.JobDetails, "CompanyId", "CompanyName", candidateDetail.JobDetailId);
            return View(candidateDetail);
        }

        // POST: Portal/CandidateDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CandidateDetailId,CandidateFirstName,CandidateLastName,CandidateEmailId,DateofBirth,CandidateExperience,JobDetailId")] CandidateDetail candidateDetail)
        {
            if (id != candidateDetail.CandidateDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateDetailExists(candidateDetail.CandidateDetailId))
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
            ViewData["JobDetailId"] = new SelectList(_context.JobDetails, "CompanyId", "CompanyName", candidateDetail.JobDetailId);
            return View(candidateDetail);
        }

        // GET: Portal/CandidateDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateDetail = await _context.CandidateDetails
                .Include(c => c.JobDetail)
                .FirstOrDefaultAsync(m => m.CandidateDetailId == id);
            if (candidateDetail == null)
            {
                return NotFound();
            }

            return View(candidateDetail);
        }

        // POST: Portal/CandidateDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidateDetail = await _context.CandidateDetails.FindAsync(id);
            _context.CandidateDetails.Remove(candidateDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateDetailExists(int id)
        {
            return _context.CandidateDetails.Any(e => e.CandidateDetailId == id);
        }
    }
}
