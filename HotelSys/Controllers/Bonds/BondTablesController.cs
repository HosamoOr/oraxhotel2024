//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using HotelSys.Models;
//using Microsoft.AspNetCore.Authorization;

//namespace HotelSys.Controllers
//{
//    [Authorize]
//    public class BondTablesController : Controller
//    {
//        private readonly Hotel_alkheerContext _context;

//        public BondTablesController(Hotel_alkheerContext context)
//        {
//            _context = context;
//        }

//        // GET: BondTables
//        public async Task<IActionResult> Index()
//        {
//            var hotelSysContextConnection = _context.BondTable.Include(b => b.IdAccountNavigation).Include(b => b.IdBillNavigation).Include(b => b.IdBondPayNavigation).Include(b => b.IdItemExpensesNavigation);
//            return View(await hotelSysContextConnection.ToListAsync());
//        }

//        // GET: BondTables/Details/5
//        public async Task<IActionResult> Details(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var bondTable = await _context.BondTable
//                .Include(b => b.IdAccountNavigation)
//                .Include(b => b.IdBillNavigation)
//                .Include(b => b.IdBondPayNavigation)
//                .Include(b => b.IdItemExpensesNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (bondTable == null)
//            {
//                return NotFound();
//            }

//            return View(bondTable);
//        }

//        // GET: BondTables/Create
//        public IActionResult Create()
//        {
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name");

           
//            ViewData["IdBoxAccount"] = new SelectList(_context.AccountTable, "Id", "Name");

//            ViewData["IdBill"] = new SelectList(_context.BillsTable, "Id", "Type");
//            ViewData["IdBondPay"] = new SelectList(_context.BondTable, "Id", "ReceiptsOrExpenses");
//            ViewData["IdItemExpenses"] = new SelectList(_context.ItemsExpensesTable, "Id", "Name");
//            return View();
//        }

//        // POST: BondTables/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,ReceiptsOrExpenses,Type,TypePay,NumReference,Date,Amount,LocPay,DeserveAmount,WorthyDate,Why,Hand,NumCheck,NumCard,Note,Createat,IsDonePay,IdBondPay,IdAccount,IdBill,IdItemExpenses,IdBank")] BondTable bondTable)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(bondTable);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", bondTable.IdAccount);
//            ViewData["IdBill"] = new SelectList(_context.BillsTable, "Id", "Type", bondTable.IdBill);
//            ViewData["IdBondPay"] = new SelectList(_context.BondTable, "Id", "ReceiptsOrExpenses", bondTable.IdBondPay);
//            ViewData["IdItemExpenses"] = new SelectList(_context.ItemsExpensesTable, "Id", "Name", bondTable.IdItemExpenses);
//            return View(bondTable);
//        }

//        // GET: BondTables/Edit/5
//        public async Task<IActionResult> Edit(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var bondTable = await _context.BondTable.FindAsync(id);
//            if (bondTable == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", bondTable.IdAccount);
//            ViewData["IdBill"] = new SelectList(_context.BillsTable, "Id", "Type", bondTable.IdBill);
//            ViewData["IdBondPay"] = new SelectList(_context.BondTable, "Id", "ReceiptsOrExpenses", bondTable.IdBondPay);
//            ViewData["IdItemExpenses"] = new SelectList(_context.ItemsExpensesTable, "Id", "Name", bondTable.IdItemExpenses);
//            return View(bondTable);
//        }

//        // POST: BondTables/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(long id, [Bind("Id,ReceiptsOrExpenses,Type,TypePay,NumReference,Date,Amount,LocPay,DeserveAmount,WorthyDate,Why,Hand,NumCheck,NumCard,Note,Createat,IsDonePay,IdBondPay,IdAccount,IdBill,IdItemExpenses,IdBank")] BondTable bondTable)
//        {
//            if (id != bondTable.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(bondTable);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!BondTableExists(bondTable.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", bondTable.IdAccount);
//            ViewData["IdBill"] = new SelectList(_context.BillsTable, "Id", "Type", bondTable.IdBill);
//            ViewData["IdBondPay"] = new SelectList(_context.BondTable, "Id", "ReceiptsOrExpenses", bondTable.IdBondPay);
//            ViewData["IdItemExpenses"] = new SelectList(_context.ItemsExpensesTable, "Id", "Name", bondTable.IdItemExpenses);
//            return View(bondTable);
//        }

//        // GET: BondTables/Delete/5
//        public async Task<IActionResult> Delete(long? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var bondTable = await _context.BondTable
//                .Include(b => b.IdAccountNavigation)
//                .Include(b => b.IdBillNavigation)
//                .Include(b => b.IdBondPayNavigation)
//                .Include(b => b.IdItemExpensesNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (bondTable == null)
//            {
//                return NotFound();
//            }

//            return View(bondTable);
//        }

//        // POST: BondTables/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(long id)
//        {
//            var bondTable = await _context.BondTable.FindAsync(id);
//            _context.BondTable.Remove(bondTable);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool BondTableExists(long id)
//        {
//            return _context.BondTable.Any(e => e.Id == id);
//        }
//    }
//}
