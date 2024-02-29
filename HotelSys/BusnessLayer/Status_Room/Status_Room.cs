using DataModels;
using DevExpress.CodeParser;
using DevExpress.Pdf.ContentGeneration.Interop;
using DevExpress.XtraRichEdit.Model;
using HotelSys.Accounting_Layer;

using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HotelSys.BusnessLayer
{
    public class Status_RoomService
    {
        private readonly HotelAlkheerDB _db;

        public Status_RoomService(HotelAlkheerDB context)
        {
            _db = context;
        }

        public long create(RoomsTable  idRoom)
        {
            long st = -1;
            using (var t = _db.BeginTransaction())
            {
                try
                {

                    StatusCurrentTable model = new StatusCurrentTable
                    {
                        IdRoom = idRoom.Id,
                        Createat = DateTime.Now,
                        Status = Status_RoomsName.listStatus[0].index.ToString()


                    };


                    var id_ = _db.InsertWithIdentity(model);
                    int idd = Convert.ToInt32(id_);

                    DetialsStatusTable dst = new DetialsStatusTable
                    {
                        IdRoom = model.IdRoom,
                        IdEmp = model.IdEmp,
                        Status = model.Status,
                        Createat = DateTime.Now,
                        StartDate = DateTime.Now,
                        EndDate = null,
                        Detials = model.Detials,
                        IdReception = model.IdReception,
                        IdStatusBefore = null,
                        //IdSub

                    };

                     st = Convert.ToInt64(_db.InsertWithIdentity(dst));

                    t.Commit();

                       
                   

                }
                catch (ApplicationException e)
                {
                    t.Rollback();
                   
                }
            }
            
            return st;

        }



        public long EditAndAdd_(StatusCurrentTable model)
        {
            long st = -1;
            //using (var t = _db.BeginTransaction())
            //{
                try
                {
                    var Last_status_room = _db.DetialsStatusTables.
                Where(id => id.IdRoom == model.IdRoom).
                OrderByDescending(x=>x.Id).
                FirstOrDefault();

                    if (Last_status_room != null)
                    {
                        Last_status_room.EndDate = DateTime.Now;
                        _db.Update(Last_status_room);

                    }

                    int statusINT = Convert.ToInt32(model.Status);

                    if(statusINT <= 3)
                    {
                        model.StartDate=DateTime.Now;

                    }
                long? lastID = null;


                    if(Last_status_room !=null)
                {
                    lastID = Last_status_room.Id;
                }

                    DetialsStatusTable dst = new DetialsStatusTable
                    {
                        IdRoom = model.IdRoom,
                        IdEmp = model.IdEmp,
                        Status = model.Status,
                        Createat = DateTime.Now,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Detials = model.Detials,
                        IdReception = model.IdReception,
                        IdStatusBefore = lastID,
                        //IdSub

                        //IdStatusCurrent = model.Id,


                    };

                    st = Convert.ToInt64(_db.InsertWithIdentity(dst));
                //t.Commit();




            }
            catch (ApplicationException e)
            {
                //t.Rollback();

            }
        //}
                return st;

        }


        string getColorByStatus(string stringCol)
        {
            var color = "#008000";

            switch(stringCol)
            {
                case "2":
                    color = "#FFA500"; break;
                case "3":
                    color = "#808080"; break;
                case "4":
                    color = "#0000FF"; break;
                case "5":
                    color = "#FF0000"; break;

            }
            return color;
        }

        public List<Status_Current_WithBalanceViewModel> ListStatusCurrentWithBalance(int idSub)
        {
            List<Status_Current_WithBalanceViewModel> mo = _db.GetBalanceRoom(idSub).

                 Select(xx => new Status_Current_WithBalanceViewModel
                 {
                     Id = xx.id_room,
                     IdRoom = xx.id_room,
                     Status = xx.status,
                    // Createat = xx.Createat,
                   //  Detials = xx.Detials,
                     EndDate = xx.end_date,
                     StartDate = xx.start_date,
                    // IdEmp = xx.IdEmp,
                     IdReception = xx.IdReception,
                    
                     color = getColorByStatus(xx.status),
                   balance= Convert.ToDouble( xx.balance),
                   nameRoom=xx.name_room,
                   nameCuOrCo=xx.nameAccount


                 }).ToList();

            return mo;
        }


        public List<Status_Current_RoomViewModel> ListStatusCurrentB(int idSub)
        {
            List<Status_Current_RoomViewModel> mo = _db.GetBalanceRoom(idSub).

                 Select(xx => new Status_Current_RoomViewModel
                 {
                     Id = xx.id,
                     Status = xx.status,
                    // Createat = xx.Createat,
                     //Detials = xx.Detials,
                     EndDate = xx.end_date,
                     StartDate = xx.start_date,
                  //   IdEmp = xx.IdEmp,
                     IdReception = xx.IdReception,
                     IdRoom = xx.id_room,
                     color = getColorByStatus(xx.status),
                     balance=Convert.ToDouble( xx.balance),
                     nameCuOrCo=xx.nameAccount,
                     RoomModel = new RoomViewModel
                     {
                         IdR = xx.id_room,
                         NameR = xx.name_room,
                         
                         //CountBathroom = xx.Fkdetialsstatusroom.CountBathroom,
                        // NumFloor = xx.Fkdetialsstatusroom.NumFloor,
                         //CountBedDouble = xx.Fkdetialsstatusroom.CountBedDouble,
                         //CountBedSingle = xx.Fkdetialsstatusroom.CountBedSingle,
                        // CountRooms = xx.Fkdetialsstatusroom.CountRooms,
                        // CountTv = xx.Fkdetialsstatusroom.CountTv,
                        // CountWallet = xx.Fkdetialsstatusroom.CountWallet,
                         //IdHo = xx.Fkdetialsstatusroom.IdHo,
                        // Note = xx.Fkdetialsstatusroom.Note,
                         //TypeCondition = xx.Fkdetialsstatusroom.TypeCondition,
                        // PrivateFeatures = xx.Fkdetialsstatusroom.PrivateFeatures,
                        // PublicFeatures = xx.Fkdetialsstatusroom.PublicFeatures,
                         Roomstabletyperoomstable = new TypeRoomsTable
                         {
                             NameT = xx.type_room,
                             Color=xx.color

                             
                         }
                     }


                 }).ToList();


            List<Status_Current_RoomViewModel> mo2 = _db.StatusCurrentTables.
                Where(x=>x.Status=="1" || x.Status == "2"|| x.Status == "3").
                Where(y=>y.IdReception==null).

               Select(xx => new Status_Current_RoomViewModel
               {
                   Id = xx.Id,
                   Status = xx.Status,
                   //Createat = xx.Createat,
                   //Detials = xx.Detials,
                   EndDate = xx.EndDate,
                   StartDate = xx.StartDate,
                   //IdEmp = xx.IdEmp,
                   IdReception = xx.IdReception,
                   IdRoom = xx.IdRoom,
                   color = getColorByStatus(xx.Status),
                   RoomModel = new RoomViewModel
                   {
                       IdR = xx.Fkdetialsstatusroom.Id,
                       NameR = xx.Fkdetialsstatusroom.NameR,
                       //CountBathroom = xx.Fkdetialsstatusroom.CountBathroom,
                       //NumFloor = xx.Fkdetialsstatusroom.NumFloor,
                       //CountBedDouble = xx.Fkdetialsstatusroom.CountBedDouble,
                       //CountBedSingle = xx.Fkdetialsstatusroom.CountBedSingle,
                       //CountRooms = xx.Fkdetialsstatusroom.CountRooms,
                       //CountTv = xx.Fkdetialsstatusroom.CountTv,
                       //CountWallet = xx.Fkdetialsstatusroom.CountWallet,
                       //IdHo = xx.Fkdetialsstatusroom.IdHo,
                       //Note = xx.Fkdetialsstatusroom.Note,
                       //TypeCondition = xx.Fkdetialsstatusroom.TypeCondition,
                       //PrivateFeatures = xx.Fkdetialsstatusroom.PrivateFeatures,
                       //PublicFeatures = xx.Fkdetialsstatusroom.PublicFeatures,
                       Roomstabletyperoomstable = new TypeRoomsTable
                       {
                           NameT = xx.Fkdetialsstatusroom.Fkroomstyperoom.NameT,
                             Color = xx.Fkdetialsstatusroom.Fkroomstyperoom.Color,
                       }
                   }


               }).ToList();


            List<Status_Current_RoomViewModel> mo3 = _db.GetRoomStatusWittoutIn(1).
              
             Select(xx => new Status_Current_RoomViewModel
             {
                 Id = xx.id,
                 Status = xx.status,
                 // Createat = xx.Createat,
                 //Detials = xx.Detials,
                 EndDate = xx.end_date,
                 StartDate = xx.start_date,
                 //   IdEmp = xx.IdEmp,
                 IdReception = xx.IdReception,
                 IdRoom = xx.id_room,
                 color = getColorByStatus(xx.status),
                
                 nameCuOrCo = xx.nameAccount,
                 RoomModel = new RoomViewModel
                 {
                     IdR = xx.id_room,
                     NameR = xx.name_room,

                     //CountBathroom = xx.Fkdetialsstatusroom.CountBathroom,
                     // NumFloor = xx.Fkdetialsstatusroom.NumFloor,
                     //CountBedDouble = xx.Fkdetialsstatusroom.CountBedDouble,
                     //CountBedSingle = xx.Fkdetialsstatusroom.CountBedSingle,
                     // CountRooms = xx.Fkdetialsstatusroom.CountRooms,
                     // CountTv = xx.Fkdetialsstatusroom.CountTv,
                     // CountWallet = xx.Fkdetialsstatusroom.CountWallet,
                     //IdHo = xx.Fkdetialsstatusroom.IdHo,
                     // Note = xx.Fkdetialsstatusroom.Note,
                     //TypeCondition = xx.Fkdetialsstatusroom.TypeCondition,
                     // PrivateFeatures = xx.Fkdetialsstatusroom.PrivateFeatures,
                     // PublicFeatures = xx.Fkdetialsstatusroom.PublicFeatures,
                     Roomstabletyperoomstable = new TypeRoomsTable
                     {
                         NameT = xx.type_room,
                         Color =xx.color,

                     }
                 }

             }).ToList();


            /////////dele double with status 4

            var moCl = mo.Where(x => x.Status == "4").ToList();

            var mo2Cl = mo3;

            for (int i= 0; i < moCl.Count; i++)
            {
                int idr = moCl[i].IdRoom;


                for (int j = 0; j < mo2Cl.Count; j++)
                {
                    if(idr== mo2Cl[j].IdRoom)
                    {
                        mo3.Remove(mo2Cl[j]);

                    }
                    
                }

            }

           //---------------

            mo.AddRange(mo2);

            mo.AddRange(mo3);


         var ss=   mo.OrderBy(x => x.RoomModel.NameR).ToList();


            return ss;
        }


       
      public ListStatusDT ALL_between_dates(int idSub, paramModel request)
        {
            int limit = request.limit;
            int offset = request.offset;

            String searchText = request.search;
            if (!"".Equals(searchText) && searchText != null)
            {
                //queryparm.put("searchText", "%" + searchText + "%");
            }
            String sort = request.sort;
            String order = request.order;

            long iduser = request.userid;

            ListStatusDT model = new ListStatusDT();
            int cout=0;


            List<Status_Current_RoomViewModel> mo =  _db.StatusCurrentTables.
               
                //Where(y => request.startDate >= y.StartDate).
                //Where(y=> request.endDate <= y.EndDate).

               Select(xx => new Status_Current_RoomViewModel
               {
                   Id = xx.Id,
                   Status = xx.Status=="1"?"فارغة": xx.Status == "2"?"نظافة": xx.Status == "3"?"صيانة": xx.Status == "4"?"حجز مؤقت":  "مؤجر ",
                   //Createat = xx.Createat,
                   //Detials = xx.Detials,
                   EndDate = xx.EndDate,
                   StartDate = xx.StartDate,
                   //IdEmp = xx.IdEmp,
                   IdReception = xx.IdReception,
                   IdRoom = xx.IdRoom,
                   color = getColorByStatus(xx.Status),
                   RoomModel = new RoomViewModel
                   {
                       IdR = xx.Fkdetialsstatusroom.Id,
                       NameR = xx.Fkdetialsstatusroom.NameR,
                       //CountBathroom = xx.Fkdetialsstatusroom.CountBathroom,
                       //NumFloor = xx.Fkdetialsstatusroom.NumFloor,
                       //CountBedDouble = xx.Fkdetialsstatusroom.CountBedDouble,
                       //CountBedSingle = xx.Fkdetialsstatusroom.CountBedSingle,
                       //CountRooms = xx.Fkdetialsstatusroom.CountRooms,
                       //CountTv = xx.Fkdetialsstatusroom.CountTv,
                       //CountWallet = xx.Fkdetialsstatusroom.CountWallet,
                       //IdHo = xx.Fkdetialsstatusroom.IdHo,
                       //Note = xx.Fkdetialsstatusroom.Note,
                       //TypeCondition = xx.Fkdetialsstatusroom.TypeCondition,
                       //PrivateFeatures = xx.Fkdetialsstatusroom.PrivateFeatures,
                       //PublicFeatures = xx.Fkdetialsstatusroom.PublicFeatures,
                       Roomstabletyperoomstable = new TypeRoomsTable
                       {
                           NameT = xx.Fkdetialsstatusroom.Fkroomstyperoom.NameT,
                           Color = xx.Fkdetialsstatusroom.Fkroomstyperoom.Color,
                       }
                   }
               }).OrderByDescending(x => x.IdReception).
                  Skip(offset).
                 Take(limit)
            //.DistinctBy(p => p.Id)

            .ToList();
            cout = _db.StatusCurrentTables.

                Where(y => request.startDate >= y.StartDate).
                Where(y => request.endDate <= y.EndDate).Count();


            if (!string.IsNullOrEmpty(searchText))
            {
                //model = _db.StatusCurrentTables .Where(x => x..Name.ToLower().Contains(searchText.ToLower())
                //                              || (x.room.NameRoom != null && x.room.NameRoom.ToLower().Contains(searchText.ToLower()))
                //                              || (x.IdReception != 0 && x.IdReception.ToString().Contains(searchText.ToLower()))

                //                               || (x.Price != 0 && x.Price.ToString().Contains(searchText.ToLower()))

                //                                 || (x.QtyTime != 0 && x.QtyTime.ToString().Contains(searchText.ToLower()))

                //                              || x.StartDate.ToString().Contains(searchText.ToLower())

                //                               || x.EndDate.ToString().Contains(searchText.ToLower())


                //                              //|| (x.VehicleModel != null && x.Date.ToString().Contains(searchText.ToLower()))


                //                              // || x.Date.ToLower().Contains(searchText.ToLower())
                //                              ).ToList();


               // cout =


            }

            



            var ss = mo.OrderBy(x => x.RoomModel.NameR).ToList();

            model.lisy= ss;
            model.countRow = cout;


            return model;
        }


        // حالة جميع الغرف
        public List<Status_Current_RoomViewModel> ListStatusCurrent(int idSub)
        {
           List< Status_Current_RoomViewModel> mo = _db.StatusCurrentTables.

                Select(xx => new Status_Current_RoomViewModel { 
                Id=xx.Id,
                Status=xx.Status,
                Createat=xx.Createat,
                Detials=xx.Detials,
                EndDate=xx.EndDate,
                StartDate=xx.StartDate,
                IdEmp=xx.IdEmp,
                IdReception=xx.IdReception,
                IdRoom=xx.IdRoom,
                color= getColorByStatus(xx.Status),
                RoomModel =new RoomViewModel
                {
                    IdR=xx.Fkdetialsstatusroom.Id,
                    NameR= xx.Fkdetialsstatusroom.NameR,
                    CountBathroom= xx.Fkdetialsstatusroom.CountBathroom,
                    NumFloor= xx.Fkdetialsstatusroom.NumFloor,
                    CountBedDouble= xx.Fkdetialsstatusroom.CountBedDouble,
                    CountBedSingle= xx.Fkdetialsstatusroom.CountBedSingle,
                    CountRooms= xx.Fkdetialsstatusroom.CountRooms,
                    CountTv= xx.Fkdetialsstatusroom.CountTv,
                    CountWallet= xx.Fkdetialsstatusroom.CountWallet,
                    IdHo= xx.Fkdetialsstatusroom.IdHo,
                    Note= xx.Fkdetialsstatusroom.Note,
                    TypeCondition= xx.Fkdetialsstatusroom.TypeCondition,
                    PrivateFeatures= xx.Fkdetialsstatusroom.PrivateFeatures,
                    PublicFeatures= xx.Fkdetialsstatusroom.PublicFeatures,
                    Roomstabletyperoomstable=new TypeRoomsTable
                    {
                        NameT=xx.Fkdetialsstatusroom.Fkroomstyperoom.NameT
                        
                    }
                }
                

                }).ToList();

            return mo;
        }


        public Status_Current_RoomViewModel Find(int idStat,int idSub)
        {
           var mo = _db.StatusCurrentTables.
                Where(ss=>ss.Id==idStat).

                 Select(xx => new Status_Current_RoomViewModel
                 {
                     Id = xx.Id,
                     Status = xx.Status,
                     Createat = xx.Createat,
                     Detials = xx.Detials,
                     EndDate = xx.EndDate,
                     StartDate = xx.StartDate,
                     IdEmp = xx.IdEmp,
                     IdReception = xx.IdReception,
                     IdRoom = xx.IdRoom,
                     color = getColorByStatus(xx.Status),
                     RoomModel = new RoomViewModel
                     {
                         IdR = xx.Fkdetialsstatusroom.Id,
                         NameR = xx.Fkdetialsstatusroom.NameR,
                         CountBathroom = xx.Fkdetialsstatusroom.CountBathroom,
                         NumFloor = xx.Fkdetialsstatusroom.NumFloor,
                         CountBedDouble = xx.Fkdetialsstatusroom.CountBedDouble,
                         CountBedSingle = xx.Fkdetialsstatusroom.CountBedSingle,
                         CountRooms = xx.Fkdetialsstatusroom.CountRooms,
                         CountTv = xx.Fkdetialsstatusroom.CountTv,
                         CountWallet = xx.Fkdetialsstatusroom.CountWallet,
                         IdHo = xx.Fkdetialsstatusroom.IdHo,
                         Note = xx.Fkdetialsstatusroom.Note,
                         TypeCondition = xx.Fkdetialsstatusroom.TypeCondition,
                         PrivateFeatures = xx.Fkdetialsstatusroom.PrivateFeatures,
                         PublicFeatures = xx.Fkdetialsstatusroom.PublicFeatures,
                         Roomstabletyperoomstable = new TypeRoomsTable
                         {
                             NameT = xx.Fkdetialsstatusroom.Fkroomstyperoom.NameT

                         }
                     }


                 }).FirstOrDefault();

            return mo;
        }

        // تغيير حالة الشقة والخحز 
        public long changeStatusForLogout(LogoutRoomViewModel model)
        {
            Status_RoomService sr = new Status_RoomService(_db);
            string st = Status_RoomsName.listStatus[0].index.ToString();
            Status_Current_RoomViewModel stViewModel = new Status_Current_RoomViewModel
            {
                IdRoom = model.IdRoom,
                Status = st,
                IdReception = null,
                // StartDate = model.StartDate,
                // EndDate = model.EndDate,

                //IdEmp = mo.IdEmp
            };

            var cStIDdetials = sr.changByRoom(stViewModel);
            int idStatus = 3;
            if(model.OutOrCancel=="2")
            {
                idStatus = 4;
            }

            ReceptionService receService = new ReceptionService(_db);
            receService.updateStatus(idStatus, model.IdReception);

            return cStIDdetials;
        }


        public long changByRoom(Status_Current_RoomViewModel mo)
        {
            var st = mo.Status;
            var cStatus = _db.StatusCurrentTables.Where(x => x.IdRoom == mo.IdRoom)

                .FirstOrDefault();


            cStatus.Status = st;


            cStatus.IdReception = mo.IdReception;
            cStatus.StartDate = mo.StartDate;
            cStatus.EndDate = mo.EndDate;
            // cStatus.IdEmp = mo.IdEmp;
           

            Status_RoomService sr = new Status_RoomService(_db);

            var ids = _db.Update(cStatus); // sr.UpdateStatus(cStatus);

            cStatus.Id = ids;
             var cStIDdetials = sr.EditAndAdd_(cStatus);
            return cStIDdetials;
        }

        public int ChangeStatus_( Status_Current_RoomViewModel collection)
        {
           
                var mo = _db.StatusCurrentTables.Where(xx=>xx.IdRoom==collection.IdRoom || xx.Id== collection.Id).FirstOrDefault();
                mo.Status = collection.Status;
            mo.IdReception =  collection.IdReception;
            mo.StartDate = collection.StartDate;
            mo.EndDate = collection.EndDate;
            mo.IdEmp = collection.IdEmp;



           


             return UpdateStatus(mo);



            }


        public int UpdateStatus(StatusCurrentTable collection)
        {

            EditAndAdd_(collection);


            return _db.Update(collection);



        }



    }
}
