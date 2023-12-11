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
    public class AccountApplicationsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public AccountApplicationsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AccountApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountApplication>>> GetAccountApplications(int? orderBy = null, int? searchuser = null)
        {
            IQueryable<AccountApplication> query = _context.AccountApplications.AsQueryable();

            if (searchuser != null)
            {
                query = query.Where(u => u.AccountApplicationUserId == searchuser);
            }

            if (orderBy != null)
            {
                query = query.Where(prsn => prsn.StatusId == orderBy);        
            }

            var AccountApplications = await query.ToListAsync();

            if (!AccountApplications.Any())
            {
                return NotFound();
            }

            return Ok(AccountApplications);
        }


        // GET: api/AccountApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountApplication>> GetAccountApplication(string id)
        {
          if (_context.AccountApplications == null)
          {
              return NotFound();
          }
            var accountApplication = await _context.AccountApplications.FindAsync(id);

            if (accountApplication == null)
            {
                return NotFound();
            }

            return accountApplication;
        }

        // PUT: api/AccountApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountApplication(string id, AccountApplication accountApplication)
        {
            if (id != accountApplication.IdAccountApplication)
            {
                return BadRequest();
            }

            _context.Entry(accountApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountApplicationExists(id))
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

        // POST: api/AccountApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountApplication>> PostAccountApplication(AccountApplication accountApplication)
        {
          if (_context.AccountApplications == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.AccountApplications'  is null.");
          }
            _context.AccountApplications.Add(accountApplication);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountApplicationExists(accountApplication.IdAccountApplication))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountApplication", new { id = accountApplication.IdAccountApplication }, accountApplication);
        }

        // DELETE: api/AccountApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountApplication(string id)
        {
            if (_context.AccountApplications == null)
            {
                return NotFound();
            }
            var accountApplication = await _context.AccountApplications.FindAsync(id);
            if (accountApplication == null)
            {
                return NotFound();
            }

            _context.AccountApplications.Remove(accountApplication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountApplicationExists(string id)
        {
            return (_context.AccountApplications?.Any(e => e.IdAccountApplication == id)).GetValueOrDefault();
        }
    }
}
