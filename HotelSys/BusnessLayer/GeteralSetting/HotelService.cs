﻿//using DataModels;
//using HotelSys.ViewModel;
//using LinqToDB;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HotelSys.BusnessLayer
//{
//    public class HotelService
//    {

//        private readonly HotelAlkheerDB _db;

//        public HotelService(HotelAlkheerDB context)
//        {
//            _db = context;
//        }

//        public OrgShortViewModel getDataShortOrg()
//        {
//            var x = _db.OrgsTables.

//                FirstOrDefault();

//            String urlLogo = "";
//            if (x.Logo != null)
//            {
//                urlLogo = @"/Upload/" + x.Logo;
//            }


//            OrgShortViewModel hotelModel = new OrgShortViewModel
//            {
//                Id = x.Id,
//                Address = x.Address,
//                City = x.City,
//                Country = x.Country,
               
                
//                Logo = urlLogo,
               
//                NameH = x.NameH,
//                NumEn = x.NumEn,
//                Phone = x.Phone,
//                Regin = x.Regin,
//                Website = x.Website,

//            };
//            return hotelModel;
//        }

//        public OrgViewModel  getDataHotel()
//        {
//            var x = _db.OrgsTables.

//                FirstOrDefault();

//            String urlLogo = "";
//            if (x.Logo != null)
//            {
//                urlLogo = @"/Upload/" + x.Logo;
//            }


//            OrgViewModel hotelModel = new OrgViewModel
//            {
//                Id = x.Id,
//                Address = x.Address,
//                City = x.City,
//                Country = x.Country,
//                Email = x.Email,
//                IdSub = x.IdSub,
//                Logo = urlLogo,
//                MailBox = x.MailBox,
//                NameH = x.NameH,
//                NumEn = x.NumEn,
//                Phone = x.Phone,
//                Regin = x.Regin,
//                Website = x.Website,
//                IdCountry = x.IdCountry,

//            };
//            return hotelModel;
//        }

//        public async Task<bool> EditAsync(int id, [FromForm] OrgViewModel model)
//        {

//            var table = _db.OrgsTables.Where(x => x.Id == id).FirstOrDefault();

//                table.NumEn = model.NumEn;
//                table.Phone = model.Phone;
//                table.Regin = model.Regin;
//                table.Website = model.Website;
//                table.NameH = model.NameH;
//                table.Address = model.Address;
//                table.Email = model.Email;
//                table.City = model.City;
//                table.Country = model.Country;
//                table.MailBox = model.MailBox;
//            table.IdCountry = model.IdCountry;
//            table.Country = "";

//           if(model.ThumbnailImage!=null)

//                if (table.Logo != model.ThumbnailImage.FileName)
//                {

//                    FileService CS = new FileService();


//                    if (model.ThumbnailImage != null)
//                    {
//                        var fileName = await CS.SaveFile(model.ThumbnailImage,"");
//                        //await CS.Delete(table.logo);

//                        if (fileName != null)
//                        {
//                            table.Logo = fileName;
//                        }


//                    }


//            }
//           try
//            {
//                _db.Update(table);
//            }
//            catch(Exception ee)
//            {

//            }
           
//            return true;

//        }
//    }
//}