using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.DataSources;
using HotelSys.Models;
using HotelSys.ViewModel;
using HotelSys.ViewModel.RPT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HotelSys.Controllers
{
    public class ReportCustomersController : Controller
    {
        private readonly HotelAlkheerDB _db;
        int _idSub = 1;
        public ReportCustomersController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult CustInHotel()
        {


           
           var ss= CultureInfo.GetCultureInfo("ar-BH").DateTimeFormat.DayNames;

          var datee=  DateTime.UtcNow.ToString("D", new CultureInfo("ar-AE"));


            DateTime current= DateTime.Now;

           var datee2= current .ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));

           


            //string nameDay = weekDaysAr[current.Day];
            //string nameMount = weekDaysAr[current.Month];

            String nameYear = current.Year.ToString();


            List<String> valueDay=new List<String>();

          //var init= current.AddDays(-7);

            for(int i = 0;i<7;i++)
            {
                
                var datee3 = current.AddDays(i*-1).ToString("dddd dd, MMMM, yyyy", new CultureInfo("ar-AE"));

               

                valueDay.Add(datee3);
            }

            ViewData["valueDay"] = new SelectList(valueDay);





            return View();
           
        }

        public IActionResult Viewer(DateTime ?fromDate,DateTime? toDate)
        {
            if(fromDate == null || toDate == null)
            {
                fromDate = DateTime.Now;
                toDate = DateTime.Now;
            }
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            return View();
        }
        public IActionResult IndexClientBalance()
        {
            return View();
        }
        public IActionResult Viewer_clientBalabc()
        {
            
            return View();
        }

        public IActionResult ViewerCuInHoNow()
        {

            return View();
        }
        
        public async Task<ActionResult> GetDataFarzAsync(paramModel request)
        {


            String searchText = request.search;
           
            String sort = request.sort;
            String order = request.order;

            int iduser = request.userid;

            ReceptionService rs=new ReceptionService(_db);
            DateTime st=DateTime.Now;
            DateTime en=DateTime.Now;

            //الساكنين الان
           // List<ReceptionViewModel> lst = await rs.List_In_Hotel(_idSub, request);
            
          
             List<ReceptionViewModel> lst =await rs.ALL_between_dates(_idSub, request, st, en);

            var totalRecords = _db.RecetionTables.
               Where(x => x.IsChechin == true).Where(x => x.CheckinDate >= st && x.CheckinDate <= en).ToList().Count;


            var pageNumber = request.pageNumber;


            return Json(new
            {


                sort = sort,
                order = order,
                searchText = "%" + searchText + "%",
                page = pageNumber,


                pageSize = request.pageSize,
                pageNumber = pageNumber,

                total = totalRecords,


                list = lst

            });

            //return pageInfo;
        }

        public async Task<ActionResult> GetDataFarz5Async(paramModel request)
        {


            String searchText = request.search;

            String sort = request.sort;
            String order = request.order;

            int iduser = request.userid;

            ReceptionService rs = new ReceptionService(_db);
            DateTime st = DateTime.Now;
            DateTime en = DateTime.Now;

            clientsBalanceDS cbs = new clientsBalanceDS(_db);
            ClientsBalanceViewModel model = cbs.listPara(1, request);





            var totalRecords = model.count;

            var pageNumber = request.pageNumber;


            return Json(new
            {


                sort = sort,
                order = order,
                searchText = "%" + searchText + "%",
                page = pageNumber,


                pageSize = request.pageSize,
                pageNumber = pageNumber,

                total = totalRecords,


                list = model.items

            });

            //return pageInfo;
        }


    }
}