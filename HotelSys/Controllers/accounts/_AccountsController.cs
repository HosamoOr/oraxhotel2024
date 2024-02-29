using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text.Json;
using HotelSys.Accounting_Layer.Bill;
using DevExtreme.AspNet.Mvc;

namespace HotelSys.Controllers.accounts
{
    [Authorize]
    public class _AccountsController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public _AccountsController(HotelAlkheerDB context)
        {
            _db = context;


        }

        public IActionResult Index()
        {
            _AccountService as_s = new _AccountService(_db);
            List<_AccountViewModel> li = as_s.List();
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name");
            return View(li);
        }

        public IActionResult Create()
        {
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(_AccountViewModel accountModel)
        {
            if (ModelState.IsValid)
            {
                //_AccountService as_s = new _AccountService(_db);
                //await as_s.CreateAsync_(accountModel);

                var modelTree = new TreeAccountViewModel
                {
                    Code = accountModel.Code,
                    Id=accountModel.Id.ToString(),
                    Name = accountModel.Name,

                    IdGroup=accountModel.IdGroup.ToString(),
                    
                };


                _TreeAccountService trs = new _TreeAccountService(_db);
               var idInsert = await trs.AddOrEditAccTreeAsync(modelTree);


                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name", accountModel.IdGroup);
            return View(accountModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _db.AccountTables.Find(Convert.ToInt32(id));


            if (model == null)
            {
                return NotFound();
            }
            var mo = new _AccountViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Createat = model.Createat,
                IdGroup = model.IdGroup,
                Status = model.Status,

                Name = model.Name,
                IsPrivate = model.IsPrivate,


            };
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name", mo.IdGroup);
            return View(mo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(_AccountViewModel accountModel)
        {
            if (ModelState.IsValid)
            {
                _AccountService as_s = new _AccountService(_db);
                await as_s.Edit(accountModel);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name", accountModel.IdGroup);
            return View(accountModel);
        }

        public async Task<string> GetBoxs(int id_Sub)
        {
            var models = _db.AccountTables
                .Where(x => x.IdGroup == Static_Group_Accounts.Boxs)
                .Select(xx => new _AccountShortViewModel
                {
                    Id = xx.Id,
                    Name = xx.Name
                })
                .ToList();


            var json = JsonSerializer.Serialize(models);
            return json;
        }




        public async Task<ActionResult> GetDataAsync(JqueryDatatableParam param)
        {
            var gs = new _AccountService(_db);

            List<_AccountViewModel> Parts = new List<_AccountViewModel>();


            var modell = await gs.ListDT(param);


            Parts = modell.list;



            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 1)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Id).ToList() : Parts.OrderByDescending(c => c.Id).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 2)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Name).ToList() : Parts.OrderByDescending(c => c.Name).ToList();
            }




            var totalRecords = modell.countRow;



            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }



    }
}