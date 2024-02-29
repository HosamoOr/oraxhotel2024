using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using HotelSys.Models;
using Microsoft.AspNetCore.Authorization;
using DataModels;
using HotelSys.ViewModel;
using LinqToDB;

namespace HotelSys.Controllers
{
    [Authorize]
    public class SettingReceptionController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public SettingReceptionController(HotelAlkheerDB context)
        {
            _db = context;
        }

       
       
        // GET: SettingReception/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}


            var model =  _db.SettingReceptionTables.
                Select(x => new SettingReceptionViewModel { 
                    IsCheckinTime=Convert.ToBoolean( x.IsCheckinTime),
                    TimeCheckin=x.TimeCheckin,
                    IsCheckoutTime=Convert.ToBoolean( x.IsCheckoutTime),
                    TimeCheckout=x.TimeCheckout,
                    IsIntervalChangeroom=Convert.ToBoolean( x.IsIntervalChangeroom),
                    IntervalChangeroom=x.IntervalChangeroom,
                    IsMounthReceptionCheckout=Convert.ToBoolean( x.IsMounthReceptionCheckout),
                    Id=x.Id,
                    IdSub=x.IdSub, 
                    IncudePriceTax=x.IncudePriceTax==null?false: Convert.ToBoolean(x.IncudePriceTax)

                
                }).FirstOrDefault();
                ;
            if (model == null)
            {
                model = new SettingReceptionViewModel {
                    IsCheckinTime = false,
                    IsCheckoutTime = false,
                    IsIntervalChangeroom=false,
                    IsMounthReceptionCheckout=false,
                    IncudePriceTax=false
                    
                };
                View(model);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingReceptionViewModel settingReception)
        {
            //if (id != settingReception.Id)
            //{
            //    return NotFound();
            //}

            var model = _db.SettingReceptionTables.Find(id);

           

            if (ModelState.IsValid)
            {
                try
                {
                    if (model == null)
                    {
                        model = new DataModels.SettingReceptionTable();
                    }

                        model.IsCheckinTime = settingReception.IsCheckinTime;
                    model.TimeCheckin = settingReception.TimeCheckin;
                    model.IsCheckoutTime = settingReception.IsCheckoutTime;
                    model.TimeCheckout = settingReception.TimeCheckout;
                    model.IsIntervalChangeroom = settingReception.IsIntervalChangeroom;
                    model.IntervalChangeroom = settingReception.IntervalChangeroom;
                    model.IsMounthReceptionCheckout = settingReception.IsMounthReceptionCheckout;
                    model.IncudePriceTax = settingReception.IncudePriceTax;

                    if (model.IsCheckinTime==false)
                    {
                        model.TimeCheckin = null;
                    }
                    if(model.IsCheckoutTime == false)
                    {
                        model.TimeCheckout = null;
                    }
                    if (model.IsIntervalChangeroom ==false)
                    {
                        model.IntervalChangeroom = null;
                    }
                  

                    if (model.Id == 0)
                    {
                       await _db.InsertAsync(model);
                    }
                    else
                    {
                        await _db.UpdateAsync(model);
                    }


                   
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                   
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(settingReception);
        }

      
    }
}
