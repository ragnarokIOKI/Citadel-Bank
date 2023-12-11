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
    public class TransactionsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public TransactionsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(bool? orderBy = null, string? searchAcc = null, int? orderbyTranType = null)
        {
            IQueryable<Transaction> query = _context.Transactions.AsQueryable();

            if (searchAcc != null && !searchAcc.Equals("0"))
            {
                query = query.Where(u => u.TransactionAccountId.Contains(searchAcc));
            }
            else if (searchAcc != null && searchAcc.Equals("0"))
            {
                return NotFound();
            }

            if (orderBy != null)
            {
                query = query.Where(prsn => prsn.TransactionDeleted == orderBy);
            }

            if (orderbyTranType != null)
            {
                query = query.Where(prsn => prsn.TransactionTypeId == orderbyTranType);
            }

            var Transactions = await query.ToListAsync();

            if (!Transactions.Any())
            {
                return NotFound();
            }

            return Ok(Transactions);
        }


        public class IndexViewModel
        {
            public IEnumerable<Transaction> Transactions { get; set; }
        }


        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
          if (_context.Transactions == null)
          {
              return NotFound();
          }
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.IdTransaction)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
          if (_context.Transactions == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.Transactions'  is null.");
          }
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new { id = transaction.IdTransaction }, transaction);
        }

        [HttpPut("TransactionsIDDelete")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Transaction>>> DeleteIds(int[] id)
        {
            foreach (int TransactionId in id)
            {
                var Transaction = await _context.Transactions.FindAsync(TransactionId);
                if (Transaction != null)
                {
                    Transaction.TransactionDeleted = false;
                    _context.Transactions.Update(Transaction);
                    await _context.SaveChangesAsync();
                }

            }

            return await _context.Transactions.ToListAsync();
        }

        [HttpPut("TransactionsIDReturn")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Transaction>>> ReturnIds(int[] id)
        {
            foreach (int TransactionId in id)
            {
                var Transaction = await _context.Transactions.FindAsync(TransactionId);
                if (Transaction != null)
                {
                    Transaction.TransactionDeleted = true;
                    _context.Transactions.Update(Transaction);
                    await _context.SaveChangesAsync();
                }
            }

            return await _context.Transactions.ToListAsync();
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteTransaction(int? id)
        {
            if (_context.Transactions == null)
            {
                return NotFound();
            }
            var Transaction = await _context.Transactions.FindAsync(id);
            if (Transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(Transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transactions?.Any(e => e.IdTransaction == id)).GetValueOrDefault();
        }
    }
}
