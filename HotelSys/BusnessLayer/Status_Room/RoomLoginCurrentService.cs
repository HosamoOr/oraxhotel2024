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
    public class RoomLoginCurrentService
    {
        int idSub = 1;

        private readonly HotelAlkheerDB _db;

        public RoomLoginCurrentService(HotelAlkheerDB context)
        {
            _db = context;
        }

      
        }
}
