using DataModels;
using HotelSys.Accounting_Layer.bords;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class BondService
    {
        private readonly HotelAlkheerDB _db;

        public BondService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public async Task<Value_Return> CreateAsync(BondViewModel model)
        {
            bond_model bm = new bond_model(_db);



            BondTable model1 = new BondTable { 
            Id=0,
            Amount= model.Amount,
            Createat=DateTime.Now,
            Date=model.Date,
            IdAccount=model.IdAccount,
            Time=model.Time,
            Hand=model.Hand,
            IdBank= model.IdBank,
            Type= model.Type,
            IdBondPay= model.IdBondPay,
            IdCurrancy= model.IdCurrancy,
            IdItemExpenses= model.IdItemExpenses,
            IdReception= model.IdReception,
            IsDonePay= model.IsDonePay,
            LocPay= model.LocPay,
            Note=model.Note,
            NumCard= model.NumCard,
            NumCheck= model.NumCheck,
            NumReference= model.NumReference,
            TypePay= model.TypePay,
            Why= model.Why,
            WorthyDate= model.WorthyDate
            
            };
            int idAccountTo = model.id_accountTo;
            int idAccountFrom = model.id_accountForm;
            Value_Return vr = new Value_Return();

            vr= bm.addAsync(model1, idAccountFrom, idAccountTo);

            return vr;

        }

        public async Task<Value_Return> update(BondViewModel model)
        {
            bond_model bm = new bond_model(_db);

            BondTable model1 = new BondTable
            {
                Id = model.Id,
                Amount = model.Amount,
                Createat = DateTime.Now,
                Date = model.Date,
                IdAccount = model.IdAccount,
                Time = model.Time,
                Hand = model.Hand,
                IdBank = model.IdBank,
                Type = model.Type,
                IdBondPay = model.IdBondPay,
                IdCurrancy = model.IdCurrancy,
                IdItemExpenses = model.IdItemExpenses,
                IdReception = model.IdReception,
                IsDonePay = model.IsDonePay,
                LocPay = model.LocPay,
                Note = model.Note,
                NumCard = model.NumCard,
                NumCheck = model.NumCheck,
                NumReference = model.NumReference,
                TypePay = model.TypePay,
                Why = model.Why,
                WorthyDate = model.WorthyDate,
                

            };
            int idAccountTo = model.id_accountTo;
            int idAccountFrom = model.id_accountForm;
            Value_Return vr = new Value_Return();

            vr = await bm.UpdateAsync(model1, idAccountFrom, idAccountTo);

            return vr;

        }


        public async Task<Value_Return> DeleteAsync(long id)
        {
            var model = _db.BondTables.Where(xx => xx.Id == id).FirstOrDefault();
            bond_model bm = new bond_model(_db);
            Value_Return vr = new Value_Return();

            if (model != null)
            {
                

                vr = await bm.DeleteAsync(model);

                return vr;
            }
            else
            {
                vr.success = false;
                vr.message ="البيانات غير موجودة";
                return vr;

            }

        }

            public BondViewModel getOne(long id)
        {
            var model = _db.BondTables.Where(xx=>xx.Id==id).
                Select(y=>new BondViewModel 
                { 
                    Amount=y.Amount,
                    Createat=y.Createat,
                    Date=y.Date,
                    Hand    = y.Hand,
                    Id=y.Id,
                    IdAccount=y.IdAccount,
                    IdBank=y.IdBank,
                    IdBondPay=y.IdBondPay,  
                    IdCurrancy=y.IdCurrancy,
                    IdItemExpenses=y.IdItemExpenses,
                    IdReception=y.IdReception,
                    IsDonePay=y.IsDonePay,
                    LocPay=y.LocPay,
                    Note=y.Note,
                    NumCard=y.NumCard,
                    NumCheck=y.NumCheck,
                    NumReference=y.NumReference,
                    Time=y.Time,
                    Type=y.Type,
                    TypePay=y.TypePay,
                    Why=y.Why,
                    WorthyDate=y.WorthyDate,
                    
                    
                    
                   
                    //customerOrCompany=
                
                
                } ).
                
                FirstOrDefault();

            if (model==null)
            {
                return new BondViewModel();
            }

            var rs = _db.EntriesAccTables.Where(x => x.IdDocumentDand == model.Id).ToList();

            model.id_accountForm = rs.Where(x => x.DebtOrCredit == '-').FirstOrDefault().IdAccount;
            model.id_accountTo = rs.Where(x => x.DebtOrCredit == '+').FirstOrDefault().IdAccount;

            return model;



        }

        public async Task<ResultBond> ListDT(JqueryDatatableParam param, string type)
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;

            int totalRecords = 0;


            var Parts = new List<BondViewModel>();


            Parts = _db.BondTables.
                Where(x => x.Type == type ).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
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
                    TypePay =   t.TypePay =="1" ? "نقدا" : t.TypePay =="2" ? "كمبياله" : t.TypePay == "3" ? "تحويل بنكي":"",
                    Why = t.Why,
                    WorthyDate = t.WorthyDate,
                    nameCustomer = t.Fkbondaccount.Name,

                    datewithTime= t.Date.ToString("d/M/yyyy") +" " +t.Time.Value.ToString()


                }).

              ToList();



            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = _db.BondTables.
                Where(x => x.Type == type)
               .Where(x => x.Fkbondaccount.Name.Contains(searchText) || x.Amount.ToString().Contains(searchText)
               || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText)).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
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
                     nameCustomer = t.Fkbondaccount.Name,
                     datewithTime = t.Date.ToString("d/M/yyyy") + " " + t.Time.Value.ToString()


                 }).
              ToList();


                totalRecords = _db.BondTables.
                Where(x => x.Type == type).Where(x => x.Fkbondaccount.Name.Contains(searchText) || x.Amount.ToString().Contains(searchText)
              || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText))


                .Count();

            }


            else
            {
                totalRecords = _db.BondTables.
               Where(x => x.Type == type).Count();
            }



            ResultBond model = new ResultBond
            {
                list = Parts,
                countRow = totalRecords
            };

            return model;



        }


        public async Task<ResultBond> ListExWithItem(JqueryDatatableParam param, string type="2")
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;

            int totalRecords = 0;


            var Parts = new List<BondViewModel>();


            Parts = _db.BondTables.
                Where(x => x.Type == type && x.IdItemExpenses !=null).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
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
                    TypePay = t.TypePay == "1" ? "نقدا" : t.TypePay == "2" ? "كمبياله" : t.TypePay == "3" ? "تحويل بنكي" : "",
                    Why = t.Why,
                    WorthyDate = t.WorthyDate,
                    nameCustomer = t.Fkbondaccount.Name,

                    datewithTime = t.Date.ToString("d/M/yyyy") + " " + t.Time.Value.ToString()
                }).

              ToList();



            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = _db.BondTables.
                Where(x => x.Type == type)
               .Where(x => x.Fkbondaccount.Name.Contains(searchText) || x.Amount.ToString().Contains(searchText)
               || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText)).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
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
                     nameCustomer = t.Fkbondaccount.Name,
                     datewithTime = t.Date.ToString("d/M/yyyy") + " " + t.Time.Value.ToString()

                 }).
              ToList();


                totalRecords = _db.BondTables.
                Where(x => x.Type == type).Where(x => x.Fkbondaccount.Name.Contains(searchText) || x.Amount.ToString().Contains(searchText)
              || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText))


                .Count();

            }


            else
            {
                totalRecords = _db.BondTables.
               Where(x => x.Type == type).Count();
            }



            ResultBond model = new ResultBond
            {
                list = Parts,
                countRow = totalRecords
            };

            return model;



        }




    }
}
