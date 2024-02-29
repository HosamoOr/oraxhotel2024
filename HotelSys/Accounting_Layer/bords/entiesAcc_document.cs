
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using HotelSys.Accounting_Layer;

namespace HotelSys.ModelAccount.bords
{
  public  class donds_document_bond
    {
         private readonly HotelAlkheerDB _db;

        public donds_document_bond(HotelAlkheerDB context)
        {
            _db = context;
        }
        public async Task Master_ChickAsync(BondTable model, int  id_accountForm, int  id_accountTo)
        {

            await addDondsdailyBord(model, id_accountForm, id_accountTo);

            //if (model.Type == type_document.taking.id_document)
            //{
              
            //}
            //else if (model.Type == type_document.pay.id_document)
            //{
            //    await addDondsdailyBordPay(model, id_accountForm, id_accountTo);
            //}

        }

        public  async Task Master_ChickAsyncForUpdate(BondTable model, int id_accountForm, int id_accountTo)
        {

            var DondsdailyForDelete = _db.EntriesAccTables.
                    Where(x => x.IdDocumentDand == model.Id
                    && x.TypeDocument == model.Type
                    && x.BillOrBand == "Band"
                    ).ToList();
            var date = DateTime.Now;


          if(DondsdailyForDelete.FirstOrDefault().Date!= null)
            {
                date = Convert.ToDateTime(DondsdailyForDelete.FirstOrDefault().Date);
            }
          model.Date = date;

            for (int i = 0; i < DondsdailyForDelete.Count(); i++)
            {
                model.Date = Convert.ToDateTime( DondsdailyForDelete[i].Date);

                _db.Delete(DondsdailyForDelete[i]);

            }

            await addDondsdailyBord(model, id_accountForm, id_accountTo);

          
        }


        public  void Master_ChickAsyncForDelete(BondTable model)
        {
            var DondsdailyForDelete = _db.EntriesAccTables.
                    Where(x => x.IdDocumentDand == model.Id
                    && x.TypeDocument == model.Type
                    && x.BillOrBand == "Band"
                    ).ToList();

            for (int i = 0; i < DondsdailyForDelete.Count(); i++)
                _db.Delete(DondsdailyForDelete[i]);


        }

          async Task addDondsdailyBord(BondTable model_bord, int id_accountForm, int _id_accountTo)
        {

            string idtype_document = model_bord.Type;              //type_document.taking.id_document;


            dondsdaily_model don = new dondsdaily_model(_db);

           await don.add_Double(
                "Band", id_accountForm, Convert.ToInt32(_id_accountTo), 
                model_bord.Amount, idtype_document, model_bord.Id, 
                model_bord.IdCurrancy ,model_bord.IdReception,model_bord.Date, model_bord.Note);
        }

        //async Task addDondsdailyBordPay(BondTable model_bord , int ? _id_accountForm, int? _id_accountTo)
        //{
           
        //     string idtype_document = type_document.pay.id_document;
        //    int id_accountTo = Static_Accounts.Main_Box;
        //    if (_id_accountTo != null)
        //        id_accountTo = Convert.ToInt32(_id_accountTo);
        //    dondsdaily_model don = new dondsdaily_model(_db);

        //   await don.add_Double("Band",
                
        //        Convert.ToInt32(_id_accountForm), id_accountTo, model_bord.Amount, idtype_document, model_bord.Id, model_bord.IdCurrancy);

        //}
    }
}
