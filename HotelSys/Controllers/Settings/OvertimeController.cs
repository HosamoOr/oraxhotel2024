using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;

namespace HotelSys.Controllers
{
    [Authorize]
   // [Authorize(Roles = "admin")]
    public class OvertimeController : Controller
    {
        private readonly HotelAlkheerDB _context;
        int idUser = 1;

        public OvertimeController(HotelAlkheerDB context)
        {
            _context = context;
        }

        // GET: OvertimeTables
        public async Task<IActionResult> Index()
        {
            return View( _context.OvertimeTables.ToList());
        }

        // GET: OvertimeTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtimeTable =  _context.OvertimeTables
                .Where(m => m.Id == id).FirstOrDefault();
            if (overtimeTable == null)
            {
                return NotFound();
            }

            return View(overtimeTable);
        }

        // GET: OvertimeTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OvertimeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OvertimeTable model)
        {

            model.IdUser = idUser;
            model.Createat = DateTime.Now;
            _context.Insert(model);

            return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                

               
            }
            //return View(model);
        }

        // GET: OvertimeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtimeTable =  _context.OvertimeTables.Find(Convert.ToInt32( id));
            if (overtimeTable == null)
            {
                return NotFound();
            }
            return View(overtimeTable);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  OvertimeTable model)
        {

            if (id != model.Id)
            {
                return NotFound();
            }
            model.IdUser = idUser;
            model.Createat = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OvertimeTableExists(model.Id))
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
            return View(model);
        }

        // GET: OvertimeTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var overtimeTable =  _context.OvertimeTables
                .Where(m => m.Id == id).FirstOrDefault();
            if (overtimeTable == null)
            {
                return NotFound();
            }

            return View(overtimeTable);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var overtimeTable =  _context.OvertimeTables.Find(id);
           await _context.DeleteAsync(overtimeTable);
           
            return RedirectToAction(nameof(Index));
        }

        private bool OvertimeTableExists(int id)
        {
            return _context.OvertimeTables.Any(e => e.Id == id);
        }
    }
}
