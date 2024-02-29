using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using static HotelSys.Compnet.Helper;
using DataModels;
using LinqToDB;
using HotelSys.ViewModel;
using HotelSys.BusnessLayer;
using HotelSys.Accounting_Layer;

namespace HotelSys.Controllers
{
    public class ItemsExpensesController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public ItemsExpensesController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: ItemsExpenses
        public async Task<IActionResult> Index()
        {
            return View( _db.ItemsExpensesTables.ToList());
        }

       
       
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ItemsExpensesTable
                {
                    Id = 0,
                    CreateAt = DateTime.Now,
                    
                });
            else
            {
                var transactionModel =  _db.ItemsExpensesTables.Find(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, ItemsExpensesTable transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    transactionModel.CreateAt = DateTime.Now;

                    _AccountViewModel modelAcc = new _AccountViewModel
                    {
                        Id = transactionModel.Id,
                        Name = transactionModel.Name,
                        IsPrivate = false,
                        IdGroup = Static_Group_Accounts.ItemsExpenses

                    };
                    _AccountService as_s = new _AccountService(_db);
                    int idAcunt = await as_s.CreateAsync(modelAcc);

                    transactionModel.IdAccount = idAcunt;

                    _db.Insert(transactionModel);
                   
                }
                //Update
                else
                {
                    try
                    {
                        var m=_db.ItemsExpensesTables.Find(id);

                        m.Name = transactionModel.Name;

                        _db.Update(transactionModel);


                        var a=_db.AccountTables.Find(transactionModel.IdAccount);
                        a.Name = transactionModel.Name;
                        _db.Update(a);
                       // await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemsExpensesTableExists(transactionModel.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.ItemsExpensesTables.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel =  _db.ItemsExpensesTables
                .FirstOrDefault(m => m.Id == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel =  _db.ItemsExpensesTables.Find(id);
           await _db.DeleteAsync(transactionModel);
          //  await _db.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.ItemsExpensesTables.ToList()) });
        }

        private bool ItemsExpensesTableExists(int id)
        {
            return _db.ItemsExpensesTables.Any(e => e.Id == id);
        }
    }
}
