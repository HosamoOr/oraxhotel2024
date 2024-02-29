using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class BoxService
    {
        private readonly HotelAlkheerDB _db;

        public BoxService(HotelAlkheerDB context)
        {
            _db = context;
        }
        public async Task<int> AddAsync(BoxViewModel box)
        {
            BoxsTable model = new BoxsTable
            {
                Id = box.Id,
                IdAccount = box.IdAccount,
                IsDefault = box.IsDefault,
                IsPrivate = box.IsPrivate,
                Name = box.Name,
                IdSub = box.IdSub
            };

            _AccountViewModel modelAcc = new _AccountViewModel
            {
                Id = box.Id,
                Name = box.Name,
                IsPrivate = false,
                IdGroup = Static_Group_Accounts.Boxs

            };
            _AccountService as_s = new _AccountService(_db);
          int  idAcunt = await as_s.CreateAsync(modelAcc);

            model.IdAccount = idAcunt;


         var idob=   _db.InsertWithIdentity(model);
            int idBox = Convert.ToInt32(idob);


            return idBox;

        }

        public async Task<int> Edit(int id, BoxViewModel box)
        {

            var boxsTable = _db.BoxsTables.Where(x => x.Id == id).FirstOrDefault();
            var acc = _db.AccountTables.Where(x => x.Id == boxsTable.IdAccount).FirstOrDefault();



            if (acc == null || acc.IsPrivate == true)
            {
                return -1;
               // return NotFound();
            }
            else
            {
                acc.Name = box.Name;
                _db.Update(acc);
            }




            if (boxsTable == null || boxsTable.IsPrivate == true)
            {
                return -1;
               // return NotFound();
            }
            else
            {
                boxsTable.Name = box.Name;
                boxsTable.IsDefault = box.IsDefault;
                _db.Update(boxsTable);
                return 1;
            }
        }

        public async Task<int> AddBoxToUser(string  idUser,int idBox,bool isDefult)
        {
            BoxsUserTable boxsUserTable = new BoxsUserTable
            {
                IdAspUser = idUser,
                IdBox = idBox,
                IsDefult = isDefult
            };
            await _db.InsertAsync(boxsUserTable);
            return 1;
        }
    }
}
