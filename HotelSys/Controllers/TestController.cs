using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelSys.Controllers
{
    public class TestController : Controller
    {
        public TestController()
        {

        }
        public IActionResult Index()
        {
            HotelAlkheerDB db = new HotelAlkheerDB();
            //db.UserTables.
            return View();
        }
    }
}
