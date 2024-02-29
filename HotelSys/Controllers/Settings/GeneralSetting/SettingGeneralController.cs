using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModels;

using LinqToDB;

namespace HotelSys.Controllers
{
    public class SettingGeneralController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public SettingGeneralController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: SettingGeneral_

        

            // GET: SettingGeneral_/Edit/5
            public async Task<IActionResult> Edit()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var settingGeneralTable =  _db.SettingGeneralTables.FirstOrDefault();
            if (settingGeneralTable == null)
            {
                settingGeneralTable = new SettingGeneralTable { Id = 0, ServicesIncludeTax = false ,EnableTaxNum=true};
                _db.Insert(settingGeneralTable);
            }
            return View(settingGeneralTable);
        }

        // POST: SettingGeneral_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  SettingGeneralTable settingGeneralTable)
        {
            if (id != settingGeneralTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(settingGeneralTable);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingGeneralTableExists(settingGeneralTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(settingGeneralTable);
        }

        // GET: SettingGeneral_/Delete/5
       
       
        private bool SettingGeneralTableExists(int id)
        {
            return _db.SettingGeneralTables.Any(e => e.Id == id);
        }
    }
}
