using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
  

        public class _item_Status_Room
        {
            public int index { get; set; }
            public string name_status_Ar { get; set; }

            public string name_status_En { get; set; }
            public _item_Status_Room(int index, string nameAr, string nameEn)
            {
                this.index = index;
                name_status_Ar = nameAr;

                name_status_En = nameEn;
            }
        }


        public  class Status_RoomsName
        {

        public static List<_item_Status_Room> listStatus = new List<_item_Status_Room> 
        
        { new _item_Status_Room(1, "فارغة", "Empty"),

            new _item_Status_Room(2, "تنضيف", "Clean"),
            new _item_Status_Room(3, "صيانة", "Repair"),
            new _item_Status_Room(4, "حجز بدون تسجيل دخول ", "Reservation_without_entry"),
             new _item_Status_Room(5, "شاغرة", "Busy")


        };





    }

        
}
