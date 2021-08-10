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

        public async Task<IActionResult> AddItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            var BillableItem = new BillToItem
            {
                BillID = bill.ID,
            };
            return View(BillableItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem([Bind("BillID,ItemID,Counter")] BillToItem data)
        {
            Bill bill;
            if (ModelState.IsValid)
            {
                bill = await _context.Bill.Include(p => p.Items).Include(p => p.Costumer).FirstOrDefaultAsync(m => m.ID == data.BillID);
                bill.Items.Add(data);
                var item = await _context.BillableItems.FirstOrDefaultAsync(m => m.ID == data.ItemID);
                bill.TotalCostumer += item.CostumerPart * data.Counter;
                if (bill.Costumer.Insured)
                {
                    bill.TotalInsureance += item.InsurancePart * data.Counter;
                }
                else
                {
                    bill.TotalCostumer += item.InsurancePart * data.Counter;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Bills", new { id = data.BillID });
            }
            return RedirectToAction("Details", "Bills", new { id = data.BillID });
        }

        private bool BillToItemExists(int id)
        {
            return _context.BillToItem.Any(e => e.ID == id);
        }
    }
}
