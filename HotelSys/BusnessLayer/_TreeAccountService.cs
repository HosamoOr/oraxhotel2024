using DataModels;
using HotelSys.Accounting_Layer;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class _TreeAccountService
    {
        private readonly HotelAlkheerDB _db;

        public _TreeAccountService(HotelAlkheerDB context)
        {
            _db = context;
        }
      

       private int InserErrorAcc(AccountTable model)
        {
            int ida = model.Id;
            ++ida;
            model.Id = ida;
            try
            {
                _db.Insert(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                InserErrorAcc(model);

            }
            return 0;

        }

        private int InserErrorGroup(GroupAccountTable model)
        {
            int ida = model.Id;
            ++ida;
            model.Id = ida;
            try
            {
                _db.Insert(model);
                return model.Id;
            }
            catch (Exception ex)
            {
                InserErrorGroup(model);

            }
            return 0;

        }

        public async Task<int> AddOrEditAccTreeAsync(TreeAccountViewModel model)
        {
            int idAc=Convert.ToInt32(model.Id);
            int idGroup = Convert.ToInt32(model.IdGroup);

            


            if (idAc==0)
            {
                var m = _db.AccountTables.Where(x => x.IdGroup == idGroup).
                   OrderByDescending(x=>x.Id).FirstOrDefault();

                int max = 1;
                String lastId = "0";
                if (m!= null)
                {
                   var  maxID =  m.Id.ToString();

                    lastId = maxID.Substring(model.IdGroup.Length );
                }



                int idm = 0;
                if(lastId !="")
                {
                    idm = Convert.ToInt32(lastId);
                }
                    
                ++idm;
                String padding = "";

                if (idm >= 0 && idm <= 9)
                {
                    padding = "000";

                }
                else if (idm >= 10 && idm <= 99)
                {
                    padding = "00";
                }
                //else if (idm >= 100 && idm <= 999)
                //{
                //    padding = "000";
                //}
                //else if (idm >= 1000 && idm <= 9999)
                //{
                //    padding = "0000";
                //}
                //else if (idm >= 10000 && idm <= 99999)
                //{
                //    padding = "00000";
                //}
                //else if (idm >= 100000 && idm <= 999999)
                //{
                //    padding = "000000";
                //}
                //else
                //{
                //    padding = "000000000";

                //}
                String tempId = model.IdGroup + padding+ idm.ToString();
                int ida = Convert.ToInt32(tempId);


                AccountTable at = new AccountTable
                {
                    Id = ida,
                    Name = model.Name,
                    IdGroup = idGroup,
                    Status = Status_Account.active,
                    Createat = DateTime.Now,
                    IsPrivate = false,
                    Code = idm

                };

                try
                {
                    _db.Insert(at);
                    return at.Id;
                }
                catch (Exception ex)
                {
                    int st = InserErrorAcc(at);
                    return st;
                }

            }
            else
            {
                int st = 0;
                var m = _db.AccountTables.Find(idAc);
                

                if (m != null)
                {
                    if (m.IsPrivate ==false)
                    {
                        m.Name = model.Name;

                        m.IdGroup = idGroup;

                        m.Status = model.Status;
                        //m.Createat = DateTime.Now;

                        st = await _db.UpdateAsync(m);
                    }
                    else
                    {
                        return -1; //error الحساب خاص 
                    }

                        

                }


                return st;
            }


            return 0;

        }


        public async Task<int> AddOrEditGroupTreeAsync(TreeAccountViewModel model)
        {
            int idgroup = int.Parse(model.Id);
            int idMainGroup = Convert.ToInt32(model.IdGroup);
            if (idgroup == 0)
            {
                var m = _db.GroupAccountTables.Where(x => x.IdMainGroup == idMainGroup).ToArray();

                int max = 1;
                if (m.Length > 0)
                {
                    String lastId = "0";
                    var maxID = m.Max(x => x.Id).ToString();

                    lastId = maxID.Substring(model.IdGroup.Length);

                  
                    int idm = 0;

                    if(lastId !=""  )
                    {
                        idm = Convert.ToInt32(lastId);
                    }
                        
                        
                    ++idm;
                    String padding = "";

                    if (idm >= 0 && idm <= 9)
                    {
                        padding = "000";

                    }
                    else if (idm >= 10 && idm <= 99)
                    {
                        padding = "00";
                    }


                    String tempId = model.IdGroup + padding+ idm.ToString();
                    int ida = Convert.ToInt32(tempId);

                    GroupAccountTable at = new GroupAccountTable
                    {
                        Id = ida,
                        Name = model.Name,

                        IdMainGroup = idMainGroup,
                        IsRoot=false,

                        IsPrivate = false,
                       
                    };

                    try
                    {
                        _db.Insert(at);
                        return at.Id;
                    }
                    catch (Exception ex)
                    {
                        int st = InserErrorGroup(at);
                        return st;
                    }

                }

            }
            else
            {
                int st = 0;
                var m = _db.GroupAccountTables.Find(idgroup);
                if (m != null)
                {
                    if (m.IsPrivate == false)
                    {
                        m.Name = model.Name;

                        m.IdMainGroup = idMainGroup;

                        //m.Createat = DateTime.Now;

                        st = await _db.UpdateAsync(m);
                    }
                    else
                    {
                        return -1; //error لا يمكن التعديل المجموعه خاص 
                    }


                }


                return st;
            }


            return 0;

        }



        public async Task<int> deleteAccTree(int  idd)
        {
            int st = 0;
            var m = _db.AccountTables.Where(x => x.Id == idd).FirstOrDefault();

            if (m != null && m.IsPrivate !=true)
            {
                try
                {
                    st = _db.Delete(m);
                }
                catch
                {
                    return -1;
                }
            }

            return st;

        }
        public async Task<int> deleteGroupTree(int idd)
        {
            int st = 0;
            var m = _db.GroupAccountTables.Where(x => x.Id == idd).FirstOrDefault();

            if (m != null && m.IsPrivate != true)
            {
                try
                {
                    st = _db.Delete(m);
                }
                catch
                {
                    return -1;
                }
            }

            return st;

        }

    }
}
