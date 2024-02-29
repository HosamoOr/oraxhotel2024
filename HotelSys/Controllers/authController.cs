using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    public class authController : Controller
    {
        // GET: auth
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(int id, [Bind("Username,Password")] LoginViewModel user)
        {
            //if (id != book.Id)
            //{
            //    return NotFound();
            //}
            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (Exception rr)
                {
                    //if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: auth/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: auth/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: auth/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: auth/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: auth/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: auth/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: auth/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}