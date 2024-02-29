using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    public class ReceptionServiceController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public ReceptionServiceController(HotelAlkheerDB context)
        {
            _db = context;
        }

        public async Task<ServicesReceptionViewModel> GetInitBill_New()
        {
          

            bool isServiceTax = false;


            var settingServiceTax = _db.SettingGeneralTables.FirstOrDefault();
            if (settingServiceTax != null)
            {
                isServiceTax = settingServiceTax.ServicesIncludeTax;
            }

            string taxtIncludeTax = "السعر شامل الضريبة";
            if (isServiceTax == false)
            {
                taxtIncludeTax = "السعر غير شامل الضريبة";
            }


            ServicesReceptionViewModel model = new ServicesReceptionViewModel
            {
               
                ServicesIncludeTax = isServiceTax,
                txtIncludeTax = taxtIncludeTax

            };


            return model;

        }
        public async Task<String> GetInitBill()
        {
            var gs = new GroupService(_db);


            var Parts = await gs.ListAll();

            bool isServiceTax = false;


            var settingServiceTax = _db.SettingGeneralTables.FirstOrDefault();
            if(settingServiceTax!=null)
            {
                isServiceTax = settingServiceTax.ServicesIncludeTax;
            }

            string taxtIncludeTax = "السعر شامل الضريبة";
            if (isServiceTax==false)
            {
                taxtIncludeTax = "السعر غير شامل الضريبة";
            }



            ServicesReceptionViewModel model = new ServicesReceptionViewModel
            {
                dataGroup= Parts,
                ServicesIncludeTax= isServiceTax,
                txtIncludeTax= taxtIncludeTax

            };
            


            var output = JsonConvert.SerializeObject(model);



            return output;

        }

        [NoDirectAccess]
        public async Task<IActionResult> PreViewBillService(long id = 0)
        {
            if (id == 0)
                return View(new ReceptionViewModel());
            else
            {
                bills_Service service = new bills_Service(_db);

                var billDa = service.getOne(id);


                var x = _db.OrgsTables.FirstOrDefault();

                String urlLogo = "";
                if (x.Logo != null)
                {
                    urlLogo = @"/Upload/" + x.Logo;
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
                ////
                ///
                QRService qrS = new QRService();

                var qrModel = qrS.GenerateFile(" sevicse Inv-  id_Bill: " + id.ToString() + " Total:" + billDa.Total.ToString()

                    + " totalVAT:" + billDa.TotalTaxPrice.ToString() + " totalBALADI:" + billDa.TotalBaladiTaxPrice.ToString()

                    );



                showServiceBillVM model = new showServiceBillVM
                {
                    billData = billDa,
                    hotelData = hotelModel,
                    qR = qrModel

                };


                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }





    }
}
