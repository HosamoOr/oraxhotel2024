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
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    [Authorize]
    public class FollowersController : Controller
    {
        private readonly HotelAlkheerDB _db;
        int idsub=1;

        public FollowersController(HotelAlkheerDB context)
        {
            _db = context;
        }
       


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateJson(CustomerViewModel customerT)
        {
            // if (ModelState.IsValid)
            FollowersService fl = new FollowersService(_db);
            Int64 st = customerT.IdcumtomerAll;
         
            string typeop = "ADD";
            ListIdLongAcc til = new ListIdLongAcc();
            til.IDs = new List<long>();

            if(customerT.Nationality==null)
            {
                customerT.Nationality = "";
            }

            if(customerT.Id_Area==-1)
            {
                customerT.Id_Area = null;
            }

            if (customerT.IdcumtomerAll==0)
            {
                

                til = await fl.CreateAsync(customerT);

                st = til.IDs[0];

                return Json(new
                {

                    id = st,
                    idacc = til.IDs[1],
                    idmy = til.IDs[2],

                    name = customerT.Name,
                    privateNote = customerT.PrivateNote,
                    status = til.status,
                    mess = "New Customer successful"
                });
            }
            else
            {
                CustomerService customer = new CustomerService(_db);

                til =  await customer.EditAll(customerT);
               
                typeop = "Update";
                return Json(new
                {

                    id = customerT.IdcumtomerAll,
                    idmy = customerT.IdmyCu,
                    idacc = customerT.IdAccount,
                    //idmy = til.IDs[2],

                    name = customerT.Name,
                    status = til.status,
                    //privateNote = customerT.PrivateNote,
                    mess = "Edit Custmer successful"
                });
            }

             if(til.status< 0  )
            {

                return Json(new
                {

                    id = til.IDs[0],

                    idacc = til.IDs[1],
                    idmy = til.IDs[2],

                    name = til.modelCu .Name,
                    //privateNote = customerT.PrivateNote,
                    status = til.status,
                    mess= til.messege,
                    model = til.modelCu
                });

            }
              
            // ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", customerT.IdAccount);
            return Json(new
            {
                id = st,
                mess="حدث خطاء ما ادى الى عدم حفظ البيانات!"
            });
        }



     
    }
}