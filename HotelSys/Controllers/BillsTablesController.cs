using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSys.Models;

namespace HotelSys.Controllers
{
    public class BillsTablesController : Controller
    {
        private readonly Hotel_alkheerContext _context;

        public BillsTablesController(Hotel_alkheerContext context)
        {
            _context = context;
        }

        // GET: BillsTables
        public async Task<IActionResult> Index()
        {
            var hotelDb_2Context = _context.BillsTables.Include(b => b.IdAccountNavigation).Include(b => b.IdReceptionNavigation);
            return View(await hotelDb_2Context.ToListAsync());
        }

        // GET: BillsTables/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billsTable = await _context.BillsTables
                .Include(b => b.IdAccountNavigation)
                .Include(b => b.IdReceptionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billsTable == null)
            {
                return NotFound();
            }

            return View(billsTable);
        }

        // GET: BillsTables/Create
        public IActionResult Create()
        {
            ViewData["IdAccount"] = new SelectList(_context.AccountTables, "Id", "Name");
            ViewData["IdReception"] = new SelectList(_context.RecetionTables, "Id", "Source");
            return View();
        }

        // POST: BillsTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,TypePay,NumReference,Date,Total,IsForRoom,DeserveAmount,TypeDiscount,QtyDiscount,PayAmount,RestAmount,NumCheck,NumCard,Note,Createat,IdAccount,IdReception,IdBank,CustomerOrCompany,IdCurrancy,TotalTaxPrice,TotalTaxRate,IncludeTax,TotalBaladiTaxPrice,TotalBaladiTaxRate,IsBaladiTax")] BillsTable billsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAccount"] = new SelectList(_context.AccountTables, "Id", "Name", billsTable.IdAccount);
            ViewData["IdReception"] = new SelectList(_context.RecetionTables, "Id", "Source", billsTable.IdReception);
            return View(billsTable);
        }

        // GET: BillsTables/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billsTable = await _context.BillsTables.FindAsync(id);
            if (billsTable == null)
            {
                return NotFound();
            }
            ViewData["IdAccount"] = new SelectList(_context.AccountTables, "Id", "Name", billsTable.IdAccount);
            ViewData["IdReception"] = new SelectList(_context.RecetionTables, "Id", "Source", billsTable.IdReception);
            return View(billsTable);
        }

        // POST: BillsTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,TypePay,NumReference,Date,Total,IsForRoom,DeserveAmount,TypeDiscount,QtyDiscount,PayAmount,RestAmount,NumCheck,NumCard,Note,Createat,IdAccount,IdReception,IdBank,CustomerOrCompany,IdCurrancy,TotalTaxPrice,TotalTaxRate,IncludeTax,TotalBaladiTaxPrice,TotalBaladiTaxRate,IsBaladiTax")] BillsTable billsTable)
        {
            if (id != billsTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillsTableExists(billsTable.Id))
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
            ViewData["IdAccount"] = new SelectList(_context.AccountTables, "Id", "Name", billsTable.IdAccount);
            ViewData["IdReception"] = new SelectList(_context.RecetionTables, "Id", "Source", billsTable.IdReception);
            return View(billsTable);
        }

        // GET: BillsTables/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billsTable = await _context.BillsTables
                .Include(b => b.IdAccountNavigation)
                .Include(b => b.IdReceptionNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billsTable == null)
            {
                return NotFound();
            }

            return View(billsTable);
        }

        // POST: BillsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var billsTable = await _context.BillsTables.FindAsync(id);
            _context.BillsTables.Remove(billsTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillsTableExists(long id)
        {
            return _context.BillsTables.Any(e => e.Id == id);
        }
    }
}
