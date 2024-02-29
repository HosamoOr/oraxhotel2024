using DataModels;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class GroupService
    {
        private readonly HotelAlkheerDB _db;

        public GroupService(HotelAlkheerDB context)
        {
            _db = context;
        }
        //DataTable  GroupServicesViewModel
        public async Task<List<GroupServicesViewModel>> ListDT(JqueryDatatableParam param)
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;


            var Parts = _db.GroupServicesTables.
                

               OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
                                  Select(xx=>new GroupServicesViewModel
                                  { 
                                      Id=xx.Id,
                                      Name=xx.Name,
                                      NameEn=xx.NameEn,
                                      IdSub=xx.IdSub,
                                      countBrchService=xx.Fkgroupservicesproductservices.Count()
                                  
                                  }).

              ToList();



            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = Parts.Where(x => x.Name.ToLower().Contains(searchText.ToLower())

                                              ).ToList();
            }

            return Parts;



        }


        public async Task<List<GroupServicesViewModel>> ListAll()
        {

           
            var Parts = _db.GroupServicesTables.


               OrderByDescending(x => x.Id).
                                
                                  Select(xx => new GroupServicesViewModel
                                  {
                                      Id = xx.Id,
                                      Name = xx.Name,
                                      NameEn = xx.NameEn,
                                      IdSub = xx.IdSub,
                                      countBrchService = xx.Fkgroupservicesproductservices.Count()

                                  }).

              ToList();



            return Parts;



        }


    }
}
