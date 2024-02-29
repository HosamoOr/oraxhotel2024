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
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{

    [Authorize]
    //  [Authorize(Roles = "admin")]
    public class HotelBrchController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public HotelBrchController(HotelAlkheerDB context)
        {
            _db = context;
        }


        // GET: Hotel/Edit/5
        public async Task<IActionResult> Index()
        {
           
                var list = _db.HotelsBranchTables.
                     Select(x => new HotelBrchViewModel
                     {
                         Id = x.Id,
                         NameH = x.NameH,
                         Address = x.Address,
                         IdCountry = x.IdCountry,
                         countFloot =  x.CountFloot ==null? 0 : x.CountFloot,
                         count_Room= x.Fkroomshotels==null?0: x.Fkroomshotels.Count() ,
                         count_users=0
                        


                     }).
                     ToList();

                return View(list);
           
           
        }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new HotelBrchViewModel());
            else
            {
                try
                {
                    var transactionModel = _db.HotelsBranchTables.Where(x=>x.Id== id).
                        Select(x=>new HotelBrchViewModel
                        {
                            Id = x.Id,
                            NameH = x.NameH,
                            Address= x.Address,
                            IdCountry = x.IdCountry,
                            City = x.City,
                            countFloot= x.CountFloot,
                            Country = x.Country,
                            Phone = x.Phone,
                            

                        }).FirstOrDefault();
                        ;
                    if (transactionModel == null)
                    {
                        return NotFound();
                    }
                    return View(transactionModel);
                }
                catch (Exception ex)
                {
                    return View(new CountryTable());
                }

            }
        }
        private bool HotelsTableExists(int id)
        {
            return _db.HotelsBranchTables.Any(e => e.Id == id);
        }
    }
}
