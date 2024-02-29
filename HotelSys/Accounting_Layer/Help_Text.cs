using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class Value_Return
    {
        public bool success { set; get; }
        public string message { set; get; }
        public int count_row { set; get; }

        public int id_int { set; get; }

        public long id_long { set; get; }

    }
    public static class messageApp
    {
        public static List<String> txt_message = new List<string>
    {
     "خطا في البيانات",
     "تم اضافة السجل بنجاح",
     "تم تحديث بيانات السجل بنجاح"
    };
    }
    public static class noteAccount
    {
        public static string note_dondsdailyFrom = "من حـ/";
        public static string note_dondsdailyTo = "الى حـ/";

        public static string note_tax_bill_reception_last = " مبلغ القيمة المضافة فاتورة   ";
        public static string note_bill_last = " مبلغ قيمة فاتورة   ";
        public static string note_taxBALABI_bill_reception_last = " مبلغ القيمة المضافة فاتورة   ";

        public static string note_bill_reception = " تأجير  ";
        public static string note_bill_service = " خدمات  ";
        public static string note_bill_back_service = " مردود خدمات  ";


    }
    public static class Static_Accounts
    {
        public static int Main_Box = 131001;
        public static int Paying = 31;
        public static int Sale = 41;
        public static int back_Paying = 32;
        public static int Back_Sale = 42;

        //ايرادات الخدمات
        public static int Income = 611;
        //ايرادات الأيجار
        public static int IncomeRent = 211002;
        //ضريبة مبيعات 
        public static int VAT = 43;
        //ضريبة بلدي مبيعات 
        public static int VAT_BALADY = 44;

    }

    public static class Static_Group_Accounts
    {
        //عملاء افراد
        public static int Customer_Individual = 224;
        //عملاء شركات
        public static int Customer_company = 225;

        //الصناديق
        public static int Boxs = 131;

        public static int ItemsExpenses = 229;
        

    }

    public static class Status_Account
    {
        public static string active = "active";
        public static string disable = "disable";

    }


   


    public class item_document
    {
        public string id_document { get; set; }
        public string name_document { get; set; }

        public string name_en_document { get; set; }
        public item_document(string id, string name,string nameEn)
        {
            id_document = id;
            name_document = name;
            name_en_document = nameEn;
        }
    }
    public  class type_document
    {
        public static item_document default_ = new item_document("0", "الافتراضي", "default");

        public static item_document taking = new item_document("1", "سند قبض", "taking");
        public static item_document pay = new item_document("2", "سند صرف", "pay");
        public static item_document back_reception = new item_document("3", "ايجار مسترد-بدل ايجار", "back reception");
        public static item_document draft = new item_document("4", "سند كمبيالة", "draft");


        public static item_document reservation = new item_document("5", "فاتورة حجز", "reservation");
        public static item_document rent = new item_document("6", "فاتورة ايجار", "rent");
        public static item_document service = new item_document("7", "فاتورة خدمات", "service");
        public static item_document bac_service = new item_document("8", "فاتورة مردود خدمات", "back service");

        


        public static item_document paying = new item_document("10", "فاتورة مشتريات","");
        public static item_document back_paying = new item_document("11", "فاتورة مردود مشتريات", "");
        public static item_document sale = new item_document("12", "فاتورة مبيعات","");
        public static item_document back_sale = new item_document("13", "فاتورة مردود مبيعات","");

        public static item_document dond_day = new item_document("14", "قيد يومية","");
        public static item_document open_balance = new item_document("15", "رصيد افتتاحي","");


        public static List<item_document> itemDoc = new List<item_document>{
            default_,
                       taking,
                       pay,
                        back_reception,
                       draft,
                       reservation,
                       rent,
                       service,
                        bac_service,
                       // paying,

                    };
    }

    public static class TimeDefualt
    {
        public static TimeSpan timeDefualt = TimeSpan.Parse("12:00");

    }

   public static class pay_type
    {
        public static item_document cash = new item_document("1", "نقدا", "");
        public static item_document last = new item_document("2", "كمبيالة", "");
        public static item_document trasportBank = new item_document("3", "تحويل بنكي", "");

       
    }
    public static class Nots
    {
        public static string getBondNote(string type)
        {
            string note = "سند";
            

           if(type =="1")
            {
                note += type_document.taking.name_document;
            }
           else if(type =="2")
            {
                note += type_document.pay.name_document ;
            }
           else if(type =="3")
            {
                note += type_document.back_reception.name_document ;
            }
           else if (type == "4")
            {
                note += type_document.draft.name_document ;
            }
            return note;


        }
    
}

}
