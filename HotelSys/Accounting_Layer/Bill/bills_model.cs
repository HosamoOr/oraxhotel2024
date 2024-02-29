using DataModels;

using HotelSys.Accounting_Layer;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSys.ModelAccount.Bill;
using HotelSys.ViewModel;
using System.Data.Entity;
//using HotelSys.Models;

namespace HotelSys.Accounting_Layer.Bill
{

    public class bills_Service
    {
        private readonly HotelAlkheerDB _db;

        public bills_Service(HotelAlkheerDB context)
        {
            _db = context;
        }


        public Value_Return addReceptionAsync(
          BillReceptionViewModel billViewModel)
        {

            Value_Return vr = new Value_Return();

            long IdRec=Convert.ToInt64(billViewModel.IdReception);

            BillsTable model = new BillsTable
            {
               // Id = billViewModel.Id,
                Createat = DateTime.Now,
                CustomerOrCompany = billViewModel.CustomerOrCompany,
                Date = billViewModel.Date,
                DeserveAmount = billViewModel.DeserveAmount,
                IdAccount = billViewModel.IdAccount,

                IdCurrancy = billViewModel.IdCurrancy,
                IdReception = IdRec,// ,
                IsForRoom = true,
                Note = billViewModel.Note,

                //NumReference = billViewModel.NumReference,

                QtyDiscount = billViewModel.QtyDiscount,

                Total = billViewModel.Total,
                Type = billViewModel.Type,
                TypeDiscount = billViewModel.TypeDiscount,
                TypePay = billViewModel.TypePay,
                IncludeTax = billViewModel.IncludeTax,
                TotalTaxPrice = billViewModel.TotalTaxPrice,
                TotalTaxRate = billViewModel.TotalTaxRate,
                IsBaladiTax = billViewModel.IsBaladiTax,
                TotalBaladiTaxPrice = billViewModel.TotalBaladiTaxPrice,
                TotalBaladiTaxRate = billViewModel.TotalBaladiTaxRate
                


            };

            var id_ = _db.InsertWithIdentity(model);
            model.Id = Convert.ToInt64(id_);

            vr.id_long = model.Id;





            return vr;
        }

        // public bool updateForLogoutAsync(
        //long id,)
        // {

        // }


        public bool updateReceptionAsync(
    BillReceptionViewModel billViewModel)
        {

            var model = _db.BillsTables.Where(x => x.Id == billViewModel.Id).FirstOrDefault();

            

            if (model != null)
            {
               
                    //  model.Createat = DateTime.Now;
                    model.CustomerOrCompany = billViewModel.CustomerOrCompany;

                model.IdAccount = billViewModel.IdAccount;

                model.IdCurrancy = billViewModel.IdCurrancy;

                model.Note = billViewModel.Note;

                //NumReference = billViewModel.NumReference,

                model.QtyDiscount = billViewModel.QtyDiscount;
                model.DeserveAmount = billViewModel.DeserveAmount;

                model.Total = billViewModel.Total;
                // model.Type = billViewModel.Type;
                model.TypeDiscount = billViewModel.TypeDiscount;
                //TypePay = billViewModel.TypePay

                var st = _db.Update(model);
                if (st > 0)
                    return true;
                else
                    return false;


            }


            return false;
        }
        //مع تسجيل الدخول فيها قيود
        public bool updateReceptionLoginAsync(
  BillReceptionViewModel billViewModel)
        {

            var model = _db.BillsTables.Where(x => x.Id == billViewModel.Id).FirstOrDefault();

            if (model != null)
            {

                //  model.Createat = DateTime.Now;
                model.CustomerOrCompany = billViewModel.CustomerOrCompany;

                model.IdAccount = billViewModel.IdAccount;

                model.IdCurrancy = billViewModel.IdCurrancy;

                model.Note = billViewModel.Note;

                //NumReference = billViewModel.NumReference,

                model.QtyDiscount = billViewModel.QtyDiscount;
                model.DeserveAmount = billViewModel.DeserveAmount;

                model.Total = billViewModel.Total;
                // model.Type = billViewModel.Type;
                model.TypeDiscount = billViewModel.TypeDiscount;
                //TypePay = billViewModel.TypePay

                int id_accountForm = model.IdAccount;

                int id_accountTo = Static_Accounts.IncomeRent;

                //item null
                List<DetialsBillsTable> item = new List<DetialsBillsTable>();


                var st = UpdateAsync(model, id_accountForm, id_accountTo, item);
                //if (st > 0)
                    return true;
                //else
                //    return false;


            }


            return false;
        }

