using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    public class H_CountryController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public H_CountryController(HotelAlkheerDB context)
        {
            _db = context;


        }

        // GET: Transaction
        public IActionResult Index()
        {

           var  list = _db.CountryTables.
                Select(x=>new CountyViewModel
                {
                    Id=x.Id,
                    Name=x.Name,
                    countCity = x.Citytablecountrytables.Count(),
                    

                }).
                ToList();

            return View(list);
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new CountryTable());
            else
            {
                try
                {
                    var transactionModel = _db.CountryTables.Find(id);
                    if (transactionModel == null)
                    {
                        return NotFound();
                    }
                    return View(transactionModel);
                }
                catch (Exception ex)
                {
                    return View(new CountryTable());
                }
              
            }
        }

        public async Task<String> GetCountrys(int id_Sub)
        {
            var cs = new CountryService(_db);

            var models = cs.getList();

            var json = JsonSerializer.Serialize(models);
            return json;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, CountryTable CountryTable)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _db.Insert(CountryTable);
                }
                //Update
                else
                {
                    try
                    {
                        _db.Update(CountryTable);
                    }
                    catch (Exception ee)
                    {
                        if (!TransactionModelExists(CountryTable.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }

                var list = _db.CountryTables.
                Select(x => new CountyViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    countCity = x.Citytablecountrytables.Count(),


                }).
                ToList();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll" , list) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", CountryTable) });
        }

        
        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CountryTable = _db.CountryTables.Find(id);

          int s=  _db.Delete(CountryTable);
            var list = _db.CountryTables.
             Select(x => new CountyViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 countCity = x.Citytablecountrytables.Count(),



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

        private bool TransactionModelExists(int id)
        {
            return _db.CountryTables.Any(e => e.Id == id);
        }
    }
}
