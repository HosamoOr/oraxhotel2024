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
    public class H_CityController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public H_CityController(HotelAlkheerDB context)
        {
            _db = context;


        }

        // GET: Transaction
        public IActionResult Index()
        {

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


            CityTable model=new CityTable();
            List<CountryTable> ci = new List<CountryTable>();
            ci.Add(new CountryTable
            {
                Id = -1,
                Name = "اختر"
            });

            var ciT = _db.CountryTables.ToList();


            ci.AddRange(ciT);
            int idC = -1;
            if (model.IdCountry != null)
            {
                idC = model.Id;
            }


            ViewData["IdCountry"] = new SelectList(ci, "Id", "Name", idC);

            if (id == 0)
                return View(model);
            else
            {
                model =  _db.CityTables.Find(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        public async Task<String> GetCitys(int id_Sub)
        {
            var cs = new CityService(_db);

            var models = cs.getList();

            var json = JsonSerializer.Serialize(models);
            return json;
        }
        public async Task<String> GetBycountry(int id)
        {
            var cs = new CityService(_db);

            var models = cs.getBycountryId(id);

            var json = JsonSerializer.Serialize(models);
            return json;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, CityTable cityTable)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    
                    try
                    {
                        _db.InsertWithIdentity(cityTable);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //Update
                else
                {
                    try
                    {
                        _db.Update(cityTable);
                    }
                    catch (Exception ee)
                    {
                        if (!TransactionModelExists(cityTable.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }

              
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", cityTable) });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var cityTable = _db.CityTables.Find(id);

            int s = _db.Delete(cityTable);


            if (s > 0)
            {

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            else
            {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_ViewAll") });

            }


        }

        //// POST: Transaction/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var cityTable = _db.CityTables.Find(id);

        //  int s=  _db.Delete(cityTable);
           

        //    if (s> 0)
        //    {
              
        //        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
        //    }
        //    else
        //    {
        //        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_ViewAll", cityTable) });

        //    }

        //}
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param, int id)
        {

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };

            CityService cu = new CityService(_db);

            IEnumerable<CityViewModel> lstData;
            int totalRecords = 0;


            var mo = await cu.DTbyIDcntry(para, id);

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
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.countArea).ToList() : lstData.OrderByDescending(c => c.countArea).ToList();
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



        public async Task<ActionResult> GetDataFarzAsync(paramModel para)
            {
                
              
                var searchText = para.search;

               
                var totalRecords = 0;
                int limit = para.limit;
            int offset = para.offset;

           
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = para.sort;
            String order = para.order;

            int idcountry = para.userid;

            CityService cu = new CityService(_db);

            IEnumerable<CityViewModel> lstData;

            
                var mo = await cu.DTbyIDcntry(para, idcountry);

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
                lstData = sortDirection == "asc" ? lstData.OrderBy(c => c.countArea).ToList() : lstData.OrderByDescending(c => c.countArea).ToList();
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


                sort = sort,
                order = order,
                searchText = "%" + searchText + "%",
                page = pageNumber,


                pageSize = para.pageSize,
                pageNumber = pageNumber,

                total = totalRecords,


                list = lstData

            });




            //return pageInfo;
        }



        private bool TransactionModelExists(int id)
        {
            return _db.CityTables.Any(e => e.Id == id);
        }
    }
}
