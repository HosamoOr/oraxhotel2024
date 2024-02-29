/*

using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace ElectricityWPF2
{
    public class person_model
    {
        public async Task<bool> add(APersonTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            bool state = false;
            AAccountTable ac = await (from a in Db.AAccountTables
                                     join p in Db.APersonTables
                                     on a.Id equals p.IdAccount
                                     select a).FirstOrDefaultAsync();
            int st = 0;
            if (ac != null && !ac.IsPrivate)
            {
                st = await Db.InsertOrReplaceAsync(model);
                if (st > 0)
                {
                    state = true;
                }
                else
                {
                    state = false;
                }
            }
            else
            { state = false; }
            return state;
        }
        public async Task<bool> update(APersonTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            bool state = false;
            int st = 0;

            st = await Db.UpdateAsync(model);
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
        public async Task<bool> delete(APersonTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            bool state = false;
            int st = 0;

            st = await Db.DeleteAsync(model);
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

    }
}
*/