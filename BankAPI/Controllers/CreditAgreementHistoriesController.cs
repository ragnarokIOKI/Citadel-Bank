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
    public class CreditAgreementHistoriesController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public CreditAgreementHistoriesController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditAgreementHistory>>> GetCreditAgreementHistorys(string? search = null, string? orderBy = null)
        {
            var query = _context.CreditAgreementHistories.AsQueryable();
            var CreditAgreementHistorys = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.CreditAgreementId.Contains(search));
            }

            if (!CreditAgreementHistorys.Any())
            {
                return NotFound();
            }

            return Ok(CreditAgreementHistorys);
        }
        public class IndexViewModel
        {
            public IEnumerable<CreditAgreementHistory> CreditAgreementHistorys { get; set; }
        }


        // GET: api/CreditAgreementHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditAgreementHistory>> GetCreditAgreementHistory(int id)
        {
          if (_context.CreditAgreementHistories == null)
          {
              return NotFound();
          }
            var creditAgreementHistory = await _context.CreditAgreementHistories.FindAsync(id);

            if (creditAgreementHistory == null)
            {
                return NotFound();
            }

            return creditAgreementHistory;
        }

        private bool CreditAgreementHistoryExists(int id)
        {
            return (_context.CreditAgreementHistories?.Any(e => e.IdCreditAgreementHistory == id)).GetValueOrDefault();
        }
    }
}
