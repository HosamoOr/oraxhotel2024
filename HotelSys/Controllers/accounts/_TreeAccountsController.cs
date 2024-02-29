using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.BusnessLayer;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text.Json;
using HotelSys.Accounting_Layer.Bill;
using DevExtreme.AspNet.Mvc;

namespace HotelSys.Controllers.accounts
{
    [Authorize]
    public class _TreeAccountsController : Controller
    {

        private readonly HotelAlkheerDB _db;

        public _TreeAccountsController(HotelAlkheerDB context)
        {
            _db = context;


        }

        
        public IActionResult TreeView()
        {
            _AccountService as_s = new _AccountService(_db);
            List<_AccountViewModel> li = as_s.List();
            ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name");
            return View(li);
        }

        public IActionResult TreeView1()
        {
            //_AccountService as_s = new _AccountService(_db);
            //List<_AccountViewModel> li = as_s.List();
            //ViewData["IdGroup"] = new SelectList(_db.GroupAccountTables, "Id", "Name");
            return View();
        }

        public IActionResult TreeView2()
        {
            var gorupAll =new List<GroupAccountTable>();

            var dofulatgroup = new GroupAccountTable
            {
                Id = -1,
                Name = "اختر الحساب الاب"
            };


            gorupAll.Add(dofulatgroup) ;

            gorupAll.AddRange(_db.GroupAccountTables);

            ViewData["IdGroup"] = new SelectList(gorupAll, "Id", "Name", dofulatgroup.Id);

            return View();
        }
        public IActionResult TreeView3()
        {
            return View();
        }
        public ActionResult GetPlainDataForDragAndDrop(DataSourceLoadOptions loadOptions)
        {
            List<TreeAccountViewModel> ac1 = _db.GroupAccountTables.Select(x => new TreeAccountViewModel
            {
                showName = x.Id.ToString() + "-" + x.Name,
                Name= x.Name,
                IdGroup = x.IsRoot == true ? null : x.IdMainGroup.ToString(),
                Id = x.Id.ToString(),
                Icon = "activefolder",
                IsDirectory = x.IsRoot == true ? true : false,
                IsExpanded = x.IsRoot == true ? true : false,
                mainORsub = "main",
                IsPrivate=x.IsPrivate ,
                IsRoot=x.IsRoot,
               

            }).ToList();


            List<TreeAccountViewModel> ac2 = _db.AccountTables.Select(x => new TreeAccountViewModel
            {
                Id = x.Id.ToString(),
                showName = x.Id.ToString() + "-" + x.Name,
                Name = x.Name,
                IdGroup = x.IdGroup.ToString(),
                Icon = "file",
                IsDirectory = false,
                IsExpanded = false,
                mainORsub = "sub",
                IsPrivate = x.IsPrivate,
                Status = x.Status,
                Code = x.Code,

            }).ToList();

            ac1.AddRange(ac2);



            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(DevExtreme.AspNet.Data.DataSourceLoader.Load(ac1, loadOptions)), "application/json");
        }

        //[HttpGet]
        //public ActionResult GetHierarchicalData(DataSourceLoadOptions loadOptions)
        //{
        //    List<TreeAccountViewModel> ac1 = _db.GroupAccountTables.Select(x => new TreeAccountViewModel
        //    {
        //        Name = x.Name,
        //        IdGroup = x.IsRoot == true ? null : x.IdMainGroup.ToString(),
        //        Id = x.Id.ToString(),
        //        Icon = "activefolder",
        //        IsDirectory = true,
        //        IsExpanded = true,
        //        Items = x.FkGroupAccountGroupAccountBackReferences.Select(x => new TreeAccountViewModel
        //        {
        //            Name = x.Name,
        //            IdGroup = x.IsRoot == true ? null : x.IdMainGroup.ToString(),
        //            Id = x.Id.ToString(),
        //            Icon = "activefolder",
        //            IsDirectory = true,
        //            IsExpanded = true,

        //        }).ToList()




        //    }).ToList();





        //    List<TreeAccountViewModel> ac2 = _db.AccountTables.Select(x => new TreeAccountViewModel
        //    {
        //        Id = x.Id.ToString(),
        //        Name = x.Name,
        //        IdGroup = x.IdGroup.ToString(),
        //        Icon = "file",
        //        IsDirectory = true,
        //        IsExpanded = true

