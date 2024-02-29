using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class LogoutService
    {
        private readonly HotelAlkheerDB _db;
        int IdSub = 1;

        public LogoutService(HotelAlkheerDB context)
        {
            _db = context;
        }

        private TimingLogViewModel getTimRece(TimingLogViewModel t,int IdSub)
        {
           

            var itLogout = _db.SettingReceptionTables.
                  //Where(x => x.IdSub == 1).
                  FirstOrDefault();
            if(itLogout!=null)

            if (itLogout.IsCheckoutTime == true)
            {
                t.source = 1;
                t.timeDefualt =  itLogout.TimeCheckout;

                if (t.timeNow > itLogout.TimeCheckout)
                {
                    t.isoverflow = true;

                }
            }
            else
            {

                if (t.timeNow > TimeDefualt.timeDefualt)
                {
                    t.isoverflow = true;

                }
            }
            return t;

        }
        public  SummaryLogOutViewModel getSummary(LogoutRoomViewModel model)
        {
            var itemReception = _db.GetBalanceReception(model.IdReception, model.IdAccount, model.IdSub).ToList();

            double sumFromPrice = 0;
            double sumToPrice = 0;
            double balance = 0;
            if (itemReception != null)
            {

                for (int i = 0; i < itemReception.Count(); i++)
                {
                    var temp1 = Convert.ToDouble(itemReception[i].FromPrice);
                    sumFromPrice += temp1;
                    var temp2 = Convert.ToDouble(itemReception[i].ToPrice);
                    sumToPrice += temp2;


                   
                }
                //sumFromPrice *= -1;

                balance = sumFromPrice + sumToPrice;

            }
            item_document ItemDocument = type_document.itemDoc[0];
            // المستند المتوقع ان المبالغ باقية علية هو اكبر مبلغ مدين
            if (balance < 0)
            {
                var maxD = itemReception.OrderBy(x => x.FromPrice).First(); //for maximum
                int index = Convert.ToInt32(maxD.typeDocumnet);

                ItemDocument = type_document.itemDoc[index];

                // mass = "يتبقى مبالغ على العميل",
            }

            // فحص تاريخ خروج العميل مع تاريخ انتهاء العقد


            var moRece = _db.RecetionTables.Where(x => x.Id == model.IdReception).FirstOrDefault();


         //   int countReception = (moRece.EndDate.Date - moRece.StartDate.Date).TotalDays;

            int countReception = (moRece.EndDate.Date - DateTime.Now.Date).Days;
            // int countReceptionNow = ( DateTime.Now.Date - moRece.StartDate.Date).Days;

            double FinalBalance = 0;

            //فحص الوقت هل تجاوز وقت الخروج الافتراضي او المحدد من قبل المستخدم\

            TimingLogViewModel timTemp = new TimingLogViewModel
            {
                isoverflow = false,
                isSelectUser = false,
                timeNow = DateTime.Now.TimeOfDay, //DateTime.Now.ToString("h:mm:ss tt")
                source = 1,
                timeDefualt= TimeDefualt.timeDefualt
            };


            //بنفس تاريخ الخروج
            if (countReception == 0 || countReception==1)
            {

            }
           
            // اعلى من تاريخ الخروج
            else if(countReception < 0)
            {
                // تضاف عليه فاتورة

              
                FinalBalance = Convert.ToDouble(moRece.Price) * 
                    Convert.ToDouble(countReception);

                balance = FinalBalance;
                ItemDocument = type_document.itemDoc[1];

                


            }
            // اقل من تاريخ الخروج ولا يساوي تاريخ الدخول
            else if (countReception > 1)
            {
                //اقل و بنفس تاريخ الدخول  
                if (countReception == moRece.QtyTime)
                {
                    countReception = countReception - 1;
                }
                // يضاف له سند 
                FinalBalance = Convert.ToDouble(moRece.Price) * Convert.ToDouble( countReception);
                balance = FinalBalance;
                ItemDocument = type_document.itemDoc[3];
                
            }



            if (countReception != 0)
            {
                timTemp = getTimRece(timTemp, IdSub);
            }


             SummaryLogOutViewModel temp = new SummaryLogOutViewModel
            {
                item= itemReception,
                balance=balance,
                sumFromPrice=sumFromPrice,
                sumToPrice=sumToPrice,
                itemDocument = ItemDocument,
                countDayReception= countReception,
                timinglog = timTemp
            };
            return temp;
        }

    }
}
