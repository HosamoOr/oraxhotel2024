using DataModels;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class CityService
    {
        private readonly HotelAlkheerDB _db;

        public CityService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public List<CityViewModel> getList()
        {
            var list = _db.CityTables.Select(x=>new CityViewModel
            {
                Name = x.Name,  
                Id = x.Id,

            } ).ToList();
            return list;

        }



        public List<CityViewModel> getBycountryId(int idcountry)
        {
            var list = _db.CityTables.
                Where(x => x.IdCountry == idcountry).

                Select(x => new CityViewModel
                {
                    Name = x.Name,
                    Id = x.Id,


                }).ToList();
            return list;

        }



        public async Task<DTCityViewModel> DTbyIDcntry( paramModel request, int idcountry)
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

            int countRo = 0;

            var model = new List<CityViewModel>();

            if (string.IsNullOrEmpty(searchText) && idcountry==-1)
            {
                model =  _db.CityTables.
                
                Select(x => new CityViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    NameCountry=x.Citytablecountrytable.Name,
                    countArea=x.Areatablecitytables.Count(),
                    


                })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.CityTables.Count();
            }
                
else if(string.IsNullOrEmpty(searchText) && idcountry != -1)
            {

                model =  _db.CityTables.
                   
                 Where(x => x.IdCountry == idcountry).
              Select(x => new CityViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                  NameCountry = x.Citytablecountrytable.Name,
                  countArea = x.Areatablecitytables.Count(),



              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.CityTables.
                    
                 Where(x => x.IdCountry == idcountry).ToList().Count();

            }
            else if ( !string.IsNullOrEmpty(searchText) && idcountry == -1)
            {

                model = _db.CityTables. Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).
              Select(x => new CityViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                  NameCountry = x.Citytablecountrytable.Name,
                  countArea = x.Areatablecitytables.Count(),



              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.CityTables.Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).ToList().Count();

            }

            else if (!string.IsNullOrEmpty(searchText) && idcountry != -1)
            {

                model = _db.CityTables.
                 Where(x => x.IdCountry == idcountry && x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).
              Select(x => new CityViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                  NameCountry = x.Citytablecountrytable.Name,
                  countArea = x.Areatablecitytables.Count(),



              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.CityTables.
                 Where(x => x.IdCountry == idcountry && x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).ToList(). Count();

            }



            DTCityViewModel m = new DTCityViewModel
            {
                list = model,
                countRow = countRo

            };

            return m;
        }




    }
}
