using DataModels;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.Accounting_Layer
{
    public class ProductService
    {
        private readonly HotelAlkheerDB _db;

        public ProductService(HotelAlkheerDB context)
        {
            _db = context;
        }
        //DataTable  
        public async Task<List<ProductViewModel>> ListDT(JqueryDatatableParam param,int ? id)
        {

            var searchText = param.sSearch;

            var limit = param.iDisplayLength;
            var offset = param.iDisplayStart;


            var Parts = _db.ProductTables.
                Where(y=>y.IdGroup== id).

               OrderByDescending(x => x.Id).
                                 Skip(offset).
                                  Take(limit).

                                  Select(xx=>new ProductViewModel
                                  { 
                                      Id=xx.Id,
                                      Name=xx.Name,
                                      NameEn=xx.NameEn,
                                      Price=xx.Price,
                                      IdGroup=xx.IdGroup,
                                      IdTaxGroup=xx.IdTaxGroup,
                                      nameTaxGroup=xx.Producttabletaxgrouptable.Name,
                                      tax_rate=xx.Producttabletaxgrouptable.Rate,
                                      tax_price= xx.Producttabletaxgrouptable.Rate ==null? 0: xx.Price * xx.Producttabletaxgrouptable.Rate  /100,
                                      baladi_rate= xx.Producttabletaxgrouptable.BaladiRate==null?0:  xx.Producttabletaxgrouptable.IsBaladiTax ==true ? xx.Producttabletaxgrouptable.BaladiRate:0,

                                      baladi_price= xx.Producttabletaxgrouptable.BaladiRate == null ? 0 : xx.Producttabletaxgrouptable.IsBaladiTax == true ? (xx.Price * xx.Producttabletaxgrouptable.BaladiRate / 100) : 0 ,
                                      isBaladi = xx.Producttabletaxgrouptable.IsBaladiTax == true ?true:false

                                  }). ToList();


            if (!string.IsNullOrEmpty(searchText))
            {
                Parts = Parts.Where(x => x.Name.ToLower().Contains(searchText.ToLower())

                                              ).ToList();
            }

            var SerIncudeTax = _db.SettingGeneralTables.FirstOrDefault();
            if(SerIncudeTax != null)
            {
                bool isServiceIncTax = SerIncudeTax.ServicesIncludeTax;

                if(isServiceIncTax==false)
                {

                    Parts.ForEach(c => c.Price = c.Price-Convert.ToDouble( c.tax_price-c.baladi_price));

                }
            }

            

            return Parts;



        }



    }
}
