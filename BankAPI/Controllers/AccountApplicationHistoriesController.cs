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
    public class AccountApplicationHistoriesController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public AccountApplicationHistoriesController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountApplicationHistory>>> GetAccountApplicationHistorys(string? search = null)
        {
            var query = _context.AccountApplicationHistories.AsQueryable();
            var AccountApplicationHistorys = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.AccountApplicationId.Contains(search));
            }

            if (!AccountApplicationHistorys.Any())
            {
                return NotFound();
            }

            return Ok(AccountApplicationHistorys);
        }
        public class IndexViewModel
        {
            public IEnumerable<AccountApplicationHistory> AccountApplicationHistorys { get; set; }
        }


        // GET: api/AccountApplicationHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountApplicationHistory>> GetAccountApplicationHistory(int id)
        {
          if (_context.AccountApplicationHistories == null)
          {
              return NotFound();
          }
            var accountApplicationHistory = await _context.AccountApplicationHistories.FindAsync(id);

            if (accountApplicationHistory == null)
            {
                return NotFound();
            }

            return accountApplicationHistory;
        }
        private bool AccountApplicationHistoryExists(int id)
        {
            return (_context.AccountApplicationHistories?.Any(e => e.IdAccountApplicationHistory == id)).GetValueOrDefault();
        }
    }
}
