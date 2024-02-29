using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelSys.Models;

namespace HotelSys.Controllers
{
    public class AreaTablesController : Controller
    {
        private readonly Hotel_alkheerContext _context;

        public AreaTablesController(Hotel_alkheerContext context)
        {
            _context = context;
        }

        // GET: AreaTables
        public async Task<IActionResult> Index()
        {
            var hotelDb_2Context = _context.AreaTables.Include(a => a.IdCityNavigation);
            return View(await hotelDb_2Context.ToListAsync());
        }

        // GET: AreaTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaTable = await _context.AreaTables
                .Include(a => a.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaTable == null)
            {
                return NotFound();
            }

            return View(areaTable);
        }

        // GET: AreaTables/Create
        public IActionResult Create()
        {
            ViewData["IdCity"] = new SelectList(_context.CityTables, "Id", "Name");
            return View();
        }

        // POST: AreaTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IdCity")] AreaTable areaTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCity"] = new SelectList(_context.CityTables, "Id", "Name", areaTable.IdCity);
            return View(areaTable);
        }

        // GET: AreaTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaTable = await _context.AreaTables.FindAsync(id);
            if (areaTable == null)
            {
                return NotFound();
            }
            ViewData["IdCity"] = new SelectList(_context.CityTables, "Id", "Name", areaTable.IdCity);
            return View(areaTable);
        }

        // POST: AreaTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdCity")] AreaTable areaTable)
        {
            if (id != areaTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaTableExists(areaTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCity"] = new SelectList(_context.CityTables, "Id", "Name", areaTable.IdCity);
            return View(areaTable);
        }

        // GET: AreaTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaTable = await _context.AreaTables
                .Include(a => a.IdCityNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaTable == null)
            {
                return NotFound();
            }

            return View(areaTable);
        }

        // POST: AreaTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var areaTable = await _context.AreaTables.FindAsync(id);
            _context.AreaTables.Remove(areaTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaTableExists(int id)
        {
            return _context.AreaTables.Any(e => e.Id == id);
        }
    }
}
