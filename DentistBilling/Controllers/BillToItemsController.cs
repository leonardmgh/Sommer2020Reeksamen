using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentistBilling.Data;
using DentistBilling.Models;

namespace DentistBilling.Controllers
{
    public class BillToItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillToItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillToItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BillToItem.Include(b => b.Bill).Include(b => b.BillableItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BillToItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToItem = await _context.BillToItem
                .Include(b => b.Bill)
                .Include(b => b.BillableItem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billToItem == null)
            {
                return NotFound();
            }

            return View(billToItem);
        }

        // GET: BillToItems/Create
        public IActionResult Create()
        {
            ViewData["BillID"] = new SelectList(_context.Bill, "ID", "ID");
            ViewData["ItemID"] = new SelectList(_context.BillableItems, "ID", "Description");
            return View();
        }

        // POST: BillToItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BillID,Counter,ItemID")] BillToItem billToItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billToItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillID"] = new SelectList(_context.Bill, "ID", "ID", billToItem.BillID);
            ViewData["ItemID"] = new SelectList(_context.BillableItems, "ID", "Description", billToItem.ItemID);
            return View(billToItem);
        }

        // GET: BillToItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToItem = await _context.BillToItem.FindAsync(id);
            if (billToItem == null)
            {
                return NotFound();
            }
            ViewData["BillID"] = new SelectList(_context.Bill, "ID", "ID", billToItem.BillID);
            ViewData["ItemID"] = new SelectList(_context.BillableItems, "ID", "Description", billToItem.ItemID);
            return View(billToItem);
        }

        // POST: BillToItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BillID,Counter,ItemID")] BillToItem billToItem)
        {
            if (id != billToItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billToItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillToItemExists(billToItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillID"] = new SelectList(_context.Bill, "ID", "ID", billToItem.BillID);
            ViewData["ItemID"] = new SelectList(_context.BillableItems, "ID", "Description", billToItem.ItemID);
            return View(billToItem);
        }

        // GET: BillToItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billToItem = await _context.BillToItem
                .Include(b => b.Bill)
                .Include(b => b.BillableItem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billToItem == null)
            {
                return NotFound();
            }

            return View(billToItem);
        }

        // POST: BillToItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billToItem = await _context.BillToItem.FindAsync(id);
            _context.BillToItem.Remove(billToItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillToItemExists(int id)
        {
            return _context.BillToItem.Any(e => e.ID == id);
        }
    }
}
