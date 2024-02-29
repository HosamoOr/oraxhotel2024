using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{

    [Authorize]
    public class HomeDoneReceptionController : Controller
    {
        int idsub = 1;
        private readonly HotelAlkheerDB _db;

        public HomeDoneReceptionController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        

      

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(int id, Status_Current_RoomViewModel collection)
        {
            try
            {
                Status_RoomService sr = new Status_RoomService(_db);
                var mo = sr.ChangeStatus_(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       

        //DataTable
            public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param, int id)
        {

            ReceptionService rs = new ReceptionService(_db);
           // List<ReceptionViewModel> li = rs.List(1);

          
           

              var  model = await rs.ListDoneDT(idsub, param);


            var Parts = model.list;

          //Parts.ToList().ForEach(x => x.Date = x.Date.ToString("dd'/'MM'/'yyyy"));

          var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.IdReception).ToList() : Parts.OrderByDescending(c => c.IdReception).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                try
                {
                    Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.customer.Name).ToList() : Parts.OrderByDescending(c => c.customer.Name).ToList();

                }
                catch { }
               }
            else if (sortColumnIndex == 2)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.room.NameRoom).ToList() : Parts.OrderByDescending(c => c.room.NameRoom).ToList();
            }
            /*   else if (sortColumnIndex == 5)
               {
                   Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Salary) : Parts.OrderByDescending(c => c.Salary);
               }
               else
               {
                   Func<Employee, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name :
                                                                  sortColumnIndex == 1 ? e.Position :
                                                                  e.Location;

                   Parts = sortDirection == "asc" ? Parts.OrderBy(orderingFunction) : Parts.OrderByDescending(orderingFunction);
               }*/

            //var displayResult = Parts.Skip(param.iDisplayStart)
            //                .Take(param.iDisplayLength).ToList();

            var totalRecords = model.rowCount;
           
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }


        //[HttpGet("{id}/{idroom}")]
        public async Task<ActionResult> cancelRec(long id,int idroom=0)
        {

            var rse = new ReceptionService(_db);

           var st= rse.CancelReception(id,idroom);




            if (st=="succeful")
            {
                
              
                return Json(new { isValid = true });

                    }
            else
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index") });
            }




        }


    }
}