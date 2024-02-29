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
    public class ReportReceptionController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public ReportReceptionController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
           
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);

            datesViewModel model = new datesViewModel
            {
                start = fromDate,
                end = DateTime.Now,
                isAll = true
            };



            return View(model);
           
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

            int limit = request.limit;
            int offset = request.offset;

            String searchText = request.search;
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = request.sort;
            String order = request.order;

            int id_account = request.userid;



            List<_ReportBalanceAccountViewModel> lstData = (from t1 in _db.GetBalanceAccount(id_account, 1)


                                                       select new _ReportBalanceAccountViewModel
                                                       {
                                                           id=t1.id,
                                                           billOrBandDocumnet=t1.billOrBandDocumnet,
                                                           dateDocumnet=t1.dateDocumnet,
                                                           FromPrice=t1.FromPrice,
                                                           ToPrice=t1.ToPrice,
                                                           debt_or_Credit=t1.debt_or_Credit,
                                                           IdDocument=t1.IdDocument,
                                                           id_account=t1.id_account,
                                                           NameAccount=t1.NameAccount,
                                                           id_currancy=t1.id_currancy,
                                                           typeDocumnet=t1.typeDocumnet,
                                                           Balance= t1.FromPrice-t1.ToPrice,
                                                           Note=t1.Note,
                                                          


                                                       }).OrderByDescending(x => x.id).
                    Skip(offset).
                   Take(limit).
                  // DistinctBy(p => p.Id).
                   ToList()
                   ;


            double sumBalan = 0;

            for(int i = 0; i < lstData.Count(); i++)
            {
                double sub =Convert.ToDouble(lstData[i].ToPrice+lstData[i].FromPrice  );

                sumBalan = sumBalan +  sub;

                lstData[i].Balance= sumBalan;
            }
            
            //Parts.ToList().ForEach(x => x.Date = x.Date.ToString("dd'/'MM'/'yyyy"));

            if (!string.IsNullOrEmpty(searchText))
            {
                lstData = lstData.Where(x => x.NameAccount.ToLower().Contains(searchText.ToLower())
                                              || (x.dateDocumnet != null && x.dateDocumnet.ToLower().Contains(searchText.ToLower()))
                                              || (x.IdDocument != null && x.IdDocument.ToString().Contains(searchText.ToLower()))
                                              
                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                             // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).ToList();
            }

            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            /*if (sortColumnIndex == 0)
            {
                //
                //lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.Id).ToList() : lstData.OrderByDescending(c => c.Id).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.PlateNumber).ToList() : lstData.OrderByDescending(c => c.PlateNumber).ToList();
            }*/


            var totalRecords = _db.GetBalanceAccount(id_account, 1).ToList().Count;


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


                list = lstData

            });

            //return pageInfo;
        }


    }
}