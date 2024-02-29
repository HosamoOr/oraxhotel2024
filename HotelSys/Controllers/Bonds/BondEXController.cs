using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    [Authorize]
    public class BondEXController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public BondEXController(HotelAlkheerDB context)
        {
            _db = context;
        }

        public ActionResult Index()
        {
            
            return View();
        }

        // GET: BondQController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BondQController/Create

        [NoDirectAccess]
        public ActionResult Create()
        {
            var acc = new List<AccountTable>();

            acc.Add(new AccountTable
            {
                Id = -1,
                Name = "اختر حساب"

            });

            acc.AddRange(_db.AccountTables.ToList());


            ViewData["IdAccount"] = new SelectList(acc, "Id", "Name");


            var boxAccount = _db.AccountTables.Where(x => x.IdGroup == Static_Group_Accounts.Boxs).ToList();


            ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name", Static_Accounts.Main_Box);


            ViewData["IdBill"] = new SelectList(_db.BillsTables, "Id", "Type");
            ViewData["IdBondPay"] = new SelectList(_db.BondTables, "Id", "ReceiptsOrExpenses");
            ViewData["IdItemExpenses"] = new SelectList(_db.ItemsExpensesTables, "Id", "Name");

            BondViewModel model = new BondViewModel
            {
                Id = 0,
                Date = DateTime.Now,
                Time = DateTime.Now.TimeOfDay

            };

            return View(model);
        }

        // POST: BondQController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<ActionResult> Create(BondViewModel modelForm)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(modelForm);
                }

                
                else
                {
                    BondController bc = new BondController(_db);

                   if(modelForm.IdReception == null)
                    {
                        AccReceptionController arc=new AccReceptionController(_db);

                     var inRoom=   arc.GetReceptionByIdAccAsModel(modelForm.IdAccount);
                        if(inRoom != null)
                        {
                            modelForm.IdReception = inRoom.IdReception;
                            modelForm.Note = " (سند صرف للعميل - الشقة "+ inRoom.RoomModel.NameR+"("+ inRoom.IdRoom+") "; 
                        }
                    }

                    var js = await bc.Createjson(modelForm);



                    return js;
                }

              
            }
            catch
            {
                return View();
            }
        }


        [NoDirectAccess]
        public async Task<IActionResult> ListReceptioSQ()
        {
            return View();

        }
        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(long id = 0)
        {
            var acc = new List<AccountTable>();

            
                acc.Add(new AccountTable
                {
                    Id = -1,
                    Name = "اختر حساب"

                });
            
          
            acc.AddRange(_db.AccountTables.ToList());

            var boxAccount = _db.AccountTables.Where(x => x.IdGroup == Static_Group_Accounts.Boxs).ToList();

            int idBoxAccountTo = Static_Accounts.Main_Box;


            //ViewData["IdBill"] = new SelectList(_db.BillsTables, "Id", "Type");
            //ViewData["IdBondPay"] = new SelectList(_db.BondTables, "Id", "ReceiptsOrExpenses");
            //ViewData["IdItemExpenses"] = new SelectList(_db.ItemsExpensesTables, "Id", "Name");




            if (id == 0)
            {
                ViewData["IdAccount"] = new SelectList(acc, "Id", "Name");
                ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name", idBoxAccountTo);

               
                BondViewModel model = new BondViewModel
                {
                    Id = 0,
                    Date = DateTime.Now,
                    Time = DateTime.Now.TimeOfDay

                };


                return View(model);
            }
               
            else
            {
                var service = new BondService(_db);

                var model =  service.getOne(id);


                idBoxAccountTo = model.id_accountTo;

               // model.id_accountForm = model.IdAccount; 


                if (model == null)
                {
                    return NotFound();
                }
                ViewData["IdAccount"] = new SelectList(acc, "Id", "Name", model.id_accountForm);
                ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name", idBoxAccountTo);


                return View(model);
            }
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(long id, BondViewModel transactionModel)
        {

           // 


            if (ModelState.IsValid )
            {

                transactionModel.IdAccount = transactionModel.id_accountForm;

                transactionModel.Createat = DateTime.Now;

                if (transactionModel.Time == null)
                {
                    transactionModel.Time = DateTime.Now.TimeOfDay;
                }
                //Insert
                if (id == 0)
                {
                  

                    BondController bc = new BondController(_db);

                    if (transactionModel.IdReception == null)
                    {
                        AccReceptionController arc = new AccReceptionController(_db);

                        var inRoom = arc.GetReceptionByIdAccAsModel(transactionModel.IdAccount);
                        if (inRoom.Id != 0)
                        {
                            transactionModel.IdReception = inRoom.IdReception;
                            transactionModel.Note = " (سند صرف للعميل - الشقة " + inRoom.RoomModel.NameR + "(" + inRoom.IdRoom + ") ";
                        }
                    }

                    var js = await bc.Createjson(transactionModel);



                }
                //Update
                else
                {
                    try
                    {
                        BondController bc = new BondController(_db);
                        var js = await bc.Updatejson(transactionModel);
                    }
                    catch (Exception e)
                    {
                        if (!TransactionModelExists(transactionModel.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }

          

            var acc = new List<AccountTable>();

            acc.Add(new AccountTable
            {
                Id = -1,
                Name = "اختر حساب"

            });
            acc.AddRange(_db.AccountTables.ToList());

            var boxAccount = _db.AccountTables.Where(x => x.IdGroup == Static_Group_Accounts.Boxs).ToList();

          //  int idBoxAccountFrom = Static_Accounts.Main_Box;


            //ViewData["IdBill"] = new SelectList(_db.BillsTables, "Id", "Type");
            //ViewData["IdBondPay"] = new SelectList(_db.BondTables, "Id", "ReceiptsOrExpenses");
            //ViewData["IdItemExpenses"] = new SelectList(_db.ItemsExpensesTables, "Id", "Name");

            ViewData["IdAccount"] = new SelectList(acc, "Id", "Name");
            ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name");






            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }


        // GET: BondQController/Edit/5
        public ActionResult Edit(long id)
        {
            return View();
        }

        // POST: BondQController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> GetDataAsyncAll(JqueryDatatableParam param)
        {
            var gs = new BondService(_db);

            List<BondViewModel> Parts = new List<BondViewModel>();


            var modell = await gs.ListDT(param,"2");


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
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.NumReference).ToList() : Parts.OrderByDescending(c => c.NumReference).ToList();
            }
            else if (sortColumnIndex == 3)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.IdReception).ToList() : Parts.OrderByDescending(c => c.IdReception).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.nameCustomer).ToList() : Parts.OrderByDescending(c => c.nameCustomer).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Amount).ToList() : Parts.OrderByDescending(c => c.Amount).ToList();
            }
            else if (sortColumnIndex == 6)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Why).ToList() : Parts.OrderByDescending(c => c.Why).ToList();
            }
            else if (sortColumnIndex ==7)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Date).ToList() : Parts.OrderByDescending(c => c.Date).ToList();
            }
            else if (sortColumnIndex == 8)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.TypePay).ToList() : Parts.OrderByDescending(c => c.TypePay).ToList();
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


        // GET: BondQController/Delete/5
        public async Task<ActionResult> Delete(long id)
        {
            var service = new BondService(_db);

            var model =await service.DeleteAsync(id);

            if(model.success!=false)
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            else
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }



           
        }

        // POST: BondQController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        private bool TransactionModelExists(long id)
        {
            return _db.BondTables.Any(e => e.Id == id);
        }
    }
}
