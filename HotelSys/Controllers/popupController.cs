using DataModels;
using HotelSys.Compnet;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    public class popupController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public popupController(HotelAlkheerDB context)
        {
            _db = context;


        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {

            List<TransactionModel> list = new List<TransactionModel>();

            list.Add(new TransactionModel
            {
                AccountNumber = "",
                Amount = 0,
                BankName = "b",
                BeneficiaryName = "bb",
                SWIFTCode = "s",
                TransactionId = 0,
                Date = DateTime.Now,

            });

            return View(list);
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new TransactionModel());
            else
            {
                var transactionModel =  _db.UserTables.Find(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    transactionModel.Date = DateTime.Now;
                   

                }
                //Update
                else
                {
                    try
                    {
                        
                    }
                    catch (Exception ee)
                    {
                        if (!TransactionModelExists(transactionModel.TransactionId))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll" ) });
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

            var transactionModel = new TransactionModel
            {
                AccountNumber = "",
                Amount = 0,
                BankName = "b",
                BeneficiaryName = "bb",
                SWIFTCode = "s",
                TransactionId = 0,
                Date = DateTime.Now,

            };
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
            
            return View();
        }

        private bool TransactionModelExists(int id)
        {
            return _db.UserTables.Any(e => e.Id == id);
        }
    }
}
