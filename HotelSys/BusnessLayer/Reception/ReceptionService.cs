using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class ReceptionService
    {
        int idSub = 1;

        private readonly HotelAlkheerDB _db;

        public ReceptionService(HotelAlkheerDB context)
        {
            _db = context;
        }

        private long saveReceptiom(ReceptionViewModel model)
        {

            if(model.IdReception == 0)
                {
                RecetionTable rt = new RecetionTable
                {
                    ChechoutDate = model.ChechoutDate,
                    CheckinDate = model.CheckinDate,

                    IdCo = model.IdCo,
                    IdRoom = model.IdRoom,
                    IsChechin = false,
                    IsChechout = false,
                    Price = model.Price,
                    QtyTime = model.QtyTime,
                    Source = model.Source,
                    StartDate = model.StartDate,// DateTime.Now,// mo.StartDate,
                    EndDate = model.EndDate,//DateTime.Now,//mo.EndDate,
                    TypeDate = model.TypeDate,
                    Unit = model.Unit,
                    IdMyCustomer = model.IdCustomer,
                    Status=1 ,
                    WhyVisit=model.WhyVisit,    
                    AreaFrom = model.AreaFrom,

                    



                };

                var NewReception = _db.InsertWithIdentity(rt);

                long repIdn = Convert.ToInt64(NewReception);
                return repIdn;
            }
            else if (model.IdReception > 0)
            {
              var temp=  _db.RecetionTables.Where(xx => xx.Id == model.IdReception).FirstOrDefault();
               // temp.ChechoutDate = model.ChechoutDate;

               // temp.CheckinDate = model.CheckinDate;

                temp.IdCo = model.IdCo;
                temp.IdRoom = model.IdRoom;
               // temp.IsChechin = false;
               // temp.IsChechout = false;
                temp.Price = model.Price;
                temp.QtyTime = model.QtyTime;
                temp.Source = model.Source;
                temp.StartDate = model.StartDate;// DateTime.Now,// mo.StartDate,
                temp.EndDate = model.EndDate;//DateTime.Now,//mo.EndDate,
                temp.TypeDate = model.TypeDate;
                temp.Unit = model.Unit;
                temp.IdMyCustomer = model.IdCustomer;
                temp.AreaFrom = model.AreaFrom;
                temp.WhyVisit  = model.WhyVisit;

                 _db.Update(temp);







                return temp.Id;
            }
           
            return 0;
        }
        public bool updateStatus(int status,long idreception)
        {
            var temp = _db.RecetionTables.Where(xx => xx.Id == idreception).FirstOrDefault();
            temp.Status = status;
            if(status==2)
            {
                temp.IsChechin=true;
                temp.CheckinDate=DateTime.Now;

            }
            else if (status == 3)
            {
                temp.IsChechout = true;
                temp.ChechoutDate = DateTime.Now;

            }

            int stuuu=   _db.Update(temp);
            if (stuuu > 0)

                return true;

            else return false;
        }

        public bool updateQty(int QtyTime, long idreception, DateTime EndDate)
        {
            var temp = _db.RecetionTables.Where(xx => xx.Id == idreception).FirstOrDefault();
            temp.QtyTime = temp.QtyTime +QtyTime;
            temp.EndDate = EndDate;

            int stuuu = _db.Update(temp);
            if (stuuu > 0)

                return true;

            else return false;

        }
        public async Task<ListIdLong> CreateAsync(ReceptionViewModel mo)
        {

            ListIdLong tii = new ListIdLong();

          


                using (var t = _db.BeginTransaction())
            {
                try
                {
                    CustomerService cs = new CustomerService(_db);

                    int IDAccountReception = Convert.ToInt32(mo.IDAccountReception);



                    MyCustomer myCu = new MyCustomer
                    {
                        Id = mo.customer.IdmyCu,
                        IdCustomer = mo.customer.IdcumtomerAll,
                        IdAccount = mo.customer.IdAccount,
                        PrivateNote = mo.customer.PrivateNote,
                        Idsub = mo.customer.IdSub
                    };

                    if (mo.customer.is_my == false)
                    {
                        myCu = await cs.AddToMyCustomer(myCu, mo.NameCustomer);

                        if (IDAccountReception == 0) //mo.IdCo ==0 || mo.IdCo==null
                        {
                            IDAccountReception = Convert.ToInt32(myCu.IdAccount);
                        }
                    }

                    DateTime dateSt = new DateTime(mo.StartDate.Year, mo.StartDate.Month, mo.StartDate.Day);
                    DateTime dateEn = new DateTime(mo.EndDate.Year, mo.EndDate.Month, mo.EndDate.Day);

                    var temp = _db.RecetionTables.Where(xx => xx.Id == mo.IdReception).FirstOrDefault();


                    if (myCu.Id != 0)
                    {
                        try
                        {
                            mo.StartDate = dateSt;
                            mo.EndDate = dateEn;
                            mo.IdCustomer = myCu.Id;
                            long repIdn = 0;


                            repIdn = saveReceptiom(mo);



                            //**followers
                            FollowersService fs = new FollowersService(_db);
                            if (mo.followers != null)
                            {
                                if (mo.IdReception == 0)
                                {

                                    await fs.AddForReception(repIdn, mo.followers, mo.customer.IdSub);

                                }

                                else
                                {
                                    await fs.EditForReception(repIdn, mo.followers, mo.customer.IdSub);

                                }
                            }

                            //status

                            if(mo.IdReception==0)
                            {
                                Status_RoomService sr = new Status_RoomService(_db);
                                var st = Status_RoomsName.listStatus[3].index.ToString();

                                Status_Current_RoomViewModel stViewModel = new Status_Current_RoomViewModel
                                {
                                    IdRoom = mo.IdRoom,
                                    Status = st,
                                    IdReception = repIdn,
                                    StartDate = dateSt,
                                    EndDate = dateEn,

                                    //IdEmp = mo.IdEmp
                                };

                                var cStIDdetials = sr.changByRoom(stViewModel);
                            }
                            

                            //bill type reception 
                            long idbill = 0;


                            bills_Service bs = new bills_Service(_db);
                            mo.bill.IdReception = repIdn;

                            mo.bill.IdAccount = IDAccountReception;

                            Value_Return vr = new Value_Return();

                            if (mo.IdReception == 0)
                            {
                                mo.bill.Type = type_document.reservation.id_document;
                                mo.bill.TypePay = pay_type.last.id_document;
                                mo.bill.Date = DateTime.Now;
                                t.Commit();

                                vr = bs.addReceptionAsync(mo.bill);
                                idbill = vr.id_long;

                            }
                            else
                            {

                                


                                if(mo.status==2 && temp.Price != mo.Price)
                                {
                                    
                                        bs.updateReceptionLoginAsync(mo.bill);

                                   
                                }
                                else
                                {
                                    bs.updateReceptionAsync(mo.bill);
                                }    
                               

                              
                                idbill = mo.bill.Id;
                            }

                            t.Commit();
                            tii.IDs = new List<long>();

                            tii.IDs.Add(repIdn);
                            tii.IDs.Add(idbill);
                            tii.IDs.Add(Convert.ToInt64(myCu.IdAccount));
                           
                            return tii;



                        }
                        catch (Exception dd)
                        {

                        }




                    }
                   
                    // return vr;
                }
                catch (ApplicationException e)
                {
                    await t.RollbackAsync();
                    //vr.success = false;
                    //vr.id_long = billViewModel.Id;
                    //vr.message = messageApp.txt_message[0];
                }



            }
           
            return tii;
        }

        public  ReceptionViewModel getOneAsync(long id)
        {
            var model =  _db.RecetionTables.
                Where(xxx=>xxx.Id==id).
          Select(x => new ReceptionViewModel
          {
              IdReception = x.Id,
              Source = x.Source,
              StartDate = x.StartDate.Date,
              EndDate = x.EndDate.Date,
              IsChechin = x.IsChechin,
              CheckinDate = x.CheckinDate,
              IsChechout = x.IsChechout,
              ChechoutDate = x.ChechoutDate,
              IdCo = x.IdCo,
              Price = x.Price,
              QtyTime = x.QtyTime,
              Unit = x.Unit,
              TypeDate = x.TypeDate,
              status=x.Status,
              AreaFrom = x.AreaFrom,
              WhyVisit = x.WhyVisit,
            

              room = new PriceRoomsViewModel
              {
                  //Id = x.Recetiontableroomstable.Id,
                  NameRoom = x.Recetiontableroomstable.NameR,
                  NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT,
                  IdRoom= x.Recetiontableroomstable.Id,
                 // IdTaxGroup=x.Recetiontableroomstable.
                  
              },
              company = new _CompanyViewModel
              {
                  IdCo = x.Fkcompanyrecetion.Id,
                  NameCo = x.Fkcompanyrecetion.Name,
                  IdAccountCo = x.Fkcompanyrecetion.IdAccount
              },

              followers = x.Fkcustomersreceptionreceptions
              //.Where(y => y.IdReceptoin == x.Id) //&& y.CuType == TypeCustomerHelp.FollowerType.type

            .Select(
                zz => new FollowerViewModel
                {
                    follwerCusomer = new CustomerViewModel
                    {

                        IdmyCu = 0,
                           IdcumtomerAll=zz.IdCustomer,
                           
                           is_my = false,
                        Name = zz.Followerreceptiontablecustomertable.Name,
                        Sex = zz.Followerreceptiontablecustomertable.Sex,
                        Email = zz.Followerreceptiontablecustomertable.Email,
                    },
                    Relation = zz.Relation,
                    Duration = zz.Duration,
                    DurationFrom = zz.DurationFrom,
                    DurationTo = zz.DurationTo


                }).ToList(),

              bill = new BillReceptionViewModel
              {
                
                      Id=x.Fkbillsreceptions.Where(ee=>ee.IdReception==x.Id).FirstOrDefault().Id,
                      Createat= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Createat,
                      CustomerOrCompany= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().CustomerOrCompany,
                      DeserveAmount= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().DeserveAmount,
                      Date= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Date,
                      IdAccount= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().IdAccount,
                      IdCurrancy= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().IdCurrancy,
                      IdReception= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().IdReception,
                      Note=x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Note,
                      NumReference= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().NumReference,
                      QtyDiscount= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().QtyDiscount,
                      Total= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Total,
                      Type= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Type,
                      TypeDiscount= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TypeDiscount,
                      TypePay= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TypePay,
                      QtyDiscountRate= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().QtyDiscount /
                      x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().Total *100,

                      IncludeTax = Convert.ToBoolean( x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().IncludeTax) ,
                      TotalTaxPrice = x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TotalTaxPrice,
                      TotalTaxRate = x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TotalTaxRate,

                      IsBaladiTax = Convert.ToBoolean(x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().IsBaladiTax),
                      TotalBaladiTaxPrice= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TotalBaladiTaxPrice,
                      TotalBaladiTaxRate= x.Fkbillsreceptions.Where(ee => ee.IdReception == x.Id).FirstOrDefault().TotalBaladiTaxRate,



              },


              customer =
              new CustomerViewModel
              {
                  IdmyCu = x.Recetiontablemycustomer.Id,
                  IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                  // IdAccount=yy.
                  is_my = true,
                  Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                  Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                  Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,
                  Createat= x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                  IdAccount=x.Recetiontablemycustomer.IdAccount,
                  LocRelease= x.Recetiontablemycustomer.Mycustomerscustomertable.LocRelease,
                  EndDate= x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                  LocWork= x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork,
                  Nationality= x.Recetiontablemycustomer.Mycustomerscustomertable.Nationality,
                  NumProof= x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,
                  PhoneWork= x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                  PrivateNote= x.Recetiontablemycustomer.PrivateNote,
                  PublicNote= x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                  ReleaseDate= x.Recetiontablemycustomer.Mycustomerscustomertable.ReleaseDate,
                  Type= x.Recetiontablemycustomer.Mycustomerscustomertable.Type,
                  TypeProof= x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof,
                  TypeWork= x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                  VisitEndDate= x.Recetiontablemycustomer.VisitEndDate,
                  IdSub= x.Recetiontablemycustomer.Idsub



              },



             
              
              //_db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

            //.Select(
            //    yy => new CustomerViewModel
            //    {
            //        IdmyCu = yy.IdCustomer,
            //        IdcumtomerAll=yy.Fkcustomersreceptioncustomer.Id,
            //           // IdAccount=yy.
            //           is_my = true,
            //        Name = yy.Fkcustomersreceptioncustomer.Name,
            //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
            //        Email = yy.Fkcustomersreceptioncustomer.Email,



            //    }).FirstOrDefault(),

              total = Convert.ToDouble(x.Price * x.QtyTime )


          })

           .FirstOrDefault();

            return model;

        }
   

        public async Task<ReceptionDT> ListDataTable(int idSub, JqueryDatatableParam param)
        {

            List<ReceptionViewModel> Parts = new List<ReceptionViewModel>();

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };


            int limit = para.limit;
            int offset = para.offset;


            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = para.sort;
            String order = para.order;

            long iduser = para.userid;

            List<ReceptionViewModel> list = new List<ReceptionViewModel>();

            int rowCount = 0;

            if (string.IsNullOrEmpty(searchText))
            {
                list = await _db.RecetionTables.
                              
                            Select(x => new ReceptionViewModel
                            {
                                IdReception = x.Id,
                                Source = x.Source,
                                StartDate = x.StartDate.Date,
                                EndDate = x.EndDate.Date,
                                IsChechin = x.IsChechin,
                                CheckinDate = x.CheckinDate,
                                IsChechout = x.IsChechout,
                                ChechoutDate = x.ChechoutDate,
                                IdCo = x.IdCo,
                                Price = x.Price,
                                QtyTime = x.QtyTime,
                                Unit = x.Unit,
                                TypeDate = x.TypeDate,
                                status = x.Status,
                                AreaFrom=x.AreaFrom,
                                WhyVisit=x.WhyVisit,

                                room = new PriceRoomsViewModel
                                {
                                    Id = x.Recetiontableroomstable.Id,
                                    NameRoom = x.Recetiontableroomstable.NameR,
                                    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                                },
                                company = new _CompanyViewModel
                                {
                                    IdCo = x.Fkcompanyrecetion.Id,
                                    NameCo = x.Fkcompanyrecetion.Name,
                                    IdAccountCo = x.Fkcompanyrecetion.IdAccount
                                },

                                followers = x.Fkcustomersreceptionreceptions
                              //.
                              //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                              .Select(
                                  zz => new FollowerViewModel
                                  {
                                      follwerCusomer = new CustomerViewModel
                                      {

                                          IdmyCu = 0,
                                          IdcumtomerAll = zz.IdCustomer,

                                          is_my = false,
                                          Name = zz.Followerreceptiontablecustomertable.Name,
                                          Sex = zz.Followerreceptiontablecustomertable.Sex,
                                          Email = zz.Followerreceptiontablecustomertable.Email,
                                      },
                                      Relation = zz.Relation,
                                      Duration = zz.Duration,
                                      DurationFrom = zz.DurationFrom,
                                      DurationTo = zz.DurationTo


                                  }).ToList(),

                                bill = new BillReceptionViewModel
                                {
                                    // Id=x.Fkbillsreceptions.i

                                },

                                customer =
                             new CustomerViewModel
                             {
                                 IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                 IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                 // IdAccount=yy.
                                 is_my = true,
                                 Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                 Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                 Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                             },




                                //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                                //.Select(
                                //    yy => new CustomerViewModel
                                //    {
                                //        IdmyCu = yy.IdCustomer,
                                //           // IdAccount=yy.
                                //           is_my = true,
                                //        Name = yy.Fkcustomersreceptioncustomer.Name,
                                //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                                //        Email = yy.Fkcustomersreceptioncustomer.Email,



                                //    }).FirstOrDefault(),

                                total = Convert.ToDouble(x.Price * x.QtyTime)


                            }).OrderByDescending(x => x.IdReception).
                                   Skip(offset).
                                  Take(limit)
                             //.DistinctBy(p => p.Id)

                             .ToListAsync();

                rowCount = _db.RecetionTables.Count();

            }




            else if (!string.IsNullOrEmpty(searchText))
            {

                list = await _db.RecetionTables.
                             Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).
                           Select(x => new ReceptionViewModel
                           {
                               IdReception = x.Id,
                               Source = x.Source,
                               StartDate = x.StartDate.Date,
                               EndDate = x.EndDate.Date,
                               IsChechin = x.IsChechin,
                               CheckinDate = x.CheckinDate,
                               IsChechout = x.IsChechout,
                               ChechoutDate = x.ChechoutDate,
                               IdCo = x.IdCo,
                               Price = x.Price,
                               QtyTime = x.QtyTime,
                               Unit = x.Unit,
                               TypeDate = x.TypeDate,
                               status = x.Status,
                               AreaFrom = x.AreaFrom,
                               WhyVisit = x.WhyVisit,


                               room = new PriceRoomsViewModel
                               {
                                   Id = x.Recetiontableroomstable.Id,
                                   NameRoom = x.Recetiontableroomstable.NameR,
                                   NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                               },
                               company = new _CompanyViewModel
                               {
                                   IdCo = x.Fkcompanyrecetion.Id,
                                   NameCo = x.Fkcompanyrecetion.Name,
                                   IdAccountCo = x.Fkcompanyrecetion.IdAccount
                               },

                               followers = x.Fkcustomersreceptionreceptions
                             //.
                             //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                             .Select(
                                 zz => new FollowerViewModel
                                 {
                                     follwerCusomer = new CustomerViewModel
                                     {

                                         IdmyCu = 0,
                                         IdcumtomerAll = zz.IdCustomer,

                                         is_my = false,
                                         Name = zz.Followerreceptiontablecustomertable.Name,
                                         Sex = zz.Followerreceptiontablecustomertable.Sex,
                                         Email = zz.Followerreceptiontablecustomertable.Email,
                                     },
                                     Relation = zz.Relation,
                                     Duration = zz.Duration,
                                     DurationFrom = zz.DurationFrom,
                                     DurationTo = zz.DurationTo


                                 }).ToList(),

                               bill = new BillReceptionViewModel
                               {
                                   // Id=x.Fkbillsreceptions.i

                               },

                               customer =
                            new CustomerViewModel
                            {
                                IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                // IdAccount=yy.
                                is_my = true,
                                Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                            },




                               //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                               //.Select(
                               //    yy => new CustomerViewModel
                               //    {
                               //        IdmyCu = yy.IdCustomer,
                               //           // IdAccount=yy.
                               //           is_my = true,
                               //        Name = yy.Fkcustomersreceptioncustomer.Name,
                               //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                               //        Email = yy.Fkcustomersreceptioncustomer.Email,



                               //    }).FirstOrDefault(),

                               total = Convert.ToDouble(x.Price * x.QtyTime)


                           }).OrderByDescending(x => x.IdReception).
                                  Skip(offset).
                                 Take(limit)
                            //.DistinctBy(p => p.Id)

                            .ToListAsync();
                rowCount = _db.RecetionTables.
                            Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower()))


                             //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                             .Count();

            }
            ReceptionDT model = new ReceptionDT
            {
                list = list,
                rowCount = rowCount
            };

            return model;
        }


        public async Task<List<ReceptionViewModel>> ListBetweenDate(int idSub, paramModel request, DateTime start, DateTime end)
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


          DateTime ChicOutCheck=new DateTime(end.Year, end.Month, end.Day,11,59,59,000);

            DateTime chinInCheck = new DateTime(start.Year, start.Month, start.Day, 11, 59, 59, 000);







            var model = await _db.RecetionTables.
               Where(x => x.IsChechin == true).
               Where(x => x.IsChechout == false || x.ChechoutDate == null || x.ChechoutDate >= ChicOutCheck).
               Where(x=>x.CheckinDate<= chinInCheck).

             Select(x => new ReceptionViewModel
             {
                 IdReception = x.Id,
                 Source = x.Source,
                 StartDate = x.StartDate.Date,
                 EndDate = x.EndDate.Date,
                 IsChechin = x.IsChechin,
                 CheckinDate = x.CheckinDate,
                 IsChechout = x.IsChechout,
                 ChechoutDate = x.ChechoutDate,
                 IdCo = x.IdCo,
                 Price = x.Price,
                 QtyTime = x.QtyTime,
                 Unit = x.Unit,
                 TypeDate = x.TypeDate,
                 AreaFrom = x.AreaFrom,
                 WhyVisit = x.WhyVisit,


                 //room = new PriceRoomsViewModel
                 //{
                 //    Id = x.Recetiontableroomstable.Id,
                 //    NameRoom = x.Recetiontableroomstable.NameR,
                 //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                 //},
                 company = new _CompanyViewModel
                 {
                     IdCo = x.Fkcompanyrecetion.Id,
                     NameCo = x.Fkcompanyrecetion.Name,
                     IdAccountCo = x.Fkcompanyrecetion.IdAccount
                 },

                 followers = x.Fkcustomersreceptionreceptions
                 //.
                 //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

               .Select(
                   zz => new FollowerViewModel
                   {
                       follwerCusomer = new CustomerViewModel
                       {

                           IdmyCu = 0,
                           IdcumtomerAll = zz.IdCustomer,

                           is_my = false,
                           Name = zz.Followerreceptiontablecustomertable.Name,
                           Sex = zz.Followerreceptiontablecustomertable.Sex,
                           Email = zz.Followerreceptiontablecustomertable.Email,
                       },
                       Relation = zz.Relation,
                       Duration = zz.Duration,
                       DurationFrom = zz.DurationFrom,
                       DurationTo = zz.DurationTo


                   }).ToList(),


                 customer =
              new CustomerViewModel
              {
                  IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                  IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                  // IdAccount=yy.
                  is_my = true,
                  Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                  Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                  Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,
                  Nationality = x.Recetiontablemycustomer.Mycustomerscustomertable.Nationality,
                  LocWork = x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork,
                  TypeWork = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                  PhoneWork = x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                  Createat = x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                  EndDate = x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                  NumProof = x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,

                  TypeProof = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "جواز سفر":"عام",
                  PublicNote = x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                  PrivateNote = x.Recetiontablemycustomer.PrivateNote






              },




                 //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                 //.Select(
                 //    yy => new CustomerViewModel
                 //    {
                 //        IdmyCu = yy.IdCustomer,
                 //           // IdAccount=yy.
                 //           is_my = true,
                 //        Name = yy.Fkcustomersreceptioncustomer.Name,
                 //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                 //        Email = yy.Fkcustomersreceptioncustomer.Email,



                 //    }).FirstOrDefault(),

                 // total = Convert.ToDouble(x.Price * x.QtyTime)


             }).OrderByDescending(x => x.IdReception).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToListAsync();



            if (!string.IsNullOrEmpty(searchText))
            {
                model = model.Where(x => x.customer.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.room.NameRoom != null && x.room.NameRoom.ToLower().Contains(searchText.ToLower()))
                                              || (x.IdReception != 0 && x.IdReception.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).ToList();
            }

            return model;
        }

        public async Task<List<ReceptionViewModel>> ALL_between_dates(int idSub, paramModel request, DateTime start, DateTime end)
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


            DateTime ChicOutCheck = new DateTime(end.Year, end.Month, end.Day, 11, 59, 59, 000);

            DateTime chinInCheck = new DateTime(start.Year, start.Month, start.Day, 11, 59, 59, 000);


            List<ReceptionViewModel> model = new List<ReceptionViewModel>();

            List<ReceptionViewModel> model2 = new List<ReceptionViewModel>();



            if (end.Date >= DateTime.Now.Date)
            {

                model = await _db.RecetionTables.
              Where(x => x.IsChechin == true).
            Where(
             x =>
             x.IsChechout == false || x.ChechoutDate == null).


           Select(x => new ReceptionViewModel
           {
               IdReception = x.Id,
               Source = x.Source,
               StartDate = x.StartDate.Date,
               EndDate = x.EndDate.Date,
               IsChechin = x.IsChechin,
               CheckinDate = x.CheckinDate,
               IsChechout = x.IsChechout,
               ChechoutDate = x.ChechoutDate,
               IdCo = x.IdCo,
               Price = x.Price,
               QtyTime = x.QtyTime,
               Unit = x.Unit,
               TypeDate = x.TypeDate,
               AreaFrom = x.AreaFrom,
               WhyVisit = x.WhyVisit,


               //room = new PriceRoomsViewModel
               //{
               //    Id = x.Recetiontableroomstable.Id,
               //    NameRoom = x.Recetiontableroomstable.NameR,
               //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

               //},
               company = new _CompanyViewModel
               {
                   IdCo = x.Fkcompanyrecetion.Id,
                   NameCo = x.Fkcompanyrecetion.Name,
                   IdAccountCo = x.Fkcompanyrecetion.IdAccount
               },

               followers = x.Fkcustomersreceptionreceptions
             //.
             //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

             .Select(
                 zz => new FollowerViewModel
                 {
                     follwerCusomer = new CustomerViewModel
                     {

                         IdmyCu = 0,
                         IdcumtomerAll = zz.IdCustomer,

                         is_my = false,
                         Name = zz.Followerreceptiontablecustomertable.Name,
                         Sex = zz.Followerreceptiontablecustomertable.Sex,
                         Email = zz.Followerreceptiontablecustomertable.Email,
                     },
                     Relation = zz.Relation,
                     Duration = zz.Duration,
                     DurationFrom = zz.DurationFrom,
                     DurationTo = zz.DurationTo


                 }).ToList(),


               customer =
            new CustomerViewModel
            {
                IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                // IdAccount=yy.
                is_my = true,
                Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,
                Nationality = x.Recetiontablemycustomer.Mycustomerscustomertable.Nationality,
                LocWork = x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork,
                TypeWork = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                PhoneWork = x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                Createat = x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                EndDate = x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                NumProof = x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,
                TypeProof = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "2" ? "جواز سفر " : "عام",
                PublicNote = x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                PrivateNote = x.Recetiontablemycustomer.PrivateNote






            },




               //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

               //.Select(
               //    yy => new CustomerViewModel
               //    {
               //        IdmyCu = yy.IdCustomer,
               //           // IdAccount=yy.
               //           is_my = true,
               //        Name = yy.Fkcustomersreceptioncustomer.Name,
               //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
               //        Email = yy.Fkcustomersreceptioncustomer.Email,



               //    }).FirstOrDefault(),

               // total = Convert.ToDouble(x.Price * x.QtyTime)


           }).OrderByDescending(x => x.IdReception).
                  Skip(offset).
                 Take(limit)
            //.DistinctBy(p => p.Id)

            .ToListAsync();




                var reSettign = _db.SettingReceptionTables.FirstOrDefault();

                if (reSettign != null)
                {
                    // reSettign.

                }

                model2 = await _db.RecetionTables.
                   Where(x => x.IsChechin == true).
               Where(x => x.IsChechout == false || x.ChechoutDate == null || x.ChechoutDate >= ChicOutCheck).
               Where(x => x.CheckinDate <= chinInCheck).



                Select(x => new ReceptionViewModel
                {
                    IdReception = x.Id,
                    Source = x.Source,
                    StartDate = x.StartDate.Date,
                    EndDate = x.EndDate.Date,
                    IsChechin = x.IsChechin,
                    CheckinDate = x.CheckinDate,
                    IsChechout = x.IsChechout,
                    ChechoutDate = x.ChechoutDate,
                    IdCo = x.IdCo,
                    Price = x.Price,
                    QtyTime = x.QtyTime,
                    Unit = x.Unit,
                    TypeDate = x.TypeDate,
                    AreaFrom = x.AreaFrom,
                    WhyVisit = x.WhyVisit,


                    //room = new PriceRoomsViewModel
                    //{
                    //    Id = x.Recetiontableroomstable.Id,
                    //    NameRoom = x.Recetiontableroomstable.NameR,
                    //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                    //},
                    company = new _CompanyViewModel
                    {
                        IdCo = x.Fkcompanyrecetion.Id,
                        NameCo = x.Fkcompanyrecetion.Name,
                        IdAccountCo = x.Fkcompanyrecetion.IdAccount
                    },

                    followers = x.Fkcustomersreceptionreceptions
                  //.
                  //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                  .Select(
                      zz => new FollowerViewModel
                      {
                          follwerCusomer = new CustomerViewModel
                          {

                              IdmyCu = 0,
                              IdcumtomerAll = zz.IdCustomer,

                              is_my = false,
                              Name = zz.Followerreceptiontablecustomertable.Name,
                              Sex = zz.Followerreceptiontablecustomertable.Sex,
                              Email = zz.Followerreceptiontablecustomertable.Email,
                          },
                          Relation = zz.Relation,
                          Duration = zz.Duration,
                          DurationFrom = zz.DurationFrom,
                          DurationTo = zz.DurationTo


                      }).ToList(),


                    customer =
                 new CustomerViewModel
                 {
                     IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                     IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                     // IdAccount=yy.
                     is_my = true,
                     Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                     Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                     Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,
                     Nationality = x.Recetiontablemycustomer.Mycustomerscustomertable.Nationality,
                     LocWork = x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork,
                     TypeWork = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                     PhoneWork = x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                     Createat = x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                     EndDate = x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                     NumProof = x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,
                     TypeProof = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "2" ? "جواز سفر " : "عام",
                     PublicNote = x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                     PrivateNote = x.Recetiontablemycustomer.PrivateNote






                 },




                    //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                    //.Select(
                    //    yy => new CustomerViewModel
                    //    {
                    //        IdmyCu = yy.IdCustomer,
                    //           // IdAccount=yy.
                    //           is_my = true,
                    //        Name = yy.Fkcustomersreceptioncustomer.Name,
                    //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                    //        Email = yy.Fkcustomersreceptioncustomer.Email,



                    //    }).FirstOrDefault(),

                    // total = Convert.ToDouble(x.Price * x.QtyTime)


                }).OrderByDescending(x => x.IdReception).
                       Skip(offset).
                      Take(limit)
                 //.DistinctBy(p => p.Id)

                 .ToListAsync();


                if (model2.Count() > 0)
                {
                    model.AddRange(model2);
                }

                var testListNoDups = model.GroupBy(x => new { x.IdReception })
                                                  .Select(x => x.First())
                                                  .ToList();



                if (!string.IsNullOrEmpty(searchText))
                {
                    model = model.Where(x => x.customer.Name.ToLower().Contains(searchText.ToLower())
                                                  || (x.room.NameRoom != null && x.room.NameRoom.ToLower().Contains(searchText.ToLower()))
                                                  || (x.IdReception != 0 && x.IdReception.ToString().Contains(searchText.ToLower()))

                                                   || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                     || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                                  || x.StartDate.ToString().Contains(searchText.ToLower())

                                                   || x.EndDate.ToString().Contains(searchText.ToLower())


                                                  //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                                  // || x.Date.ToLower().Contains(searchText.ToLower())
                                                  ).ToList();
                }
            }

            return model;
        }



        public async Task<List<ReceptionViewModel>> List_In_Hotel(int idSub, paramModel request)
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




            var model = await _db.RecetionTables.
               Where(x => x.IsChechin == true).
               Where(x => x.IsChechout == false || x.ChechoutDate == null).
               //Where(x => x.CheckinDate <= chinInCheck).

             Select(x => new ReceptionViewModel
             {
                 IdReception = x.Id,
                 Source = x.Source,
                 StartDate = x.StartDate.Date,
                 EndDate = x.EndDate.Date,
                 IsChechin = x.IsChechin,
                 CheckinDate = x.CheckinDate,
                 IsChechout = x.IsChechout,
                 ChechoutDate = x.ChechoutDate,
                 IdCo = x.IdCo,
                 Price = x.Price,
                 QtyTime = x.QtyTime,
                 Unit = x.Unit,
                 TypeDate = x.TypeDate,
                 AreaFrom = x.AreaFrom,
                 WhyVisit = x.WhyVisit,


                 //room = new PriceRoomsViewModel
                 //{
                 //    Id = x.Recetiontableroomstable.Id,
                 //    NameRoom = x.Recetiontableroomstable.NameR,
                 //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                 //},
                 company = new _CompanyViewModel
                 {
                     IdCo = x.Fkcompanyrecetion.Id,
                     NameCo = x.Fkcompanyrecetion.Name,
                     IdAccountCo = x.Fkcompanyrecetion.IdAccount
                 },

                 followers = x.Fkcustomersreceptionreceptions
                 //.
                 //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

               .Select(
                   zz => new FollowerViewModel
                   {
                       follwerCusomer = new CustomerViewModel
                       {

                           IdmyCu = 0,
                           IdcumtomerAll = zz.IdCustomer,

                           is_my = false,
                           Name = zz.Followerreceptiontablecustomertable.Name,
                           Sex = zz.Followerreceptiontablecustomertable.Sex,
                           Email = zz.Followerreceptiontablecustomertable.Email,
                       },
                       Relation = zz.Relation,
                       Duration = zz.Duration,
                       DurationFrom = zz.DurationFrom,
                       DurationTo = zz.DurationTo


                   }).ToList(),


                 customer =
              new CustomerViewModel
              {
                  IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                  IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                  // IdAccount=yy.
                  is_my = true,
                  Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                  Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                  Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,
                  Nationality = x.Recetiontablemycustomer.Mycustomerscustomertable.Nationality,
                  LocWork = x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork,
                  TypeWork = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                  PhoneWork = x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                  Createat = x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                  EndDate = x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                  NumProof = x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,
                  TypeProof = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "2" ? "جواز سفر " : "عام",
                  PublicNote = x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                  PrivateNote = x.Recetiontablemycustomer.PrivateNote






              },




                 //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                 //.Select(
                 //    yy => new CustomerViewModel
                 //    {
                 //        IdmyCu = yy.IdCustomer,
                 //           // IdAccount=yy.
                 //           is_my = true,
                 //        Name = yy.Fkcustomersreceptioncustomer.Name,
                 //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                 //        Email = yy.Fkcustomersreceptioncustomer.Email,



                 //    }).FirstOrDefault(),

                 // total = Convert.ToDouble(x.Price * x.QtyTime)


             }).OrderByDescending(x => x.IdReception).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToListAsync();



            if (!string.IsNullOrEmpty(searchText))
            {
                model = model.Where(x => x.customer.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.room.NameRoom != null && x.room.NameRoom.ToLower().Contains(searchText.ToLower()))
                                              || (x.IdReception != 0 && x.IdReception.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).ToList();
            }

            return model;
        }



        public List<ReceptionViewModel> List(int idSub)
        {
            var model = _db.RecetionTables.
                Select(x => new ReceptionViewModel
                {
                    IdReception = x.Id,
                    Source = x.Source,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsChechin = x.IsChechin,
                    CheckinDate = x.CheckinDate,
                    IsChechout = x.IsChechout,
                    ChechoutDate = x.ChechoutDate,
                    IdCo = x.IdCo,
                    Price = x.Price,
                    QtyTime = x.QtyTime,
                    Unit = x.Unit,
                    TypeDate = x.TypeDate,
                    AreaFrom = x.AreaFrom,
                    WhyVisit = x.WhyVisit,


                    room = new PriceRoomsViewModel
                    {
                        Id = x.Recetiontableroomstable.Id,
                        NameRoom = x.Recetiontableroomstable.NameR,
                        NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                    },
                    company = new _CompanyViewModel
                    {
                        IdCo = x.Fkcompanyrecetion.Id,
                        NameCo = x.Fkcompanyrecetion.Name,
                        IdAccountCo = x.Fkcompanyrecetion.IdAccount
                    },

                    followers = x.Fkcustomersreceptionreceptions
               //.
               //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

               .Select(
                   zz => new FollowerViewModel
                   {
                       follwerCusomer = new CustomerViewModel
                       {

                           IdmyCu = 0,
                           IdcumtomerAll = zz.IdCustomer,

                           is_my = false,
                           Name = zz.Followerreceptiontablecustomertable.Name,
                           Sex = zz.Followerreceptiontablecustomertable.Sex,
                           Email = zz.Followerreceptiontablecustomertable.Email,
                       },
                       Relation = zz.Relation,
                       Duration = zz.Duration,
                       DurationFrom = zz.DurationFrom,
                       DurationTo = zz.DurationTo


                   }).ToList(),

                    bill = new BillReceptionViewModel
                    {
                        // Id=x.Fkbillsreceptions.i

                    },


                    customer =
              new CustomerViewModel
              {
                  IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                  IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                  // IdAccount=yy.
                  is_my = true,
                  Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                  Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                  Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



              },



                    //  customer =_db.CustomersReceptionTables.Where(y=>y.IdReceptoin==x.Id && y.CuType=="cu")

                    //.Select(
                    //    yy=>new CustomerViewModel 
                    //    { 
                    //        IdmyCu=yy.IdCustomer,
                    //       // IdAccount=yy.
                    //       is_my=true,
                    //       Name=yy.Fkcustomersreceptioncustomer.Name,
                    //       Sex = yy.Fkcustomersreceptioncustomer.Sex,
                    //       Email = yy.Fkcustomersreceptioncustomer.Email,



                    //    }).FirstOrDefault(),


                }).ToList();

            return model;

        }




        //المؤقته
        public async Task<ReceptionDT> ListTempDT(int idSub, JqueryDatatableParam param)
        {

            List<ReceptionViewModel> Parts = new List<ReceptionViewModel>();

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };


            int limit = para.limit;
            int offset = para.offset;

            
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = para.sort;
            String order = para.order;

            long iduser = para.userid;

            List<ReceptionViewModel> list = new List<ReceptionViewModel>();

            int rowCount = 0;

            if (string.IsNullOrEmpty(searchText))
            {
                list = await _db.RecetionTables.
                               Where(x => x.Status == 1).
                            Select(x => new ReceptionViewModel
                            {
                                IdReception = x.Id,
                                Source = x.Source,
                                StartDate = x.StartDate.Date,
                                EndDate = x.EndDate.Date,
                                IsChechin = x.IsChechin,
                                CheckinDate = x.CheckinDate,
                                IsChechout = x.IsChechout,
                                ChechoutDate = x.ChechoutDate,
                                IdCo = x.IdCo,
                                Price = x.Price,
                                QtyTime = x.QtyTime,
                                Unit = x.Unit,
                                TypeDate = x.TypeDate,
                                status = x.Status,
                                AreaFrom = x.AreaFrom,
                                WhyVisit = x.WhyVisit,


                                room = new PriceRoomsViewModel
                                {
                                    Id = x.Recetiontableroomstable.Id,
                                    NameRoom = x.Recetiontableroomstable.NameR,
                                    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                                },
                                company = new _CompanyViewModel
                                {
                                    IdCo = x.Fkcompanyrecetion.Id,
                                    NameCo = x.Fkcompanyrecetion.Name,
                                    IdAccountCo = x.Fkcompanyrecetion.IdAccount
                                },

                                followers = x.Fkcustomersreceptionreceptions
                              //.
                              //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                              .Select(
                                  zz => new FollowerViewModel
                                  {
                                      follwerCusomer = new CustomerViewModel
                                      {

                                          IdmyCu = 0,
                                          IdcumtomerAll = zz.IdCustomer,

                                          is_my = false,
                                          Name = zz.Followerreceptiontablecustomertable.Name,
                                          Sex = zz.Followerreceptiontablecustomertable.Sex,
                                          Email = zz.Followerreceptiontablecustomertable.Email,
                                      },
                                      Relation = zz.Relation,
                                      Duration = zz.Duration,
                                      DurationFrom = zz.DurationFrom,
                                      DurationTo = zz.DurationTo


                                  }).ToList(),

                                bill = new BillReceptionViewModel
                                {
                                    // Id=x.Fkbillsreceptions.i

                                },

                                customer =
                             new CustomerViewModel
                             {
                                 IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                 IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                 // IdAccount=yy.
                                 is_my = true,
                                 Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                 Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                 Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                             },




                                //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                                //.Select(
                                //    yy => new CustomerViewModel
                                //    {
                                //        IdmyCu = yy.IdCustomer,
                                //           // IdAccount=yy.
                                //           is_my = true,
                                //        Name = yy.Fkcustomersreceptioncustomer.Name,
                                //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                                //        Email = yy.Fkcustomersreceptioncustomer.Email,



                                //    }).FirstOrDefault(),

                                total = Convert.ToDouble(x.Price * x.QtyTime)


                            }).OrderByDescending(x => x.IdReception).
                                   Skip(offset).
                                  Take(limit)
                             //.DistinctBy(p => p.Id)

                             .ToListAsync();

                rowCount = _db.RecetionTables.Where(x => x.Status == 1).Count();

            }




            else if (!string.IsNullOrEmpty(searchText))
            {

                list = await _db.RecetionTables.
                              Where(x => x.Status == 1)
                             .Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).
                           Select(x => new ReceptionViewModel
                           {
                               IdReception = x.Id,
                               Source = x.Source,
                               StartDate = x.StartDate.Date,
                               EndDate = x.EndDate.Date,
                               IsChechin = x.IsChechin,
                               CheckinDate = x.CheckinDate,
                               IsChechout = x.IsChechout,
                               ChechoutDate = x.ChechoutDate,
                               IdCo = x.IdCo,
                               Price = x.Price,
                               QtyTime = x.QtyTime,
                               Unit = x.Unit,
                               TypeDate = x.TypeDate,
                               status = x.Status,
                               AreaFrom = x.AreaFrom,
                               WhyVisit = x.WhyVisit,


                               room = new PriceRoomsViewModel
                               {
                                   Id = x.Recetiontableroomstable.Id,
                                   NameRoom = x.Recetiontableroomstable.NameR,
                                   NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                               },
                               company = new _CompanyViewModel
                               {
                                   IdCo = x.Fkcompanyrecetion.Id,
                                   NameCo = x.Fkcompanyrecetion.Name,
                                   IdAccountCo = x.Fkcompanyrecetion.IdAccount
                               },

                               followers = x.Fkcustomersreceptionreceptions
                             //.
                             //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                             .Select(
                                 zz => new FollowerViewModel
                                 {
                                     follwerCusomer = new CustomerViewModel
                                     {

                                         IdmyCu = 0,
                                         IdcumtomerAll = zz.IdCustomer,

                                         is_my = false,
                                         Name = zz.Followerreceptiontablecustomertable.Name,
                                         Sex = zz.Followerreceptiontablecustomertable.Sex,
                                         Email = zz.Followerreceptiontablecustomertable.Email,
                                     },
                                     Relation = zz.Relation,
                                     Duration = zz.Duration,
                                     DurationFrom = zz.DurationFrom,
                                     DurationTo = zz.DurationTo


                                 }).ToList(),

                               bill = new BillReceptionViewModel
                               {
                                   // Id=x.Fkbillsreceptions.i

                               },

                               customer =
                            new CustomerViewModel
                            {
                                IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                // IdAccount=yy.
                                is_my = true,
                                Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                            },




                               //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                               //.Select(
                               //    yy => new CustomerViewModel
                               //    {
                               //        IdmyCu = yy.IdCustomer,
                               //           // IdAccount=yy.
                               //           is_my = true,
                               //        Name = yy.Fkcustomersreceptioncustomer.Name,
                               //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                               //        Email = yy.Fkcustomersreceptioncustomer.Email,



                               //    }).FirstOrDefault(),

                               total = Convert.ToDouble(x.Price * x.QtyTime)


                           }).OrderByDescending(x => x.IdReception).
                                  Skip(offset).
                                 Take(limit)
                            //.DistinctBy(p => p.Id)

                            .ToListAsync();
                rowCount = _db.RecetionTables.
                              Where(x => x.Status == 1)
                             .Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower()))


                             //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                             .Count();

            }
            ReceptionDT model = new ReceptionDT
            {
                list = list,
                rowCount = rowCount
            };

            return model;
        }

        //الموكده
        public async Task<ReceptionDT> ListDoneDT(int idSub, JqueryDatatableParam param)
        {

            List<ReceptionViewModel> Parts = new List<ReceptionViewModel>();

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };


            int limit = para.limit;
            int offset = para.offset;


            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = para.sort;
            String order = para.order;

            long iduser = para.userid;

            List<ReceptionViewModel> list = new List<ReceptionViewModel>();

            int rowCount = 0;

            if (string.IsNullOrEmpty(searchText))
            {
                list = await _db.RecetionTables.
                               Where(x => x.IsChechin == true).
                            Select(x => new ReceptionViewModel
                            {
                                IdReception = x.Id,
                                Source = x.Source,
                                StartDate = x.StartDate.Date,
                                EndDate = x.EndDate.Date,
                                IsChechin = x.IsChechin,
                                CheckinDate = x.CheckinDate,
                                IsChechout = x.IsChechout,
                                ChechoutDate = x.ChechoutDate,
                                IdCo = x.IdCo,
                                Price = x.Price,
                                QtyTime = x.QtyTime,
                                Unit = x.Unit,
                                TypeDate = x.TypeDate,
                                status = x.Status,
                                AreaFrom = x.AreaFrom,
                                WhyVisit = x.WhyVisit,


                                room = new PriceRoomsViewModel
                                {
                                    Id = x.Recetiontableroomstable.Id,
                                    NameRoom = x.Recetiontableroomstable.NameR,
                                    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                                },
                                company = new _CompanyViewModel
                                {
                                    IdCo = x.Fkcompanyrecetion.Id,
                                    NameCo = x.Fkcompanyrecetion.Name,
                                    IdAccountCo = x.Fkcompanyrecetion.IdAccount
                                },

                                followers = x.Fkcustomersreceptionreceptions
                              //.
                              //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                              .Select(
                                  zz => new FollowerViewModel
                                  {
                                      follwerCusomer = new CustomerViewModel
                                      {

                                          IdmyCu = 0,
                                          IdcumtomerAll = zz.IdCustomer,

                                          is_my = false,
                                          Name = zz.Followerreceptiontablecustomertable.Name,
                                          Sex = zz.Followerreceptiontablecustomertable.Sex,
                                          Email = zz.Followerreceptiontablecustomertable.Email,
                                      },
                                      Relation = zz.Relation,
                                      Duration = zz.Duration,
                                      DurationFrom = zz.DurationFrom,
                                      DurationTo = zz.DurationTo


                                  }).ToList(),

                                bill = new BillReceptionViewModel
                                {
                                    // Id=x.Fkbillsreceptions.i

                                },

                                customer =
                             new CustomerViewModel
                             {
                                 IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                 IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                 // IdAccount=yy.
                                 is_my = true,
                                 Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                 Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                 Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                             },




                                //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                                //.Select(
                                //    yy => new CustomerViewModel
                                //    {
                                //        IdmyCu = yy.IdCustomer,
                                //           // IdAccount=yy.
                                //           is_my = true,
                                //        Name = yy.Fkcustomersreceptioncustomer.Name,
                                //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                                //        Email = yy.Fkcustomersreceptioncustomer.Email,



                                //    }).FirstOrDefault(),

                                total = Convert.ToDouble(x.Price * x.QtyTime)


                            }).OrderByDescending(x => x.IdReception).
                                   Skip(offset).
                                  Take(limit)
                             //.DistinctBy(p => p.Id)

                             .ToListAsync();

                rowCount = _db.RecetionTables.Where(x => x.IsChechin == true).Count();

            }




            else if (!string.IsNullOrEmpty(searchText))
            {

                list = await _db.RecetionTables.
                                Where(x => x.IsChechin == true)
                             .Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).
                           Select(x => new ReceptionViewModel
                           {
                               IdReception = x.Id,
                               Source = x.Source,
                               StartDate = x.StartDate.Date,
                               EndDate = x.EndDate.Date,
                               IsChechin = x.IsChechin,
                               CheckinDate = x.CheckinDate,
                               IsChechout = x.IsChechout,
                               ChechoutDate = x.ChechoutDate,
                               IdCo = x.IdCo,
                               Price = x.Price,
                               QtyTime = x.QtyTime,
                               Unit = x.Unit,
                               TypeDate = x.TypeDate,
                               status = x.Status,
                               AreaFrom = x.AreaFrom,
                               WhyVisit = x.WhyVisit,


                               room = new PriceRoomsViewModel
                               {
                                   Id = x.Recetiontableroomstable.Id,
                                   NameRoom = x.Recetiontableroomstable.NameR,
                                   NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                               },
                               company = new _CompanyViewModel
                               {
                                   IdCo = x.Fkcompanyrecetion.Id,
                                   NameCo = x.Fkcompanyrecetion.Name,
                                   IdAccountCo = x.Fkcompanyrecetion.IdAccount
                               },

                               followers = x.Fkcustomersreceptionreceptions
                             //.
                             //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                             .Select(
                                 zz => new FollowerViewModel
                                 {
                                     follwerCusomer = new CustomerViewModel
                                     {

                                         IdmyCu = 0,
                                         IdcumtomerAll = zz.IdCustomer,

                                         is_my = false,
                                         Name = zz.Followerreceptiontablecustomertable.Name,
                                         Sex = zz.Followerreceptiontablecustomertable.Sex,
                                         Email = zz.Followerreceptiontablecustomertable.Email,
                                     },
                                     Relation = zz.Relation,
                                     Duration = zz.Duration,
                                     DurationFrom = zz.DurationFrom,
                                     DurationTo = zz.DurationTo


                                 }).ToList(),

                               bill = new BillReceptionViewModel
                               {
                                   // Id=x.Fkbillsreceptions.i

                               },

                               customer =
                            new CustomerViewModel
                            {
                                IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                // IdAccount=yy.
                                is_my = true,
                                Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                            },




                               //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                               //.Select(
                               //    yy => new CustomerViewModel
                               //    {
                               //        IdmyCu = yy.IdCustomer,
                               //           // IdAccount=yy.
                               //           is_my = true,
                               //        Name = yy.Fkcustomersreceptioncustomer.Name,
                               //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                               //        Email = yy.Fkcustomersreceptioncustomer.Email,



                               //    }).FirstOrDefault(),

                               total = Convert.ToDouble(x.Price * x.QtyTime)


                           }).OrderByDescending(x => x.IdReception).
                                  Skip(offset).
                                 Take(limit)
                            //.DistinctBy(p => p.Id)

                            .ToListAsync();
                rowCount = _db.RecetionTables.
                                 Where(x => x.IsChechin == true).Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower()))


                             //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                             .Count();

            }
            ReceptionDT model = new ReceptionDT
            {
                list = list,
                rowCount = rowCount
            };

            return model;
        }


        //المؤقته
        public async Task<ReceptionDT> ListCancleDT(int idSub, JqueryDatatableParam param)
        {

            List<ReceptionViewModel> Parts = new List<ReceptionViewModel>();

            var searchText = param.sSearch;

            paramModel para = new paramModel
            {
                limit = param.iDisplayLength,
                offset = param.iDisplayStart,
                order = param.sColumns,
                search = searchText,

            };


            int limit = para.limit;
            int offset = para.offset;


            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = para.sort;
            String order = para.order;

            long iduser = para.userid;

            List<ReceptionViewModel> list = new List<ReceptionViewModel>();

            int rowCount = 0;

            if (string.IsNullOrEmpty(searchText))
            {
                list = await _db.RecetionTables.
                               Where(x => x.Status == 4).
                            Select(x => new ReceptionViewModel
                            {
                                IdReception = x.Id,
                                Source = x.Source,
                                StartDate = x.StartDate.Date,
                                EndDate = x.EndDate.Date,
                                IsChechin = x.IsChechin,
                                CheckinDate = x.CheckinDate,
                                IsChechout = x.IsChechout,
                                ChechoutDate = x.ChechoutDate,
                                IdCo = x.IdCo,
                                Price = x.Price,
                                QtyTime = x.QtyTime,
                                Unit = x.Unit,
                                TypeDate = x.TypeDate,
                                status = x.Status,
                                AreaFrom = x.AreaFrom,
                                WhyVisit = x.WhyVisit,


                                room = new PriceRoomsViewModel
                                {
                                    Id = x.Recetiontableroomstable.Id,
                                    NameRoom = x.Recetiontableroomstable.NameR,
                                    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                                },
                                company = new _CompanyViewModel
                                {
                                    IdCo = x.Fkcompanyrecetion.Id,
                                    NameCo = x.Fkcompanyrecetion.Name,
                                    IdAccountCo = x.Fkcompanyrecetion.IdAccount
                                },

                                followers = x.Fkcustomersreceptionreceptions
                              //.
                              //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                              .Select(
                                  zz => new FollowerViewModel
                                  {
                                      follwerCusomer = new CustomerViewModel
                                      {

                                          IdmyCu = 0,
                                          IdcumtomerAll = zz.IdCustomer,

                                          is_my = false,
                                          Name = zz.Followerreceptiontablecustomertable.Name,
                                          Sex = zz.Followerreceptiontablecustomertable.Sex,
                                          Email = zz.Followerreceptiontablecustomertable.Email,
                                      },
                                      Relation = zz.Relation,
                                      Duration = zz.Duration,
                                      DurationFrom = zz.DurationFrom,
                                      DurationTo = zz.DurationTo


                                  }).ToList(),

                                bill = new BillReceptionViewModel
                                {
                                    // Id=x.Fkbillsreceptions.i

                                },

                                customer =
                             new CustomerViewModel
                             {
                                 IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                 IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                 // IdAccount=yy.
                                 is_my = true,
                                 Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                 Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                 Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                             },




                                //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                                //.Select(
                                //    yy => new CustomerViewModel
                                //    {
                                //        IdmyCu = yy.IdCustomer,
                                //           // IdAccount=yy.
                                //           is_my = true,
                                //        Name = yy.Fkcustomersreceptioncustomer.Name,
                                //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                                //        Email = yy.Fkcustomersreceptioncustomer.Email,



                                //    }).FirstOrDefault(),

                                total = Convert.ToDouble(x.Price * x.QtyTime)


                            }).OrderByDescending(x => x.IdReception).
                                   Skip(offset).
                                  Take(limit)
                             //.DistinctBy(p => p.Id)

                             .ToListAsync();

                rowCount = _db.RecetionTables.Where(x => x.Status == 4).Count();

            }




            else if (!string.IsNullOrEmpty(searchText))
            {

                list = await _db.RecetionTables.
                              Where(x => x.Status == 4)
                             .Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower())


                                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                                              // || x.Date.ToLower().Contains(searchText.ToLower())
                                              ).
                           Select(x => new ReceptionViewModel
                           {
                               IdReception = x.Id,
                               Source = x.Source,
                               StartDate = x.StartDate.Date,
                               EndDate = x.EndDate.Date,
                               IsChechin = x.IsChechin,
                               CheckinDate = x.CheckinDate,
                               IsChechout = x.IsChechout,
                               ChechoutDate = x.ChechoutDate,
                               IdCo = x.IdCo,
                               Price = x.Price,
                               QtyTime = x.QtyTime,
                               Unit = x.Unit,
                               TypeDate = x.TypeDate,
                               status = x.Status,
                               AreaFrom = x.AreaFrom,
                               WhyVisit = x.WhyVisit,


                               room = new PriceRoomsViewModel
                               {
                                   Id = x.Recetiontableroomstable.Id,
                                   NameRoom = x.Recetiontableroomstable.NameR,
                                   NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                               },
                               company = new _CompanyViewModel
                               {
                                   IdCo = x.Fkcompanyrecetion.Id,
                                   NameCo = x.Fkcompanyrecetion.Name,
                                   IdAccountCo = x.Fkcompanyrecetion.IdAccount
                               },

                               followers = x.Fkcustomersreceptionreceptions
                             //.
                             //Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.FollowerType.type)

                             .Select(
                                 zz => new FollowerViewModel
                                 {
                                     follwerCusomer = new CustomerViewModel
                                     {

                                         IdmyCu = 0,
                                         IdcumtomerAll = zz.IdCustomer,

                                         is_my = false,
                                         Name = zz.Followerreceptiontablecustomertable.Name,
                                         Sex = zz.Followerreceptiontablecustomertable.Sex,
                                         Email = zz.Followerreceptiontablecustomertable.Email,
                                     },
                                     Relation = zz.Relation,
                                     Duration = zz.Duration,
                                     DurationFrom = zz.DurationFrom,
                                     DurationTo = zz.DurationTo


                                 }).ToList(),

                               bill = new BillReceptionViewModel
                               {
                                   // Id=x.Fkbillsreceptions.i

                               },

                               customer =
                            new CustomerViewModel
                            {
                                IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                                IdcumtomerAll = x.Recetiontablemycustomer.IdCustomer,
                                // IdAccount=yy.
                                is_my = true,
                                Name = x.Recetiontablemycustomer.Mycustomerscustomertable.Name,
                                Sex = x.Recetiontablemycustomer.Mycustomerscustomertable.Sex,
                                Email = x.Recetiontablemycustomer.Mycustomerscustomertable.Email,



                            },




                               //  customer = _db.CustomersReceptionTables.Where(y => y.IdReceptoin == x.Id && y.CuType == TypeCustomerHelp.customerType.type)

                               //.Select(
                               //    yy => new CustomerViewModel
                               //    {
                               //        IdmyCu = yy.IdCustomer,
                               //           // IdAccount=yy.
                               //           is_my = true,
                               //        Name = yy.Fkcustomersreceptioncustomer.Name,
                               //        Sex = yy.Fkcustomersreceptioncustomer.Sex,
                               //        Email = yy.Fkcustomersreceptioncustomer.Email,



                               //    }).FirstOrDefault(),

                               total = Convert.ToDouble(x.Price * x.QtyTime)


                           }).OrderByDescending(x => x.IdReception).
                                  Skip(offset).
                                 Take(limit)
                            //.DistinctBy(p => p.Id)

                            .ToListAsync();
                rowCount = _db.RecetionTables.
                              Where(x => x.Status == 4)
                             .Where(x => x.Recetiontablemycustomer.Mycustomerscustomertable.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.Recetiontableroomstable.NameR != null && x.Recetiontableroomstable.NameR.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id != 0 && x.Id.ToString().Contains(searchText.ToLower()))

                                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                                              || x.StartDate.ToString().Contains(searchText.ToLower())

                                               || x.EndDate.ToString().Contains(searchText.ToLower()))


                             //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                             .Count();

            }
            ReceptionDT model = new ReceptionDT
            {
                list = list,
                rowCount = rowCount
            };

            return model;
        }


        public string CancelReception(long id,int idroom)
        {
              LogoutRoomViewModel logoutRoomViewModel = new LogoutRoomViewModel
                {
                    IdReception = id,
                    IdRoom = idroom,
                    OutOrCancel="2"
                    

                };

                Status_RoomService sr = new Status_RoomService(_db);
                var cStIDdetials = sr.changeStatusForLogout(logoutRoomViewModel);

            if (cStIDdetials >0)
            {
                return "succeful";
            }
            else
            {
                return "fail";
            }

        }
        }
}
