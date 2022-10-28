using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WesternInn_AF_HLN_HP.Model
{
    public class Booking
    {
        //primary key 
        public int ID { get; set; }

        //foreign key
        public int RoomID { get; set; }

        //foreign key
        [Key,Required]
        [DataType(DataType.EmailAddress)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Guest Email")]
        public string GuestEmail = string.Empty;

        //checking-in date
        [Display(Name = "Check In")]
        [DataType(DataType.DateTime)]
        public DateTime CheckIn { get; set; }

        //check-out date
        [Display(Name = "Check Out")]
        [DataType(DataType.DateTime)]
        public DateTime CheckOut { get; set; }

        [Range(0,10000)]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        //Navigation Properties
        public Room? TheRoom { get; set; }
        public Guest? TheGuest { get; set; }

        internal object parse(object checkIn)
        {
            throw new NotImplementedException();
        }
    }
}
