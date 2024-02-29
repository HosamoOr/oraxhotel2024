using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.Accounting_Layer.Bill;
using HotelSys.Help;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class AccReceptionService
    {
        int idSub = 1;

        private readonly HotelAlkheerDB _db;

        public AccReceptionService(HotelAlkheerDB context)
        {
            _db = context;
        }

      public List<Status_Current_RoomViewModel> getReceByIdAccount(int idAccount)
        {
            

            var mo = _db.GetNowReceptionByIdAcc(idSub, idAccount).
               

                 Select(xx => new Status_Current_RoomViewModel
                 {
                     Id = xx.id,
                     Status = xx.status,
                     
                     EndDate = xx.end_date,
                     StartDate = xx.start_date,
                     IdReception = xx.IdReception,
                     IdRoom = xx.id_room,
                     
                     nameCuOrCo = xx.nameAccount,
                     RoomModel = new RoomViewModel
                     {
                         IdR = xx.id_room,
                         NameR = xx.name_room,

                        
                         Roomstabletyperoomstable = new TypeRoomsTable
                         {
                             NameT = xx.type_room,
                             Color = xx.color


                         }
                     }


                 }).ToList();
            return mo;
        }
     
        }
}
