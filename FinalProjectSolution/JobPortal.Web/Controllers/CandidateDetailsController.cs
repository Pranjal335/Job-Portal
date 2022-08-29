using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortal.Web.Data;
using JobPortal.Web.Models;

namespace JobPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CandidateDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CandidateDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDetail>>> GetCandidateDetails()
        {
            return await _context.CandidateDetails.ToListAsync();
        }

        // GET: api/CandidateDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDetail>> GetCandidateDetail(int id)
        {
            var candidateDetail = await _context.CandidateDetails.FindAsync(id);

            if (candidateDetail == null)
            {
                return NotFound();
            }

            return candidateDetail;
        }

        // PUT: api/CandidateDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidateDetail(int id, CandidateDetail candidateDetail)
        {
            if (id != candidateDetail.CandidateDetailId)
            {
                return BadRequest();
            }

            _context.Entry(candidateDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CandidateDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CandidateDetail>> PostCandidateDetail(CandidateDetail candidateDetail)
        {
            _context.CandidateDetails.Add(candidateDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidateDetail", new { id = candidateDetail.CandidateDetailId }, candidateDetail);
        }

        // DELETE: api/CandidateDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateDetail>> DeleteCandidateDetail(int id)
        {
            var candidateDetail = await _context.CandidateDetails.FindAsync(id);
            if (candidateDetail == null)
            {
                return NotFound();
            }

            _context.CandidateDetails.Remove(candidateDetail);
            await _context.SaveChangesAsync();

            return candidateDetail;
        }

        private bool CandidateDetailExists(int id)
        {
            return _context.CandidateDetails.Any(e => e.CandidateDetailId == id);
        }
    }
}
