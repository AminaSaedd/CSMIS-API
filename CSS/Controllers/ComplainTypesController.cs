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
    public class ComplainTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplainTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ComplainTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplainType>>> GetComplainTypes()
        {
            return await _context.ComplainTypes.ToListAsync();
        }

        // GET: api/ComplainTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplainType>> GetComplainType(int id)
        {
            var complainType = await _context.ComplainTypes.FindAsync(id);

            if (complainType == null)
            {
                return NotFound();
            }

            return complainType;
        }

        // PUT: api/ComplainTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplainType(int id, ComplainType complainType)
        {
            if (id != complainType.Id)
            {
                return BadRequest();
            }

            _context.Entry(complainType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplainTypeExists(id))
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

        // POST: api/ComplainTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComplainType>> PostComplainType(ComplainType complainType)
        {
            _context.ComplainTypes.Add(complainType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplainType", new { id = complainType.Id }, complainType);
        }

        // DELETE: api/ComplainTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplainType(int id)
        {
            var complainType = await _context.ComplainTypes.FindAsync(id);
            if (complainType == null)
            {
                return NotFound();
            }

            _context.ComplainTypes.Remove(complainType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplainTypeExists(int id)
        {
            return _context.ComplainTypes.Any(e => e.Id == id);
        }
    }
}
