using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    public class LogoutCustomerController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public LogoutCustomerController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<JsonResult> SummaryLogOut(LogoutRoomViewModel model)
        {
            try
            {
                LogoutService ls = new LogoutService(_db);

                SummaryLogOutViewModel mo = ls.getSummary(model);

                return Json(new
                {
                    sumFromPrice = mo.sumFromPrice,
                    sumToPrice = mo.sumToPrice,
                    balance = mo.balance,
                    item = mo.item,
                    itemDocument = mo.itemDocument,
                     id = 1,

                    timTemp=mo.timinglog

                });
            }
            catch
            {
                return Json(new
                {
                    id = 0,

                    mass = "حدث خطا ما اثناء جلب البيانات !!"

                });
            }
        }



        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<JsonResult> LogOut(LogoutRoomViewModel model)
        {
            try
            {

                LogoutService ls = new LogoutService(_db);

                BondService cs = new BondService(_db);
                Value_Return vr = new Value_Return();

                var mo = ls.getSummary(model);

                // فحص رصيد العميل مع السند للتصفيه الواجي من الواجهة

                //حساب القيمة المطلقة لتجاور القيم السالبة
                var B = Math.Abs(mo.balance);

                var mass = "تم تصفية الحساب للعميل وتسجيل خروجه بنجاح";
                var massError = "لم يتم تصفية العقد .. يتبقى مبالغ ماليه على العميل ";

                if (model.OutOrCancel=="2")
                {
                    mass = "تم تصفية الحساب والغاء الحجز  بنجاح";
                    massError = "لم يتم الغاء العقد .. يتبقى مبالغ ماليه على العميل ";
                }

                //الرصيد صفر
                if (B==0)
                {
                    Status_RoomService sr = new Status_RoomService(_db);
                    var cStIDdetials = sr.changeStatusForLogout(model);

                    return Json(new
                    {
                        id = cStIDdetials,

                        mass = mass,
                        status = true,

                    });
                }
                else
                { //حساب القيمة المطلقة لتجاور القيم السالبة
                    var A = Math.Abs(model.bondViewModel.Amount);

                    if (A == B)
                    {

                        vr = await cs.CreateAsync(model.bondViewModel);

                        if(model.OutOrCancel=="1")//يتم تعديل الكميات لحجز تم تسجيل دخوله فقط
                        {
                            // تعديل كمية الحجز
                            ReceptionService rs = new ReceptionService(_db);

                            DateTime endDateLogout = DateTime.Now;

                            var tii = rs.updateQty(mo.countDayReception, model.IdReception,
                                endDateLogout
                                );
                            //ERRRRRRRRRRRRRRRRRRRRRRRRRRRORRR
                            /// تعديل اجمالي الفاتورة
                            /// وقيودها المحاسبية
                        }



                        if (vr.id_long > 0)
                        {
                            Status_RoomService sr = new Status_RoomService(_db);
                            var cStIDdetials = sr.changeStatusForLogout(model);

                            return Json(new
                            {
                                id = cStIDdetials,

                                mass = mass,
                                status = true,

                            });
                        }

                        else
                        {
                            return Json(new
                            {
                                id = 0,

                                mass = massError,
                                status = false,
                                sumFromPrice = mo.sumFromPrice,
                                sumToPrice = mo.sumToPrice,
                                balance = mo.balance,
                                item = mo.item,
                                itemDocument = mo.itemDocument,


                            });
                        }

                    }



                    else
                    {
                        return Json(new
                        {
                            id = 0,

                            mass = massError,
                            status = false,
                            sumFromPrice = mo.sumFromPrice,
                            sumToPrice = mo.sumToPrice,
                            balance = mo.balance,
                            item = mo.item,
                            itemDocument = mo.itemDocument,


                        });
                    }
                }

               


             
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



    }
}