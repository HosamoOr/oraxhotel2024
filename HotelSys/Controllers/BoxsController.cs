using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DataModels;
using LinqToDB;

namespace HotelSys.Controllers
{
    public class BoxsController : Controller
    {
        private readonly HotelDb2DB _db;

        public BoxsController(HotelDb2DB context)
        {
            _db = context;
        }

        // GET: Boxs
        public async Task<IActionResult> Index()
        {
            var hotelSysContextConnection = _db.BoxsTables;
            return View( hotelSysContextConnection.ToList());
        }

        // GET: Boxs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxsTable =  _db.BoxsTables
                //.Include(b => b.IdAccountNavigation)
                .FirstOrDefault(m => m.Id == id);
            if (boxsTable == null)
            {
                return NotFound();
            }

            return View(boxsTable);
        }

        // GET: Boxs/Create
        public IActionResult Create()
        {
            //ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name");
            return View();
        }

        // POST: Boxs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( BoxsTable boxsTable)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(boxsTable);

               // await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", boxsTable.IdAccount);
            return View(boxsTable);
        }

        // GET: Boxs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxsTable =  _db.BoxsTables.Where(x=>x.Id==id).FirstOrDefault();
            if (boxsTable == null)
            {
                return NotFound();
            }
            ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", boxsTable.IdAccount);
            return View(boxsTable);
        }

        // POST: Boxs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  BoxsTable boxsTable)
        {
            if (id != boxsTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(boxsTable);
                   // await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoxsTableExists(boxsTable.Id))
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
            //ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", boxsTable.IdAccount);
            return View(boxsTable);
        }

        // GET: Boxs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boxsTable =  _db.BoxsTables
                //.Include(b => b.IdAccountNavigation)
                .FirstOrDefault(m => m.Id == id);
            if (boxsTable == null)
            {
                return NotFound();
            }

            return View(boxsTable);
        }

        // POST: Boxs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boxsTable =  _db.BoxsTables.Find(id);
            _db.Delete(boxsTable);
           //await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoxsTableExists(int id)
        {
            return _db.BoxsTables.Any(e => e.Id == id);
        }
    }
}
