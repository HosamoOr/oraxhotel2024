using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace HotelSys.BusnessLayer
{
    public class _AccountService
    {
        private readonly HotelAlkheerDB _db;

        public _AccountService(HotelAlkheerDB context)
        {
            _db = context;
        }
        public List<AccountTable> getAccByGroup(int idGroup)
        {
            var m = _db.AccountTables.Where(x => x.IdGroup == idGroup).ToList();
            return m;

        }

        //اعطاء الحسابات العامه مثل العملاء والشركات الخاصة بالمستخدم
        public List<AccountTable> getAccPublic()
        {
            var m = _db.AccountTables.Where(x => x.IsPrivate == false).ToList();
            return m;

        }

        public List<AccountTable> getAccAll()
        {
            var m = _db.AccountTables.ToList();
            return m;

        }


        public async Task<int> CreateAsync(_AccountViewModel model)
        {
            _TreeAccountService trs = new _TreeAccountService(_db);
            var modelTree = new TreeAccountViewModel
            {
                Name = model.Name,
                Id = model.Id.ToString(),
                IdGroup = model.IdGroup.ToString(),
                Code = model.Code,


            };

           var idInsert = await trs.AddOrEditAccTreeAsync(modelTree);


            //var m = _db.AccountTables.Where(x => x.IdGroup == model.IdGroup).ToArray();

            //int max = 1;
            //if (m.Length>0)

            // max = m[m.Length - 1].Id + 1;

            //string id = model.IdGroup +""+ max;
            //int IDD = int.Parse(id);

            //AccountTable at = new AccountTable
            //{
            //    //Id = IDD,
            //    Name = model.Name,
            //    IdGroup = model.IdGroup,
            //    Status = Status_Account.active,
            //    Createat = DateTime.Now,
            //    IsPrivate = false,
            //    Code= IDD

            //};
            //var st =  _db.InsertWithInt32Identity(at);
            
            return idInsert;

        }
        public async Task<int> delete(int  idd)
        {

            var m = _db.AccountTables.Where(x => x.Id == idd).FirstOrDefault();

           int st= _db.Delete(m);

            return st;

        }
        public async Task<int> Edit(_AccountViewModel model)
        {
            int st = 0;
            var m = _db.AccountTables.Find(model.Id);
            if(m!=null)
            {
                m.Name = model.Name;

                m.IdGroup = model.IdGroup;

                m.Status = model.Status;
                m.Createat = DateTime.Now;
                 
                 st =await _db.UpdateAsync(m);

            }


            return st;

        }

        public List<_AccountViewModel> List()
        {
            List<_AccountViewModel> li = _db.AccountTables.
               Select(ss => new _AccountViewModel
               {
                   Id = ss.Id,
                   Name=ss.Name,
                   Createat=ss.Createat,
                   IdGroup=ss.IdGroup,
                   Status=ss.Status,
                   IsPrivate=ss.IsPrivate,
                   IdGroupNavigation=new GroupAccountViewModel
                   {
                       Name=ss.Fkgroupaccountaccount.Name
                   }
                   
                  

               }).

               ToList();
            return li;
        }

        public async Task<ResultAccount> ListDT(JqueryDatatableParam param)
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;

            int totalRecords = 0;


            var Parts = new List<_AccountViewModel>();


            Parts = _db.AccountTables.
                 OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).

               Select(ss => new _AccountViewModel
               {
                   Id = ss.Id,
                   Name = ss.Name,
                   Createat = ss.Createat,
                   IdGroup = ss.IdGroup,
                   Status = ss.Status,
                   IsPrivate = ss.IsPrivate,
                   IdGroupNavigation = new GroupAccountViewModel
                   {
                       Name = ss.Fkgroupaccountaccount.Name
                   }

               }).

               ToList();






            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = _db.AccountTables

               .Where(x => x.Name.Contains(searchText) || x.Id.ToString().Contains(searchText)
               || x.Fkgroupaccountaccount.Name.ToString().Contains(searchText) ).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
                 Select(ss => new _AccountViewModel
                 {
                     Id = ss.Id,
                     Name = ss.Name,
                     Createat = ss.Createat,
                     IdGroup = ss.IdGroup,
                     Status = ss.Status,
                     IsPrivate = ss.IsPrivate,
                     IdGroupNavigation = new GroupAccountViewModel
                     {
                         Name = ss.Fkgroupaccountaccount.Name
                     }

                 }).
              ToList();


                totalRecords = _db.AccountTables

               .Where(x => x.Name.Contains(searchText) || x.Id.ToString().Contains(searchText)
               || x.Fkgroupaccountaccount.Name.ToString().Contains(searchText))

                .Count();

            }


            else
            {
                totalRecords = _db.AccountTables.Count();
            }



            ResultAccount model = new ResultAccount
            {
                list = Parts,
                countRow = totalRecords
            };

            return model;



        }





    }
}
