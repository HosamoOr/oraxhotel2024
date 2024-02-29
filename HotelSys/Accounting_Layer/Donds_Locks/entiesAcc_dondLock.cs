/*using DataModels;
using ElectricityWPF2.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSys.ModelAccount.Donds_Locks
{
   public class donds_document_donds_locks
    {

        public static async Task Master_ChickAsync(ADondsLocksTable model, List<ADondsdailyTable> _Dondsdily_accounts)
        {

            if (model.TypeOfDondsLocks == type_document.dond_day.name_document)
            {
                await addDondsdailyDondy_Day(model.Id, _Dondsdily_accounts);
            }
        }
        static async Task addDondsdailyDondy_Day(decimal id, List<ADondsdailyTable> _Dondsdily_accounts)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            ADondsLocksTable model = Db.GetTable<ADondsLocksTable>()
                .Where(dd => dd.Id == id && dd.TypeOfDondsLocks== type_document.dond_day.name_document).FirstOrDefault();

                await dondsdaily_model.add_MultiDondsdaily(_Dondsdily_accounts,
                    type_document.dond_day.id_document, model.Id, model.IdCurrancy);
        }
        }
}

*/