using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    [Authorize]
    public class Wellcome1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}