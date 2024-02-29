using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HotelSys.Controllers
{
    public class RPT_Room_StatusController : Controller
    {
        private readonly HotelAlkheerDB _db;
        int _idSub =1;

        public RPT_Room_StatusController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {

            ViewData["fromDate"] = DateTime.Now;
            ViewData["toDate"] = DateTime.Now;

            return View();
           
        }

        public IActionResult Viewer(int id, DateTime? fromDate, DateTime? toDate, bool? isAll)
        {
            if (fromDate == null || toDate == null)
            {
                fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);


                toDate = DateTime.Now;
            }
            ViewData["id"] = id;
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            ViewData["isAll"] = isAll;


            return View();
        }
        public async Task<ActionResult> GetDataFarzAsync(paramModel request)
        {


            String searchText = request.search;

            String sort = request.sort;
            String order = request.order;

            int iduser = request.userid;

            Status_RoomService rs = new Status_RoomService(_db);
            DateTime st = DateTime.Now;
            DateTime en = DateTime.Now;

            //الساكنين الان
            // List<ReceptionViewModel> lst = await rs.List_In_Hotel(_idSub, request);


            ListStatusDT lst =  rs.ALL_between_dates(_idSub, request);

            var totalRecords = lst.countRow;


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


                list = lst.lisy

            });

            //return pageInfo;
        }





    }
}