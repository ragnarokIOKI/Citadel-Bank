using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public TransactionTypesController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TransactionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionType>>> GetTransactionTypes()
        {
            if (_context.TransactionTypes == null)
            {
                return NotFound();
            }
            return await _context.TransactionTypes.ToListAsync();
        }

        // GET: api/TransactionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionType>> GetTransactionType(int id)
        {
            if (_context.TransactionTypes == null)
            {
                return NotFound();
            }
            var TransactionType = await _context.TransactionTypes.FindAsync(id);

            if (TransactionType == null)
            {
                return NotFound();
            }

            return TransactionType;
        }

        private bool TransactionTypeExists(int id)
        {
            return (_context.TransactionTypes?.Any(e => e.IdTransactionType == id)).GetValueOrDefault();
        }
    }
}