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
    public class AccountsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public AccountsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts(string? orderBy = null, int? searchuser = null)
        {
            var query = _context.Accounts.AsQueryable();

            if (searchuser != null)
            {
                query = query.Where(u => u.UserAccountId == searchuser);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                switch (orderBy)
                {
                    case "1":
                    case "false":
                        query = query.Where(prsn => prsn.AccountDeleted == false);
                        break;
                    case "2":
                    case "true":
                        query = query.Where(prsn => prsn.AccountDeleted == true);
                        break;
                }
            }

            var accounts = await query.ToListAsync();

            if (!accounts.Any())
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        public class IndexViewModel
        {
            public IEnumerable<Account> Accounts { get; set; }
        }


        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, Account account)
        {
            if (id != account.IdAccount)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
          if (_context.Accounts == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.Accounts'  is null.");
          }
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.IdAccount))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccount", new { id = account.IdAccount }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("AccountsIDDelete")]
        public async Task<ActionResult<IEnumerable<Account>>> DeleteIds(string[] id, string deletionReason)
        {
            foreach (string Id in id)
            {
                var Account = await _context.Accounts.FindAsync(Id);
                if (Account != null)
                {
                    Account.AccountDeleted = false;
                    Account.Account_Deletion_Reason = deletionReason;
                    _context.Accounts.Update(Account);
                    await _context.SaveChangesAsync();
                }
            }
            return await _context.Accounts.ToListAsync();
        }

        [HttpPut("AccountsIDReturn")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Account>>> ReturnIds(string[] id)
        {
            foreach (string Id in id)
            {
                var Account = await _context.Accounts.FindAsync(Id);
                if (Account != null)
                {
                    Account.AccountDeleted = true;
                    Account.Account_Deletion_Reason = "-";
                    _context.Accounts.Update(Account);
                    await _context.SaveChangesAsync();
                }
            }
            return await _context.Accounts.ToListAsync();
        }

        private bool AccountExists(string id)
        {
            return (_context.Accounts?.Any(e => e.IdAccount == id)).GetValueOrDefault();
        }
    }
}
