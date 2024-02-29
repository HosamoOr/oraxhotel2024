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
    public class TaxGroupController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public TaxGroupController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: TaxGroup
        public async Task<IActionResult> Index()
        {
            return View( _db.TaxGroupTables.ToList());
        }

        // GET: TaxGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxGroupTable =  _db.TaxGroupTables
                .FirstOrDefault(m => m.Id == id);
            if (taxGroupTable == null)
            {
                return NotFound();
            }

            return View(taxGroupTable);
        }

        // GET: TaxGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( TaxGroupTable taxGroupTable)
        {
            if (ModelState.IsValid)
            {
                if(taxGroupTable.IsBaladiTax==false)
                {
                    taxGroupTable.BaladiRate= 0;
                  

                }
                _db.Insert(taxGroupTable);
                //await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxGroupTable);
        }

        // GET: TaxGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxGroupTable =  _db.TaxGroupTables.Find( Convert.ToInt32( id));
            if (taxGroupTable == null)
            {
                return NotFound();
            }
            return View(taxGroupTable);
        }

        // POST: TaxGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  TaxGroupTable taxGroupTable)
        {
            if (id != taxGroupTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (taxGroupTable.IsBaladiTax == false)
                    {
                        taxGroupTable.BaladiRate = 0;


                    }
                    _db.Update(taxGroupTable);
                    //await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxGroupTableExists(taxGroupTable.Id))
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
            return View(taxGroupTable);
        }

        // GET: TaxGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxGroupTable =  _db.TaxGroupTables
                .FirstOrDefault(m => m.Id == id);
            if (taxGroupTable == null)
            {
                return NotFound();
            }

            return View(taxGroupTable);
        }

        // POST: TaxGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxGroupTable =  _db.TaxGroupTables.Find(id);
            _db.Delete (taxGroupTable);
            //await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxGroupTableExists(int id)
        {
            return _db.TaxGroupTables.Any(e => e.Id == id);
        }
    }
}
