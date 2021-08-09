using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentistBilling.Data;
using DentistBilling.Models;
using Microsoft.AspNetCore.Authorization;

namespace DentistBilling.Controllers
{
    [Authorize]
    public class BillableItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillableItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillableItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillableItems.ToListAsync());
        }

        // GET: BillableItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billableItems = await _context.BillableItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billableItems == null)
            {
                return NotFound();
            }

            return View(billableItems);
        }

        // GET: BillableItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillableItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,CostumerPart,InsurancePart")] BillableItems billableItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billableItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billableItems);
        }

        // GET: BillableItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billableItems = await _context.BillableItems.FindAsync(id);
            if (billableItems == null)
            {
                return NotFound();
            }
            return View(billableItems);
        }

        // POST: BillableItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,CostumerPart,InsurancePart")] BillableItems billableItems)
        {
            if (id != billableItems.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billableItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillableItemsExists(billableItems.ID))
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
            return View(billableItems);
        }

        // GET: BillableItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billableItems = await _context.BillableItems
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billableItems == null)
            {
                return NotFound();
            }

            return View(billableItems);
        }

        // POST: BillableItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billableItems = await _context.BillableItems.FindAsync(id);
            _context.BillableItems.Remove(billableItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillableItemsExists(int id)
        {
            return _context.BillableItems.Any(e => e.ID == id);
        }
    }
}
