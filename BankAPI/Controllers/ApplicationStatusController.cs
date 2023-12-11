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
    public class ApplicationStatusController : ControllerBase
    {
        private readonly Bank_DatabaseContext _context;

        public ApplicationStatusController(Bank_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationStatus>>> GetApplicationStatuses()
        {
          if (_context.ApplicationStatuses == null)
          {
              return NotFound();
          }
            return await _context.ApplicationStatuses.ToListAsync();
        }

        // GET: api/ApplicationStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationStatus>> GetApplicationStatus(int id)
        {
          if (_context.ApplicationStatuses == null)
          {
              return NotFound();
          }
            var applicationStatus = await _context.ApplicationStatuses.FindAsync(id);

            if (applicationStatus == null)
            {
                return NotFound();
            }

            return applicationStatus;
        }

        private bool ApplicationStatusExists(int id)
        {
            return (_context.ApplicationStatuses?.Any(e => e.IdApplicationStatus == id)).GetValueOrDefault();
        }
    }
}
