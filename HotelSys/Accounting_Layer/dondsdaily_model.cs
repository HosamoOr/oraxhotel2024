using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;


namespace HotelSys
{
    public  class dondsdaily_model
    {
        private readonly HotelAlkheerDB _db;
        public dondsdaily_model(HotelAlkheerDB context)
        {
            _db = context;
        }


        public async  Task<bool> addAsync(EntriesAccTable model)
        {
           
            bool state = false;
           
                try
                {
                    int st =  _db.Insert(model);
                    
                    state = true;
                    
                }
                catch (ApplicationException e)
                {
                    
                    state = false;
                    
                }
               
                return state;

           
        }
        public async Task add_MultiDondsdaily(
            List<EntriesAccTable> _Dondsdily_accounts
            //string id_type_document, long id_document, int id_currency
            )


        {
           
            var priceFrom = _Dondsdily_accounts.Where(dd => dd.DebtOrCredit == '-').Sum(ss => ss.Amount);
            var priceTo = _Dondsdily_accounts.Where(dd => dd.DebtOrCredit == '+').Sum(ss => ss.Amount);
            if (priceFrom == priceTo)
            {
                foreach (var DondA in _Dondsdily_accounts)
                {
                    //if (DondA.TypeOpration == "مدين")
                    //{
                    //    DondA.Note = noteAccount.note_dondsdailyFrom;
                    //}
                    //else
                    //{
                    //    DondA.Note = noteAccount.note_dondsdailyTo;
                    //}


                    await addAsync(DondA);
                }

            }

        }
        public async Task add_Double(
            string bill_or_band,
            int id_accountFrom, int id_accountTo, double price, string id_type_document, 
            long id_document, int? id_currency,
            long? id_reception, DateTime? date ,string ? note=""
            )
        {
            
            EntriesAccTable modelDond_From = new EntriesAccTable();
            EntriesAccTable modelDond_To = new EntriesAccTable();



            modelDond_To.IdCurrancy = modelDond_From.IdCurrancy = id_currency;
            modelDond_To.IdRecetion = modelDond_From.IdRecetion = id_reception;

            if (bill_or_band=="Band")
            {
                modelDond_From.IdDocumentDand = modelDond_To.IdDocumentDand = id_document;

                
            }
            else if (bill_or_band == "Bill")
            {
                modelDond_From.IdDocumentBill = modelDond_To.IdDocumentBill = id_document;
            }
            modelDond_From.BillOrBand = modelDond_To.BillOrBand = bill_or_band;

            modelDond_From.TypeDocument = modelDond_To.TypeDocument = id_type_document;

            modelDond_From.Amount = modelDond_To.Amount = price;

            modelDond_From.IdAccount = id_accountFrom;

            modelDond_From.DebtOrCredit = '-';

            //modelDond_From.Note = noteAccount.note_dondsdailyFrom;

            modelDond_To.IdAccount = id_accountTo;
            modelDond_To.DebtOrCredit = '+';
            // modelDond_To.Note = noteAccount.note_dondsdailyTo;

            modelDond_From.Date= modelDond_To.Date = date;

            modelDond_From.Note = modelDond_To.Note = note;


            List<EntriesAccTable> li = new List<EntriesAccTable>();
            li.Add(modelDond_From);
            li.Add(modelDond_To);


           await add_MultiDondsdaily(li);

            //decimal idF= _db.InsertWithDecimalIdentity(modelDond_From);
            // _db.InsertWithDecimalIdentity(modelDond_To);

           
        }

        public async  Task<bool> updateAsync(EntriesAccTable model)
        {
            
            bool state = false;
            int st = 0;
           // model.Price = VerfyPrice(model.TypeOpration, model.Price);
            //_db.GetTable<EntriesAccTable>().Update(tt => new EntriesAccTable
            //{

            //});

            st = await _db.UpdateAsync(model);
            if (st > 0)
            {
                state = true;
            }
            else
            {
                state = false;
            }
            return state;
        }

        public async  Task<bool> deleteAsync(decimal id)
        {
           
            bool state = false;
            var st = await _db.GetTable<EntriesAccTable>().Where(dd => dd.Id == id).DeleteAsync();
            // int st = await Db.DeleteAsync(model);
            if (st > 0)
            {
                state = true;
            }
            else
            {
                state = false;
            }

            return state;
        }
        public async  Task<bool> deleteByIdDocument(
             string bill_or_band,
            long id_document, string id_type_decument)


        {
           
            bool state = false;
            int st = 0;
            using (var t = _db.BeginTransaction())
            {
                try
                {

                    if (bill_or_band == "Band")
                    {
                        st = await _db.GetTable<EntriesAccTable>().
                   Where(dd => dd.IdDocumentDand == id_document && dd.TypeDocument == id_type_decument)
                   .DeleteAsync();
                    }
                    else
                    {
                        st = await _db.GetTable<EntriesAccTable>().
                                          Where(dd => dd.IdDocumentBill == id_document && dd.TypeDocument == id_type_decument)
                                          .DeleteAsync();
                    }

                        
                    t.Commit();
                }
                catch (ApplicationException)
                {
                    t.Rollback();
                    state = false;
                }

            }

            if (st > 0)
            {
                state = true;
            }
            else
            {
                state = false;
            }


            return state;
        }

        //****************** verfy methed

    }
}
