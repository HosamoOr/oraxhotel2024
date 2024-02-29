
using HotelSys.Models;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class OrgServiceEF
    {

          Hotel_alkheerContext _db;

        public OrgServiceEF()
        {
             _db = new Hotel_alkheerContext();
        }
        public OrgShortViewModel getDataShortOrg()
        {
            var x = _db.OrgsTables.

                FirstOrDefault();

            String urlLogo = "";
            if (x.Logo != null)
            {
                urlLogo = @"/Upload/" + x.Logo;
            }


            OrgShortViewModel orgModel = new OrgShortViewModel
            {
                Id = x.Id,
                Address = x.City+ x.Address,
                City = x.City,
                Country = x.Country,
               
                
                Logo = urlLogo,
               
                NameH = x.NameH,
                NumEn = x.NumEn,
                Phone = x.Phone,
                Regin = x.Regin,
                Website = x.Website,

            };
            return orgModel;
        }

        public OrgViewModel  getDataOrg()
        {
            var x = _db.OrgsTables.

                FirstOrDefault();

            String urlLogo = "";
            if (x.Logo != null)
            {
                urlLogo = @"/Upload/" + x.Logo;
            }


            OrgViewModel orgModel = new OrgViewModel
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
            return orgModel;
        }

        }
}
