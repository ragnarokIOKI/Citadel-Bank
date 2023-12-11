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
    public class AccountTypesController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public AccountTypesController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/AccountTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypes()
        {
          if (_context.AccountTypes == null)
          {
              return NotFound();
          }
            return await _context.AccountTypes.ToListAsync();
        }

        // GET: api/AccountTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountType>> GetAccountType(int id)
        {
          if (_context.AccountTypes == null)
          {
              return NotFound();
          }
            var accountType = await _context.AccountTypes.FindAsync(id);

            if (accountType == null)
            {
                return NotFound();
            }

            return accountType;
        }

        private bool AccountTypeExists(int id)
        {
            return (_context.AccountTypes?.Any(e => e.IdAccountType == id)).GetValueOrDefault();
        }
    }
}
