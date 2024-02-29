using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModels;

using HotelSys.ViewModel;
using LinqToDB;
using HotelSys.BusnessLayer;
using Microsoft.AspNetCore.Authorization;

namespace HotelSys.Controllers
{

    [Authorize]
  //  [Authorize(Roles = "admin")]
    public class OrgController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public OrgController(HotelAlkheerDB context)
        {
            _db = context;
        }


        // GET: Hotel/Edit/5
        public async Task<IActionResult> Index(int? id=0)
        {
            if (id == null)
            {
                return NotFound();
            }
           OrgService hs=new OrgService(_db);

            var hotelModel = hs.getDataOrg();


            if (hotelModel == null)
            {
                return NotFound();
            }
            ViewData["IdCountry"] = new SelectList(_db.CountryTables, "Id", "Name", hotelModel.IdCountry);
            
            return View(hotelModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, OrgViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    OrgService hs = new OrgService(_db);

                 var st=  await hs.EditAsync(id, model);


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsTableExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                 return RedirectToAction("Index","Org");
            }
            return View(model);
        }

        private bool HotelsTableExists(int id)
        {
            return _db.OrgsTables.Any(e => e.Id == id);
        }
    }
}
