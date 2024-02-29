using DataModels;
using HotelSys.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace HotelSys.BusnessLayer
{
    public class CountryService
    {
        private readonly HotelAlkheerDB _db;

        public CountryService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public List<CountyViewModel> getList()
        {
            var list = _db.CountryTables.Select(x=>new CountyViewModel
            {
                Name = x.Name,  
                Id = x.Id,

            } ).ToList();
            return list;

        }
    }
}
