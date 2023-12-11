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
    public class CreditApplicationHistoriesController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public CreditApplicationHistoriesController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CreditApplicationHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditApplicationHistory>>> GetCreditApplicationHistories()
        {
          if (_context.CreditApplicationHistories == null)
          {
              return NotFound();
          }
            return await _context.CreditApplicationHistories.ToListAsync();
        }

        // GET: api/CreditApplicationHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditApplicationHistory>> GetCreditApplicationHistory(int id)
        {
          if (_context.CreditApplicationHistories == null)
          {
              return NotFound();
          }
            var creditApplicationHistory = await _context.CreditApplicationHistories.FindAsync(id);

            if (creditApplicationHistory == null)
            {
                return NotFound();
            }

            return creditApplicationHistory;
        }

        // PUT: api/CreditApplicationHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditApplicationHistory(int id, CreditApplicationHistory creditApplicationHistory)
        {
            if (id != creditApplicationHistory.IdCreditApplicationHistory)
            {
                return BadRequest();
            }

            _context.Entry(creditApplicationHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditApplicationHistoryExists(id))
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

        // POST: api/CreditApplicationHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditApplicationHistory>> PostCreditApplicationHistory(CreditApplicationHistory creditApplicationHistory)
        {
          if (_context.CreditApplicationHistories == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.CreditApplicationHistories'  is null.");
          }
            _context.CreditApplicationHistories.Add(creditApplicationHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditApplicationHistory", new { id = creditApplicationHistory.IdCreditApplicationHistory }, creditApplicationHistory);
        }

        // DELETE: api/CreditApplicationHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditApplicationHistory(int id)
        {
            if (_context.CreditApplicationHistories == null)
            {
                return NotFound();
            }
            var creditApplicationHistory = await _context.CreditApplicationHistories.FindAsync(id);
            if (creditApplicationHistory == null)
            {
                return NotFound();
            }

            _context.CreditApplicationHistories.Remove(creditApplicationHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditApplicationHistoryExists(int id)
        {
            return (_context.CreditApplicationHistories?.Any(e => e.IdCreditApplicationHistory == id)).GetValueOrDefault();
        }
    }
}
