/*using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace ElectricityWPF2
{
   public class group_account_model
    {
        public async Task<bool> addAsync(AGroupAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            bool state = false;
            model.IsRoot = false;
            model.IsPrivate = false;
            int st = await Db.InsertOrReplaceAsync(model);
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
        public async Task<bool> updateAsync(AGroupAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            bool state = false;
            int st = 0;
            if (!model.IsPrivate && !model.IsRoot)
            {
                st = await Db.UpdateAsync(model);
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
        public async Task<bool> deleteAsync(AGroupAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            //var  ss=Db.Transaction;
            //ss.Connection.BeginTransaction();

            bool state = false;
            if (!model.IsPrivate && !model.IsRoot)
            {
                int st = await Db.DeleteAsync(model);
                if (st > 0)
                {
                    state = true;
                    // ss.Commit();
                }
                else
                {
                    state = false;
                    // ss.Rollback();
                }
            }
            else
            { state = false; }
            return state;
        }


        //*********
        public AGroupAccountTable[] get_root_group_account()
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            var gr = Db.GetTable<AGroupAccountTable>().Where(gg => gg.IsRoot == true && gg.IsPrivate==false).ToList();
            return gr.ToArray();
        }

        public List<AGroupAccountTable> get_child_group_account(AGroupAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            var gr = Db.GetTable<AGroupAccountTable>().Where(gg => gg.IdGroupMain == model.Id && gg.IsRoot==false).ToList();
            return gr;
        }


    }
}
*/