        public async Task<bool> EnterRoomBillAsync(
      long id)
        {

            var model = _db.BillsTables.Where(x => x.Id == id).FirstOrDefault();


            if (model != null)
            {

                model.Type = type_document.rent.id_document;
                model.Date = DateTime.Now;


                _db.Update(model);

                int id_accountForm = model.IdAccount;

                int id_accountTo = Static_Accounts.IncomeRent;

                EnteisAcc_bills_document eb = new EnteisAcc_bills_document(_db);


                var cu = _db.MyCustomers.Include("Mycustomerscustomertable").Where(x => x.IdAccount == model.IdAccount)
                    .
                    Select(c => new CustomerViewModel
                    {
                        Name = c.Mycustomerscustomertable.Name,

                    }).

                    FirstOrDefault();




                await eb.Master_ChickForAdd(model, id_accountForm, id_accountTo, cu.Name);
            }


            return false;
        }




        public async Task<Value_Return> addAsync(
            BillViewModel billViewModel
            )
        {

            Value_Return vr = new Value_Return();
            using (var t = _db.BeginTransaction())
            {
                try
                {


                    int id_accountForm = billViewModel.id_accountForm;
                    int id_accountTo = billViewModel.id_accountTo;


                    List<DetialsBillsTable> items = new List<DetialsBillsTable>();

                    if (billViewModel.IdReception == 0)
                    {
                        billViewModel.IdReception = null;
                        billViewModel.IsForRoom = false;

                    }


                    BillsTable model = new BillsTable
                    {
                        Id = billViewModel.Id,
                        Createat = DateTime.Now,
                        CustomerOrCompany = billViewModel.CustomerOrCompany,
                        Date = billViewModel.Date,
                        DeserveAmount = billViewModel.DeserveAmount,
                        IdAccount = billViewModel.IdAccount,
                        IdBank = billViewModel.IdBank,
                        IdCurrancy = billViewModel.IdCurrancy,
                        IdReception = billViewModel.IdReception,
                        IsForRoom = billViewModel.IsForRoom,
                        Note = billViewModel.Note,
                        NumCard = billViewModel.NumCard,
                        NumCheck = billViewModel.NumCheck,
                        NumReference = billViewModel.NumReference,
                        PayAmount = billViewModel.PayAmount,
                        QtyDiscount = billViewModel.QtyDiscount,
                        RestAmount = billViewModel.RestAmount,
                        Total = billViewModel.Total,
                        Type = billViewModel.Type,
                        TypeDiscount = billViewModel.TypeDiscount,
                        TypePay = billViewModel.TypePay,


                        IncludeTax = billViewModel.IncludeTax,
                        TotalTaxPrice = billViewModel.TotalTaxPrice,
                        TotalTaxRate = billViewModel.TotalTaxRate,
                        IsBaladiTax = billViewModel.IsBaladiTax,
                        TotalBaladiTaxPrice = billViewModel.TotalBaladiTaxPrice,
                        TotalBaladiTaxRate = billViewModel.TotalBaladiTaxRate,


                    };

                    var id_ = _db.InsertWithIdentity(model);
                    model.Id = Convert.ToInt64(id_);

                    vr.id_long = model.Id;

                    for (int i = 0; i < billViewModel.Items.Count; i++)
                    {
                        DetialsBillsTable temp = new DetialsBillsTable
                        {
                            Id = 0,
                            IdProduct = billViewModel.Items[i].IdProduct,
                            PriceOne = billViewModel.Items[i].PriceOne,
                            Qty = billViewModel.Items[i].Qty,
                            Total = billViewModel.Items[i].Total,
                            IdBill = model.Id,
                            TaxPrice = billViewModel.Items[i].TaxPrice,
                            TaxRate = billViewModel.Items[i].TaxRate,
                            IsBaladiTax = billViewModel.Items[i].IsBaladiTax,
                            BaladiTaxPrice = billViewModel.Items[i].BaladiTaxPrice,
                            BaladiTaxRate = billViewModel.Items[i].BaladiTaxRate,

                        };
                        _db.InsertWithIdentity(temp);

                    }




                    EnteisAcc_bills_document eb = new EnteisAcc_bills_document(_db);


                    var cu = _db.MyCustomers.Where(x => x.IdAccount == billViewModel.IdAccount).Select(x => x.Mycustomerscustomertable.Name)

                        .FirstOrDefault();

                    await eb.Master_ChickForAdd(model, id_accountForm, id_accountTo, cu);

                    vr.success = true;
                    vr.message = messageApp.txt_message[1];


                    t.Commit();
                    // return vr;
                }
                catch (ApplicationException e)
                {
                    await t.RollbackAsync();
                    vr.success = false;
                    vr.id_long = billViewModel.Id;
                    vr.message = messageApp.txt_message[0];
                }
            }

            return vr;
        }

