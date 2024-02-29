using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{

    [Authorize]
    public class TypeRoomsController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public TypeRoomsController(HotelAlkheerDB context)
        {
            _db = context;
        }
        // GET: TypeRoomsTables
        public async Task<IActionResult> Index()
        {
            return View( _db.TypeRoomsTables.ToList());
        }

        // GET: TypeRoomsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRoomsTable =  _db.TypeRoomsTables
                .FirstOrDefault(m => m.Id == id);
            if (typeRoomsTable == null)
            {
                return NotFound();
            }

            return View(typeRoomsTable);
        }

        // GET: TypeRoomsTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeRoomsTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeRoomsTable typeRoomsTable)
        {
            if (ModelState.IsValid)
            {

              
                _db.Insert(typeRoomsTable);
               
                return RedirectToAction(nameof(Index));
            }
            return View(typeRoomsTable);
        }

        // GET: TypeRoomsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int idd = Convert.ToInt32(id);

            var typeRoomsTable =  _db.TypeRoomsTables.Find(idd);
            if (typeRoomsTable == null)
            {
                return NotFound();
            }
            return View(typeRoomsTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  TypeRoomsTable typeRoomsTable)
        {
            if (id != typeRoomsTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(typeRoomsTable);
                    //await _db.SaveChangesAsync();
                }
                catch (Exception re)
                {
                    if (!TypeRoomsTableExists(typeRoomsTable.Id))
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
            return View(typeRoomsTable);
        }

        // GET: TypeRoomsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRoomsTable = await _db.TypeRoomsTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeRoomsTable == null)
            {
                return NotFound();
            }

            return View(typeRoomsTable);
        }

        // POST: TypeRoomsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeRoomsTable =  _db.TypeRoomsTables.Find(id);
           await _db.DeleteAsync(typeRoomsTable);
         
            return RedirectToAction(nameof(Index));
        }

        private bool TypeRoomsTableExists(int id)
        {
            return _db.TypeRoomsTables.Any(e => e.Id == id);
        }
    }
}