        //    }).ToList();


        //    for (int i = 0; i < ac1.Count; i++)
        //    {
        //        List<TreeAccountViewModel> temp = ac2.Where(x => x.IdGroup == ac1[i].Id).ToList();

        //        if (temp.Count > 0)
        //        {

        //            if (ac1[i].Items == null)
        //            {
        //                ac1[i].Items = new List<TreeAccountViewModel>();
        //            }

        //            ac1[i].Items.AddRange(temp);

        //        }

        //        for (int j = 0; j < ac1[i].Items.Count; j++)
        //        {
        //            List<TreeAccountViewModel> temp2 = ac2.Where(x => x.IdGroup == ac1[i].Id).ToList();
        //            if (temp2.Count > 0)
        //            {
        //                if (ac1[i].Items[j].Items == null)
        //                {
        //                    ac1[i].Items[j].Items = new List<TreeAccountViewModel>();
        //                }

        //                ac1[i].Items[j].Items.AddRange(temp2);

        //            }


        //        }


        //    }






        //    IEnumerable<TreeAccountViewModel> Products = new[] {
        //    new TreeAccountViewModel {
        //        Name = "Stores",
        //        IsExpanded = true,
        //        Items = ac1
        //    }
        //};



        //    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(DevExtreme.AspNet.Data.DataSourceLoader.Load(Products, loadOptions)), "application/json");
        //}



      

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(TreeAccountViewModel model)
        {
            int idInsert = 0;

            if (ModelState.IsValid)
            {
                if (model.mainORsub == "sub")
                {
                    _TreeAccountService trs = new _TreeAccountService(_db);
                     idInsert = await trs.AddOrEditAccTreeAsync(model);

                }
                else if (model.mainORsub == "main")
                {
                    _TreeAccountService trs = new _TreeAccountService(_db);
                     idInsert = await trs.AddOrEditGroupTreeAsync(model);
                }
            }
            String mess = "تم حفظ السجل بنجاج";
            String optxt = "اضافة";

            if(Convert.ToInt32( model.Id) >0)
            {
                optxt = "تعديل";
            }

            if (idInsert == 0)
            {

                mess = " لم يتم "+ optxt+" السجل  .. حدث خطا اثناء التعديل";
            }
            else if (idInsert == -1)
            {
                if (model.mainORsub == "main")
                {
                    mess = " لم يتم " + optxt+"  السجل  ..المجموعة اساسية بالدليل المحاسبي.. يرجى اختيار مجموعة اخرى";

                }
                else

                    mess = "لم يتم "+ optxt+"السجل..الحساب اساسي بالدليل المحاسبي.. يرجى اختيار حساب اخر";
            }



            return Json(new
            {
                id = idInsert,
                messege = mess
            });



           
        }

    
        public async Task<IActionResult> delete(TreeAccountViewModel model)
        {
            int idInsert = 0;
            if (ModelState.IsValid)
            {
                if (model.mainORsub == "sub")
                {
                    _TreeAccountService trs = new _TreeAccountService(_db);
                    
                     idInsert = await trs.deleteAccTree( Convert.ToInt32(model.Id));


                }
                else if (model.mainORsub == "main")
                {
                    _TreeAccountService trs = new _TreeAccountService(_db);
                     idInsert = await trs.deleteGroupTree(Convert.ToInt32(model.Id));
                }
            }
            String mess = "تم الحذف بنجاج";

            if(idInsert==0)
            {
                mess = "لم يتم الحذف .. حدث خطا اثناء الحذف";
            }
            else if (idInsert == -1)
            {
                if(model.mainORsub == "main")
                {
                    mess = "لم يتم الحذف ..المجموعة مرتبط بمعاملات.. يرجى حذف معاملات المجموعة اولا";

                }
                else

                mess = "لم يتم الحذف ..الحساب مرتبط بمعاملات.. يرجى حذف معاملات الحساب اولا";
            }



            return Json(new
            {
                id = idInsert,
                messege = mess
            });
                
               
        }

      



    }
}