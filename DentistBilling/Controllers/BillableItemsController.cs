using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DentistBilling.Data;
using DentistBilling.Models;

namespace DentistBilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillableItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillableItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BillableItems1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillableItems>>> GetBillableItems()
        {
            return await _context.BillableItems.ToListAsync();
        }

        // GET: api/BillableItems1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillableItems>> GetBillableItems(int id)
        {
            var billableItems = await _context.BillableItems.FindAsync(id);

            if (billableItems == null)
            {
                return NotFound();
            }

            return billableItems;
        }

        // PUT: api/BillableItems1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillableItems(int id, BillableItems billableItems)
        {
            if (id != billableItems.ID)
            {
                return BadRequest();
            }

            _context.Entry(billableItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillableItemsExists(id))
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

        // POST: api/BillableItems1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillableItems>> PostBillableItems(BillableItems billableItems)
        {
            _context.BillableItems.Add(billableItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillableItems", new { id = billableItems.ID }, billableItems);
        }

        // DELETE: api/BillableItems1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillableItems(int id)
        {
            var billableItems = await _context.BillableItems.FindAsync(id);
            if (billableItems == null)
            {
                return NotFound();
            }

            _context.BillableItems.Remove(billableItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillableItemsExists(int id)
        {
            return _context.BillableItems.Any(e => e.ID == id);
        }
    }
}
