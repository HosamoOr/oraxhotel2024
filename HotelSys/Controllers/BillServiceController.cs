using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
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
    public class BillServiceController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public BillServiceController(HotelAlkheerDB context)
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

        [HttpPost]
        public async Task<JsonResult> Editjson(BillViewModel model)
        {

            bills_Service cs = new bills_Service(_db);

            Value_Return vr = new Value_Return();

            string note = " فاتورة خدمات: الاصناف: ";

            //if (model.Type == "7")//service
            //{
               

                for (int i = 0; i < model.Items.Count; i++)
                {
                    var nu = i + 1;

                    note = note + nu + "-" + model.Items[i].NameProduct + "[" + model.Items[i].Qty + "]" + model.Items[i].Total + "--";


                }

            

            model.Note = note;

          

            try
            {
                vr = await cs.UpdateInvService(model);
            }

            catch (Exception ex)
            {
                var me = ex.Message;
                return Json(new
                {
                    id = 0,
                    name = "error ",
                    mass = me

                });
            }


            int ss = 0;
            return Json(new
            {
                id = 2,
                name = "Edit ",
                mass = vr.message

            });
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

            int idBoxAccountFrom = Static_Accounts.Main_Box;



            if (id == 0)
            {

                ReceptionServiceController rsr = new ReceptionServiceController(_db);

                var isInvSerTax =await rsr.GetInitBill_New();


                BillViewModel model = new BillViewModel
                {
                    Id = 0,
                    Date = DateTime.Now,
                    IncludeTax= isInvSerTax.ServicesIncludeTax,
                    

                };


                return View(model);
            }
               
            else
            {
                var service = new bills_Service(_db);

                var model =  service.getOne(id);
                idBoxAccountFrom = model.id_accountForm;

                model.id_accountTo = model.IdAccount;


                if (model == null)
                {
                    return NotFound();
                }
                ViewData["IdAccount"] = new SelectList(acc, "Id", "Name", model.id_accountTo);
                ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name", idBoxAccountFrom);


                return View(model);
            }
        }

        // [NoDirectAccess]


        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        public IActionResult Edit(long id = 0)
        {
            var acc = new List<AccountTable>();

            acc.Add(new AccountTable
            {
                Id = -1,
                Name = "اختر حساب"

            });
            acc.AddRange(_db.AccountTables.ToList());

            var boxAccount = _db.AccountTables.Where(x => x.IdGroup == Static_Group_Accounts.Boxs).ToList();

            int idBoxAccountFrom = Static_Accounts.Main_Box;



            if (id == 0)
            {


                BillViewModel model = new BillViewModel
                {
                    Id = 0,
                    Date = DateTime.Now,

                };


                return View(model);
            }

            else
            {
                var service = new bills_Service(_db);

               
                var model = service.getOne(id);
                idBoxAccountFrom = model.id_accountForm;

              //  model.id_accountTo = model.IdAccount;


                if (model == null)
                {
                    return NotFound();
                }
                ViewData["IdAccount"] = new SelectList(acc, "Id", "Name", model.id_accountTo);
                ViewData["IdBoxAccount"] = new SelectList(boxAccount, "Id", "Name", idBoxAccountFrom);


                return View(model);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, BondViewModel transactionModel)
        {

           // 


            if (ModelState.IsValid )
            {

                transactionModel.IdAccount = transactionModel.id_accountForm;

                //Insert
                if (id == 0)
                {
                    transactionModel.Createat = DateTime.Now;


                  
                    BondController bc = new BondController(_db);
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
            var gs = new bills_Service(_db);

            List<BillViewModel> Parts = new List<BillViewModel>();


            var modell = await gs.ListDT(param,"7");


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
                //Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Amount).ToList() : Parts.OrderByDescending(c => c.Amount).ToList();
            }
            else if (sortColumnIndex == 6)
            {
               // Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Why).ToList() : Parts.OrderByDescending(c => c.Why).ToList();
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
            var service = new bills_Service(_db);

            var model = await service.DeleteAsync(id);

            if (model.success != false)
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }
            else
            {
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll") });
            }




        }
        private bool TransactionModelExists(long id)
        {
            return _db.BondTables.Any(e => e.Id == id);
        }
    }
}
