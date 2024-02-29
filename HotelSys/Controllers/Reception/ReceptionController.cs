using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
//using HotelSys.ViewModel.reportViewModel;
using LinqToDB;
using LinqToDB.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{

    [Authorize]
    public class ReceptionController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public ReceptionController(HotelAlkheerDB context)
        {
            _db = context;
        }
        //public IActionResult Index()
        //{
        //    Status_RoomService rs = new Status_RoomService(_db);
        //    List<Status_Current_RoomViewModel> li = rs.ListStatusCurrent(1);
        //    return View(li);
        //}
        

        //public IActionResult ListReception()
        //{
        //    ReceptionService rs = new ReceptionService(_db);
        //    List<ReceptionViewModel> li = rs.List(1);
        //    return View(li);
        //}

        public ActionResult Index(int id)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(1);

            bool isIncludeTax = false;


            var prciceRoom = new PriceRoomsViewModel();

            prciceRoom = _db.PriceRoomsTables.Where(x => x.IdRoom == Convert.ToInt32(id))

               .Select(xx => new PriceRoomsViewModel
               {
                   Price = xx.Price,
                   PriceMin = xx.PriceMin,
                   PriceOvertime = xx.PriceOvertime,
                   TaxGroup = new TaxGroupViewModel
                   {
                       Rate = xx.Priceroomstabletaxgrouptable.Rate,
                       Name = xx.Priceroomstabletaxgrouptable.Name,
                       Id = xx.Id,
                       priceTax = 0,
                       IsBaladiTax = xx.Priceroomstabletaxgrouptable.IsBaladiTax == null ? false : xx.Priceroomstabletaxgrouptable.IsBaladiTax,
                       BaladiRate = xx.Priceroomstabletaxgrouptable.IsBaladiTax == null ? 0 : xx.Priceroomstabletaxgrouptable.BaladiRate,

                   }
               })
               .FirstOrDefault();



           
            double price = 0;

            //  var prciceRoom = _db.PriceRoomsTables.Where(x => x.IdRoom == Convert.ToInt32(id)).FirstOrDefault();
            if (prciceRoom != null)
            {
                price = Convert.ToDouble(prciceRoom.Price); //or min or overflow

                var SettingReceptionTable = _db.SettingReceptionTables.FirstOrDefault();
                if (SettingReceptionTable != null)
                {
                    isIncludeTax = SettingReceptionTable.IncudePriceTax == null ? false : Convert.ToBoolean(SettingReceptionTable.IncudePriceTax);


                        var taxP = (Convert.ToDouble(prciceRoom.Price) * prciceRoom.TaxGroup.Rate) / 100;

                        // prciceRoom.Price = prciceRoom.Price + taxP;

                        prciceRoom.TaxGroup.priceTax = Convert.ToDouble(taxP);

                }
            }
            else
            {
                prciceRoom = new PriceRoomsViewModel {
                    TaxGroup = new TaxGroupViewModel
                    { IsBaladiTax = false,
                    BaladiRate=0,
                    priceTax=0,
                    Rate=0

                } };
            }
            var model = new ReceptionViewModel {
                IdReception = 0,
                StartDate = startDate,
                EndDate = endDate,
                Price = price,
                QtyTime = 1,
              
                IsChechin=false,
                IsChechout=false,
                NameCustomer="",
                

                room = prciceRoom,
                followers = new List<FollowerViewModel>(),
                customer = new CustomerViewModel(),
                bill=new BillReceptionViewModel { 
                DeserveAmount=0,
                QtyDiscount=0,
                Total=0,
                TypeDiscount="-1",
                IncludeTax= isIncludeTax,
                IsBaladiTax= prciceRoom.TaxGroup.IsBaladiTax,
                QtyDiscountRate=0,
                TotalTaxPrice = prciceRoom.TaxGroup.priceTax,

                TotalBaladiTaxPrice = 0,
                TotalBaladiTaxRate = prciceRoom.TaxGroup.BaladiRate,
                   TotalTaxRate = prciceRoom.TaxGroup.Rate

                },
                
                company=new _CompanyViewModel {
                IdCo=null,
                
                },
                status=0,
                summaryPrice=new SummaryPriceReceptionViewModel
                {
                    sum_Ex=0,
                    sum_Pay=0,
                    sum_services=0,
                    final_total=0,
                    
                }
                  
                    
            
            };


            List<RoomViewModel> roomLi = new List<RoomViewModel>();

            roomLi.Add(new RoomViewModel
            {
                IdR = 0,
                NameR = "Select"

            });
           


            var temmproomLi = _db.StatusCurrentTables.
                Where(r => r.Status == "1" || r.Status == "2" || r.Status == "3").

                Select(

                x => new RoomViewModel
                {
                    IdR = x.IdRoom,
                    NameR = x.Fkdetialsstatusroom.NameR,


                }).ToList();

            roomLi.AddRange(temmproomLi);



            model.room.IdRoom = id;

            ViewData["IdRoom"] =new SelectList(roomLi, "IdR", "NameR", model.room.IdRoom);




            List<sourceReceViewModel> sList = new List<sourceReceViewModel>();

            sList.Add(
                new sourceReceViewModel { 
                    id="1",
                    name="الاستقبال"
            });

            sList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "غير ذلك"
                });

            ViewData["Idsource"] = new SelectList(sList, "id", "name", model.Source);


            //------------- للخدمات
            //var _ServicesIncludeTax = false;

            //var settingGeneralTable = _db.SettingGeneralTables.FirstOrDefault();


            //if (settingGeneralTable == null)
            //{
            //    settingGeneralTable = new SettingGeneralTable { Id = 0, ServicesIncludeTax = _ServicesIncludeTax };
            //    _db.Insert(settingGeneralTable);
            //}
            //_ServicesIncludeTax= settingGeneralTable.ServicesIncludeTax;


            //ViewData["includeTex"] = _ServicesIncludeTax;


            //-----------
            //------------- للخدمات
            var _IncudePriceTax = false;

            var settingGeneralTable = _db.SettingReceptionTables.FirstOrDefault();


            if (settingGeneralTable != null)
            {
                _IncudePriceTax = settingGeneralTable.IncudePriceTax==null?false:Convert.ToBoolean(settingGeneralTable.IncudePriceTax);
            }
           


            model.bill.IncludeTax = _IncudePriceTax;



            List<sourceReceViewModel> TypeList = new List<sourceReceViewModel>();

            TypeList.Add(
              new sourceReceViewModel
              {
                  id = "1",
                  name = "ميلادي"
              });

            TypeList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "هجري"
                });

            //sList.Add(
            //   new sourceReceViewModel
            //   {
            //       id = "2",
            //       name = "ساعات"
            //   });

            ViewData["TypeDate"] = new SelectList(TypeList, "id", "name", model.TypeDate);


            List<sourceReceViewModel> UnitList = new List<sourceReceViewModel>();

            UnitList.Add(
              new sourceReceViewModel
              {
                  id = "1",
                  name = "يومي"
              });

            UnitList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "شهري"
                });

            //sList.Add(
            //   new sourceReceViewModel
            //   {
            //       id = "2",
            //       name = "ساعات"
            //   });

            ViewData["Unit"] = new SelectList(UnitList, "id", "name", model.Unit);
            ViewData["HeaderPage"] = "اضافة حجز";
            ViewData["titlePageType"] = 1;
            
          

            return View(model);
        }


      



        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReceptionService rService = new ReceptionService(_db);
            ReceptionViewModel model = rService.getOneAsync(Convert.ToInt64(id));

            if (model == null)
            {
                return NotFound();
            }
            List<RoomViewModel> roomLi = new List<RoomViewModel>();

            roomLi.Add(new RoomViewModel
            {
                IdR=-1,
                NameR="Select"

            });
            roomLi.Add(new RoomViewModel
            {
                IdR =model.room.IdRoom,
                NameR = model.room.NameRoom,
                
            });


            var temmproomLi = _db.StatusCurrentTables.
                Where(r => r.Status == "1" || r.Status == "2" || r.Status == "3").

                Select(

                x => new RoomViewModel
                {
                    IdR = x.IdRoom,
                    NameR = x.Fkdetialsstatusroom.NameR,


                }).ToList();

            roomLi.AddRange(temmproomLi);
            // model.room.IdRoom = id;

            
            ViewData["IdRoom"] = new SelectList(roomLi, "IdR", "NameR", model.room.IdRoom);




            List<sourceReceViewModel> sList = new List<sourceReceViewModel>();

            sList.Add(
                new sourceReceViewModel
                {
                    id = "1",
                    name = "الاستقبال"
                });

            sList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "غير ذلك"
                });

            ViewData["Idsource"] = new SelectList(sList, "id", "name", model.Source);



            List<sourceReceViewModel> TypeList = new List<sourceReceViewModel>();

            TypeList.Add(
              new sourceReceViewModel
              {
                  id = "1",
                  name = "ميلادي"
              });

            TypeList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "هجري"
                });

            //sList.Add(
            //   new sourceReceViewModel
            //   {
            //       id = "2",
            //       name = "ساعات"
            //   });

            ViewData["TypeDate"] = new SelectList(TypeList, "id", "name", model.TypeDate);


            List<sourceReceViewModel> UnitList = new List<sourceReceViewModel>();

            UnitList.Add(
              new sourceReceViewModel
              {
                  id = "1",
                  name = "يومي"
              });

            UnitList.Add(
                new sourceReceViewModel
                {
                    id = "2",
                    name = "شهري"
                });

            //sList.Add(
            //   new sourceReceViewModel
            //   {
            //       id = "2",
            //       name = "ساعات"
            //   });

            ViewData["Unit"] = new SelectList(UnitList, "id", "name", model.Unit);
            ViewData["HeaderPage"] = "تعديل حجز";
            ViewData["titlePageType"] = 2 ;

            //***المقبوضات
            List<BondShortViewModel> payBandList = _db.GetPayBondListForReception("1",id).
                Select(y=>new BondShortViewModel { 
                id=y.id,
                txt = y.id + " | " + y.amount + " ( " + y.typepay + " )",
                    amount = y.amount,
                    date = y.date
                }).
                ToList();
            ViewData["payBandList"] = new SelectList(payBandList, "id", "txt", 0);

            //***المصروفات
            List<BondShortViewModel> exBandList = _db.GetPayBondListForReception("2", id).
                Select(y => new BondShortViewModel
                {
                    id = y.id,
                    txt = y.id + " | " + y.amount + " ( " + y.typepay + " )",
                    amount=y.amount,
                    date=y.date
                }).
                ToList();
            ViewData["exBandList"] = new SelectList(exBandList, "id", "txt", 0);

            //***الخدمات
            var serviceBillList = _db.BillsTables.Where(x => x.Type == "7" && x.IdReception == id).
                 Select(y => new BondShortViewModel
                 {
                     id = y.Id,
                     txt = y.Id + " | " + y.DeserveAmount + " ( " + y.TypePay + " )",
                     amount = Convert.ToInt64( y.DeserveAmount),

                     date = y.Date
                 }).
                ToList();

            ViewData["serviceBillList"] = new SelectList(serviceBillList, "id", "txt", 0);
            //summary Price 

            double sumPay = payBandList.Sum(x => x.amount);
            double sumEX = exBandList.Sum(x => x.amount);
            double sumService = serviceBillList.Sum(x => x.amount);


            SummaryPriceReceptionViewModel suPrice = new SummaryPriceReceptionViewModel
            {
                sum_Pay = sumPay,
                sum_Ex = sumEX,
                sum_services = sumService
            };

            model.summaryPrice = suPrice;


            return View("Index", model);
        }





        //public async Task<IActionResult> ChangeStatus(int id)
        //{
        //    Status_RoomService rs = new Status_RoomService(_db);
        //    var mo = rs.Find(id, 1);

        //    ViewData["IdType"] = new SelectList(_db.TypeRoomsTables, "Id", "NameT", mo.RoomModel.IdType);

        //    return View(mo);

        //}


        [HttpPost]
         public JsonResult getPriceRoom(sourceReceViewModel collection)
        {

            PriceRoomsViewModel prciceRoom = new PriceRoomsViewModel
            {
                Price = 0,
                PriceMin = 0,
                PriceOvertime = 0,
                TaxGroup=new TaxGroupViewModel()


            };
            if (collection.id !="0")
            {
                prciceRoom = _db.PriceRoomsTables.Where(x => x.IdRoom == Convert.ToInt32(collection.id))

                .Select(xx => new PriceRoomsViewModel
                {
                    Price = xx.Price,
                    PriceMin = xx.PriceMin,
                    PriceOvertime = xx.PriceOvertime,
                    TaxGroup = new TaxGroupViewModel
                    {
                        Rate = xx.Priceroomstabletaxgrouptable.Rate,
                        Name = xx.Priceroomstabletaxgrouptable.Name,
                        Id = xx.Id,
                        priceTax = 0,
                        IsBaladiTax = xx.Priceroomstabletaxgrouptable.IsBaladiTax == null ? false : xx.Priceroomstabletaxgrouptable.IsBaladiTax,
                        BaladiRate = xx.Priceroomstabletaxgrouptable.BaladiRate,
                        





                    }
                })
                .FirstOrDefault();




                var SettingReceptionTable = _db.SettingReceptionTables.FirstOrDefault();
                if (SettingReceptionTable != null)
                {
                    // if (SettingReceptionTable.IncudePriceTax == false)
                    {
                        var taxP = (Convert.ToDouble(prciceRoom.Price) * prciceRoom.TaxGroup.Rate) / 100;

                        // prciceRoom.Price = prciceRoom.Price + taxP;

                        prciceRoom.TaxGroup.priceTax = Convert.ToDouble(taxP);

                    }


                }


            }


            return Json(prciceRoom);
        }

        //ReceptionViewModel


        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync( ReceptionViewModel collection)
        {
            try
            {
                ReceptionService rs = new ReceptionService(_db);
                ListIdLong tii= await rs.CreateAsync(collection);



               // if (collection.titlePageType == 1)

                    return Json(new
                    {
                        id = tii.IDs[0],
                        idBill = tii.IDs[1],
                        idCustomerAccount = tii.IDs[2],
                        mass = "تم حفظ الحجز بنجاح",
                       

                    });
                //else
                //{
                //    return Json(new
                //    {
                //        id = tii.IDs[0],
                //        //idBill = tii.IDs[1],
                //        idCustomerAccount = tii.IDs[2],
                //        mass = "تم تعديل الحجز بنجاح"

                //    });
                //}
            }
            catch(Exception ee)
            {
                return Json(new
                {
                    id = 0,
                    mass = "حدث خطا ما اثناء حفظ البيانات !!"

                });
            }
         
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<JsonResult> EnterRoom(EnterRoomViewModel model)
        {
            try
            {
                bills_Service rs = new bills_Service(_db);
                var sru = await rs.EnterRoomBillAsync(model.IdBill);

                Status_RoomService sr = new Status_RoomService(_db);
                var st = Status_RoomsName.listStatus[4].index.ToString();
                Status_Current_RoomViewModel stViewModel = new Status_Current_RoomViewModel
                {
                    IdRoom = model.IdRoom,
                    Status = st,
                    IdReception = model.IdReception,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    

                    //IdEmp = mo.IdEmp
                };

                var cStIDdetials = sr.changByRoom(stViewModel);

                ReceptionService receService = new ReceptionService(_db);
                receService.updateStatus(2, model.IdReception);


                return Json(new
                {
                    id = cStIDdetials,
                   
                    mass = "تم تسجيل الدخول الى الشقة بنجاح",
                    status= st

                });
            }
            catch
            {
                return Json(new
                {
                    id = 0,
                    mass = "حدث خطا ما اثناء حفظ البيانات !!"

                });
            }
        }
        [NoDirectAccess]
        public async Task<IActionResult> PreViewReception(long id = 0)
        {
            if (id == 0)
                return View(new ReceptionViewModel());
            else
            {
                ReceptionService service = new ReceptionService(_db);

                var receptionDat = service.getOneAsync(id);

                var li = _db.ConditionReceptionTables.ToList();

                


                var x = _db.OrgsTables.FirstOrDefault();

                String urlLogo = "";
                if (x.Logo != null)
                {
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Upload\\");

                    urlLogo = basePath + x.Logo;
                }
                else
                {
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\img\\logo.jpg");

                    urlLogo = basePath;
                }


                OrgViewModel hotelModel = new OrgViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    Email = x.Email,
                    IdSub = x.IdSub,
                    Logo = urlLogo,
                    MailBox = x.MailBox,
                    NameH = x.NameH,
                    NumEn = x.NumEn,
                    Phone = x.Phone,
                    Regin = x.Regin,
                    Website = x.Website,

                };



                PrintReceptionVewModel model = new PrintReceptionVewModel
                {
                    receptionData=receptionDat,
                    conditions = li,
                    hotelData = hotelModel

                };


                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }



    
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ChangeStatus(int id, Status_Current_RoomViewModel collection)
        //{
        //    try
        //    {

        //      var mo=  _db.StatusCurrentTables.Find(collection.Id);
        //        mo.Status = collection.Status;

        //        _db.Update(mo);

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Reception

        // GET: Reception/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reception/Create
     
        public ActionResult showInvoice(long id=0)
        {
            if (id == 0)
            {

                return View(new ReceptionViewModel());
            }
            else
            {
                try
                {
                    BillReceprionService brs = new BillReceprionService(_db);
                    var model=brs.GetShowBillReception(id);



                    if (model == null)
                    {
                        return NotFound();
                    }
                    return View(model);
                }

                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                // 


            }
        }



        public ActionResult DXViewerbill(long id = 0)
        {
            if (id == 0)
            {

                return View();
            }
            else
            {
                ViewData["Id"] = id;
                return View();

            }
        }

        public ActionResult t1()
        {
            return View();
        }

        public ActionResult t2()
        {
            return View();
        }
        //
        // POST: Reception/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Reception/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Reception/Edit/5
        

        // GET: Reception/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reception/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}