using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    public class H_AreaController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public H_AreaController(HotelAlkheerDB context)
        {
            _db = context;


        }

        // GET: Transaction
        public IActionResult Index()
        {

            //var  list = _db.AreaTables.
            //     Select(x=>new AreaViewModel
            //     {
            //         Id = x.Id,
            //         Name = x.Name,
            //         IdCity = x.IdCity,
            //         NameCity=x.Areatablecitytable.Name,
            //         countCustomer=  x.Customertableareatables.Count()

            //     }).
            //     ToList();

            List<CountryTable> ci = new List<CountryTable>();
            ci.Add(new CountryTable
            {
                Id = -1,
                Name = "اختر"
            });

            var ciT = _db.CountryTables.ToList();


            ci.AddRange(ciT);
            int idC = -1;



            ViewData["IdCountry"] = new SelectList(ci, "Id", "Name");

            return View();
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var model = new AreaTable();


            if (id != 0)
            { 
                model =  _db.AreaTables.Find(id);
                if (model == null)
                {
                    return NotFound();
                }
                
            }

            List<CityTable> ci = new List<CityTable>();
            ci.Add(new CityTable
            {
                Id = -1,
                Name = "اختر"
            });
           
                var ciT = _db.CityTables.ToList();
           

            ci.AddRange(ciT);
            int idC = -1;
            if (model.IdCity != null)
            {
                idC = model.IdCity;
            }


            ViewData["IdCity"] = new SelectList(ci, "Id", "Name", idC);

            return View(model);
        }

        public async Task<String> GetByCity(int id)
        {
            var cs = new AreaService(_db);

            var models = cs.getByCityId(id);

            var json = JsonSerializer.Serialize(models);
            return json;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, AreaTable areaTable)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _db.Insert(areaTable);
                }
                //Update
                else
                {
                    try
                    {
                        _db.Update(areaTable);
                    }
                    catch (Exception ee)
                    {
                        if (!TransactionModelExists(areaTable.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
              
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", areaTable) });
        }

        
        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var areaTable = _db.AreaTables.Find(id);

          int s=  _db.Delete(areaTable);
            var list = _db.AreaTables.
              Select(x => new AreaViewModel
              {
                  Id = x.Id,
                  Name = x.Name,
                  IdCity = x.IdCity,
                  NameCity = x.Areatablecitytable.Name,
                  countCustomer = x.Customertableareatables.Count()

              }).
              ToList();

            if (s> 0)
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", list) });
            }
            else
            {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_ViewAll", list) });

            }

        }


        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param,int id)
        {

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };

            AreaService cu = new AreaService(_db);

            IEnumerable<AreaViewModel> lstData;
            int totalRecords = 0;


            var mo = await cu.DTbyIDcntry_City(para, id);

            lstData = mo.list;
            totalRecords = mo.countRow;

            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.Id).ToList() : lstData.OrderByDescending(c => c.Id).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.Name).ToList() : lstData.OrderByDescending(c => c.Name).ToList();
            }
            else if (sortColumnIndex == 2)
            {
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.countCustomer).ToList() : lstData.OrderByDescending(c => c.countCustomer).ToList();
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



            var pageNumber = para.pageNumber;



            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = lstData
            });






            //return pageInfo;
        }


        private bool TransactionModelExists(int id)
        {
            return _db.AreaTables.Any(e => e.Id == id);
        }
    }
}
