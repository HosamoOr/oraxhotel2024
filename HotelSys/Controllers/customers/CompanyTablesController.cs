//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using HotelSys.Models;

//namespace HotelSys.Controllers
//{
//    public class CompanyTablesController : Controller
//    {
//        private readonly Hotel_alkheerContext _context;

//        public CompanyTablesController(Hotel_alkheerContext context)
//        {
//            _context = context;
//        }

//        // GET: CompanyTables
//        public async Task<IActionResult> Index()
//        {
//            var hotelSysContextConnection = _context.CompanyTable.Include(c => c.IdAccountNavigation);
//            return View(await hotelSysContextConnection.ToListAsync());
//        }

//        // GET: CompanyTables/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var companyTable = await _context.CompanyTable
//                .Include(c => c.IdAccountNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (companyTable == null)
//            {
//                return NotFound();
//            }

//            return View(companyTable);
//        }

//        // GET: CompanyTables/Create
//        public IActionResult Create()
//        {
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name");
//            return View();
//        }

//        // POST: CompanyTables/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,IdAccount,IdSub")] CompanyTable companyTable)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(companyTable);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", companyTable.IdAccount);
//            return View(companyTable);
//        }

//        // GET: CompanyTables/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var companyTable = await _context.CompanyTable.FindAsync(id);
//            if (companyTable == null)
//            {
//                return NotFound();
//            }
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", companyTable.IdAccount);
//            return View(companyTable);
//        }

//        // POST: CompanyTables/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdAccount,IdSub")] CompanyTable companyTable)
//        {
//            if (id != companyTable.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(companyTable);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CompanyTableExists(companyTable.Id))
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
//            ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", companyTable.IdAccount);
//            return View(companyTable);
//        }

//        // GET: CompanyTables/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var companyTable = await _context.CompanyTable
//                .Include(c => c.IdAccountNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (companyTable == null)
//            {
//                return NotFound();
//            }

//            return View(companyTable);
//        }

//        // POST: CompanyTables/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var companyTable = await _context.CompanyTable.FindAsync(id);
//            _context.CompanyTable.Remove(companyTable);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool CompanyTableExists(int id)
//        {
//            return _context.CompanyTable.Any(e => e.Id == id);
//        }
//    }
//}
