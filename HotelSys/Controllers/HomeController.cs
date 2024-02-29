using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using DataModels;
using HotelSys.ViewModel.Home;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
//using HotelSys.Models;

namespace HotelSys.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HotelAlkheerDB _db;

       
        public HomeController(ILogger<HomeController> logger, HotelAlkheerDB context)
        {
            _logger = logger;
            _db = context;
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





            HomeViewModel model = new HomeViewModel();

            List<SimpleReportViewModel> dountData = new List<SimpleReportViewModel>();



            dountData.Add(new SimpleReportViewModel { 
                Quantity= empty,
                title="فارغ",
                color= "#008000"
            });

            dountData.Add(new SimpleReportViewModel
            {
                Quantity = Busy,
                title = "مؤجر",
                color = "#FF0000"
            });

            dountData.Add(new SimpleReportViewModel
            {
                Quantity = Repair,
                title = "صيانة",
                color = "#808080"
            });

            dountData.Add(new SimpleReportViewModel
            {
                Quantity = Reservation_without_entry,
                title = "حجز مؤقت",
                color = "#0000FF"
            });

           
            dountData.Add(new SimpleReportViewModel
            {
                Quantity = clean,
                title = "نظافة",
                color = "#FFA500"
            });

            model.dountData = dountData;

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult View1()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
