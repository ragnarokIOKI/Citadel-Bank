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
    public class CardsController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public CardsController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards(string? searchCardNum = null, string? searchAccountNum = null, string? orderBy = null)
        {
            var query = _context.Cards.AsQueryable();
            var Cards = await query.ToListAsync();

            if (!string.IsNullOrWhiteSpace(searchCardNum))
            {
                query = query.Where(u => u.CardNumber.Contains(searchCardNum));
            }

            if (!string.IsNullOrWhiteSpace(searchAccountNum))
            {
                query = query.Where(u => u.CardAccountId.Contains(searchAccountNum));
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                switch (orderBy)
                {
                    case "1":
                        Cards = await _context.Cards.Where(prsn => prsn.CardDeleted == false).ToListAsync();
                        break;
                    case "2":
                        Cards = await _context.Cards.Where(prsn => prsn.CardDeleted == true).ToListAsync();
                        break;
                    case "false":
                        Cards = await _context.Cards.Where(prsn => prsn.CardDeleted == false).ToListAsync();
                        break;
                    case "true":
                        Cards = await _context.Cards.Where(prsn => prsn.CardDeleted == true).ToListAsync();
                        break;
                }

            }
            else
            {
                Cards = await query
                                    .ToListAsync();
            }

            if (!Cards.Any())
            {
                return NotFound();
            }

            return Ok(Cards);
        }
        public class IndexViewModel
        {
            public IEnumerable<Card> Cards { get; set; }
        }


        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(string id)
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(string id, Card card)
        {
            if (id != card.CardNumber)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
          if (_context.Cards == null)
          {
              return Problem("Entity set 'Bank_DatabaseContext.Cards'  is null.");
          }
            _context.Cards.Add(card);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.CardNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCard", new { id = card.CardNumber }, card);
        }

        [HttpPut("CardsIDDelete")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Card>>> DeleteIds(string[] id, string deletionReason)
        {
            foreach (string Id in id)
            {
                var Card = await _context.Cards.FindAsync(Id);
                if (Card != null)
                {
                    Card.CardDeleted = false;
                    Card.Card_Deletion_Reason = deletionReason;
                    _context.Cards.Update(Card);
                    await _context.SaveChangesAsync();
                }
            }
            return await _context.Cards.ToListAsync();
        }

        [HttpPut("CardsIDReturn")]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Card>>> ReturnIds(string[] id)
        {
            foreach (string Id in id)
            {
                var Card = await _context.Cards.FindAsync(Id);
                if (Card != null)
                {
                    Card.CardDeleted = true;
                    Card.Card_Deletion_Reason = "-";
                    _context.Cards.Update(Card);
                    await _context.SaveChangesAsync();
                }
            }
            return await _context.Cards.ToListAsync();
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteCard(int? id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            var Card = await _context.Cards.FindAsync(id);
            if (Card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(Card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(string id)
        {
            return (_context.Cards?.Any(e => e.CardNumber == id)).GetValueOrDefault();
        }
    }
}
