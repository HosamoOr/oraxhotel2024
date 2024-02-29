using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.Accounting_Layer.bords;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public BillController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  JsonResult updateBillReceptionjson(BillReceptionViewModel model)
        {

            bills_Service cs = new bills_Service(_db);

           bool ss =  cs.updateReceptionAsync(model);

           
            return Json(new
            {
                id = ss,
                name = "Add ",
                mass = "تم حفظ الحجز Bill بنجاح"

            });
        }


        [HttpPost]
        public async Task<JsonResult> Createjson(BillViewModel model)
        {

            bills_Service cs = new bills_Service(_db);

            Value_Return vr = new Value_Return();

            string note = " فاتورة خدمات: الاصناف: ";

            if (model.Type == "7")//service
            {
                if(model.IdReception==null)
                {

                    AccReceptionController asc=new AccReceptionController(_db);
                    var mm = asc.GetReceptionByIdAccAsModel(model.IdAccount);
                    if(mm != null)
                    {
                        model.IdReception = mm.IdReception;
                        model.IsForRoom = true;
                        
                    }

                }

                for (int i = 0; i < model.Items.Count; i++)
                {
                    var nu = i + 1;

                    note = note +nu + "-" + model.Items[i].NameProduct + "[" + model.Items[i].Qty + "]" + model.Items[i].Total+"--";

                  
                }

            }

            model.Note = note;

            //if (model.CustomerOrCompany == "cu")
            //{
            //    var mycu = _db.MyCustomers.Where(x => x.IdCustomer == model.IdAccount).FirstOrDefault();
            //    model.IdAccount = Convert.ToInt32(mycu.IdAccount);
            //    if (model.TypePay == "2")
            //    {
            //        model.id_accountForm = model.IdAccount;
            //    }

            //}

            //model.id_accountTo = model.IdAccount;



            try
            {
                vr = await cs.addAsync(model);
            }

            catch(Exception ex)
            {
                var me = ex.Message;
            }
           

            int ss = 0;
            return Json(new
            {
                id = vr.id_long,
                name = "Add ",
                mass= vr.message

            });
        }


     
}


    
}