namespace HotelSys.ViewModel
{
    public class RoomLoginViewModel
    {
        public bool isSelect { get; set; }

        public string Status { get; set; }

        

        public long ? IdReception { get; set; }

        public long IdBill { get; set; }

        public int IdRoom { get; set; }

        public int? IdAccount { get; set; }
        public int? IdSub { get; set; }

        public long IdCustomer { get; set; } // bigint

        public string nameCuOrCo {  get; set; }

        public string CustomerOrCompany { get; set; }

        public RoomViewModel RoomModel { get; set; }

    }
}
