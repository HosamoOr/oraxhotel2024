using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.bords;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static HotelSys.Compnet.Helper;

namespace HotelSys.Controllers
{
    [Authorize]
    public class BondController : Controller
    {
        private readonly HotelAlkheerDB _db;

        private readonly UserManager<IdentityUser> _userManager;

        public BondController(HotelAlkheerDB context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Createjson(BondViewModel model)
        {

            BondService cs = new BondService(_db);
                Value_Return vr = new Value_Return();
            //if(model.customerOrCompany=="cu")
            //{
            //    var mycu = _db.MyCustomers.Where(x => x.IdCustomer == model.IdAccount).FirstOrDefault();
            //    model.IdAccount =Convert.ToInt32(  mycu.IdAccount);

            //}

            //  model.id_accountTo = model.IdAccount;

            //model.id_accountForm = model.IdAccount;

      


            vr = await cs.CreateAsync(model);

           
            return Json(new
            {
                id = vr.id_long,
                name = "",
                mass= vr.message

            });
        }

        [HttpPost]
        public async Task<JsonResult> Updatejson(BondViewModel model)
        {

            BondService cs = new BondService(_db);
            Value_Return vr = new Value_Return();
            //if(model.customerOrCompany=="cu")
            //{
            //    var mycu = _db.MyCustomers.Where(x => x.IdCustomer == model.IdAccount).FirstOrDefault();
            //    model.IdAccount =Convert.ToInt32(  mycu.IdAccount);

            //}

            //  model.id_accountTo = model.IdAccount;

            //model.id_accountForm = model.IdAccount;
            vr = await cs.update(model);


            return Json(new
            {
                id = vr.id_long,
                name = "",
                mass = vr.message

            });
        }


        [NoDirectAccess]
        public async Task<IActionResult> PreViewSanadReception(long id = 0)
        {
            if (id == 0)
                return View(new ReceptionViewModel());
            else
            {
                bond_model service = new bond_model(_db);

                var bondData = service.getOne(id);


                var x = _db.OrgsTables.FirstOrDefault();

                string urlLogo = "";
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



                showBondVM model = new showBondVM
                {
                   
                    bondData = bondData,
                    hotelData = hotelModel,
                   

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