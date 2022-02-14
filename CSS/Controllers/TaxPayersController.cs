using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSS.Data;
using CSS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CSS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPayersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaxPayersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaxPayers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxPayer>>> GetTaxPayers()
        {
            var t=await _context.TaxPayers.ToListAsync();
            return t;
        }

        // GET: api/TaxPayers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxPayer>> GetTaxPayer(int id)
        {
            var taxPayer = await _context.TaxPayers.FirstOrDefaultAsync(t => t.Id == id);
         

            if (taxPayer == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return taxPayer;
        }

        // PUT: api/TaxPayers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxPayer(int id, TaxPayer taxPayer)
        {
            if (id != taxPayer.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxPayer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxPayerExists(id))
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

        // POST: api/TaxPayers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxPayer>> PostTaxPayer(TaxPayer taxPayer)
        {
            _context.TaxPayers.Add(taxPayer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxPayer", new { id = taxPayer.Id }, taxPayer);
        }

        // DELETE: api/TaxPayers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxPayer(int id)
        {
            var taxPayer = await _context.TaxPayers.FindAsync(id);
            if (taxPayer == null)
            {
                return NotFound();
            }

            _context.TaxPayers.Remove(taxPayer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxPayerExists(int id)
        {
            return _context.TaxPayers.Any(e => e.Id == id);
        }
    }
}
