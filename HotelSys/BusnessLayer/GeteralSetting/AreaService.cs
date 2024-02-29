using DataModels;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class AreaService
    {
        private readonly HotelAlkheerDB _db;

        public AreaService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public List<AreaViewModel> getByCityId(int idCity)
        {
            var list = _db.AreaTables.
                Where(x=>x.IdCity == idCity).
                
                Select(x=>new AreaViewModel
                {
                Name = x.Name,  
                Id = x.Id,
                

            } ).ToList();
            return list;

        }


        public async Task<DTAreaViewModel> DTbyIDcntry_City(paramModel request,int idcity)
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

            var model = new List<AreaViewModel>();

            if (string.IsNullOrEmpty(searchText) && idcity == -1 )
            {
                model = _db.AreaTables.

                Select(x => new AreaViewModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    NameCountry = x.Areatablecitytable.Citytablecountrytable.Name,
                    IdCity=x.Areatablecitytable.Id,
                    NameCity=x.Areatablecitytable.Name,
                    countCustomer=x.Customertableareatables.Count(),

                    IdCountry=Convert.ToInt32( x.Areatablecitytable.IdCountry)
                    
                })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.AreaTables.Count();
            }

            else if (string.IsNullOrEmpty(searchText) && idcity != -1)
            {

                model = _db.AreaTables.

                 Where(x => x.IdCity == idcity).
              Select(x => new AreaViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                  NameCountry = x.Areatablecitytable.Citytablecountrytable.Name,
                  IdCity = x.Areatablecitytable.Id,
                  NameCity = x.Areatablecitytable.Name,
                  countCustomer = x.Customertableareatables.Count(),

                  IdCountry = Convert.ToInt32(x.Areatablecitytable.IdCountry)


              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.AreaTables.

                 Where(x => x.IdCity == idcity).ToList().Count();

            }
            else if (!string.IsNullOrEmpty(searchText) && idcity == -1)
            {

                model = _db.AreaTables.Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                                             || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                             || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).
              Select(x => new AreaViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                 
                  NameCountry = x.Areatablecitytable.Citytablecountrytable.Name,
                  IdCity = x.Areatablecitytable.Id,
                  NameCity = x.Areatablecitytable.Name,
                  countCustomer = x.Customertableareatables.Count(),

                  IdCountry = Convert.ToInt32(x.Areatablecitytable.IdCountry)


              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.AreaTables.Where(x => x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).ToList().Count();

            }

            else if (!string.IsNullOrEmpty(searchText) && idcity != -1)
            {

                model = _db.AreaTables.
                 Where(x => x.IdCity == idcity && x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).
              Select(x => new AreaViewModel
              {
                  Name = x.Name,
                  Id = x.Id,
                  NameCountry = x.Areatablecitytable.Citytablecountrytable.Name,
                  IdCity = x.Areatablecitytable.Id,
                  NameCity = x.Areatablecitytable.Name,
                  countCustomer = x.Customertableareatables.Count(),

                  IdCountry = Convert.ToInt32(x.Areatablecitytable.IdCountry)


              })
              .OrderByDescending(x => x.Id).
                    Skip(offset).
                   Take(limit)
              //.DistinctBy(p => p.Id)

              .ToList();

                countRo = _db.AreaTables.
                 Where(x => x.IdCity == idcity && x.Name.ToLower().Contains(searchText.ToLower())
                                              || (x.NameEn != null && x.NameEn.ToLower().Contains(searchText.ToLower()))
                                              || (x.Id.ToString() != null && x.Id.ToString().Contains(searchText.ToLower()))
                                             ).ToList().Count();

            }



            DTAreaViewModel m = new DTAreaViewModel
            {
                list = model,
                countRow = countRo

            };

            return m;
        }




    }
}
