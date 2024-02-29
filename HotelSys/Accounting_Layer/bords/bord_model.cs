using DataModels;

using HotelSys.Accounting_Layer;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSys.ModelAccount.bords;
using HotelSys.ViewModel;

namespace HotelSys.Accounting_Layer.bords
{

    public class bond_model
    {
        private readonly HotelAlkheerDB _db;

        public bond_model(HotelAlkheerDB context)
        {
            _db = context;
        }

        public  Value_Return addAsync(BondTable model,int  id_accountForm,int  id_accountTo)
        {
           
            Value_Return vr = new Value_Return();
             // try
                {


                model.Note = model.Note +"-"+ Nots.getBondNote(model.Type);

                 var id_ =  _db.InsertWithIdentity(model);
                    model.Id = Convert.ToInt64(id_);


                model.Note= model.Note+" رقم " +"(" + model.Id +")";
                donds_document_bond ddb = new donds_document_bond(_db);


                 ddb.Master_ChickAsync(model, id_accountForm, id_accountTo);
                   
                    vr.success = true;
                    vr.message = messageApp.txt_message[1];

                }
               // catch (ApplicationException e)
                {
                    
                    vr.success = false;
                vr.id_long = model.Id;
                   // vr.message = "";
                }
            
            return vr;
        }


        public async Task<Value_Return> UpdateAsync(BondTable model, int id_accountForm, int id_accountTo)
        {
           
            Value_Return vr = new Value_Return();
            using (var t = _db.BeginTransaction())
            {
                try
                {

                    var id_ = model.Id;

                    await _db.UpdateAsync(model);
                    
                    donds_document_bond ddb = new donds_document_bond(_db);



                    await ddb.Master_ChickAsyncForUpdate(model, id_accountForm, id_accountTo);
                    t.Commit();
                    vr.success = true;
                    vr.message = messageApp.txt_message[1];

                }
                catch (ApplicationException e)
                {
                    t.Rollback();
                    vr.success = false;
                    vr.message = e.Message;
                }
            }
            return vr;
        }


        public async Task<Value_Return> DeleteAsync(BondTable model)
        {
            
            Value_Return vr = new Value_Return();
            using (var t = _db.BeginTransaction())
            {
                try
                {
                    donds_document_bond ddb = new donds_document_bond(_db);

                     ddb.Master_ChickAsyncForDelete(model);

                    var id_ = await _db.DeleteAsync(model);
                    t.Commit();
                    vr.success = true;
                    vr.message = messageApp.txt_message[1];

                }
                catch (ApplicationException e)
                {
                    t.Rollback();
                    vr.success = false;
                    vr.message = e.Message;
                }
            }
            return vr;
        }

        public BondViewModel getOne(long id)
        {

            var model = _db.BondTables.Where(t => t.Id == id).
                Select(t => new BondViewModel
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Createat = t.Createat,
                    Date = t.Date,
                    Hand = t.Hand,
                    IdAccount = t.IdAccount,
                    IdBank = t.IdBank,
                    IdBondPay = t.IdBondPay,
                    IdCurrancy = t.IdCurrancy,
                    IdReception = t.IdReception,
                    LocPay = t.LocPay,
                    IdItemExpenses = t.IdItemExpenses,
                    IsDonePay = t.IsDonePay,
                    Note = t.Note,
                    NumCard = t.NumCard,
                    NumCheck = t.NumCheck,
                    NumReference = t.NumReference,
                    Time = t.Time,
                    Type = t.Type,
                    TypePay = t.TypePay,
                    Why = t.Why,
                    WorthyDate = t.WorthyDate,

                    


                }).FirstOrDefault();

            return model;

        }


    }
}
