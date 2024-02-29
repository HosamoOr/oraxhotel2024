using DataModels;
using HotelSys.ViewModel;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSys.BusnessLayer
{
    public class RoomsService
    {

        private readonly HotelAlkheerDB _db;

        public RoomsService(HotelAlkheerDB context)
        {
            _db = context;
        }
        public int create(RoomsTable model )
        {
            
            var id_ = _db.InsertWithIdentity(model);
           int idd = Convert.ToInt32(id_);

            PriceRoomsTable prt = new PriceRoomsTable
            {
                IdRoom= idd,
                Price=0,
                PriceMin=0,
                PriceOvertime=0
                
            };

           int st= Convert.ToInt32( _db.InsertWithIdentity(prt));
            model.Id = idd;

            Status_RoomService srs = new Status_RoomService(_db);
           long stStatus= srs.create(model);

            return st;
          
        }


        public async Task<int> Edit(RoomViewModel model)
        {
            int st = 0;
            var m = _db.RoomsTables.Find(model.IdR);
            if (m != null)
            {
                m.NameR = model.NameR;

                m.IdType = Convert.ToInt32( model.IdType);
                m.Note = model.Note;
                m.NumFloor = model.NumFloor;
                m.PrivateFeatures = model.PrivateFeatures;
                m.PublicFeatures = model.PublicFeatures;
                m.TypeCondition = model.TypeCondition;
                m.CountWallet = model.CountWallet;
                m.CountTv = model.CountTv;
                m.CountRooms = model.CountRooms;
                m.CountBedSingle = model.CountBedSingle;
                m.CountBedDouble = model.CountBedDouble;
                m.CountBathroom = model.CountBathroom;
               



                st = await _db.UpdateAsync(m);

            

            }


            return st;

        }


        public List<RoomViewModel> List()
        {
            List<RoomViewModel> li = _db.RoomsTables.
               Select(ss => new RoomViewModel
               {
                   IdR = ss.Id,
                   NameR = ss.NameR,
                   NumFloor = ss.NumFloor,
                   CountRooms = ss.CountRooms,
                   CountBathroom = ss.CountBathroom,
                   CountBedDouble = ss.CountBedDouble,
                   CountBedSingle = ss.CountBedSingle,
                   CountTv = ss.CountTv,
                   CountWallet = ss.CountWallet,
                   IdHo = ss.IdHo,
                   Note = ss.Note,
                   PrivateFeatures = ss.PrivateFeatures,
                   PublicFeatures = ss.PublicFeatures,
                   TypeCondition = ss.TypeCondition,
                   Roomstabletyperoomstable = ss.Fkroomstyperoom

               }).

               ToList();
            return li;
        }


        public RoomViewModel One(int? id)
        {
            var model = _db.RoomsTables.
                Where(x => x.Id == id).Select(yy => new RoomViewModel
                {
                    IdR = yy.Id,
                    NameR = yy.NameR,
                    NumFloor = yy.NumFloor,
                    CountRooms = yy.CountRooms,
                    CountBathroom = yy.CountBathroom,
                    CountBedDouble = yy.CountBedDouble,
                    CountBedSingle = yy.CountBedSingle,
                    CountTv = yy.CountTv,
                    CountWallet = yy.CountWallet,
                    IdHo = yy.IdHo,
                    Note = yy.Note,
                    PrivateFeatures = yy.PrivateFeatures,
                    PublicFeatures = yy.PublicFeatures,
                    TypeCondition = yy.TypeCondition,
                    Roomstabletyperoomstable = yy.Fkroomstyperoom



                }).FirstOrDefault();
                
                
           
            return model;

        }

        public List<PriceRoomsViewModel> ListPrice()
        {
            List<PriceRoomsViewModel> li = _db.PriceRoomsTables.
               Select(ss => new PriceRoomsViewModel
               {
                   Id = ss.Id,
                   NameRoom=ss.Fkroomspriceroom.NameR,
                   IdRoom=ss.IdRoom,
                   Price=ss.Price,
                   PriceMin=ss.PriceMin,
                   PriceOvertime=ss.PriceOvertime,
                   NameType=ss.Fkroomspriceroom.Fkroomstyperoom.NameT,
                   NumFloor=ss.Fkroomspriceroom.NumFloor,
                   CountRooms=ss.Fkroomspriceroom.CountRooms,
                   IdTaxGroup=ss.IdTaxGroup,
                  

               }).

               ToList();
            return li;
        }



        public async Task<int> UpdatePriceByTypeAsync(PriceByTypeViewModel modelP)
        {

            var lidt=_db.PriceRoomsTables.Where(x=>x.Fkroomspriceroom.IdType==modelP.Id).ToList();
            int st = 0;
            for(int i=0;i<lidt.Count;i++)
            {
                PriceRoomsViewModel submodel = new PriceRoomsViewModel
                {
                    Id = lidt[i].Id,
                    IdRoom = lidt[i].IdRoom,
                    IdTaxGroup = modelP.IdTaxGroup,
                    Price = modelP.Price,
                    PriceMin = modelP.PriceMin,
                    PriceOvertime = modelP.PriceOvertime,
                    
                };

              await  UpdatePriceAsync(submodel);
                st++;
            }

           
                ;


            return st;

        }


        public async Task<int> UpdatePriceAsync(PriceRoomsViewModel model)
        {

            PriceRoomsTable prt = _db.PriceRoomsTables.Find(model.Id);

            prt.Price = model.Price;
            prt.PriceMin = model.PriceMin;
            prt.PriceOvertime = model.PriceOvertime;
            prt.IdTaxGroup = model.IdTaxGroup;

            if(model.IdTaxGroup==-1)
            {
                prt.IdTaxGroup = null;
            }

            int st = await _db.UpdateAsync(prt);

           
            return st;

        }


        public List<PriceByTypeViewModel> ListTypePrice()
        {
            List<PriceByTypeViewModel> li = _db.TypeRoomsTables.
               Select(ss => new PriceByTypeViewModel
               {
                   Id = ss.Id,
                  NameT=ss.NameT,
                  IdSub=ss.IdSub,
                  Price=0,
                  PriceMin=0,
                  PriceOvertime=0,
                  countRoom=ss.Fkroomstyperooms.Count()


               }).

               ToList();
            return li;
        }




    }
}
