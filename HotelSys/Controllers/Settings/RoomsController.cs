using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelSys.Controllers
{

    [Authorize]

    //[Authorize(Roles = "admin")]
    public class RoomsController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public RoomsController(HotelAlkheerDB context)
        {
            _db = context;
        }


        public IActionResult Index()
        {

            RoomsService rs = new RoomsService(_db);

            List<RoomViewModel> li = rs.List();

            return View(li);
        }

        public IActionResult Create()
        {

            ViewData["IdType"] = new SelectList(_db.TypeRoomsTables, "Id", "NameT");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RoomsTable roomsTable)
        {
            if (ModelState.IsValid)
            {
                roomsTable.IdHo = 2;

                RoomsService rs = new RoomsService(_db);

               int st= rs.create(roomsTable);
                if(st>0)
                return RedirectToAction(nameof(Index));
            }


            ViewData["IdHo"] = new SelectList(_db.OrgsTables, "Id", "NameH", roomsTable.IdHo);
            ViewData["IdType"] = new SelectList(_db.TypeRoomsTables, "Id", "NameT", roomsTable.IdType);
            return View(roomsTable);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomsService roomsService = new RoomsService(_db);
            var model = roomsService.One(id);

            if (model == null)
            {
                return NotFound();
            }
            
            ViewData["IdType"] = new SelectList(_db.TypeRoomsTables, "Id", "NameT", model.IdType);
            return View(model);
        }


      



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoomsService rs = new RoomsService(_db);

                await rs.Edit(model);

                return RedirectToAction(nameof(Index));
            }
           
            return View(model);
        }



        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomsService roomsService = new RoomsService(_db);
            var model = roomsService.One(id);

            if (model == null)
            {
                return NotFound();
            }
           
            return View(model);
        }

        // POST: CompanyTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = _db.RoomsTables.Find(Convert.ToInt32(id));

            await _db.DeleteAsync(model);

            return RedirectToAction(nameof(Index));
        }

    }
}