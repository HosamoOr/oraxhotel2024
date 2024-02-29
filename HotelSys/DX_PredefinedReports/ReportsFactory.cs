using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys
{
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["CusInHotelRPT"] = () => new PredefinedReports.CusInHotelRPT(),

             ["XtraReport1"] = () => new PredefinedReports.XtraReport1()
        };
    }
}
