using DataModels;
using HotelSys.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Controllers
{
    public class RoomLoginCurrentController : Controller
    {
        private readonly HotelAlkheerDB _db;

        public RoomLoginCurrentController(HotelAlkheerDB context)
        {
            _db = context;

        }

        //DataTable
        public async Task<ActionResult> GetDataFarz1Async(JqueryDatatableParam param)
        {
            var Parts = _db.GetRoomReceptionWithIn(1).Select(xx => new RoomLoginViewModel
            {
               
                Status = xx.status,

              
                IdReception = xx.IdReception,
                IdRoom = xx.id_room,
                IdAccount=xx.id_account,

                nameCuOrCo = xx.nameAccount,
                CustomerOrCompany=xx.customer_or_company,
                
                RoomModel = new RoomViewModel
                {
                    IdR = xx.id_room,
                    NameR = xx.name_room,


                    Roomstabletyperoomstable = new TypeRoomsTable
                    {
                        NameT = xx.type_room,
                        Color = xx.color


                    }
                }


            }).ToList();

           

            var ss = HttpContext.Request.QueryString.Value;

            string page = HttpContext.Request.Query["iSortCol_0"];


            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.Query["iSortCol_0"]);
            var sortDirection = HttpContext.Request.Query["iSortCol_0"];

            if (sortColumnIndex == 0)
            {
                //
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.IdReception).ToList() : Parts.OrderByDescending(c => c.IdReception).ToList();
                //  Parts = Parts.OrderBy(c => c.Company).ToList();
            }
            else if (sortColumnIndex == 1)
            {
                Parts = sortDirection == "asc" ? Parts.OrderBy(c => c.RoomModel.NameR).ToList() : Parts.OrderByDescending(c => c.RoomModel.NameR).ToList();
            }



            var totalRecords = Parts.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = Parts
            });

        }

    }
}
