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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{

    [Authorize]
    public class HomeReceptionController : Controller
    {
        int idsub = 1;
        private readonly HotelAlkheerDB _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeReceptionController(HotelAlkheerDB context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            Status_RoomService rs = new Status_RoomService(_db);
            List<Status_Current_RoomViewModel> li = rs.ListStatusCurrentB(1);

            int empty = li.Where(x => x.Status == "1").Count();
            int clean = li.Where(x => x.Status == "2").Count();
            int Repair = li.Where(x => x.Status == "3").Count();
            int Reservation_without_entry = li.Where(x => x.Status == "4").Count();
            int Busy = li.Where(x => x.Status == "5").Count();

            List<Status_Current_RoomViewModel> Busylist = li.Where(x => x.Status == "5").ToList();

            for (int i = 0; i < Busylist.Count; i++)
            {
                DateTime futurDate = Convert.ToDateTime(Busylist[i].EndDate);
                DateTime TodayDate = DateTime.Now;
                double numberOfDays = (futurDate - TodayDate).TotalDays;

                int intOfDay = Convert.ToInt32(numberOfDays);

                if (intOfDay < 0)
                {
                    intOfDay = intOfDay * -1;
                }
                Busylist[i].qty_left = intOfDay.ToString();

                if (numberOfDays >= 0 && numberOfDays < 1)
                {
                    Busylist[i].str_date_logout = "اليوم"; // alyoom 
                }
                else if (numberOfDays >= 1 && numberOfDays < 2)
                {
                    Busylist[i].str_date_logout = "غدا"; // ghaden 
                }
                else if (numberOfDays >= 2 && numberOfDays < 3)
                {
                    Busylist[i].str_date_logout = "بعد غدا "; // bad ghd  
                }
                else if (numberOfDays >= 3 && numberOfDays < 3)
                {
                    Busylist[i].str_date_logout = futurDate.ToShortDateString();
                }
                else if (numberOfDays <= -1 && numberOfDays > -2)
                {
                    Busylist[i].str_date_logout = "امس"; // Ams 
                }
                else if (numberOfDays <= -2 && numberOfDays > -3)
                {
                    Busylist[i].str_date_logout = "قبل امس"; // qabl ams 
                }
                else if (numberOfDays <= -3 && numberOfDays > -30)
                {
                    Busylist[i].str_date_logout = " منذ " + intOfDay + " ايام ماضية "; // midho    ayam 
                }
                else
                {
                    Busylist[i].str_date_logout = futurDate.ToShortDateString();
                }

            }




            HomeReceptionViewModel model = new HomeReceptionViewModel { 
                countAll= li.Count(),
            countEmpty = empty,
            countClean=clean,
            countRepair=Repair,
            countReservation_without_entry=Reservation_without_entry,
            countBusy=Busy,
            RoomsStatusItems= li
            };
            //ListStatusCurrent
            return View(model);
        }
        public IActionResult Home_logout()
        {
            Status_RoomService rs = new Status_RoomService(_db);
            List<Status_Current_RoomViewModel> li = rs.ListStatusCurrentB(1);



            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            bool isAdmin = currentUser.IsInRole("Admin");
            var id = _userManager.GetUserId(User); // Get 

 

            //int empty = li.Where(x => x.Status == "1").Count();
            //int clean = li.Where(x => x.Status == "2").Count();
            //int Repair = li.Where(x => x.Status == "3").Count();
            //int Reservation_without_entry = li.Where(x => x.Status == "4").Count();
            int Busy = li.Where(x => x.Status == "5").Count();

            List<Status_Current_RoomViewModel> Busylist= li.Where(x => x.Status == "5").ToList();

            for(int i = 0; i < Busylist.Count; i++)
            {
                DateTime futurDate = Convert.ToDateTime(Busylist[i].EndDate);
                DateTime TodayDate = DateTime.Now;
                double numberOfDays = (futurDate - TodayDate).TotalDays;

                int intOfDay= Convert.ToInt32(numberOfDays);

                if (intOfDay < 0)
                {
                    intOfDay = intOfDay * -1;
                }
                Busylist[i].qty_left = intOfDay.ToString();

                if (intOfDay == 0 )
                {
                    Busylist[i].str_date_logout = "اليوم"; // alyoom 
                }
                else  if (numberOfDays >=1 && numberOfDays<2)
                {
                    Busylist[i].str_date_logout = "غدا"; // ghaden 
                }
                else if (numberOfDays >= 2 && numberOfDays < 3)
                {
                    Busylist[i].str_date_logout = "بعد غدا "; // bad ghd  
                }
                else if (numberOfDays >= 3 && numberOfDays < 3)
                {
                    Busylist[i].str_date_logout = futurDate.ToShortDateString();  
                }
                else if (numberOfDays <= -1 && numberOfDays > -2)
                {
                    Busylist[i].str_date_logout = "امس"; // Ams 
                }
                else if (numberOfDays <= -2 && numberOfDays > -3)
                {
                    Busylist[i].str_date_logout = "قبل امس"; // qabl ams 
                }
                else if (numberOfDays <= -3 && numberOfDays > -30)
                {
                    Busylist[i].str_date_logout = " منذ " + intOfDay + " ايام ماضية "; // midho    ayam 
                }
                else
                {
                    Busylist[i].str_date_logout = futurDate.ToShortDateString();
                }
              
            }



            HomeReceptionViewModel model = new HomeReceptionViewModel
            {
                countAll = li.Count(),
                //countEmpty = empty,
                //countClean = clean,
                //countRepair = Repair,
                //countReservation_without_entry = Reservation_without_entry,
                countBusy = Busy,
                RoomsStatusItems = li
            };
            //ListStatusCurrent
            return View(model);
        }


        

        public IActionResult ListReception()
        {
            ReceptionService rs = new ReceptionService(_db);
            List<ReceptionViewModel> li = rs.List(1);
            return View(li);
        }





        public async Task<IActionResult> ChangeStatus(int id)
        {
            Status_RoomService rs = new Status_RoomService(_db);
            var mo = rs.Find(id, 1);

            ViewData["IdType"] = new SelectList(_db.TypeRoomsTables, "Id", "NameT", mo.RoomModel.IdType);

            return View(mo);

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

            List<ReceptionViewModel> Parts = new List<ReceptionViewModel>();
           

           

                var model =await  rs.ListDataTable(idsub, param);
            Parts = model.list;


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
                //Status_RoomService rs = new Status_RoomService(_db);
                //List<Status_Current_RoomViewModel> li = rs.ListStatusCurrentB(1);

                //int empty = li.Where(x => x.Status == "1").Count();
                //int clean = li.Where(x => x.Status == "2").Count();
                //int Repair = li.Where(x => x.Status == "3").Count();
                //int Reservation_without_entry = li.Where(x => x.Status == "4").Count();
                //int Busy = li.Where(x => x.Status == "5").Count();

                //HomeReceptionViewModel model52 = new HomeReceptionViewModel
                //{
                //    countAll = li.Count(),
                //    countEmpty = empty,
                //    countClean = clean,
                //    countRepair = Repair,
                //    countReservation_without_entry = Reservation_without_entry,
                //    countBusy = Busy,
                //    RoomsStatusItems = li
                //};
              
                return Json(new { isValid = true });

                //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            else
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index") });
            }




        }


    }
}