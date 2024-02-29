using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.ViewModel
{

    public class JqueryDatatableParam
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public int iColumns { get; set; }
        public int iSortCol_0 { get; set; }
        public string sSortDir_0 { get; set; }
        public int iSortingCols { get; set; }
        public string sColumns { get; set; }
    }

    public class TowIdInt
    {
        public int ID { set; get; }
        public int ID2 { set; get; }

    }

    public class ListIdLong
    {
        //public long ID { set; get; }
        //public long ID2 { set; get; }

        public List<long> IDs { set; get; }

      public int status { set; get; }



    }

    public class ListIdLongAcc
    {
        //public long ID { set; get; }
        //public long ID2 { set; get; }

        public List<long> IDs { set; get; }

        public int status { set; get; }
        public String messege { set; get; }
        public CustomerViewModel modelCu { get; set; }

    }


    public class paramModel
        {
            public string search { set; get; }

            public string sort { set; get; }
            public string order { set; get; }
            public int limit { set; get; }
            public int offset { set; get; }
            public int pageSize { set; get; }

            public int pageNumber { set; get; }

            public int userid { set; get; }

           public DateTime startDate { set; get; }

           public DateTime endDate { set; get; }

        public bool isAll { set; get; }
        

    }
}
