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
    public class CustomersController : Controller
    {
        private readonly HotelAlkheerDB _db;
        int idsub = 1;

        public CustomersController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public async Task<IActionResult> Index()
        {

            CustomerService cu = new CustomerService(_db);

            paramModel para = new paramModel
            {
                limit = 20,
                offset = 0,


            };

            var modL = await cu.ListMy(idsub, para);


            var list = modL.list;

            return View(list);
        }
        public async Task<IActionResult> del_Index()
        {
            return View();

        }


        public async Task<IActionResult> allCusomer()
        {

            //CustomerService cu = new CustomerService(_db);
            //paramModel p = new paramModel();
            //var list = await cu.ListAll(p);
            return View();
        }



        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create1()
        {
            // ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name");
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1(CustomerViewModel customerT)
        {
            if (ModelState.IsValid)
            {

                CustomerService cu = new CustomerService(_db);
                //  Int64 st=
                await cu.CreateAsync(customerT);


                return RedirectToAction(nameof(Index));
            }
            // ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", customerT.IdAccount);
            return View(customerT);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateJson(CustomerViewModel customerT)
        {
            // if (ModelState.IsValid)
            CustomerService cu = new CustomerService(_db);
            long st = customerT.IdcumtomerAll;

            string typeop = "ADD";
            ListIdLongAcc til = new ListIdLongAcc();
            til.IDs = new List<long>();

            if (customerT.Nationality == null)
            {
                customerT.Nationality = "";
            }

            if (customerT.Id_Area == -1)
            {
                customerT.Id_Area = null;
            }

            if (customerT.IdcumtomerAll == 0)
            {


                til = await cu.CreateAsync(customerT);

                st = til.IDs[0];

                return Json(new
                {

                    id = st,
                    idacc = til.IDs[1],
                    idmy = til.IDs[2],

                    name = customerT.Name,
                    privateNote = customerT.PrivateNote,
                    til.status,
                    mess = "New Customer successful"
                });
            }
            else
            {
                til = await cu.EditAll(customerT);

                typeop = "Update";
                return Json(new
                {

                    id = customerT.IdcumtomerAll,
                    idmy = customerT.IdmyCu,
                    idacc = customerT.IdAccount,
                    //idmy = til.IDs[2],

                    name = customerT.Name,
                    til.status,
                    //privateNote = customerT.PrivateNote,
                    mess = "Edit Custmer successful"
                });
            }

            if (til.status < 0)
            {

                return Json(new
                {

                    id = til.IDs[0],

                    idacc = til.IDs[1],
                    idmy = til.IDs[2],

                    name = til.modelCu.Name,
                    //privateNote = customerT.PrivateNote,
                    til.status,
                    mess = til.messege,
                    model = til.modelCu
                });

            }

            // ViewData["IdAccount"] = new SelectList(_db.AccountTables, "Id", "Name", customerT.IdAccount);
            return Json(new
            {
                id = st,
                mess = "حدث خطاء ما ادى الى عدم حفظ البيانات!"
            });
        }


        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(long id = 0)
        {


            if (id == 0)
            {

                CustomerViewModel model = new CustomerViewModel
                {
                    IdmyCu = 0,
                    IdcumtomerAll = 0,
                    IdAccount = 0,

                };


                return View(model);
            }

            else
            {
                CustomerService cu = new CustomerService(_db);

                var model = await cu.one(id);

                if (model == null)
                {
                    return NotFound();
                }


                return View(model);
            }
        }







        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerService cu = new CustomerService(_db);

            var model = await cu.one(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CustomerViewModel model)
        {
            if (id != model.IdmyCu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                try
                {

                    CustomerService cu = new CustomerService(_db);

                    var stat = await cu.Edit(id, model);
                    if (stat == 0)
                    {
                        // لم يتم التعديل
                    }
                    else
                    {
                        //Done
                    }
                }
                catch (Exception rrr)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: CustomerTables/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerService cu = new CustomerService(_db);

            var model = await cu.one(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerService cu = new CustomerService(_db);

            var model = await cu.oneForAll(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: CustomerTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customerTable = await _db.MyCustomers.Where(x => x.Id == id).FirstOrDefaultAsync();
            _db.Delete(customerTable);

            return RedirectToAction(nameof(Index));
        }

        //DataTable
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param, int id)
        {
            List<CustomerViewModel> Parts = new List<CustomerViewModel>();
            CustomerService cu = new CustomerService(_db);

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };
            var totalRecords = 0;

            if (id == 1)
            {

                var mo = await cu.ListMy(idsub, para);

                Parts = mo.list;
                totalRecords = mo.countRow;

            }
            else
            {


                Parts = await cu.ListAll(para);
                totalRecords = _db.CustomerTables.Count(); //error

            }


            //Parts.ToList().ForEach(x => x.Date = x.Date.ToString("dd'/'MM'/'yyyy"));

            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.TypeProof).ToList() : Parts.OrderByDescending(c => c.TypeProof).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.NumProof).ToList() : Parts.OrderByDescending(c => c.NumProof).ToList();
            }
            else if (sortColumnIndex == 2)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Name).ToList() : Parts.OrderByDescending(c => c.Name).ToList();
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





            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }
        public async Task<ActionResult> GetDataFarzAsync(paramModel request, int id)
        {

            int limit = request.limit;
            int offset = request.offset;

            string searchText = request.search;
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            string sort = request.sort;
            string order = request.order;

            long iduser = request.userid;

            CustomerService cu = new CustomerService(_db);

            IEnumerable<CustomerViewModel> lstData;

            var totalRecords = 0; //_db.CustomerTables.ToList().Count
            if (id == 1)
            {
                var mo = await cu.ListMy(idsub, request);
                lstData = mo.list;
                totalRecords = mo.countRow;
            }

            else
            {
                lstData = await cu.ListAll(request);
            }



            //if(iduser!=-1)
            //{
            //    lstData =  (from t1 in _db.RecoredingTables
            //                     from t2 in _db.PartfolioTables
            //                    // join t2 in _db.PartfolioTables on t1.PlateNumber equals t2.PlateNumber

            //                     where 
            //                    t1.PlateNumber == t2.PlateNumber && 
            //                     t1.Userid== iduser

            //                     select new RecoreingViewModel
            //                     {
            //                         Id = t1.Id,
            //                         PlateNumber = t1.PlateNumber,
            //                         Note = t1.Note,
            //                         Date = t1.Date,
            //                         Lat = t1.Lat,
            //                         mapGoogle = "http://maps.google.com/maps?q=" + t1.Lat + "," + t1.@long,
            //                         User = new UserViewModel
            //                         {
            //                             Fullname = t1.User.Fullname,
            //                             Username = t1.User.Username,

            //                         }
            //                     }).


            //                     OrderByDescending(x => x.Id).
            //                     Skip(offset).
            //                     Take(limit).

            //                     ToList().DistinctBy(p => p.Id);
            //}


            //Parts.ToList().ForEach(x => x.Date = x.Date.ToString("dd'/'MM'/'yyyy"));

            if (!string.IsNullOrEmpty(searchText))
            {
                //lstData = lstData.Where(x => x.PlateNumber.ToLower().Contains(searchText.ToLower())
                //                              || (x.User.Username != null && x.User.Username.ToLower().Contains(searchText.ToLower()))
                //                              || (x.User.Fullname != null && x.User.Fullname.ToLower().Contains(searchText.ToLower()))
                //                              || x.Id.ToString().Contains(searchText.ToLower())

                //                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                //                              || x.Date.ToLower().Contains(searchText.ToLower())
                //                              ).ToList();
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






            var pageNumber = request.pageNumber;


            return Json(new
            {


                sort,
                order,
                searchText = "%" + searchText + "%",
                page = pageNumber,


                request.pageSize,
                pageNumber,

                total = totalRecords,


                list = lstData

            });

            //return pageInfo;
        }


    }
}