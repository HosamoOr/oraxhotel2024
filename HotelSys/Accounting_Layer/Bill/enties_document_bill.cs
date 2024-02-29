
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using HotelSys.Accounting_Layer;

namespace HotelSys.ModelAccount.Bill
{
  public  class EnteisAcc_bills_document
    {
         private readonly HotelAlkheerDB _db;

        public EnteisAcc_bills_document(HotelAlkheerDB context)
        {
            _db = context;
        }
        public async Task Master_ChickForAdd(BillsTable model, int  id_accountForm, int  id_accountTo,String nameCusOrCo)
        {
            if (model.Type == type_document.reservation.id_document)
            {
                // لا قيود لفاتورة الحجز
            
            }
            //للكل 
            else 
            {

                addEntiesAccBills(model, id_accountForm, id_accountTo,nameCusOrCo);
                // باقي قيود الخصم لاحقا
              
            }
            
        }

        public  async Task Master_ChickAsyncForUpdate(BillsTable model, int  id_accountForm, int  id_accountTo, String nameCusOrCo)
          
        {
                var DondsdailyForDelete = _db.EntriesAccTables.
                    Where(x => x.IdDocumentBill == model.Id 
                    &&  x.TypeDocument == model.Type
                    && x.BillOrBand=="Bill"  
                    ).ToList();

                for(int i=0;i< DondsdailyForDelete.Count();i++)
                _db.Delete(DondsdailyForDelete[i]);

            await addEntiesAccBills(model, id_accountForm, id_accountTo, nameCusOrCo);

           

        }


        public  void Master_ChickAsyncForDelete(BillsTable model)
        {

            var DondsdailyForDelete = _db.EntriesAccTables.
                    Where(x => x.IdDocumentBill == model.Id
                    && x.TypeDocument == model.Type
                    && x.BillOrBand == "Bill"
                    ).ToList();

                for (int i = 0; i < DondsdailyForDelete.Count(); i++)
                    _db.Delete(DondsdailyForDelete[i]);


        

        }

         

        async Task addEntiesAccBills(BillsTable model , int _id_accountForm,
           int  _id_accountTo, String nameCusOrCo
            )
        {
            dondsdaily_model don = new dondsdaily_model(_db);
            String noteAsType = "";

            if(model.Type=="6")
            {
                noteAsType = noteAccount.note_bill_reception;
            }
            else if (model.Type == "7")
            {
                noteAsType = noteAccount.note_bill_service;

            }
            else if (model.Type == "8")
            {
                noteAsType = noteAccount.note_bill_back_service;

            }

            List<EntriesAccTable> li = new List<EntriesAccTable>();

           
           
             EntriesAccTable modelDond_From = new EntriesAccTable();

            modelDond_From.IdCurrancy = model.IdCurrancy;
            modelDond_From.IdRecetion = model.IdReception;
            modelDond_From.BillOrBand = "Bill";
            modelDond_From.IdDocumentBill = model.Id;
            modelDond_From.TypeDocument = model.Type;
            modelDond_From.Amount = Convert.ToDouble(model.DeserveAmount);

            modelDond_From.IdAccount = _id_accountForm;

            modelDond_From.DebtOrCredit = '-';
            modelDond_From.Date = model.Date;
            modelDond_From.Note = noteAccount.note_bill_last + noteAsType + " رقم "+model.Id + " خاصة بالعميل " + nameCusOrCo+"("+ model.IdAccount+")";
                li.Add(modelDond_From);

            //---------------------------
            EntriesAccTable modelDond_To = new EntriesAccTable();
            double priceAfeterVAT = Convert.ToDouble(model.DeserveAmount) - Convert.ToDouble(model.TotalTaxPrice)- Convert.ToDouble(model.TotalBaladiTaxPrice);

            modelDond_To.IdCurrancy = model.IdCurrancy;
            modelDond_To.IdRecetion = model.IdReception;
            modelDond_To.BillOrBand = "Bill";
            modelDond_To.IdDocumentBill = model.Id;
            modelDond_To.TypeDocument = model.Type;
            modelDond_To.Amount = priceAfeterVAT;

            modelDond_To.IdAccount = _id_accountTo;

            modelDond_To.DebtOrCredit = '+';
            modelDond_To.Date = model.Date;
            modelDond_To.Note = noteAccount.note_tax_bill_reception_last + noteAsType + " رقم " + model.Id + " خاصة بالعميل " + nameCusOrCo + "(" + model.IdAccount + ")"; ;
            li.Add(modelDond_To);




            if (model.TotalTaxPrice > 0 && model.TotalTaxRate > 0)
            {
                EntriesAccTable modelDond_To1 = new EntriesAccTable();

                modelDond_To1.IdCurrancy = model.IdCurrancy;
                modelDond_To1.IdRecetion = model.IdReception;
                modelDond_To1.BillOrBand = "Bill";
                modelDond_To1.IdDocumentBill = model.Id;
                modelDond_To1.TypeDocument = model.Type;
                modelDond_To1.Amount = model.TotalTaxPrice;

                modelDond_To1.IdAccount = Static_Accounts.VAT;

                modelDond_To1.DebtOrCredit = '+';
                modelDond_To1.Date = model.Date;

                modelDond_To1.Note = noteAccount.note_tax_bill_reception_last + noteAsType + " رقم " + model.Id + " خاصة بالعميل " + nameCusOrCo + "(" + model.IdAccount + ")"; ;
                li.Add(modelDond_To1);
            }

            if (model.TotalBaladiTaxPrice > 0 && model.TotalBaladiTaxRate > 0)
            {
                EntriesAccTable modelDond_To1 = new EntriesAccTable();

                modelDond_To1.IdCurrancy = model.IdCurrancy;
                modelDond_To1.IdRecetion = model.IdReception;
                modelDond_To1.BillOrBand = "Bill";
                modelDond_To1.IdDocumentBill = model.Id;
                modelDond_To1.TypeDocument = model.Type;
                modelDond_To1.Amount = model.TotalBaladiTaxPrice;

                modelDond_To1.IdAccount = Static_Accounts.VAT_BALADY;

                modelDond_To1.DebtOrCredit = '+';
                modelDond_To1.Date = model.Date;
                modelDond_To1.Note = noteAccount.note_taxBALABI_bill_reception_last + noteAsType + " رقم " + model.Id + " خاصة بالعميل " + nameCusOrCo + "(" + model.IdAccount + ")"; ;
                li.Add(modelDond_To1);
            }

            await don.add_MultiDondsdaily(li);



         //   if (model.IsBaladiTax==false && model.TotalTaxPrice== 0 && model.TotalTaxRate == 0)
         //   {
         //       await don.add_Double("Bill",

         //Convert.ToInt32(_id_accountForm), _id_accountTo, Convert.ToDouble(model.DeserveAmount),
         //model.Type, model.Id, model.IdCurrancy, model.IdReception, model.Date

         //);
         //   }

         



        }
    }
}
