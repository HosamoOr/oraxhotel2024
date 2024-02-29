using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DataModels;
using LinqToDB;
using HotelSys.ViewModel;
using HotelSys.Accounting_Layer;

namespace HotelSys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public ProductsController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: Products
        public async Task<IActionResult> IndexAll()
        {
            var hotelSysContextConnection = _db.ProductTables.Include(p => p.Fkgroupservicesproductservice);
            return View( hotelSysContextConnection.ToList());
        }

        // GET: Products/Details/5  

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _db.GroupServicesTables.
                Select(xx => new GroupServicesViewModel
                {
                    Id = xx.Id,
                    Name = xx.Name,
                    IdSub = xx.IdSub,
                    NameEn = xx.NameEn,
                    countBrchService = xx.Fkgroupservicesproductservices.Count(),

                    products = xx.Fkgroupservicesproductservices.Select(yy => new ProductViewModel
                    {
                        Id = yy.Id,
                        IdGroup = yy.IdGroup,
                        Name = yy.Name,
                        NameEn = yy.NameEn,
                        Price = yy.Price,
                        IdTaxGroup=yy.IdTaxGroup,
                        nameTaxGroup=yy.Producttabletaxgrouptable.Name

                    }).ToArray()
                })
                //.Include(p => p.Fkgroupservicesproductservice)
                .FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        //DataTable
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param, int? id)
        {
            var gs = new ProductService(_db);

            var Parts = await gs.ListDT(param,id);


            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Id).ToList() : Parts.OrderByDescending(c => c.Id).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.Name).ToList() : Parts.OrderByDescending(c => c.Name).ToList();
            }



            var totalRecords = _db.ProductTables.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model =  _db.GroupServicesTables.
                Select(xx=>new GroupServicesViewModel { 
                Id=xx.Id,
                Name=xx.Name,
                IdSub=xx.IdSub,
                NameEn=xx.NameEn,
                countBrchService=xx.Fkgroupservicesproductservices.Count(),

                products=xx.Fkgroupservicesproductservices.Select(yy=>new ProductViewModel { 
                Id=yy.Id,
                IdGroup=yy.IdGroup,
                Name=yy.Name,
                NameEn=yy.NameEn,
                Price=yy.Price,
                IdTaxGroup=yy.IdTaxGroup,
                nameTaxGroup=yy.Producttabletaxgrouptable.Name
                
                }).ToArray()
                })
                //.Include(p => p.Fkgroupservicesproductservice)
                .FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdGroup"] = new SelectList(_db.GroupServicesTables, "Id", "Name");

            ViewData["IdGroupTax"] = new SelectList(_db.TaxGroupTables, "Id", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductTable productTable)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(productTable);
              
                return RedirectToAction(nameof(Index), new { id = productTable.IdGroup } );
            }
            ViewData["IdGroup"] = new SelectList(_db.GroupServicesTables, "Id", "Name", productTable.IdGroup);
            ViewData["IdGroupTax"] = new SelectList(_db.TaxGroupTables, "Id", "Name",productTable.IdTaxGroup);
            return View(productTable);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTable =  _db.ProductTables.Find(Convert.ToInt32( id));
            if (productTable == null)
            {
                return NotFound();
            }
            ViewData["IdGroup"] = new SelectList(_db.GroupServicesTables, "Id", "Name", productTable.IdGroup);
            ViewData["IdGroupTax"] = new SelectList(_db.TaxGroupTables, "Id", "Name", productTable.IdTaxGroup);
            return View(productTable);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ProductTable productTable)
        {
            if (id != productTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(productTable);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTableExists(productTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = productTable.IdGroup });
            }
            ViewData["IdGroup"] = new SelectList(_db.GroupServicesTables, "Id", "Name", productTable.IdGroup);
            ViewData["IdGroupTax"] = new SelectList(_db.TaxGroupTables, "Id", "Name", productTable.IdTaxGroup);
            return View(productTable);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTable =  _db.ProductTables
                .Include(p => p.Fkgroupservicesproductservice)
                .FirstOrDefault(m => m.Id == id);
            if (productTable == null)
            {
                return NotFound();
            }

            return View(productTable);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productTable =  _db.ProductTables.Find(id);
            await _db.DeleteAsync(productTable);

            return   RedirectToAction(nameof(Index), new { id = productTable.IdGroup });
        }

        private bool ProductTableExists(int id)
        {
            return _db.ProductTables.Any(e => e.Id == id);
        }
    }
}
