using DataModels;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using HotelSys.ViewModel.RPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSys.DataSources
{
    
        [DisplayName("clientsBalanceDS")]
        public class clientsBalanceDS
        {
            protected HotelAlkheerDB db { get; set; }
            public clientsBalanceDS(HotelAlkheerDB dbContext)
            {
                this.db = dbContext;
                Items = InitializeList();
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

            public clientsBalanceDS(int noOfItems)
            {

            Items = InitializeList();
                //GetRange(0, noOfItems);
            }
            public List<ItemClientsBalance> Items { get; set; }

            public List<ItemClientsBalance> InitializeList()
            {


            List<ItemClientsBalance> listBa = (from t1 in db.GetClientsBalance(1)


                                              select new ItemClientsBalance
                                              {
                                                  FromPrice = t1.FromPrice,
                                                  idCustomer = t1.idCustomer,
                                                  id_account = t1.id_account,
                                                  messag = t1.messag,
                                                  name = t1.name,
                                                  ToPrice = t1.ToPrice,


                                              }).ToList();

            // DistinctBy(p => p.Id).

            OrgServiceEF hs = new OrgServiceEF();

            var hotelModel = hs.getDataShortOrg();


            for (int i = 0; i < listBa.Count(); i++)
                {

                listBa[i].def = i + 1;
                listBa[i].hotel = hotelModel;

                }

            if (listBa.Count == 0)
            {
                listBa.Add(
                    new ItemClientsBalance
                    {
                        hotel = hotelModel,

                    });

            }



            return listBa;



            }

        public List<ItemClientsBalance> GetData(int noOfItems)
            {
            List<ItemClientsBalance> revertList = new List<ItemClientsBalance>(Items);

                revertList.Reverse();
                var li= revertList.Take(noOfItems).ToList();

           
            return li;

            }



        public ClientsBalanceViewModel listPara(int idSub, paramModel request)
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


            List<ItemClientsBalance> listBa = (from t1 in db.GetClientsBalance(1)


                                               select new ItemClientsBalance
                                               {
                                                   FromPrice = t1.FromPrice,
                                                   idCustomer = t1.idCustomer,
                                                   id_account = t1.id_account,
                                                   messag = t1.messag,
                                                   name = t1.name,
                                                   ToPrice = t1.ToPrice,
                                                   balance=Convert.ToDouble( t1.ToPrice - t1.FromPrice)


                                               }).
                                               //.OrderByDescending(x => x.IdReception).
                    ToList();

            

            




            if (!string.IsNullOrEmpty(searchText))
            {
                listBa = listBa.Where(x => x.name.ToLower().Contains(searchText.ToLower())

                  || (x.id_account != 0 && x.id_account.ToString().Contains(searchText.ToLower()))
                   || (x.idCustomer != 0 && x.idCustomer.ToString().Contains(searchText.ToLower()))

                                               || (x.FromPrice != 0 && x.FromPrice.ToString().Contains(searchText.ToLower()))

                                                 || (x.ToPrice != 0 && x.ToPrice.ToString().Contains(searchText.ToLower()))

                                              ).ToList();

            }

            

            var takSkip =
                listBa.
                Skip(offset).
                   Take(limit).ToList();

            for (int i = 0; i < takSkip.Count(); i++)
            {

                takSkip[i].def = i + 1;
            }

            int coutRow = listBa.Count();

            ClientsBalanceViewModel model = new ClientsBalanceViewModel
            {
                items = takSkip,
                count = coutRow
            };


            return model;
        }




    }

}
