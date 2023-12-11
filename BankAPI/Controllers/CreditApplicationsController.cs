using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditApplicationsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public CreditApplicationsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditApplication>>> GetCreditApplications(int? orderBy = null, int? searchAmount = null, int? searchUser = null)
        {
            var query = _context.CreditApplications.AsQueryable();

            if (searchUser != null)
            {
                query = query.Where(u => u.CreditApplicationUserId == searchUser);
            }

            if (searchAmount != null)
            {
                query = query.Where(u => u.ApplicationAmount == searchAmount);
            }

            if (orderBy != null)
            {
                    query = query.Where(prsn => prsn.StatusId == orderBy);
            }

            var creditApplications = await query.ToListAsync();

            if (!creditApplications.Any())
            {
                return NotFound();
            }

            return Ok(creditApplications);
        }

        // GET: api/CreditApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditApplication>> GetCreditApplication(string id)
        {
          if (_context.CreditApplications == null)
          {
              return NotFound();
          }
            var creditApplication = await _context.CreditApplications.FindAsync(id);

            if (creditApplication == null)
            {
                return NotFound();
            }

            return creditApplication;
        }

        // PUT: api/CreditApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditApplication(string id, CreditApplication creditApplication)
        {
            if (id != creditApplication.IdCreditApplication)
            {
                return BadRequest();
            }

            _context.Entry(creditApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditApplicationExists(id))
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

        // POST: api/CreditApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditApplication>> PostCreditApplication(CreditApplication creditApplication)
        {
          if (_context.CreditApplications == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.CreditApplications'  is null.");
          }
            _context.CreditApplications.Add(creditApplication);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CreditApplicationExists(creditApplication.IdCreditApplication))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCreditApplication", new { id = creditApplication.IdCreditApplication }, creditApplication);
        }

        // DELETE: api/CreditApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditApplication(string id)
        {
            if (_context.CreditApplications == null)
            {
                return NotFound();
            }
            var creditApplication = await _context.CreditApplications.FindAsync(id);
            if (creditApplication == null)
            {
                return NotFound();
            }

            _context.CreditApplications.Remove(creditApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditApplicationExists(string id)
        {
            return (_context.CreditApplications?.Any(e => e.IdCreditApplication == id)).GetValueOrDefault();
        }
    }
}
