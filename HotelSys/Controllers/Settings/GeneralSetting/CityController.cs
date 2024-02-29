//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using DataModels;
//using LinqToDB;

//namespace HotelSys.Controllers
//{
//    public class CityController : Controller
//    {
//        private readonly HotelAlkheerDB _context;

//        public CityController(HotelAlkheerDB context)
//        {
//            _context = context;
//        }

//        // GET: City
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.CityTables.ToListAsync());
//        }

//        // GET: City/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cityTable = await _context.CityTables
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (cityTable == null)
//            {
//                return NotFound();
//            }

//            return View(cityTable);
//        }

//        // GET: City/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

        
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name")] CityTable cityTable)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Insert(cityTable);
                
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cityTable);
//        }

//        // GET: City/Edit/5
//        public async Task<IActionResult> Edit(int id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cityTable =  _context.CityTables.Find(id);
//            if (cityTable == null)
//            {
//                return NotFound();
//            }
//            return View(cityTable);
//        }

//        // POST: City/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CityTable cityTable)
//        {
//            if (id != cityTable.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(cityTable);
                   
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CityTableExists(cityTable.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(cityTable);
//        }

//        // GET: City/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var cityTable =  _context.CityTables
//                .FirstOrDefault(m => m.Id == id);
//            if (cityTable == null)
//            {
//                return NotFound();
//            }

//            return View(cityTable);
//        }

//        // POST: City/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var cityTable =  _context.CityTables.Find(id);

//            _context.Delete(cityTable);

           
//            return RedirectToAction(nameof(Index));
//        }

//        private bool CityTableExists(int id)
//        {
//            return _context.CityTables.Any(e => e.Id == id);
//        }
//    }
//}
