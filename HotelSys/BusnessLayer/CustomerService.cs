using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class CustomerService
    {
        private readonly HotelAlkheerDB _db;

        public CustomerService(HotelAlkheerDB context)
        {
            _db = context;
        }
      

        public async Task<DTCusViewModel> ListMy(int idSub, paramModel request)
        {

            int limit = request.limit;
            int offset = request.offset;

            String searchText = request.search;
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = request.sort;
            String order = request.order;

            long iduser = request.userid;

            int countRo=0;

            var model =await _db.MyCustomers.
              Select(x => new CustomerViewModel
              {
                  IdmyCu = x.Id,
                  IdcumtomerAll = x.IdCustomer,
                  Createat = x.Createat,
                  Email = x.Mycustomerscustomertable.Email,
                  Name = x.Mycustomerscustomertable.Name,
                  EndDate = x.Mycustomerscustomertable.EndDate,
                  LocRelease = x.Mycustomerscustomertable.LocRelease,
                  LocWork = x.Mycustomerscustomertable.LocWork,
                  Nationality = x.Mycustomerscustomertable.Nationality,
                  NumProof = x.Mycustomerscustomertable.NumProof,
                  PhoneWork = x.Mycustomerscustomertable.PhoneWork,
                  PrivateNote = x.PrivateNote,
                  PublicNote = x.Mycustomerscustomertable.PublicNote,
                  ReleaseDate = x.Mycustomerscustomertable.ReleaseDate,
                  Sex = x.Mycustomerscustomertable.Sex,
                  Type = x.Mycustomerscustomertable.Type,
                  TypeProof = x.Mycustomerscustomertable.TypeProof,
                 // TypeProof = x.Mycustomerscustomertable.TypeProof =="1" ? "بطاقة شخصية" : x.Mycustomerscustomertable.TypeProof == "2"? "جواز سفر" :"عام ",
                  TypeWork = x.Mycustomerscustomertable.TypeWork,
                  IdAccount = x.IdAccount,
                  IdSub = x.Idsub,
                  VisitEndDate=x.VisitEndDate,
                  is_my = true,

                  Id_Area= x.Mycustomerscustomertable.IdArea,
                  IdNationality = x.Mycustomerscustomertable.IdNationality,
                  area = new AreaViewModel
                  {
                      Id = x.Mycustomerscustomertable.Customertableareatable.Id,
                      Name = x.Mycustomerscustomertable.Customertableareatable.Name,
                      NameCity = x.Mycustomerscustomertable.Customertableareatable.Areatablecitytable.Name,
                      IdCity = x.Mycustomerscustomertable.Customertableareatable.IdCity,
                      IdCountry= x.Mycustomerscustomertable.Customertableareatable.Areatablecitytable.Citytablecountrytable.Id


                  }



              })
              .OrderByDescending(x => x.IdmyCu).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToListAsync();

            countRo = _db.MyCustomers.Count();


            if (!string.IsNullOrEmpty(searchText))
            {

                model = await _db.MyCustomers.
                 Where(x => x.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Mycustomerscustomertable.NumProof != null && x.Mycustomerscustomertable.NumProof.ToLower().Contains(searchText.ToLower()))
                                              || (x.Mycustomerscustomertable.PhoneWork != null && x.Mycustomerscustomertable.PhoneWork.ToLower().Contains(searchText.ToLower()))
                                              || x.Mycustomerscustomertable.LocRelease.ToString().Contains(searchText.ToLower())

                                               || x.Mycustomerscustomertable.TypeProof.ToString().Contains(searchText.ToLower())).
              Select(x => new CustomerViewModel
              {
                  IdmyCu = x.Id,
                  IdcumtomerAll = x.IdCustomer,
                  Createat = x.Createat,
                  Email = x.Mycustomerscustomertable.Email,
                  Name = x.Mycustomerscustomertable.Name,
                  EndDate = x.Mycustomerscustomertable.EndDate,
                  LocRelease = x.Mycustomerscustomertable.LocRelease,
                  LocWork = x.Mycustomerscustomertable.LocWork,
                  Nationality = x.Mycustomerscustomertable.Nationality,
                  NumProof = x.Mycustomerscustomertable.NumProof,
                  PhoneWork = x.Mycustomerscustomertable.PhoneWork,
                  PrivateNote = x.PrivateNote,
                  PublicNote = x.Mycustomerscustomertable.PublicNote,
                  ReleaseDate = x.Mycustomerscustomertable.ReleaseDate,
                  Sex = x.Mycustomerscustomertable.Sex,
                  Type = x.Mycustomerscustomertable.Type,
                  TypeProof = x.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Mycustomerscustomertable.TypeProof == "2" ? "جواز سفر" : "عام ",
                  TypeWork = x.Mycustomerscustomertable.TypeWork,
                  IdAccount = x.IdAccount,
                  IdSub = x.Idsub,
                  VisitEndDate = x.VisitEndDate,
                  is_my = true,
                  IdNationality = x.Mycustomerscustomertable.IdNationality,
                  Id_Area = x.Mycustomerscustomertable.IdArea,
                  area = new AreaViewModel
                  {
                      Id = x.Mycustomerscustomertable.Customertableareatable.Id,
                      Name = x.Mycustomerscustomertable.Customertableareatable.Name,
                      NameCity = x.Mycustomerscustomertable.Customertableareatable.Areatablecitytable.Name,
                      IdCity = x.Mycustomerscustomertable.Customertableareatable.IdCity,

                  }



              })
              .OrderByDescending(x => x.IdmyCu).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToListAsync();

                countRo = _db.MyCustomers.Where(x => x.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Mycustomerscustomertable.NumProof != null && x.Mycustomerscustomertable.NumProof.ToLower().Contains(searchText.ToLower()))
                                              || (x.Mycustomerscustomertable.PhoneWork != null && x.Mycustomerscustomertable.PhoneWork.ToLower().Contains(searchText.ToLower()))
                                              || x.Mycustomerscustomertable.LocRelease.ToString().Contains(searchText.ToLower())

                                               || x.Mycustomerscustomertable.TypeProof.ToString().Contains(searchText.ToLower())).Count();

            }

            DTCusViewModel m = new DTCusViewModel
            {
                list= model,
                countRow= countRo

            };

            return m;
        }



     
        public async Task<List<CustomerViewModel>> ListAll(paramModel request)
        {


            int limit = request.limit;
            int offset = request.offset;

            String searchText = request.search;
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = request.sort;
            String order = request.order;

            long iduser = request.userid;



            var model = await _db.CustomerTables.
              Select(x => new CustomerViewModel
              {
                  //IdmyCu = x.Id,
                  IdcumtomerAll = x.Id,
                  Createat = x.Createat,
                  Email = x.Email,
                  Name = x.Name,
                  EndDate = x.EndDate,
                  LocRelease = x.LocRelease,
                  LocWork = x.LocWork,
                  Nationality = x.Nationality,
                  NumProof = x.NumProof,
                  PhoneWork = x.PhoneWork,
                  //PrivateNote = x.PrivateNote,
                  PublicNote = x.PublicNote,
                  ReleaseDate = x.ReleaseDate,
                  Sex = x.Sex,
                  Type = x.Type,
                  TypeProof = x.TypeProof,
                  TypeWork = x.TypeWork,
                  // IdAccount = x.IdAccount,
                  // IdSub = x.Idsub,
                  // VisitEndDate = x.VisitEndDate,
                  is_my = false,

                  Id_Area = x.IdArea,
                  IdNationality = x.IdNationality,
                  area = new AreaViewModel
                  {
                      Id = x.Customertableareatable.Id,
                      Name = x.Customertableareatable.Name,
                      NameCity = x.Customertableareatable.Areatablecitytable.Name,
                      IdCity = x.Customertableareatable.IdCity,

                  }



              }).OrderByDescending(x => x.IdcumtomerAll).
                    Skip(offset).
                   Take(limit)
                   //.DistinctBy(p => p.Id)
                  
              .ToListAsync();


            if (!string.IsNullOrEmpty(searchText))
            {
                model = model.Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NumProof != null && x.NumProof.ToLower().Contains(searchText.ToLower()))
                                              || (x.PhoneWork != null && x.PhoneWork.ToLower().Contains(searchText.ToLower()))
                                             // || x.LocRelease.ToString().Contains(searchText.ToLower())

                                               || x.TypeProof.ToString().Contains(searchText.ToLower())

                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                             // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).ToList();
            }

            return model;
        }



        public async Task<CustomerViewModel> one(long? id)
        {
            var model = await _db.MyCustomers.Select(x =>
           new CustomerViewModel
           {
               IdmyCu = x.Id,
               IdcumtomerAll = x.IdCustomer,
               Createat = x.Createat,
               Email = x.Mycustomerscustomertable.Email,
               Name = x.Mycustomerscustomertable.Name,
               EndDate = x.Mycustomerscustomertable.EndDate,
               LocRelease = x.Mycustomerscustomertable.LocRelease,
               LocWork = x.Mycustomerscustomertable.LocWork,
               Nationality = x.Mycustomerscustomertable.Nationality,
               NumProof = x.Mycustomerscustomertable.NumProof,
               PhoneWork = x.Mycustomerscustomertable.PhoneWork,
               PrivateNote = x.PrivateNote,
               PublicNote = x.Mycustomerscustomertable.PublicNote,
               ReleaseDate = x.Mycustomerscustomertable.ReleaseDate,
               Sex = x.Mycustomerscustomertable.Sex,
               Type = x.Mycustomerscustomertable.Type,
               TypeProof = x.Mycustomerscustomertable.TypeProof,
               TypeWork = x.Mycustomerscustomertable.TypeWork,
               IdAccount = x.IdAccount,
               IdSub = x.Idsub,
               VisitEndDate = x.VisitEndDate,
               is_my = true,

               Id_Area = x.Mycustomerscustomertable.IdArea,
               IdNationality= x.Mycustomerscustomertable.IdNationality,

               area = new AreaViewModel
               {
                   Id = x.Mycustomerscustomertable.Customertableareatable.Id,
                   Name = x.Mycustomerscustomertable.Customertableareatable.Name,
                   NameCity = x.Mycustomerscustomertable.Customertableareatable.Areatablecitytable.Name,
                   IdCity = x.Mycustomerscustomertable.Customertableareatable.IdCity,

               }

           }).Where(x => x.IdmyCu == id).FirstOrDefaultAsync();

            return model;
        }


        public async Task<CustomerViewModel> oneForAll(long? id)
        {
            var model = await _db.CustomerTables.Select(x =>
           new CustomerViewModel
           {
               //IdmyCu = x.Id,
               IdcumtomerAll = x.Id,
               Createat = x.Createat,
               Email = x.Email,
               Name = x.Name,
               EndDate = x.EndDate,
               LocRelease = x.LocRelease,
               LocWork = x.LocWork,
               Nationality = x.Nationality,
               NumProof = x.NumProof,
               PhoneWork = x.PhoneWork,
               //PrivateNote = x.PrivateNote,
               PublicNote = x.PublicNote,
               ReleaseDate = x.ReleaseDate,
               Sex = x.Sex,
               Type = x.Type,
               TypeProof = x.TypeProof,
               TypeWork = x.TypeWork,
              // IdAccount = x.IdAccount,
              // IdSub = x.Idsub,
              // VisitEndDate = x.VisitEndDate,
               is_my = false,
               Id_Area = x.IdArea,
               area = new AreaViewModel
               {
                   Id = x.Customertableareatable.Id,
                   Name = x.Customertableareatable.Name,
                   NameCity = x.Customertableareatable.Areatablecitytable.Name,
                   IdCity = x.Customertableareatable.IdCity,

               }
           }).Where(x => x.IdcumtomerAll == id).FirstOrDefaultAsync();

            return model;
        }

        public async Task<MyCustomer> AddToMyCustomer(
            MyCustomer MyCu,
            string nameCustomer
         
            )
        {
            var mycu = _db.MyCustomers.Where(x => x.IdCustomer == MyCu.IdCustomer).FirstOrDefault();
            if (mycu == null)
            {
                _AccountService ass = new _AccountService(_db);

                _AccountViewModel _avm = new _AccountViewModel
                {
                    IdGroup = Static_Group_Accounts.Customer_Individual,
                    Status = Status_Account.active,
                    Name = nameCustomer


                };

                int idac = await ass.CreateAsync(_avm);

                mycu = new MyCustomer
                {
                    IdCustomer = MyCu.IdCustomer,
                    VisitEndDate = DateTime.Now,
                    IdAccount = idac,
                    Createat = DateTime.Now,
                    Idsub = MyCu.Idsub,
                    PrivateNote = MyCu.PrivateNote


                };
                var idMyCustomer = _db.InsertWithIdentity(mycu);

                mycu.Id = Convert.ToInt64( idMyCustomer);

                return mycu;

            }
            return mycu;


        }

        public async Task<ListIdLongAcc> EditAll(CustomerViewModel model)
        {


            ListIdLongAcc isFound = new ListIdLongAcc
            {
                status = 0,
            };

            var cu = _db.CustomerTables.Find(model.IdcumtomerAll);

            if(model.Nationality ==null || model.Nationality =="")
            {
                model.Nationality=cu.Nationality;
            }
            if (model.TypeProof == null || model.TypeProof == "")
            {
                model.TypeProof = cu.TypeProof;
            }

            if (cu != null)
                {
                    cu.Name = model.Name;
                    cu.Sex = model.Sex;
                    cu.Email = model.Email;
                    cu.EndDate = model.EndDate;
                    cu.LocRelease = model.LocRelease;
                    cu.LocWork = model.LocWork;
                    cu.Nationality = model.Nationality;
                    cu.NumProof = model.NumProof;
                    cu.PhoneWork = model.PhoneWork;
                    cu.PublicNote = model.PublicNote;
                    cu.ReleaseDate = model.ReleaseDate;
                    cu.Type = model.Type;
                    cu.TypeProof = model.TypeProof;
                    cu.TypeWork = model.TypeWork;
                cu.IdNationality= model.IdNationality;

                cu.IdArea = model.Id_Area;

                _db.Update(cu);

                  return isFound;

                }
            isFound.status = -5; //error in save
                return isFound;

          
        }



        public async Task<int> Edit(long id, CustomerViewModel model)
        {
            var mycu = _db.MyCustomers.Find(id);
            if (mycu != null)
            {
                mycu.Createat = DateTime.Now;
                mycu.PrivateNote = model.PrivateNote;
                await _db.UpdateAsync(mycu);

                var cu = _db.CustomerTables.Find(model.IdcumtomerAll);

                if (cu != null)
                {
                    cu.Name = model.Name;
                    cu.Sex = model.Sex;
                    cu.Email = model.Email;
                    cu.EndDate = model.EndDate;
                    cu.LocRelease = model.LocRelease;
                    cu.LocWork = model.LocWork;
                    cu.Nationality = model.Nationality;
                    cu.NumProof = model.NumProof;
                    cu.PhoneWork = model.PhoneWork;
                    cu.PublicNote = model.PublicNote;
                    cu.ReleaseDate = model.ReleaseDate;
                    cu.Type = model.Type;
                    cu.TypeProof = model.TypeProof;
                    cu.TypeWork = model.TypeWork;
                    cu.IdArea = model.Id_Area;
                    cu.IdNationality = model.IdNationality; 

                    await _db.UpdateAsync(cu);

                }
                return 1;

            }
            return 0;

        }

        public async Task<ListIdLongAcc> checkFoundCu(CustomerViewModel model)
        {

            CustomerViewModel foundCu = new CustomerViewModel();
            ListIdLongAcc tii = new ListIdLongAcc
            {
                status = 0,
            };


            //--- التحقق بالهوية

            foundCu = _db.MyCustomers.Where(x => x.Mycustomerscustomertable.TypeProof == model.TypeProof && x.Mycustomerscustomertable.NumProof == model.NumProof).
                  Select(x => new CustomerViewModel
                  {
                      Createat = x.Createat,
                      IdAccount = x.IdAccount,
                      Email = x.Mycustomerscustomertable.Email,
                      EndDate = x.Mycustomerscustomertable.EndDate,
                      IdcumtomerAll = x.IdCustomer,
                      IdmyCu = x.Id,
                      Name = x.Mycustomerscustomertable.Name,
                      LocRelease = x.Mycustomerscustomertable.LocRelease,
                      LocWork = x.Mycustomerscustomertable.LocWork,
                      Nationality = x.Mycustomerscustomertable.Nationality,
                      NumProof = x.Mycustomerscustomertable.NumProof,
                      PhoneWork = x.Mycustomerscustomertable.PhoneWork,
                      PublicNote = x.Mycustomerscustomertable.PublicNote,
                      PrivateNote = x.PrivateNote,
                      ReleaseDate = x.Mycustomerscustomertable.ReleaseDate,
                      Sex = x.Mycustomerscustomertable.Sex,
                      Type = x.Mycustomerscustomertable.Type,
                      TypeProof = x.Mycustomerscustomertable.TypeProof,
                      TypeWork = x.Mycustomerscustomertable.TypeWork,
                      VisitEndDate = x.VisitEndDate,
                      IdNationality = x.Mycustomerscustomertable.IdNationality,
                      Id_Area= x.Mycustomerscustomertable.IdArea,

                  }).
                  FirstOrDefault();
            if (foundCu != null)
            {
                tii.IDs = new List<long>();

                if (foundCu.Name != model.Name)
                {
                    tii.status = -2; // متساوي بالهوية مختلف بالاسم

                    tii.messege = "رقم الهوية تتبع اسم عميل اخر";
                }
                else
                {
                    tii.status = -1; // متساوي بالهوية , بالاسم
                    tii.messege = "بيانات العميل مدخلة سابقا";
                }
                tii.IDs.Add(Convert.ToInt64(foundCu.IdcumtomerAll));

                tii.IDs.Add(Convert.ToInt64(foundCu.IdAccount));
                tii.IDs.Add(Convert.ToInt64(foundCu.IdmyCu));

                tii.modelCu = foundCu;

                return tii;

            }
            //----------------التحقق بالاسم


            foundCu = _db.MyCustomers.Where(x => x.Mycustomerscustomertable.Name == model.Name).
                         Select(x => new CustomerViewModel
                         {
                             Createat = x.Createat,
                             IdAccount = x.IdAccount,
                             Email = x.Mycustomerscustomertable.Email,
                             EndDate = x.Mycustomerscustomertable.EndDate,
                             IdcumtomerAll = x.IdCustomer,
                             IdmyCu = x.Id,
                             Name = x.Mycustomerscustomertable.Name,
                             LocRelease = x.Mycustomerscustomertable.LocRelease,
                             LocWork = x.Mycustomerscustomertable.LocWork,
                             Nationality = x.Mycustomerscustomertable.Nationality,
                             NumProof = x.Mycustomerscustomertable.NumProof,
                             PhoneWork = x.Mycustomerscustomertable.PhoneWork,
                             PublicNote = x.Mycustomerscustomertable.PublicNote,
                             PrivateNote = x.PrivateNote,
                             ReleaseDate = x.Mycustomerscustomertable.ReleaseDate,
                             Sex = x.Mycustomerscustomertable.Sex,
                             Type = x.Mycustomerscustomertable.Type,
                             TypeProof = x.Mycustomerscustomertable.TypeProof,
                             TypeWork = x.Mycustomerscustomertable.TypeWork,
                             VisitEndDate = x.VisitEndDate,
                             IdNationality = x.Mycustomerscustomertable.IdNationality,
                             Id_Area = x.Mycustomerscustomertable.IdArea,

                         }).
                         FirstOrDefault();




            if (foundCu != null)
            {
                tii.IDs = new List<long>();

                if (foundCu.TypeProof == model.TypeProof && foundCu.NumProof == model.NumProof)
                {
                    tii.status = -1; // متساوي بالهوية , بالاسم
                    tii.messege = "بيانات العميل مدخلة سابقا";
                }
                else
                {
                    tii.status = -3; // متساوي بالاسم مختلف بالهوية

                    tii.messege = "الاسم المدخل موجود مسبقا بهوية اخرى";
                }
                tii.IDs.Add(Convert.ToInt64(foundCu.IdcumtomerAll));

                tii.IDs.Add(Convert.ToInt64(foundCu.IdAccount));
                tii.IDs.Add(Convert.ToInt64(foundCu.IdmyCu));

                tii.modelCu = foundCu;





                return tii;

            }

            return tii;
        }

        public async Task<ListIdLongAcc> CreateAsync(CustomerViewModel model)
        {

            ListIdLongAcc isFound = await checkFoundCu(model);

            if(isFound.status !=0)
            {
                return isFound;
            }

            using (var t = _db.BeginTransaction())
            {
                try
                {
                    _AccountService ass = new _AccountService(_db);

                    _AccountViewModel _avm = new _AccountViewModel
                    {
                        IdGroup = Static_Group_Accounts.Customer_Individual,
                        Status = Status_Account.active,
                        Name = model.Name


                    };


                    int idac = await ass.CreateAsync(_avm);

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
                        IdArea = model.Id_Area,
                        IdNationality= model.IdNationality,


                    };


                    var idcustomer = _db.InsertWithIdentity(cu);


                    MyCustomer mycuModel = new MyCustomer
                    {
                        IdAccount = idac,
                        Createat = DateTime.Now,
                        IdCustomer = Convert.ToInt64(idcustomer),
                        PrivateNote = model.PrivateNote,
                        Idsub = model.IdSub,

                    };
                    var idMyCustomer = _db.InsertWithIdentity(mycuModel);

                    isFound.IDs = new List<long>();

                    isFound.IDs.Add(Convert.ToInt64(idcustomer));
                    isFound.IDs.Add(idac);
                    isFound.IDs.Add(Convert.ToInt64(idMyCustomer));

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
