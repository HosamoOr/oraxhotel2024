using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using HotelSys.ViewModel;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace HotelSys.Controllers
{
    [Authorize]
    //[Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly HotelAlkheerDB _db;


        public UsersController(HotelAlkheerDB context)
        {
            _db = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var list = _db.AspNetUsers.
                Select(x=>new UserViewModel
                {
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    PhoneNumber=x.PhoneNumber,
                    boxUser = x.Boxsusertables.Select(y=>new BoxViewModel
                    {
                        Id = y.IdBox,
                        
                        IdAccount=y.Boxsusertableboxstable.IdAccount,
                        Name=y.Boxsusertableboxstable.Name,
                        IsDefault=Convert.ToBoolean( y.IsDefult),
                        

                    }).ToList(),

                }).
                ToList();
            return View(list);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers =  _db.AspNetUsers
                .FirstOrDefault(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,FirstName,LastName")] AspNetUser aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                _db.Insert(aspNetUsers);
               
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUsers);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers =  _db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AspNetUser aspNetUsers)
        {
            if (id != aspNetUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(aspNetUsers);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(aspNetUsers.Id))
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
            return View(aspNetUsers);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers =  _db.AspNetUsers
                .FirstOrDefault(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers =  _db.AspNetUsers.Find(id);
            _db.Delete(aspNetUsers);
           
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _db.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
