using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSys
{
    [DisplayName("AccountBalanceDataSet")]
    public class AccountBalanceDataSet
    {
        protected HotelAlkheerDB db { get; set; }
        public AccountBalanceDataSet(HotelAlkheerDB dbContext,int id , DateTime start,DateTime end,bool isAll,int typeRPT)
        {
            this.db = dbContext;
           Items = InitializeList(id, start, end, isAll, typeRPT);
        }

        //public AccountBalanceDataSet()
        //{
           

          

        //    Items = InitializeList(DateTime.Now,DateTime.Now);
        //}

        // private readonly HotelAlkheerDB HotelDb2DBHotelDb2DB;

        //public AccountBalanceDataSet(HotelAlkheerDB context, DateTime start, DateTime end)
        //{
        //    _db = context;
        //    Items = InitializeList(start, end);
        //}

        //public AccountBalanceDataSet(int noOfItems)
        //{

        //    Items = InitializeList().GetRange(0, noOfItems);
        //}

        public AccountBalanceDataSet(int noOfItems, DateTime start, DateTime end)
        {

           // Items = InitializeList( start,  end).GetRange(0, noOfItems);
        }

        public AccountBalanceDataSet(DateTime FromDate, DateTime ToDate)
        {

            //Items = InitializeList(FromDate, ToDate);
                //.GetRange(0, 1);
        }

        public List<_ReportBalanceAccountViewModel> Items { get; set; }

        public List<_ReportBalanceAccountViewModel> InitializeList(int id, DateTime start, DateTime end,bool isAll,int typeRPT=1)
        {
            List<_ReportBalanceAccountViewModel> model = new List<_ReportBalanceAccountViewModel>();

            if(isAll==true)
            {
                model = (from t1 in db.GetBalanceAccount(id, 1)


                         select new _ReportBalanceAccountViewModel
                         {
                             id = t1.id,
                             billOrBandDocumnet = t1.billOrBandDocumnet,
                             dateDocumnet = t1.dateDocumnet,
                             FromPrice = t1.FromPrice,
                             ToPrice = t1.ToPrice,
                             debt_or_Credit = t1.debt_or_Credit,
                             IdDocument = t1.IdDocument,
                             id_account = t1.id_account,
                             NameAccount = t1.NameAccount,
                             id_currancy = t1.id_currancy,
                             typeDocumnet = t1.typeDocumnet,
                             Balance = t1.FromPrice - t1.ToPrice,
                             Note = t1.Note,

                             



                         }).OrderByDescending(x => x.id).

                  // DistinctBy(p => p.Id).
                  ToList()
                  ;

                

            }
            else
            {
                model = (from t1 in db.GetBalanceAccountBetweenDate(id, 1, start, end)


                         select new _ReportBalanceAccountViewModel
                         {
                             id = t1.id,
                             billOrBandDocumnet = t1.billOrBandDocumnet,
                             dateDocumnet = t1.dateDocumnet,
                             FromPrice = t1.FromPrice,
                             ToPrice = t1.ToPrice,
                             debt_or_Credit = t1.debt_or_Credit,
                             IdDocument = t1.IdDocument,
                             id_account = t1.id_account,
                             NameAccount = t1.NameAccount,
                             id_currancy = t1.id_currancy,
                             typeDocumnet = t1.typeDocumnet,
                             Balance = t1.FromPrice - t1.ToPrice,
                             Note = t1.Note,



                         }).OrderByDescending(x => x.id).

                 // DistinctBy(p => p.Id).
                 ToList();

                
            }



            double sumBalan = 0;

           OrgServiceEF hs = new OrgServiceEF();

            var hotelModel = hs.getDataShortOrg();




            for (int i = 0; i < model.Count(); i++)
            {
                double sub = Convert.ToDouble(model[i].ToPrice + model[i].FromPrice);

                sumBalan = sumBalan + sub;

                model[i].Balance = sumBalan;

                model[i].hotel = hotelModel;
                if (isAll == true)
                {

                    model[i].interval = " منذ بدء التعامل";
                }
                else
                {
                    model[i].interval = " من تاريخ" + start.Date.ToString("d/M/yyyy") + " الى " + end.Date.ToString("d/M/yyyy");
                }


                if (typeRPT == 1)
                {

                    model[i].title = "كشف حساب";

                    System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

                   


                }
                else
                {
                    model[i].title = " نقدية الصندوق";
                }

            }

            if (model.Count == 0)
            {
                var acc= db.AccountTables.Where(x=>x.Id==id).FirstOrDefault();

                model.Add(
                    new _ReportBalanceAccountViewModel
                    {
                        hotel = hotelModel,
                        NameAccount= acc.Name,
                        Balance=0,

                        title = typeRPT == 1 ? "كشف حساب" : " نقدية الصندوق",



                        interval = isAll == true ? "منذ بدء التعامل" : " من تاريخ " + start.Date.ToString("d/M/yyyy")  + " الى " + end.Date.ToString("d/M/yyyy")
                    });

            }

            return model;

        }

        public List<_ReportBalanceAccountViewModel> GetData(int noOfItems)
        {
            List<_ReportBalanceAccountViewModel> revertList = new List<_ReportBalanceAccountViewModel>(Items);
            revertList.Reverse();
            return revertList.Take(noOfItems).ToList();
        }
    }
    
}