        public async Task<Value_Return> UpdateInvService(BillViewModel model)
        {
            var tab = _db.BillsTables.Where(x => x.Id == model.Id).FirstOrDefault();
            Value_Return vr = new Value_Return();

            if (tab != null)
            {
                tab.DeserveAmount = model.DeserveAmount;
                //tab.IsForRoom = model.IsForRoom;
                tab.IdBank = model.IdBank;
                tab.IdCurrancy = model.IdCurrancy;
               // tab.IdReception = model.IdReception;
              // tab.IncludeTax = model.IncludeTax;
              //tab.IsBaladiTax = model.IsBaladiTax;
              tab.Note = model.Note;
                tab.NumCard = model.NumCard;
                tab.NumCheck = model.NumCheck;
                tab.NumReference = model.NumReference;
                tab.PayAmount = model.PayAmount;
                tab.QtyDiscount = model.QtyDiscount;
                tab.RestAmount = model.RestAmount;
                tab.Total=model.Total;
                tab.TotalBaladiTaxPrice = model.TotalBaladiTaxPrice;
                tab.TotalBaladiTaxRate = model.TotalBaladiTaxRate;
                tab.TotalTaxPrice = model.TotalTaxPrice;
                tab.TotalTaxRate = model.TotalTaxRate;
               // tab.Type = model.Type;
                tab.TypeDiscount = model.TypeDiscount;
                tab.TypePay=model.TypePay;


                List<DetialsBillsTable> items = new List<DetialsBillsTable>();

                for(int i=0;i< model.Items.Count();i++)
                {
                    DetialsBillsTable temp = new DetialsBillsTable
                    {
                        Id = model.Items[i].Id,
                        BaladiTaxPrice = model.Items[i].BaladiTaxPrice,
                        BaladiTaxRate = model.Items[i].BaladiTaxRate,
                        IdBill = tab.Id,
                        IdProduct = model.Items[i].IdProduct,
                        IsBaladiTax = model.Items[i].IsBaladiTax,
                        PriceOne = model.Items[i].PriceOne,
                        Qty = model.Items[i].Qty,
                        TaxPrice = model.Items[i].TaxPrice,
                        TaxRate = model.Items[i].TaxRate,
                        Total = model.Items[i].Total,

                    };
                    items.Add(temp);

                }

                vr=await UpdateAsync(tab, model.id_accountForm, model.id_accountTo, items);

                return vr;
            }
            return vr;
        }



            public async Task<Value_Return> UpdateAsync(BillsTable model, int id_accountForm, int id_accountTo
            , List<DetialsBillsTable> items
            )
        {

            Value_Return vr = new Value_Return();

            using (var t = _db.BeginTransaction())
            {
                try
                {

                    var id_ = model.Id;

                    var st = _db.Update(model);

                    EnteisAcc_bills_document eb = new EnteisAcc_bills_document(_db);

                    var cu = _db.AccountTables.Where(x => x.Id == model.IdAccount).FirstOrDefault().Name;

                //    var cu = _db.MyCustomers.Where(x => x.IdAccount == model.IdAccount).FirstOrDefault().Mycustomerscustomertable.Name;


                    await eb.Master_ChickAsyncForUpdate(model, id_accountForm, id_accountTo, cu);

                    //Item Del And Add
                    var ItemsDel = _db.DetialsBillsTables.
                    Where(x => x.IdBill == model.Id).ToList();
                    for (int i = 0; i < ItemsDel.Count(); i++)
                        _db.Delete(ItemsDel[i]);

                    for (int i = 0; i < items.Count; i++)
                    {
                        _db.InsertWithIdentity(items[i]);
                    }




                    t.Commit();
                    vr.success = true;
                    vr.message = messageApp.txt_message[2];

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

        public BillViewModel getOne(long id)
        {
            BillViewModel model = _db.BillsTables.Where(x => x.Id == id).
                Select(xx => new BillViewModel
                {

                    Id = xx.Id,
                    Createat = xx.Createat,
                    CustomerOrCompany = xx.CustomerOrCompany,
                    Date = xx.Date,
                    DeserveAmount = xx.DeserveAmount,
                    IdAccount = xx.IdAccount,
                    IdBank = xx.IdBank,
                    IdCurrancy = xx.IdCurrancy,
                    IdReception = xx.IdReception,
                    IncludeTax = xx.IncludeTax ==null ? false : Convert.ToBoolean( xx.IncludeTax),
                    IsBaladiTax = Convert.ToBoolean(xx.IsBaladiTax),
                    IsForRoom = Convert.ToBoolean(xx.IsForRoom),
                    Note = xx.Note,
                    NumCard = xx.NumCard,
                    NumCheck = xx.NumCheck,
                    NumReference = xx.NumReference,
                    PayAmount = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(xx.PayAmount))),
                    QtyDiscount = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(xx.QtyDiscount))),
                    RestAmount = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(xx.RestAmount))),
                    Total = Convert.ToDouble(String.Format("{0:0.##}", xx.Total)),
                    TotalBaladiTaxPrice = Convert.ToDouble(String.Format("{0:0.##}", xx.TotalBaladiTaxPrice)),
                    TotalBaladiTaxRate = Convert.ToDouble(String.Format("{0:0.##}", xx.TotalBaladiTaxRate)),
                    TotalTaxPrice = Convert.ToDouble(String.Format("{0:0.##}", xx.TotalTaxPrice)),
                    TotalTaxRate = Convert.ToDouble(String.Format("{0:0.##}", xx.TotalTaxRate)),
                    Type = xx.Type,
                    TypeDiscount = xx.TypeDiscount,
                    TypePay = xx.TypePay,

