/*

using DataModels;
using ElectricityWPF2.Helper;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSys.ModelAccount.Donds_Locks
{
    class donds_locks_model
    {
        public async Task<Value_Return> addAsync(ADondsLocksTable model, List<ADondsdailyTable> _Dondsdily_accounts)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            Value_Return vr = new Value_Return();
            using (var t = Db.BeginTransaction())
            {
                try
                {
                    var id_ = await Db.InsertWithIdentityAsync(model);
                    decimal id = Convert.ToDecimal(id_);

                    await donds_document_donds_locks.Master_ChickAsync(model, _Dondsdily_accounts);
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
       
    }
}
*/