using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Help
{

    public class _itemTypeCustomer
    {
        public int index { get; set; }
        public string type { get; set; }

    }
    public class TypeCustomerHelp
    {
        public static _itemTypeCustomer customerType = new _itemTypeCustomer { 
        index=0,
        type="Customer"
        };

        public static _itemTypeCustomer FollowerType = new _itemTypeCustomer
        {
            index = 1,
            type = "Follower"
        };


    }
}
