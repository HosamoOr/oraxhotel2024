using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class BillReceprionService
    {
        private readonly HotelAlkheerDB _db;
        int IdSub = 1;

        public BillReceprionService(HotelAlkheerDB context)
        {
            _db = context;
        }

     public   showInvRecepitonVM GetShowBillReception(long id_reception)
        {
            ReceptionService service = new ReceptionService(_db);

            var receptionDat = service.getOneAsync(id_reception);

            //***اجمالي المقبوضات 
            double sumPay = _db.GetPayBondListForReception("1", id_reception).
                Select(y => new BondShortViewModel
                {
                    id = y.id,
                    txt = y.id + " | " + y.amount + " ( " + y.typepay + " )",
                    amount = y.amount,
                    date = y.date
                }).
               Sum(x => x.amount);


            //***اجمالي المصروفات
            double sumEX = _db.GetPayBondListForReception("2", id_reception).
                Select(y => new BondShortViewModel
                {
                            // id = y.id,
                            // txt = y.id + " | " + y.amount + " ( " + y.typepay + " )",
                            amount = y.amount,
                            //date = y.date
                        }).
                Sum(x => x.amount);


            //***اجمالي الخدمات
            double sumService = _db.BillsTables.Where(x => x.Type == "7" && x.IdReception == id_reception).
                 Select(y => new BondShortViewModel
                 {
                             // id = y.Id,
                             // txt = y.Id + " | " + y.DeserveAmount + " ( " + y.TypePay + " )",
                             amount = Convert.ToInt64(y.DeserveAmount),

                             // date = y.Date
                         })
                .Sum(x => x.amount);


            //summary Price 

            // double sumPay = payBandList.Sum(x => x.amount);
            // sumEX = exBandList.Sum(x => x.amount);
            //double sumService = serviceBillList.Sum(x => x.amount);

            var final_total_ = receptionDat.total - Convert.ToDouble(receptionDat.bill.QtyDiscount);

            var balancer_ = (final_total_ + sumService + sumEX) - sumPay;
            var totalVAT_ = Convert.ToDouble(receptionDat.bill.TotalTaxPrice);
            var totalBALADI_ = Convert.ToDouble(receptionDat.bill.TotalBaladiTaxPrice);

            var totalBeforVAT_ = final_total_ - (totalVAT_ + totalBALADI_);




            SummaryPriceReceptionViewModel suPrice = new SummaryPriceReceptionViewModel
            {
                sum_Pay = sumPay,
                sum_Ex = sumEX,
                sum_services = sumService,
                total = receptionDat.total,
                final_total = final_total_,

                totalVAT = totalVAT_,
                totalBALADI = totalBALADI_,
                balancer = balancer_ * -1,

                total_befor_VAT = totalBeforVAT_,



            };

            receptionDat.summaryPrice = suPrice;

            ///------------
            ///
            var x = _db.OrgsTables.FirstOrDefault();

            String urlLogo = "";
            if (x.Logo != null)
            {
                urlLogo = @"/Upload/" + x.Logo;
            }

            var texSetting = _db.SettingGeneralTables.FirstOrDefault();


            OrgViewModel hotelModel = new OrgViewModel
            {
                Id = x.Id,
                Address = x.Address,
                City = x.City,
                Country = x.Country,
                Email = x.Email,
                IdSub = x.IdSub,
                Logo = urlLogo,
                MailBox = x.MailBox,
                NameH = x.NameH,
                NumEn = x.NumEn,
                Phone = x.Phone,
                Regin = x.Regin,
                Website = x.Website,
                IdCountry = x.IdCountry,
                TaxNum = x.TaxNum,


                enbleTaxNum = texSetting.EnableTaxNum

            };




            //
            QRService qrS = new QRService();



            var qrModel = qrS.GenerateFile(
                " Name Company:" + hotelModel.NameH + " Tax_Number:" + hotelModel.TaxNum +



                " id_Bill: " + id_reception.ToString() + " Total:" + final_total_.ToString()



                + " totalVAT:" + totalVAT_.ToString() + " totalBALADI:" + totalBALADI_.ToString() +
                " balancer:" + balancer_.ToString()
                );



            showInvRecepitonVM model = new showInvRecepitonVM
            {
                receptionData = receptionDat,

                hotelData = hotelModel,
                qR = qrModel

            };
            return model;
        }

     
    }
}
