/*

using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using ElectricityWPF2.Helper;

namespace ElectricityWPF2
{
    public class account_model
    {

        public async Task<Value_Return> AddMy()
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            Value_Return vr = new Value_Return();
            using (var t = Db.BeginTransaction())
            {
                try
                {
                    var listsub = Db.GetTable<SubClientTable>().ToList();
                    //string idg = "1210";
                    for(int i=0;i<listsub.Count;i++)
                    {

                        string id = "1210" + listsub[i].ID;
                        int IDD = int.Parse(id);
                        AAccountTable item = new AAccountTable
                        {
                            Id = IDD,
                            
                            Name=listsub[i].ActivityName,
                            DateEnter=DateTime.Now,
                            
                           IdGroupAccount= 121,
                           IsEnable=true,
                           IsPrivate=false



                        };
                        vr.count_row = await Db.InsertAsync(item);
                        t.Commit();
                        listsub[i].IdAccount = IDD;
                        Db.Update(listsub[i]);
                        t.Commit();

                        vr.success = true;
                        vr.message = messageApp.txt_message[1];
                    }
                  

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
            public async Task<Value_Return> addAsync(AAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            Value_Return vr = new Value_Return();
            using (var t = Db.BeginTransaction())
            {
                try
                {
                    model.IsEnable = true;
                    model.IsPrivate = true;
                    vr.count_row = await Db.InsertAsync(model);
                    t.Commit();
                    vr.success = true;
                    vr.message = messageApp.txt_message[1];
                   
                }
                catch (ApplicationException e)
                {
                    t.Rollback();
                    vr.success = false;
                    vr.message =  e.Message;
                }
            }
            return vr;
        }

        public async Task<Value_Return> updateAsync(AAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            Value_Return vr = new Value_Return();
            using (var t = Db.BeginTransaction())
            {
                try
                {
                    
                    if (!model.IsPrivate)
                    {
                        vr.count_row = await Db.UpdateAsync(model);
                        t.Commit();
                        vr.success = true;
                        vr.message = messageApp.txt_message[1];
                    }
                    
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
        public async Task<Value_Return> deleteAsync(AAccountTable model)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            Value_Return vr = new Value_Return();
            using (var t = Db.BeginTransaction())
            {
                try
                {

                    if (!model.IsPrivate)
                    {
                        vr.count_row = await Db.DeleteAsync(model);
                        t.Commit();
                        vr.success = true;
                        vr.message = messageApp.txt_message[1];
                    }
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

        //***************
        public List<AAccountTable> get_accouts_by_group(int id_gourp_account)
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            var gr = Db.GetTable<AAccountTable>().Where(aa => aa.IdGroupAccount == id_gourp_account).ToList();
            return gr;
        }

        public List<AAccountTable> get_accouts()
        {
            ElectricityDBDB Db = new ElectricityDBDB();
            var ac = Db.GetTable<AAccountTable>().Where(x=>x.IsEnable==true).ToList();
            return ac;
        }

    }
}
*/