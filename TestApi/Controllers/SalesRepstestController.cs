using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRepstestController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public SalesRepstestController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesRepstest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesRep>>> GetSalesReps()
        {
            return await _context.SalesReps.ToListAsync();
        }

        // GET: api/SalesRepstest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesRep>> GetSalesRep(int id)
        {
            var salesRep = await _context.SalesReps.FindAsync(id);

            if (salesRep == null)
            {
                return NotFound();
            }

            return salesRep;
        }

        // PUT: api/SalesRepstest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesRep(int id, SalesRep salesRep)
        {
            if (id != salesRep.SalesRepID)
            {
                return BadRequest();
            }

            _context.Entry(salesRep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRepExists(id))
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

        // POST: api/SalesRepstest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesRep>> PostSalesRep(SalesRep salesRep)
        {
            _context.SalesReps.Add(salesRep);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesRep", new { id = salesRep.SalesRepID }, salesRep);
        }

        // DELETE: api/SalesRepstest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesRep(int id)
        {
            var salesRep = await _context.SalesReps.FindAsync(id);
            if (salesRep == null)
            {
                return NotFound();
            }

            _context.SalesReps.Remove(salesRep);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesRepExists(int id)
        {
            return _context.SalesReps.Any(e => e.SalesRepID == id);
        }
    }
}
