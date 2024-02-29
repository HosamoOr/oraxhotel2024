using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModels;
using LinqToDB;
using HotelSys.ViewModel;
using HotelSys.Accounting_Layer;
using Newtonsoft.Json;

namespace HotelSys.Controllers
{
    public class GroupServicesController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public GroupServicesController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: GroupServicesTables
        public async Task<IActionResult> Index()
        {
            return View( _db.GroupServicesTables.ToList());
        }




        // GET: GroupServicesTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupServicesTable =  _db.GroupServicesTables
                .Where(m => m.Id == id).FirstOrDefault();
            if (groupServicesTable == null)
            {
                return NotFound();
            }

            return View(groupServicesTable);
        }

        // GET: GroupServicesTables/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: GroupServicesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupServicesTable groupServicesTable)
        {
            if (groupServicesTable.Id==0)
            {
               int id1= _db.InsertWithInt32Identity(groupServicesTable);

                return Json(new {
                id=id1,
                mass="تم اضافة الخدمة بنجاح"
                });
            }
            else
            {
              int coutUp=  _db.Update(groupServicesTable);
                if(coutUp>0)

                return Json(new
                {
                    id = coutUp,
                    mass = "تم تعديل الخدمة بنجاح"
                });
            }
              return Json(new
            {
                id = 0,
                mass = "حدث خطاء اثناء حفظ البيانات"
            });
        }

        //DataTable
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param)
        {
            var gs = new GroupService(_db);


            var Parts = await gs.ListDT(param);


            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Id).ToList() : Parts.OrderByDescending(c => c.Id).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Name).ToList() : Parts.OrderByDescending(c => c.Name).ToList();
            }



            var totalRecords = _db.GroupServicesTables.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }



        public async Task<String> GetDataFarz2Async()
        {
            var gs = new GroupService(_db);


            var Parts = await gs.ListAll();

          //  var ss = HttpContext.Request.QueryString.Value;

           // var sortColumnIndex = param.iSortCol_0;         
            //if (sortColumnIndex == 0)
            //{
                
            //    Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Id).ToList() : Parts.OrderByDescending(c => c.Id).ToList();
                
            //}
            //else if (sortColumnIndex == 1)
            //{
            //    Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Name).ToList() : Parts.OrderByDescending(c => c.Name).ToList();
            //}



            var totalRecords = _db.GroupServicesTables.Count();

            var output = JsonConvert.SerializeObject(Parts);

      

            return output;



            //    Json(new


            //{
            //    iTotalRecords = totalRecords,
            //    iTotalDisplayRecords = totalRecords,
            //    aaData = Parts
            //});

        }




        // GET: GroupServicesTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupServicesTable =  _db.GroupServicesTables.Find(Convert.ToInt32( id));
            if (groupServicesTable == null)
            {
                return NotFound();
            }
            return View(groupServicesTable);
        }

        // POST: GroupServicesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
      
       

        // POST: GroupServicesTables/Delete/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(GroupServicesTable groupServicesTable)
        {
            var groupServicesTable1 = _db.GroupServicesTables.Find(Convert.ToInt32(groupServicesTable.Id));

           int count= await _db.DeleteAsync(groupServicesTable1);
            if(count>0)
            {
                return Json(new
                {
                    id = count,
                    mass = "تم حذف الخدمة بنجاح"
                });

            }
            else
            {
                return Json(new
                {
                    id = 0,
                    mass = "حدث خطا ما اثناء حذف البيانات"
                });
            }
            
            //return RedirectToAction(nameof(Index));
        }

        private bool GroupServicesTableExists(int id)
        {
            return _db.GroupServicesTables.Any(e => e.Id == id);
        }
    }
}
