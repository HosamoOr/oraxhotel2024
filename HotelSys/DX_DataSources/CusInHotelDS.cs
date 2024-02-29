using DataModels;
using HotelSys;
using HotelSys.BusnessLayer;
using HotelSys.Models;
using HotelSys.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSys
{
    [DisplayName("CusInHotelDS")]
    public class CusInHotelDS
    {
        //protected HotelAlkheerDB db { get; set; }
        public CusInHotelDS(HotelAlkheerDB dbContext, int noOfItems)
        {
            // this.db = dbContext;
            // Items = InitializeList(DateTime.Now, DateTime.Now);
        }

        public CusInHotelDS(DateTime FromDate, DateTime ToDate)
        {
            // this.db = dbContext;
            Items = InitializeList(FromDate, ToDate);
        }

        public CusInHotelDS(int noOfItems)
        {

            //Items = InitializeList(DateTime.Now, DateTime.Now);
        }


        //public mymodel()
        //{




        //    Items = InitializeList(DateTime.Now,DateTime.Now);
        //}

        // private readonly HotelAlkheerDB HotelDb2DBHotelDb2DB;

        //public mymodel(HotelAlkheerDB context, DateTime start, DateTime end)
        //{
        //    _db = context;
        //    Items = InitializeList(start, end);
        //}

        //public mymodel(int noOfItems)
        //{

        //    Items = InitializeList().GetRange(0, noOfItems);
        //}


        public List<CustRPTViewModel> Items { get; set; }

        public List<CustRPTViewModel> InitializeList(DateTime start, DateTime end)
        {

            List<CustRPTViewModel> model = new List<CustRPTViewModel>();

            List<CustRPTViewModel> model2 = new List<CustRPTViewModel>();

            var db = new Hotel_alkheerContext();

            if(end.Date >=DateTime.Now.Date)
            {
                model = db.RecetionTables.
             Where(x => x.IsChechin == true).
             Where(
              x =>
              x.IsChechout == false || x.ChechoutDate == null).



         //  Where(t => t.CheckinDate.Value.Year >= start.Year &&
         //   t.CheckinDate.Value.Month >=  start.Month &&
         //t.CheckinDate.Value.Day  >=  start.Day

         //  ).


          //Where(t => t.CheckinDate.Value.Year >= start.Year &&
          // t.CheckinDate.Value.Month >= start.Month &&
          //  t.CheckinDate.Value.Day >= start.Day
          //).
          //Where(t => t.CheckinDate.Value.Year <= end.Year &&
          // t.CheckinDate.Value.Month <= end.Month &&
          //  t.CheckinDate.Value.Day <= end.Day
          //).

          // OrderBy(t => t.CheckinDate).


          Select(x => new CustRPTViewModel
          {
              IdReception = x.Id,

              AreaFrom = x.AreaFrom,
              WhyVisit=x.WhyVisit,
              


               //room = new PriceRoomsViewModel
               //{
               //    Id = x.Recetiontableroomstable.Id,
               //    NameRoom = x.Recetiontableroomstable.NameR,
               //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

               //},
               // company = new _CompanyViewModel
               //{
               //    IdCo = x.Fkcompanyrecetion.Id,
               //    NameCo = x.Fkcompanyrecetion.Name,
               //    IdAccountCo = x.Fkcompanyrecetion.IdAccount
               //},

               followers = x.FollowerReceptionTables

                  .Select(

                      zz => new FollowerViewModel
                      {
                          follwerCusomer = new CustomerViewModel
                          {

                              IdmyCu = 0,
                              IdcumtomerAll = zz.IdCustomer,

                              is_my = false,
                              Name = zz.IdCustomerNavigation.Name,
                              Sex = zz.IdCustomerNavigation.Sex,
                              Email = zz.IdCustomerNavigation.Email,
                          },
                          Relation = zz.Relation,
                           //Duration = zz.Duration,
                           DurationFrom = zz.DurationFrom,
                          DurationTo = zz.DurationTo


                      }
                      ).ToList(),
               //.ToList(),



               customer =
           new CustomerViewModel
           {
               IdmyCu = x.IdMyCustomerNavigation.IdCustomer,
                // IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                IdcumtomerAll = x.IdMyCustomerNavigation.IdCustomer,
                // IdAccount=yy.
                is_my = true,
               Name = x.IdMyCustomerNavigation.IdCustomerNavigation.Name,

               Sex = x.IdMyCustomerNavigation.IdCustomerNavigation.Sex,
               Email = x.IdMyCustomerNavigation.IdCustomerNavigation.Email,
               Nationality = x.IdMyCustomerNavigation.IdCustomerNavigation.Nationality,
               LocWork = x.IdMyCustomerNavigation.IdCustomerNavigation.LocWork,
               TypeWork = x.IdMyCustomerNavigation.IdCustomerNavigation.TypeWork,
               PhoneWork = x.IdMyCustomerNavigation.IdCustomerNavigation.PhoneWork,
               Createat = x.IdMyCustomerNavigation.IdCustomerNavigation.Createat,
               EndDate = x.IdMyCustomerNavigation.IdCustomerNavigation.EndDate,
               NumProof = x.IdMyCustomerNavigation.IdCustomerNavigation.NumProof,
               TypeProof = x.IdMyCustomerNavigation.IdCustomerNavigation.TypeProof == "1" ? " الهوية" : x.IdMyCustomerNavigation.IdCustomerNavigation.TypeProof == "2" ? "جواز سفر ":"عام",
               PublicNote = x.IdMyCustomerNavigation.IdCustomerNavigation.PublicNote,
               PrivateNote = x.IdMyCustomerNavigation.PrivateNote,

               Id_Area= x.IdMyCustomerNavigation.IdCustomerNavigation.IdArea,
               area= new AreaViewModel
               {
                   Name = x.IdMyCustomerNavigation.IdCustomerNavigation.IdAreaNavigation.Name,
                   NameCity = x.IdMyCustomerNavigation.IdCustomerNavigation.IdAreaNavigation.IdCityNavigation.Name
               }



           },


          }).OrderByDescending(x => x.IdReception)

          //.DistinctBy(p => p.Id)

          .ToList();

            }


            var reSettign = db.SettingReceptionTables.FirstOrDefault();

            if (reSettign != null)
            {
               // reSettign.

            }
          

            DateTime ChicOutCheck = new DateTime(end.Year, end.Month, end.Day, 11, 59, 59, 000);

            DateTime chinInCheck = new DateTime(start.Year, start.Month, start.Day, 11, 59, 59, 000);

           

           model2 = db.RecetionTables.
              Where(x => x.IsChechin == true).
              Where(x => x.IsChechout == false || x.ChechoutDate == null || x.ChechoutDate >= ChicOutCheck).
              Where(x => x.CheckinDate <= chinInCheck).




            Select(x => new CustRPTViewModel
            {
                IdReception = x.Id,
               

                //room = new PriceRoomsViewModel
                //{
                //    Id = x.Recetiontableroomstable.Id,
                //    NameRoom = x.Recetiontableroomstable.NameR,
                //    NameType = x.Recetiontableroomstable.Fkroomstyperoom.NameT

                //},
                // company = new _CompanyViewModel
                //{
                //    IdCo = x.Fkcompanyrecetion.Id,
                //    NameCo = x.Fkcompanyrecetion.Name,
                //    IdAccountCo = x.Fkcompanyrecetion.IdAccount
                //},

                followers = x.FollowerReceptionTables

                    .Select(
                        
                        zz => new FollowerViewModel
                        {
                            follwerCusomer = new CustomerViewModel
                            {

                                IdmyCu = 0,
                                IdcumtomerAll = zz.IdCustomer,

                                is_my = false,
                                Name = zz.IdCustomerNavigation.Name,
                                Sex = zz.IdCustomerNavigation.Sex,
                                Email = zz.IdCustomerNavigation.Email,
                            },
                            Relation = zz.Relation,
                            //Duration = zz.Duration,
                            DurationFrom = zz.DurationFrom,
                            DurationTo = zz.DurationTo


                        }
                        ).ToList(),
                    //.ToList(),



                customer =
             new CustomerViewModel
             {
                 IdmyCu = x.IdMyCustomerNavigation.IdCustomer,
                 // IdmyCu = x.Recetiontablemycustomer.IdCustomer,
                 IdcumtomerAll = x.IdMyCustomerNavigation.IdCustomer,
                 // IdAccount=yy.
                 is_my = true,
                 Name = x.IdMyCustomerNavigation.IdCustomerNavigation.Name,

                 Sex = x.IdMyCustomerNavigation.IdCustomerNavigation.Sex,
                 Email = x.IdMyCustomerNavigation.IdCustomerNavigation.Email,
                 Nationality = x.IdMyCustomerNavigation.IdCustomerNavigation.Nationality,
                 LocWork = x.IdMyCustomerNavigation.IdCustomerNavigation.LocWork,
                 TypeWork = x.IdMyCustomerNavigation.IdCustomerNavigation.TypeWork,
                 PhoneWork = x.IdMyCustomerNavigation.IdCustomerNavigation.PhoneWork,
                 Createat = x.IdMyCustomerNavigation.IdCustomerNavigation.Createat,
                 EndDate = x.IdMyCustomerNavigation.IdCustomerNavigation.EndDate,
                 NumProof = x.IdMyCustomerNavigation.IdCustomerNavigation.NumProof,
                 TypeProof = x.IdMyCustomerNavigation.IdCustomerNavigation.TypeProof == "1" ? "الهوية" : x.IdMyCustomerNavigation.IdCustomerNavigation.TypeProof == "2" ? "جواز سفر ":"عام",
                 PublicNote = x.IdMyCustomerNavigation.IdCustomerNavigation.PublicNote,
                 PrivateNote = x.IdMyCustomerNavigation.PrivateNote


             },


            }).OrderByDescending(x => x.IdReception)

            //.DistinctBy(p => p.Id)

            .ToList();



            OrgServiceEF hs = new OrgServiceEF();

            var hotelModel = hs.getDataShortOrg();

            if(model2.Count()>0)
            {
                model.AddRange(model2);
            }


            var testListNoDups = model.GroupBy(x => new { x.IdReception })
                                              .Select(x => x.First())
                                              .ToList();


            for (int i = 0; i < testListNoDups.Count(); i++)
            {

                testListNoDups[i].def = i + 1;
               
                string folowerStr = "";

                for(int j=0;j < testListNoDups[i].followers.Count();j++)
                {
                    int n = j + 1;
                    folowerStr += n.ToString()+"- "+ testListNoDups[i].followers[j].follwerCusomer.Name + " (" + testListNoDups[i].followers[j].Relation + ") ";

                }

                testListNoDups[i].followersStr = folowerStr;
                testListNoDups[i].hotel = hotelModel;
                testListNoDups[i].interval= " من تاريخ " +start.Date.ToString("d/M/yyyy") +" الى " +end.Date.ToString("d/M/yyyy");


            }
            if(testListNoDups.Count == 0)
            {
                testListNoDups.Add(
                    new CustRPTViewModel { 
                        hotel = hotelModel,

                       interval = " من تاريخ" + start.Date.ToShortDateString() + " الى " + end.Date.ToShortDateString()
                    });

            }





            return testListNoDups;



        }

        public List<CustRPTViewModel> GetData(int noOfItems)
        {
            List<CustRPTViewModel> revertList = new List<CustRPTViewModel>(Items);
            revertList.Reverse();
            return revertList.Take(noOfItems).ToList();
        }
    }
    //public class DataItem1
    //{
    //    public DataItem1(int floor, int office, string personName, string titleOfCourtesy, string title)
    //    {
    //        Floor = floor;
    //        Office = office;
    //        PersonName = personName;
    //        TitleOfCourtesy = titleOfCourtesy;
    //        Title = title;
    //    }
    //    public int Floor { get; set; }
    //    public int Office { get; set; }
    //    public string PersonName { get; set; }
    //    public string TitleOfCourtesy { get; set; }
    //    public string Title { get; set; }
    //}
}



class Comparer : IEqualityComparer<CustRPTViewModel>
{
    public bool Equals(CustRPTViewModel x, CustRPTViewModel y)
    {
        return x.IdReception == y.IdReception ;
    }

    public int GetHashCode(CustRPTViewModel obj)
    {
        return (obj.IdReception).GetHashCode();
    }
}