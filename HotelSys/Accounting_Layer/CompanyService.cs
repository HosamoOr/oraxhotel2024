using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class CompanyService
    {
        private readonly HotelAlkheerDB _db;

        public CompanyService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public async Task<TowIdInt> CreateAsync(_CompanyViewModel model)
        {

            _AccountService ass = new _AccountService(_db);

            _AccountViewModel _avm = new _AccountViewModel
            {
                IdGroup = Static_Group_Accounts.Customer_company,
                Status = Status_Account.active,
                Name = model.NameCo


            };


            int idac = await ass.CreateAsync(_avm);

            CompanyTable cu = new CompanyTable
            {
                Id = Convert.ToInt32( model.IdCo),
                Name = model.NameCo,
                IdSub = model.IdSub,
                
                IdAccount = idac,
                

            };


            var id = _db.InsertWithIdentity(cu);

            TowIdInt tii = new TowIdInt
            {
                ID = Convert.ToInt32(id),
                ID2 = idac
            };

            return tii;


        }


    }
}
