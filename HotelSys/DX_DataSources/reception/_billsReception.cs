using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSys
{
    [DisplayName("_billsReceptionDataSet")]
    public class _billsReceptionDataSet
    {
        protected HotelAlkheerDB db { get; set; }
        public _billsReceptionDataSet(HotelAlkheerDB dbContext,long id)
        {
            this.db = dbContext;
           Items = InitializeList(id);
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

      

        public showInvRecepitonVM Items { get; set; }

        public showInvRecepitonVM InitializeList(long id)
        {
            BillReceprionService brs = new BillReceprionService(db);
            var model = brs.GetShowBillReception(id);

            return model;


        }

        //public List<_ReportBalanceAccountViewModel> GetData(int noOfItems)
        //{
        //    List<_ReportBalanceAccountViewModel> revertList = new List<_ReportBalanceAccountViewModel>(Items);
        //    revertList.Reverse();
        //    return revertList.Take(noOfItems).ToList();
        //}
    }
    
}
