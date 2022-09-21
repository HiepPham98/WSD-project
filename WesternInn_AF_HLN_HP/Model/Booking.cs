using System.ComponentModel.DataAnnotations;

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

        //check-out date
        public DateOnly CheckOut { get; set; }

        [Range(0,10000)]
        public decimal Cost { get; set; }

        //Navigation Properties
        public Room? TheRoom { get; set; }
        public Guest? TheGuest { get; set; }

    }
}
