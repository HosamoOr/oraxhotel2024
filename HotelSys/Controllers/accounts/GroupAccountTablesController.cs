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
using Microsoft.AspNetCore.Authorization;

namespace HotelSys.Controllers
{
    [Authorize]
    public class GroupAccountController : Controller
    {
        private readonly HotelAlkheerDB _context;

        public GroupAccountController(HotelAlkheerDB context)
        {
            _context = context;
        }

        // GET: GroupAccountTables
        public async Task<IActionResult> Index()
        {
            List<GroupAccountViewModel> hotelSysContextConnection = 
                _context.GroupAccountTables
                .Select(ss=>new GroupAccountViewModel 
                { 
                Id=ss.Id,
                IdMainGroup=ss.IdMainGroup,
                IdSub=ss.IdSub,
                IsPrivate=ss.IsPrivate,
                IsRoot=ss.IsRoot,
                 Name=ss.Name,
                 NameMainGroup=ss.FkGroupAccountGroupAccountBackReferences.FirstOrDefault().Name
                })
                .ToList();
            return View( hotelSysContextConnection);
        }

        // GET: GroupAccountTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupAccountTable =  _context.GroupAccountTables
                .Include(g => g.FkGroupAccountGroupAccountBackReferences)
                .FirstOrDefault(m => m.Id == id);
            if (groupAccountTable == null)
            {
                return NotFound();
            }

            return View(groupAccountTable);
        }

        // GET: GroupAccountTables/Create
        public IActionResult Create()
        {
            ViewData["IdMainGroup"] = new SelectList(_context.GroupAccountTables, "Id", "Name");
            return View();
        }

        // POST: GroupAccountTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupAccountTable groupAccountTable)
        {
            if (ModelState.IsValid)
            {
                groupAccountTable.IsRoot = false;
                groupAccountTable.IsPrivate = false;
                
                _context.Insert(groupAccountTable);
              
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMainGroup"] = new SelectList(_context.GroupAccountTables, "Id", "Name", groupAccountTable.IdMainGroup);
            return View(groupAccountTable);
        }

        // GET: GroupAccountTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupAccountTable = _context.GroupAccountTables.Find(Convert.ToInt32(id));

           
            if (groupAccountTable == null)
            {
                return NotFound();
            }
            var mo = new GroupAccountViewModel
            {
                Id = groupAccountTable.Id,
                IdMainGroup = groupAccountTable.IdMainGroup,
                IdSub = groupAccountTable.IdSub,
                IsPrivate = groupAccountTable.IsPrivate,
                IsRoot = groupAccountTable.IsRoot,
                Name = groupAccountTable.Name,
                
                //NameMainGroup= groupAccountTable
            };
            ViewData["IdMainGroup"] = new SelectList(_context.GroupAccountTables, "Id", "Name", mo.IdMainGroup);
            return View(mo);
        }

        // POST: GroupAccountTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupAccountViewModel groupAccountTable)
        {
            if (id != groupAccountTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                  var mo=  _context.GroupAccountTables.Find(groupAccountTable.Id);
                    if(mo!=null)
                    {
                        mo.Name = groupAccountTable.Name;
                        mo.IdMainGroup = groupAccountTable.IdMainGroup;

                        mo.IsPrivate = false;
                        mo.IsRoot = false;
                        _context.Update(mo);

                    }


                   
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupAccountTableExists(groupAccountTable.Id))
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
            ViewData["IdMainGroup"] = new SelectList(_context.GroupAccountTables, "Id", "Name", groupAccountTable.IdMainGroup);
            return View(groupAccountTable);
        }

        // GET: GroupAccountTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var groupAccountTable = _context.GroupAccountTables.Find(Convert.ToInt32( id));

            
            if (groupAccountTable == null)
            {
                return NotFound();
            }

            GroupAccountViewModel mo = new GroupAccountViewModel
            {
                Id = groupAccountTable.Id,
                IdMainGroup = groupAccountTable.IdMainGroup,
                IdSub = groupAccountTable.IdSub,
                IsPrivate = groupAccountTable.IsPrivate,
                Name = groupAccountTable.Name,
                IsRoot = groupAccountTable.IsRoot,
                //NameMainGroup = groupAccountTable.FkGroupAccountGroupAccountBackReferences.FirstOrDefault().Name
            };

            return View(mo);
        }

        // POST: GroupAccountTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groupAccountTable =  _context.GroupAccountTables.Find(id);

            _context.Delete(groupAccountTable);
         
            return RedirectToAction(nameof(Index));
        }

        private bool GroupAccountTableExists(int id)
        {
            return _context.GroupAccountTables.Any(e => e.Id == id);
        }
    }
}
