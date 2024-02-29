using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text.Json;

namespace HotelSys.Controllers
{
    [Authorize]
    public class AccReceptionController : Controller
    {
       
        private readonly HotelAlkheerDB _db;
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public AccReceptionController(HotelAlkheerDB context)
        {
            _db = context;
          

        }

        public IActionResult Index()
        {
            return View();
        }

       

        public JsonResult GetReceptionByIdAcc(int id)
        {
            AccReceptionService ars=new AccReceptionService(_db);

            var list = ars.getReceByIdAccount(id);
            Status_Current_RoomViewModel model=new Status_Current_RoomViewModel();

            if (list.Count() >0)
            {
                model = list.FirstOrDefault();

                if (list.Count() > 1 && model.Status == "4")
                {
                    var temp = list.Where(x => x.Status == "5").ToList();
                    if (temp.Count() > 0)
                    {
                        model = temp.FirstOrDefault();
                    }

                }
            }

           
            return Json(model);
        }
        public Status_Current_RoomViewModel GetReceptionByIdAccAsModel(int id)
        {
            AccReceptionService ars = new AccReceptionService(_db);

            var list = ars.getReceByIdAccount(id);
            Status_Current_RoomViewModel model = new Status_Current_RoomViewModel();

            if (list.Count() > 0)
            {
                model = new Status_Current_RoomViewModel();
                model = list.FirstOrDefault();

                if (list.Count() > 1 && model.Status == "4")
                {
                    var temp = list.Where(x => x.Status == "5").ToList();
                    if (temp.Count() > 0)
                    {
                        model = temp.FirstOrDefault();
                    }

                }
            }


            return model;
        }
    }
}