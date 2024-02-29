using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Help;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class FollowersService
    {
        private readonly HotelAlkheerDB _db;

        public FollowersService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public async Task<long> AddToMyFollower(long idCustomer,
       string PrivateNote, int idSub)
        {
            var mycu = _db.MyCustomers.Where(x => x.IdCustomer == idCustomer).FirstOrDefault();
            if (mycu == null)
            {
                
                MyCustomer cA = new MyCustomer
                {
                    IdCustomer = idCustomer,
                    VisitEndDate = DateTime.Now,
                  //  IdAccount = idac,
                    Createat = DateTime.Now,
                    Idsub = idSub,
                    PrivateNote = PrivateNote


                };
                var idMyCustomer = _db.InsertWithIdentity(cA);

                return Convert.ToInt64(idMyCustomer);

            }
            return mycu.Id;


        }
        public async Task<long> Delete(long id)
        {
            var model = _db.FollowerReceptionTables.Where(x => x.Id == id).FirstOrDefault();
            _db.Delete(model);
            return 1;
        }

        public async Task<long> EditForReception(
           long repIdn,
           List<FollowerViewModel> followers, int? idSub)
        {
            var tempDel = _db.FollowerReceptionTables.Where(x => x.IdReceptoin == repIdn).ToList();

            for (int i = 0; i < tempDel.Count(); i++)
            {
                await Delete(tempDel[i].Id);
            }
            var statID =await AddForReception(repIdn, followers, idSub);
            return statID;
            }


            public async Task<long> AddForReception(
            long repIdn,
            List<FollowerViewModel> followers,int ?idSub)
        {
            for (int i = 0; i < followers.Count(); i++)
            {
                //var idFollwer = await AddToMyFollower(followers[i].follwerCusomer.IdcumtomerAll,
                //      // mo.followers[i].follwerCusomer.Name,
                //      "",// mo.PrivateNote,
                //       idSub
                //       );


                FollowerReceptionTable cuRFollow = new FollowerReceptionTable
                {
                    CuType = TypeCustomerHelp.FollowerType.type,
                    IdReceptoin = repIdn,
                    IdCustomer = followers[i].follwerCusomer.IdcumtomerAll,
                    Relation = followers[i].Relation

                };

                var NewcF = _db.InsertWithIdentity(cuRFollow);


            }
            return 1;
        }

        // نفس حفظ عميل ولكن بدون حساب 
        public async Task<ListIdLongAcc> CreateAsync(CustomerViewModel model)
        {

            CustomerService cs = new CustomerService(_db);

            ListIdLongAcc isFound = await cs.checkFoundCu(model);

            if (isFound.status != 0)
            {
                return isFound;
            }

            using (var t = _db.BeginTransaction())
            {
                try
                {
                    //_AccountService ass = new _AccountService(_db);

                    //_AccountViewModel _avm = new _AccountViewModel
                    //{
                    //    IdGroup = Static_Group_Accounts.Customer_Individual,
                    //    Status = Status_Account.active,
                    //    Name = model.Name


                    //};


                    //int idac = await ass.CreateAsync(_avm);

                    CustomerTable cu = new CustomerTable
                    {
                        // Id = model.Id,
                        Name = model.Name,
                        //IdSub = model.IdSub,
                        Createat = DateTime.Now,
                        Email = model.Email,
                        ReleaseDate = model.ReleaseDate,
                        EndDate = model.EndDate,
                        // IdAccount = idac,
                        LocWork = model.LocWork,
                        LocRelease = model.LocRelease,
                        Nationality = model.Nationality,
                        NumProof = model.NumProof,
                        PhoneWork = model.PhoneWork,
                        // PrivateNote = model.PrivateNote,
                        PublicNote = model.PublicNote,

                        Sex = model.Sex,
                        Type = model.Type,
                        TypeProof = model.TypeProof,
                        TypeWork = model.TypeWork,
                        IdArea = model.Id_Area


                    };


                    var idcustomer = _db.InsertWithIdentity(cu);


                    //MyCustomer mycuModel = new MyCustomer
                    //{
                    //    IdAccount = null,
                    //    Createat = DateTime.Now,
                    //    IdCustomer = Convert.ToInt64(idcustomer),
                    //    PrivateNote = model.PrivateNote,
                    //    Idsub = model.IdSub,

                    //};
                    //var idMyCustomer = _db.InsertWithIdentity(mycuModel);

                    isFound.IDs = new List<long>();

                    isFound.IDs.Add(Convert.ToInt64(idcustomer));
                     isFound.IDs.Add(-1);

                     isFound.IDs.Add(Convert.ToInt64(-1));

                    t.Commit();
                }
                catch (ApplicationException e)
                {
                    await t.RollbackAsync();
                    //vr.success = false;
                    //vr.id_long = billViewModel.Id;
                    //vr.message = messageApp.txt_message[0];
                }
            }




            return isFound;


        }

    }
}
