using DataModels;
using HotelSys.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSys
{
    [DisplayName("mymodel")]
    public class mymodel
    {
        protected HotelAlkheerDB db { get; set; }
        public mymodel(HotelAlkheerDB dbContext)
        {
            this.db = dbContext;
            Items = InitializeList(DateTime.Now, DateTime.Now);
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

        public mymodel(int noOfItems, DateTime start, DateTime end)
        {

            Items = InitializeList( start,  end).GetRange(0, noOfItems);
        }
        public List<CustRPTViewModel> Items { get; set; }

        public List<CustRPTViewModel> InitializeList(DateTime start, DateTime end)
        {
          

            var model =  db.RecetionTables.
              Where(x => x.IsChechin == true).
              Where(t => t.CheckinDate.Value.Year >= start.Year &&
               t.CheckinDate.Value.Month >= start.Month &&
                t.CheckinDate.Value.Day >= start.Day
              ).
              Where(t => t.CheckinDate.Value.Year <= end.Year &&
               t.CheckinDate.Value.Month <= end.Month &&
                t.CheckinDate.Value.Day <= end.Day
              ).

            // OrderBy(t => t.CheckinDate).


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
                 LocWork = x.Recetiontablemycustomer.Mycustomerscustomertable.LocWork ,
                 TypeWork=  x.Recetiontablemycustomer.Mycustomerscustomertable.TypeWork,
                 PhoneWork = x.Recetiontablemycustomer.Mycustomerscustomertable.PhoneWork,
                 Createat = x.Recetiontablemycustomer.Mycustomerscustomertable.Createat,
                 EndDate = x.Recetiontablemycustomer.Mycustomerscustomertable.EndDate,
                 NumProof = x.Recetiontablemycustomer.Mycustomerscustomertable.NumProof,
                 TypeProof = x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "1" ? "الهوية" : x.Recetiontablemycustomer.Mycustomerscustomertable.TypeProof == "2" ? "جواز سفر " : "عام",

                 PublicNote = x.Recetiontablemycustomer.Mycustomerscustomertable.PublicNote,
                 PrivateNote = x.Recetiontablemycustomer.PrivateNote


             },


             }).OrderByDescending(x => x.IdReception)
                  
             //.DistinctBy(p => p.Id)

             .ToList();


            return model;



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
