using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.Compnet;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{

   // [Authorize(Roles = "admin")]

    public class BoxssController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public BoxssController(HotelAlkheerDB context)
        {
            _db = context;


        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {

            var model = _db.BoxsTables.Select(x => new BoxViewModel
            {
                Name = x.Name,
                IsPrivate = x.IsPrivate,
                IsDefault = Convert.ToBoolean( x.IsDefault),
                IdAccount = x.IdAccount,
                Id = x.Id,
                IdSub = x.IdSub

            }).ToList();
            return View(model);
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new BoxViewModel
                {
                    Id = 0,
                    IdAccount=0,
                    IsDefault=false,
                    IsPrivate=false

                };
                return View(model);
            }
                
            else
            {
                var modelTable =  _db.BoxsTables.Find(id);


                if (modelTable == null)
                {
                    return NotFound();
                }
                var model = new BoxViewModel
                {
                    Id = modelTable.Id,
                    IdAccount = modelTable.IdAccount,
                    IsDefault = Convert.ToBoolean(modelTable.IsDefault),
                    IsPrivate = modelTable.IsPrivate,
                    Name= modelTable.Name,  
                    IdSub= modelTable.IdSub

                };
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, BoxViewModel model)
        {
           

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    // transactionModel.Date = DateTime.Now;

                    BoxService bs=new BoxService(_db);
                  int idAcc=await  bs.AddAsync(model);


                }
                //Update
                else
                {
                    try
                    {

                        BoxService bs = new BoxService(_db);
                        int st = await bs.Edit(id, model);
                        if(st == -1)
                        {
                            return NotFound();
                        }


                    }
                    catch (Exception ee)
                    {
                        if (!TransactionModelExists(model.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }

                var modelist = _db.BoxsTables.Select(x => new BoxViewModel
                {
                    Name = x.Name,
                    IsPrivate = x.IsPrivate,
                    IsDefault = Convert.ToBoolean(x.IsDefault),
                    IdAccount = x.IdAccount,
                    Id = x.Id,
                    IdSub = x.IdSub

                }).ToList();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", modelist) });
                
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            _AccountService as_s = new _AccountService(_db);
            

            var model = _db.BoxsTables.Find(Convert.ToInt32( id));
            var stDelAc = await as_s.delete(model.IdAccount);


           



            if (stDelAc <= 0)
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            else
            {
                //int st = _db.Delete(model);

                var modelist = _db.BoxsTables.Select(x => new BoxViewModel
                {
                    Name = x.Name,
                    IsPrivate = x.IsPrivate,
                    IsDefault = Convert.ToBoolean(x.IsDefault),
                    IdAccount = x.IdAccount,
                    Id = x.Id,
                    IdSub = x.IdSub

                }).ToList();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", modelist) });
            }


        }

        // POST: Transaction/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{

        //    return View();
        //}

        private bool TransactionModelExists(int id)
        {
            return _db.UserTables.Any(e => e.Id == id);
        }
    }
}
