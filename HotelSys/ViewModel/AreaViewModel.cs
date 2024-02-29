using System.Collections.Generic;

namespace HotelSys.ViewModel
{
    public class AreaViewModel
    {
        public int Id { get; set; } // int
        public string Name { get; set; } // nvarchar(100)
        public int IdCity { get; set; } // int

        public string NameCity { get; set; }

        public int countCustomer { get; set; }

        public int IdCountry { get; set; }

        public string NameCountry { get; set; }


    }

    public class DTAreaViewModel
    {
        public List<AreaViewModel> list { get; set; }
        public int countRow { get; set; }

    }
}
