using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    [Authorize]

  //  [Authorize(Roles = "admin,accounting,ITmanager")]
    public class _CompanyController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public _CompanyController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: CompanyTables
        public async Task<IActionResult> Index()
        {
            var model = _db.CompanyTables
                .Select(x=>new _CompanyViewModel 
                {
                    IdCo = x.Id,
                    NameCo = x.Name,
                    IdAccountCo = x.IdAccount,
                    IdSub=x.IdSub
                }).
                ToList();
            return View(model);
        }



        //DataTable
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param)
        {
            
            var searchText = param.sSearch;

         var  limit = param.iDisplayLength;
          var  offset = param.iDisplayStart;

           
              var  Parts = _db.CompanyTables
                .Select(x => new _CompanyViewModel
                {
                    IdCo = x.Id,
                    NameCo = x.Name,
                    IdAccountCo = x.IdAccount,
                    IdSub = x.IdSub
                }).
                 OrderByDescending(x => x.IdCo).
                                   Skip(offset).
                                    Take(limit).

                ToList();



            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = Parts.Where(x => x.NameCo.ToLower().Contains(searchText.ToLower())
                                              
                                              ).ToList();
            }


            //Parts.ToList().ForEach(x => x.Date = x.Date.ToString("dd'/'MM'/'yyyy"));

            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.IdCo).ToList() : Parts.OrderByDescending(c => c.IdCo).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.NameCo).ToList() : Parts.OrderByDescending(c => c.NameCo).ToList();
            }
           
          
          
               var totalRecords = _db.CompanyTables.Count();
          
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }


        public IActionResult Create()
        {
            
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(_CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {

                CompanyService cs = new CompanyService(_db);
                var iden = cs.CreateAsync(model);

             

               
                return RedirectToAction(nameof(Index));
            }
         
            return View(model);
        }


        [HttpPost]
       
        public async Task<JsonResult> Createjson(_CompanyViewModel model)
        {
          
                CompanyService cs = new CompanyService(_db);
            TowIdInt iden = await cs.CreateAsync(model);


            return Json(new {
                id= iden.ID,
                idacc= iden.ID2,
                name =model.NameCo

            });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyTable =  _db.CompanyTables.Find(Convert.ToInt32( id));
            if (companyTable == null)
            {
                return NotFound();
            }
            //ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", companyTable.IdAccount);

            _CompanyViewModel model = new _CompanyViewModel
            {
                IdCo = companyTable.Id,
                IdAccountCo = companyTable.IdAccount,
                IdSub = companyTable.IdSub,
                NameCo = companyTable.Name
            };
            return View(model);
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,IdAccount,IdSub")] _CompanyViewModel model)
        {
            //if (id != model.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var company = _db.CompanyTables.Find(Convert.ToInt32(model.IdCo));

                    company.Name = model.NameCo;
                    await _db.UpdateAsync(company);


                  
                }
                catch (Exception ee)
                {
                    
                        return NotFound();
                   
                }
                return RedirectToAction(nameof(Index));
            }
           // ViewData["IdAccount"] = new SelectList(_context.AccountTable, "Id", "Name", companyTable.IdAccount);
            return View(model);
        }

        // GET: CompanyTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyTable = _db.CompanyTables.Find(Convert.ToInt32(id));


            if (companyTable == null)
            {
                return NotFound();
            }

            _CompanyViewModel model = new _CompanyViewModel
            {
                IdCo = companyTable.Id,
                IdAccountCo = companyTable.IdAccount,
                IdSub = companyTable.IdSub,
                NameCo = companyTable.Name
            };

            return View(model);
        }

        // POST: CompanyTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = _db.CompanyTables.Find(Convert.ToInt32(id));

          await  _db.DeleteAsync(model);
           
            return RedirectToAction(nameof(Index));
        }

      


    }
}