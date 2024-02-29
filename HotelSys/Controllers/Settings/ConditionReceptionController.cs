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
    public class ConditionReceptionController : Controller
    {
        private readonly HotelAlkheerDB _db;
        public ConditionReceptionController(HotelAlkheerDB context)
        {
            _db = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var li =  _db.ConditionReceptionTables.ToList();
            return View(li);
        }

        // GET: ConditionReceptionTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditionReceptionTable =  _db.ConditionReceptionTables.FirstOrDefault(m => m.Id == id);
            if (conditionReceptionTable == null)
            {
                return NotFound();
            }
             
            conditionReceptionTable.Num = 0;

            return View(conditionReceptionTable);
        }

        // GET: ConditionReceptionTables/Create
        public IActionResult Create()
        {
            


            ConditionReceptionTable mo = new ConditionReceptionTable { Num = 0 };

            var li = _db.ConditionReceptionTables.ToList().Count();
            if(li>=0)
            {
                mo.Num = li+1;
            }

            return View(mo);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ConditionReceptionTable conditionReceptionTable)
        {
            if (ModelState.IsValid)
            {
                
                _db.Insert(conditionReceptionTable);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conditionReceptionTable);
        }

        // GET: ConditionReceptionTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditionReceptionTable =  _db.ConditionReceptionTables.Find(Convert.ToInt32( id));
            if (conditionReceptionTable == null)
            {
                return NotFound();
            }
            return View(conditionReceptionTable);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ConditionReceptionTable conditionReceptionTable)
        {
            if (id != conditionReceptionTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(conditionReceptionTable);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConditionReceptionTableExists(conditionReceptionTable.Id))
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
            return View(conditionReceptionTable);
        }

        // GET: ConditionReceptionTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conditionReceptionTable =  _db.ConditionReceptionTables
                .FirstOrDefault(m => m.Id == id);
            if (conditionReceptionTable == null)
            {
                return NotFound();
            }

            return View(conditionReceptionTable);
        }

        // POST: ConditionReceptionTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conditionReceptionTable =  _db.ConditionReceptionTables.Find(Convert.ToInt32( id));
            await _db.DeleteAsync(conditionReceptionTable);
           
            return RedirectToAction(nameof(Index));
        }

        private bool ConditionReceptionTableExists(int id)
        {
            return _db.ConditionReceptionTables.Any(e => e.Id == id);
        }
    }
}
