using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.BusnessLayer;

using HotelSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelSys.Controllers
{
    [Authorize]
    public class PriceRoomsController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public PriceRoomsController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            RoomsService rs = new RoomsService(_db);

            List< TaxGroupTable> ll=new List<TaxGroupTable>();
            ll.Add(new TaxGroupTable {
            Id=-1,
            Name="اختر المجموعة الضريبية"
            });
            ll.AddRange(_db.TaxGroupTables.ToArray());

           ViewData["IdGroupTax"] = new SelectList(ll, "Id", "Name");
            List<PriceRoomsViewModel> li = rs.ListPrice();
            return View(li);
        }

        public IActionResult IndexByType()
        {
            RoomsService rs = new RoomsService(_db);
            List<PriceByTypeViewModel> li = rs.ListTypePrice();

            List<TaxGroupTable> ll = new List<TaxGroupTable>();
            ll.Add(new TaxGroupTable
            {
                Id = -1,
                Name = "اختر المجموعة الضريبية"
            });
            ll.AddRange(_db.TaxGroupTables.ToArray());

            ViewData["IdGroupTax"] = new SelectList(ll, "Id", "Name");

            return View(li);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateByType(
            PriceByTypeViewModel roomsPrice
           )
        {
           // if (ModelState.IsValid)
            {



                RoomsService rs = new RoomsService(_db);

                int st =await rs.UpdatePriceByTypeAsync(roomsPrice);
                if (st > 0)
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(IndexByType));
            //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(
            //ICollection<PriceRoomsViewModel> roomsPrice
            //  [Bind("Id,Price,PriceOvertime,PriceMin,IdSub,IdRoom,NameRoom,NumFloor,CountRooms,NameType")]
            PriceRoomsViewModel roomsPrice
            )
        {
           //if (roomsPrice.s)
            {
                
                RoomsService rs = new RoomsService(_db);

                int st = await rs.UpdatePriceAsync(roomsPrice);
                if (st > 0)
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}