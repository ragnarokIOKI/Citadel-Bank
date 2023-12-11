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
    public class CreditAgreementsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public CreditAgreementsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditAgreement>>> GetCreditAgreements(string? searchbyID = null, bool? orderBy = null, int? searchuser = null)
        {
            IQueryable<CreditAgreement> query = _context.CreditAgreements.AsQueryable();

            if (searchuser != null)
            {
                query = query.Where(u => u.CreditAgreementUserId == searchuser);
            }

            if (searchbyID != null)
            {
                query = query.Where(u => u.IdCreditAgreement.StartsWith(searchbyID));
            }

            if (orderBy != null)
            {
                query = query.Where(prsn => prsn.CreditAgreementDeleted == orderBy);
            }

            var CreditAgreements = await query.ToListAsync();

            if (!CreditAgreements.Any())
            {
                return NotFound();
            }

            return Ok(CreditAgreements);
        }

        public class IndexViewModel
        {
            public IEnumerable<CreditAgreement> CreditAgreements { get; set; }
        }


        // GET: api/CreditAgreements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditAgreement>> GetCreditAgreement(string id)
        {
          if (_context.CreditAgreements == null)
          {
              return NotFound();
          }
            var creditAgreement = await _context.CreditAgreements.FindAsync(id);

            if (creditAgreement == null)
            {
                return NotFound();
            }

            return creditAgreement;
        }

        // PUT: api/CreditAgreements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditAgreement(string id, CreditAgreement creditAgreement)
        {
            if (id != creditAgreement.IdCreditAgreement)
            {
                return BadRequest();
            }

            _context.Entry(creditAgreement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditAgreementExists(id))
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

        // POST: api/CreditAgreements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditAgreement>> PostCreditAgreement(CreditAgreement creditAgreement)
        {
          if (_context.CreditAgreements == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.CreditAgreements'  is null.");
          }
            _context.CreditAgreements.Add(creditAgreement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CreditAgreementExists(creditAgreement.IdCreditAgreement))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCreditAgreement", new { id = creditAgreement.IdCreditAgreement }, creditAgreement);
        }

        [HttpPut("CreditAgreementsIDDelete")]
        public async Task<ActionResult<IEnumerable<CreditAgreement>>> DeleteId([FromBody] string id)
        {
            var CreditAgreement = await _context.CreditAgreements.FindAsync(id);
            if (CreditAgreement != null)
            {
                CreditAgreement.CreditAgreementDeleted = false;
                _context.CreditAgreements.Update(CreditAgreement);
                await _context.SaveChangesAsync();
            }
            return await _context.CreditAgreements.ToListAsync();
        }

        [HttpPut("CreditAgreementsIDReturn")]
        public async Task<ActionResult<IEnumerable<CreditAgreement>>> ReturnId([FromBody] string id)
        {
            var CreditAgreement = await _context.CreditAgreements.FindAsync(id);
            if (CreditAgreement != null)
            {
                CreditAgreement.CreditAgreementDeleted = true;
                _context.CreditAgreements.Update(CreditAgreement);
                await _context.SaveChangesAsync();
            }
            return await _context.CreditAgreements.ToListAsync();
        }

        // DELETE: api/CreditAgreements/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteCreditAgreement(int? id)
        {
            if (_context.CreditAgreements == null)
            {
                return NotFound();
            }
            var CreditAgreement = await _context.CreditAgreements.FindAsync(id);
            if (CreditAgreement == null)
            {
                return NotFound();
            }

            _context.CreditAgreements.Remove(CreditAgreement);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool CreditAgreementExists(string id)
        {
            return (_context.CreditAgreements?.Any(e => e.IdCreditAgreement == id)).GetValueOrDefault();
        }
    }
}
