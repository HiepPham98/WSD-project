namespace WesternInn_AF_HLN_HP.Model
{
    public class Booking
    {
        //primary key 
        public int ID { get; set; }

        //foreign key
        public int RoomID { get; set; }

        //foreign key
        public string GuestEmail = string.Empty;

        //checking-in date
        public DateOnly CheckIn { get; set; }

        public DateOnly CheckOut { get; set; }



    }
}