                    nameCustomer = xx.Fkbillsaccount.Name,
                    room = xx.IsForRoom == true ? xx.Fkbillsreception.Recetiontableroomstable.NameR : "سند خدمات خارجي",
                    TotalBeforVAT = Convert.ToDouble(String.Format("{0:0.##}", xx.DeserveAmount)) -
                    Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(xx.TotalTaxPrice))) - Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(xx.TotalBaladiTaxPrice))),
                    
                    



                    Items = xx.Fkbillsdetialsbills.Select(y => new DetialsBillsViewModel
                    {
                        BaladiTaxPrice = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.BaladiTaxPrice))),
                        BaladiTaxRate = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.BaladiTaxRate))),
                        Id = y.Id,
                        IdBill = y.IdBill,
                        IdProduct = y.IdProduct,
                        IsBaladiTax = y.IsBaladiTax,
                        PriceOne = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.PriceOne))),
                        Qty = y.Qty,
                        TaxPrice = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.TaxPrice))),
                        TaxRate = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.TaxRate))),
                        Total = Convert.ToDouble(String.Format("{0:0.##}", Convert.ToDouble(y.Total))),

                        NameProduct = y.Fkproductservicesdetial.Name,
                        isSelect=true

                    }).ToList()



                }).
                FirstOrDefault();


            var rs = _db.EntriesAccTables.Where(x => x.IdDocumentBill == model.Id).ToList();

            model.id_accountForm = rs.Where(x => x.DebtOrCredit == '-').FirstOrDefault().IdAccount;
            model.id_accountTo = rs.Where(x => x.DebtOrCredit == '+').FirstOrDefault().IdAccount;

            return model;
        }





        public async Task<Value_Return> DeleteAsync(long id)
        {

            var model = _db.BillsTables.FirstOrDefault(x => x.Id == id);
            Value_Return vr = new Value_Return();

            if (model != null)
            {
                using (var t = _db.BeginTransaction())
                {
                    try
                    {
                        EnteisAcc_bills_document eb = new EnteisAcc_bills_document(_db);

                        eb.Master_ChickAsyncForDelete(model);

                        var modD = _db.BillsTables.Where(xx => xx.Id == model.Id).FirstOrDefault();
                        if (modD != null)
                        {
                            var id_ = await _db.DeleteAsync(modD);
                        }


                        var ItemsDel = _db.DetialsBillsTables.
                       Where(x => x.IdBill == model.Id

                       ).ToList();

                        for (int i = 0; i < ItemsDel.Count(); i++)
                            _db.Delete(ItemsDel[i]);



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


            }

            return vr;
        }


        public async Task<ResultBills> ListDT(JqueryDatatableParam param, string type)
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;

            int totalRecords = 0;


            var Parts = new List<BillViewModel>();


            Parts = _db.BillsTables.
                Where(x => x.Type == type).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
                Select(t => new BillViewModel
                {
                    Id = t.Id,

                    Createat = t.Createat,
                    Date = t.Date,
                    IdAccount = t.IdAccount,
                    IdBank = t.IdBank,
                    IdCurrancy = t.IdCurrancy,
                    IdReception = t.IdReception,

                    Note = t.Note,
                    NumCard = t.NumCard,
                    NumCheck = t.NumCheck,
                    NumReference = t.NumReference,

                    Type = t.Type,
                    TypePay = t.TypePay == "1" ? "نقدا" : t.TypePay == "2" ? "كمبياله" : t.TypePay == "3" ? "تحويل بنكي" : "",

                    CustomerOrCompany = t.CustomerOrCompany,
                    DeserveAmount = t.DeserveAmount,
                    IncludeTax = t.IncludeTax == null ? false : Convert.ToBoolean(t.IncludeTax),
                    IsBaladiTax = t.IsBaladiTax == true ? true : false,
                    IsForRoom = t.IsForRoom == true ? true : false,
                    PayAmount = t.PayAmount,
                    QtyDiscount = t.QtyDiscount,
                    RestAmount = t.RestAmount,
                    Total = t.Total,
                    TotalBaladiTaxPrice = t.TotalBaladiTaxPrice,
                    TotalBaladiTaxRate = t.TotalBaladiTaxRate,
                    TotalTaxPrice = t.TotalTaxPrice,
                    TotalTaxRate = t.TotalTaxRate,
                    TypeDiscount = t.TypeDiscount,




                    nameCustomer = t.Fkbillsaccount.Name,
                    room = t.Fkbillsreception.Recetiontableroomstable.NameR != null ? t.Fkbillsreception.Recetiontableroomstable.NameR : "",
                    TotalBeforVAT = Convert.ToDouble(t.Total) - Convert.ToDouble((t.TotalBaladiTaxPrice - t.TotalTaxPrice)),

                    countPro=t.Fkbillsdetialsbills.Count(),


                }).

              ToList();



            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = _db.BillsTables.
                Where(x => x.Type == type)

               .Where(x => x.Fkbillsaccount.Name.Contains(searchText) || x.Total.ToString().Contains(searchText)
               || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText)).

                   OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).
                 Select(t => new BillViewModel
                 {
                     Id = t.Id,

                     Createat = t.Createat,
                     Date = t.Date,
                     IdAccount = t.IdAccount,
                     IdBank = t.IdBank,
                     IdCurrancy = t.IdCurrancy,
                     IdReception = t.IdReception,

                     Note = t.Note,
                     NumCard = t.NumCard,
                     NumCheck = t.NumCheck,
                     NumReference = t.NumReference,

                     Type = t.Type,
                     TypePay = t.TypePay == "1" ? "نقدا" : t.TypePay == "2" ? "كمبياله" : t.TypePay == "3" ? "تحويل بنكي" : "",

                     CustomerOrCompany = t.CustomerOrCompany,
                     DeserveAmount = t.DeserveAmount,
                     IncludeTax = t.IncludeTax == null ? false : Convert.ToBoolean(t.IncludeTax),
                     IsBaladiTax = t.IsBaladiTax == true ? true : false,
                     IsForRoom = t.IsForRoom == true ? true : false,
                     PayAmount = t.PayAmount,
                     QtyDiscount = t.QtyDiscount,
                     RestAmount = t.RestAmount,
                     Total = t.Total,
                     TotalBaladiTaxPrice = t.TotalBaladiTaxPrice,
                     TotalBaladiTaxRate = t.TotalBaladiTaxRate,
                     TotalTaxPrice = t.TotalTaxPrice,
                     TotalTaxRate = t.TotalTaxRate,
                     TypeDiscount = t.TypeDiscount,

                     nameCustomer = t.Fkbillsaccount.Name,
                     room = t.Fkbillsreception.Recetiontableroomstable.NameR !=null ? t.Fkbillsreception.Recetiontableroomstable.NameR : "" ,
                     TotalBeforVAT = Convert.ToDouble(t.Total) - Convert.ToDouble((t.TotalBaladiTaxPrice - t.TotalTaxPrice)),
                     countPro = t.Fkbillsdetialsbills.Count(),




                 }).
              ToList();


                totalRecords = _db.BillsTables.
                Where(x => x.Type == type)

                 .Where(x => x.Fkbillsaccount.Name.Contains(searchText) || x.Total.ToString().Contains(searchText)
               || x.Id.ToString().Contains(searchText) || x.IdReception.ToString().Contains(searchText))


                .Count();

            }


            else
            {
                totalRecords = _db.BillsTables.
               Where(x => x.Type == type).Count();
            }



            ResultBills model = new ResultBills
            {
                list = Parts,
                countRow = totalRecords
            };

            return model;



        }






    }
}
